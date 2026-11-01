using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Blazored.LocalStorage;
using Formaze.Blazor.Mudblazor.Data;
using Formaze.Blazor.Mudblazor.Interfaces;

namespace Web.Services
{
    public class MyFormazeStore : IFormazeStore
    {
        private readonly ILocalStorageService _storageService;
        private readonly IHttpClientFactory _httpClientFactory;

        public MyFormazeStore(ILocalStorageService storageService, IHttpClientFactory httpClientFactory)
        {
            _storageService = storageService;
            _httpClientFactory = httpClientFactory;
        }

        public async Task DeleteAsync(string key, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<string>> ListKeysAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Form?> LoadAsync(string key, CancellationToken ct = default)
        {
            string? jwt = await _storageService.GetItemAsync<string>("jwt", ct);
            var client = _httpClientFactory.CreateClient("backend");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            using (HttpResponseMessage response = await client.GetAsync($"/api/event/{key}", ct))
            {
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(await response.Content.ReadAsStringAsync(ct));
                }

                FormazeEventDetail eventDetail = (await response.Content.ReadFromJsonAsync<FormazeEventDetail>(ct))!;
                try
                {
                    Form? myForm = JsonSerializer.Deserialize<Form>(eventDetail.FormTemplate);
                    return myForm;
                }
                catch (JsonException)
                {
                    return null;
                }
            }
        }

        public async Task SaveAsync(string key, Form Formaze, CancellationToken ct = default)
        {
            string? jwt = await _storageService.GetItemAsync<string>("jwt", ct);
            var client = _httpClientFactory.CreateClient("backend");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            FormazePutEvent putEvent = new();
            using (HttpResponseMessage response = await client.GetAsync($"/api/event/{key}", ct))
            {
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(await response.Content.ReadAsStringAsync(ct));
                }

                FormazeEventDetail eventDetail = (await response.Content.ReadFromJsonAsync<FormazeEventDetail>(ct))!;

                putEvent.Title = eventDetail.Title;
                byte[] byteString = Encoding.UTF8.GetBytes(eventDetail.Description);
                putEvent.Description = Convert.ToBase64String(byteString);
                byteString = Encoding.UTF8.GetBytes(eventDetail.AfterBuyDescription);
                putEvent.AfterBuyDescription = Convert.ToBase64String(byteString);
                putEvent.FormTemplate = string.Empty;
                putEvent.IsRegistrationOpen = eventDetail.IsRegistrationOpen;
                putEvent.OpenRegistrationDate = eventDetail.OpenRegistrationDate;
                putEvent.CloseRegistrationDate = eventDetail.CloseRegistrationDate;
                putEvent.BankAccountNumber = eventDetail.BankAccountNumber;
                putEvent.BankAccountName = eventDetail.BankAccountName;
                putEvent.IsDeleted = eventDetail.IsDeleted;
            }

            string formTemplateSerialize = JsonSerializer.Serialize<Form>(Formaze);
            byte[] formTemplateByte = Encoding.UTF8.GetBytes(formTemplateSerialize);
            putEvent.FormTemplate = Convert.ToBase64String(formTemplateByte);

            using (HttpResponseMessage response = await client.PutAsJsonAsync<FormazePutEvent>($"/api/event/{key}", putEvent, ct))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return;
                }
            }

            throw new Exception("error while sync form builder");
        }
    }

    public class FormazeEventDetail
    {
        public Guid EventId { get; set; }
        public Guid AcyouUserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AfterBuyDescription { get; set; } = string.Empty;
        public string FormTemplate { get; set; } = string.Empty;
        public bool IsRegistrationOpen { get; set; }
        public DateTime OpenRegistrationDate { get; set; }
        public DateTime CloseRegistrationDate { get; set; }
        public string BankAccountNumber { get; set; } = string.Empty;
        public string BankAccountName { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public int RowVersion { get; set; }
        public List<FormazePosterDetail> Posters { get; set; } = [];
    }

    public class FormazePosterDetail
    {
        public Guid PosterId { get; set; }
        public string FullPathUrl { get; set; } = string.Empty;
        public string MimeType { get; set; } = string.Empty;
        public long ByteLength { get; set; }
        public DateTime CreatedAt { get; set; }
        public int RowVersion { get; set; }
    }

    public class FormazePutEvent
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AfterBuyDescription { get; set; } = string.Empty;
        public string FormTemplate { get; set; } = string.Empty;
        public bool IsRegistrationOpen { get; set; }
        public DateTime OpenRegistrationDate { get; set; }
        public DateTime CloseRegistrationDate { get; set; }
        public string BankAccountNumber { get; set; } = string.Empty;
        public string BankAccountName { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
    }
}

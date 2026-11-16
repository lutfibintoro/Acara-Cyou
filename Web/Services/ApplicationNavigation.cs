using Microsoft.AspNetCore.Components;
using Web.Services.Interface;

namespace Web.Services
{
    public class ApplicationNavigation : IApplicationNavigation
    {
        private readonly NavigationManager _navigation;
        private readonly string _ladingPageBaseAddress = "http://localhost:7116" /*"https://acara.cyou"*/;

        public ApplicationNavigation(NavigationManager navigation)
        {
            _navigation = navigation;
        }

        public void NavigateToNotFound() => _navigation.NavigateTo("/not-found");
        public void NavigateToUrl(string url) => _navigation.NavigateTo(url);
        public void NavigateToFitur() => _navigation.NavigateTo($"{_ladingPageBaseAddress}/#fitur");
        public void NavigateToMasalah() => _navigation.NavigateTo($"{_ladingPageBaseAddress}/#masalah");
        public void NavigateToCaraKerja() => _navigation.NavigateTo($"{_ladingPageBaseAddress}/#cara-kerja");
        public void NavigateToTestimoni() => _navigation.NavigateTo($"{_ladingPageBaseAddress}/#testimoni");
        public void NavigateToFaq() => _navigation.NavigateTo($"{_ladingPageBaseAddress}/#faq");
        public void NavigateToPage() => _navigation.NavigateTo(_ladingPageBaseAddress);
        public void NavigateToEvents() => _navigation.NavigateTo("/events");
        public void NavigateToChallenge(string encodeReturnUrl) => _navigation.NavigateTo($"http://localhost:8081/api/google-login/challenge?returnUrl={encodeReturnUrl}", forceLoad: true);
        public void NavigateToDashboard(string eventId) => _navigation.NavigateTo($"/dashboard?eventId={eventId}");
        public void NavigateToParticipants(string eventId) => _navigation.NavigateTo($"/participants?eventId={eventId}");
        public void NavigateToParticipantsCheckIn(string eventId) => _navigation.NavigateTo($"/participants/check-in?eventId={eventId}");
        public void NavigateToFormEdit(string eventId) => _navigation.NavigateTo($"/form/edit?eventId={eventId}");
        public void NavigateToFormPreview(string eventId) => _navigation.NavigateTo($"/form/preview?eventId={eventId}");
        public void NavigateToFormShare(string eventId) => _navigation.NavigateTo($"/form/share?eventId={eventId}");
        public void NavigateToTicketEdit(string eventId) => _navigation.NavigateTo($"/ticket/edit?eventId={eventId}");
        public void NavigateToTicketPreview(string eventId) => _navigation.NavigateTo($"/ticket/preview?eventId={eventId}");
        public void NavigateToTicketPrice(string eventId) => _navigation.NavigateTo($"/ticket/price?eventId={eventId}");
        public void NavigateToFunds(string eventId) => _navigation.NavigateTo($"/funds?eventId={eventId}");
    }
}

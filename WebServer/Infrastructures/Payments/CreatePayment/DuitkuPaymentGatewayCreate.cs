using System.Security.Cryptography;
using System.Text;
using FluentResults;
using WebServer.Extensions;
using WebServer.Infrastructures.ResultError;
using WebServer.Shared.Payments.CreatePayment;

namespace WebServer.Infrastructures.Payments.CreatePayment
{
    public class DuitkuPaymentGatewayCreate : IPaymentGatewayCreate
    {
        private readonly ILogger<DuitkuPaymentGatewayCreate> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public DuitkuPaymentGatewayCreate(ILogger<DuitkuPaymentGatewayCreate> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public PaymentGatewayCreateName PaymentGatewayName => PaymentGatewayCreateName.Duitku;

        public async Task<Result<PaymentGatewayCreateResponse>> CreatePaymentAsync(PaymentGatewayCreateRequest request, CancellationToken ct)
        {
            string merchantCode = _configuration["PaymentGateway:DuitkuPG:MerchantCodeSandbox"]!;
            string apiKey = _configuration["PaymentGateway:DuitkuPG:ApiKeySandbox"]!;

            string payloadSignature = $"{merchantCode}{request.OrderId}{request.Amount}";
            byte[] keyBytes = Encoding.UTF8.GetBytes(apiKey);
            byte[] payloadBytes = Encoding.UTF8.GetBytes(payloadSignature);
            byte[] hashBytes = HMACSHA256.HashData(keyBytes, payloadBytes);
            string signature = Convert.ToHexString(hashBytes).ToLowerInvariant();

            Dictionary<string, object> requestBody = [];
            requestBody.Add("merchantCode", merchantCode);
            requestBody.Add("paymentAmount", request.Amount);
            requestBody.Add("merchantOrderId", request.OrderId);
            requestBody.Add("productDetails", request.ProductDetail);
            requestBody.Add("email", request.CustomerEmail);
            requestBody.Add("paymentMethod", request.CodePayment);
            requestBody.Add("customerVaName", request.CustomerVaName);
            requestBody.Add("returnUrl", "https://example.com");
            requestBody.Add("callbackUrl", request.CallbackUrl);
            requestBody.Add("signature", signature);
            requestBody.Add("expiryPeriod", request.ExpiredInMinute);

            var client = _httpClientFactory.CreateClient(ApplicationHttpClient.ConstantHttpName.DuitkuPG);
            using (HttpResponseMessage response = await client.PostAsJsonAsync<Dictionary<string, object>>("/v2/inquiry", requestBody, ct))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Dictionary<string, object> responseBody = (await response.Content.ReadFromJsonAsync<Dictionary<string, object>>(ct))!;

                    string vaNumber = string.Empty;
                    string qrString = string.Empty;
                    if (responseBody.ContainsKey("vaNumber"))
                        vaNumber = (string)responseBody["vaNumber"];
                    else
                        qrString = (string)responseBody["qrString"];

                    return Result.Ok(new PaymentGatewayCreateResponse()
                    {
                        MerchantCode = (string)responseBody["merchantCode"],
                        Reference = (string)responseBody["reference"],
                        Amount = (int)responseBody["amount"],
                        VaNumber = vaNumber,
                        QrString = qrString
                    });
                }
                else
                {
                    Dictionary<string, object> responseBody = (await response.Content.ReadFromJsonAsync<Dictionary<string, object>>(ct))!;
                    ThirdPartError<string> responseError = new("payment gateway", "duitku", $"{responseBody["Message"]}", FluentValidation.Severity.Error);
                    return Result.Fail<PaymentGatewayCreateResponse>(responseError);
                }
            }
        }
    }
}
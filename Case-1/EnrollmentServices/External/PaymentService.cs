using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EnrollmentServices.Dtos;
using EnrollmentServices.Exceptions;
using EnrollmentServices.External.Dtos;
using Microsoft.Extensions.Configuration;

namespace EnrollmentServices.External
{
    public class PaymentService : IPaymentService
    {
        private HttpClient _httpClient;
        private IConfiguration _config;

        public PaymentService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _config = configuration;
        }

        public async Task AddPayment(DtoPayment payment)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(payment),
                Encoding.UTF8,
                "application/json"
            );

            var url = _config["ExternalServices:Payment"];

            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _config["AppSettings:ServiceToken"]);

            var response = await _httpClient.PostAsync(
                $"{url}Payment",
                httpContent
            );

            Console.WriteLine(await response.Content.ReadAsStringAsync());

            if (!response.IsSuccessStatusCode)
            {
                throw new System.Exception(response.StatusCode.ToString());
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PaymentServices.Dtos;
using PaymentServices.Exceptions;
using PaymentServices.Models;

namespace PaymentServices.External
{
    public class DALEnrollmentService : IEnrollmentService
    {
        private HttpClient _httpClient;
        private IConfiguration _config;

        public DALEnrollmentService(
            HttpClient httpClient,
            IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task<DtoPaymentGet> GetDetail(IEnumerable<Payment> payments, int StudentId)
        {
            List<DtoCourse> courseList = new List<DtoCourse>();

            var totalCredit = 0;

            var url = _config["ExternalServices:Enrollment"];

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _config["AppSettings:ServiceToken"]);

            var response = await _httpClient.GetAsync(
                $"{url}course"
            );

            if (!response.IsSuccessStatusCode)
            {
                throw new System.Exception(response.StatusCode.ToString());
            }

            var responseData = await response.Content.ReadAsByteArrayAsync();
            var responseDeserialized = JsonSerializer.Deserialize<DtoReturnDataIEnumerableCourse>(responseData);
            var responseValue = responseDeserialized.data;

            foreach (var payment in payments)
            {
                var course = responseValue.FirstOrDefault(x => x.id == payment.CourseId);
                if (course == null) throw new DataNotFoundException(payment.CourseId.ToString());
                courseList.Add(course);

                totalCredit += course.credit;
            }

            return new DtoPaymentGet
            {
                StudentId = StudentId,
                Courses = courseList,
                TotalCredit = totalCredit,
            };

        }
    }
}
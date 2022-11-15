using CowboyAPI.Models;
using System.Text.Json;
using System.Net.Http;

namespace CowboyAPI.Helpers
{
    public static class FakeNameAPICommand
    {

        public static async Task<FakeNameDetails?> FakeNameAPIExecute(HttpClient httpClient)
        {
            var responseMessage = await httpClient.GetAsync("https://api.namefake.com/");

            if (responseMessage.IsSuccessStatusCode)
            {
                var content = await responseMessage.Content.ReadAsStreamAsync();
                var jsonResponse = await JsonSerializer.DeserializeAsync<FakeNameDetails>(content);

                return jsonResponse;
            }

            return null;

        }

        public static int CalculateAge(DateTime dob)
        {
            DateTime Now = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(dob).Ticks).Year - 1;
            DateTime PastYearDate = dob.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }
            int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
            int Hours = Now.Subtract(PastYearDate).Hours;
            int Minutes = Now.Subtract(PastYearDate).Minutes;
            int Seconds = Now.Subtract(PastYearDate).Seconds;

            return Years;
        }


    }
}

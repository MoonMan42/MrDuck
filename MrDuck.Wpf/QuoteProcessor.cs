using MrDuck.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MrDuck.Wpf
{
    public static class QuoteProcessor
    {
        public static async Task<QuoteModel> LoadQuote()
        {
            string url = "https://evilinsult.com/generate_insult.php?lang=en&type=json";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    QuoteModel quote = await response.Content.ReadAsAsync<QuoteModel>();

                    return quote;

                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }


        }
    }
}

// Author: Ezz Eldin Mohamed
// Date: December 2024
// Purpose: Currency Converter using ExchangeRate API

using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace Datatableappstry
{
    public class CurrencyConverter
    {
        public double GetExchangeRate(string fromCurrency, string toCurrency)
        {
            string apiUrl = $"https://api.exchangerate-api.com/v4/latest/{fromCurrency}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = response.Content.ReadAsStringAsync().Result;
                    var rates = JObject.Parse(jsonResponse)["rates"];
                    return rates[toCurrency].Value<double>();
                }
                else
                {
                    throw new Exception("Failed to fetch exchange rate");
                }
            }
        }

        public double ConvertCurrency(string fromCurrency, string toCurrency, double amount)
        {
            double rate = GetExchangeRate(fromCurrency, toCurrency);
            return amount * rate;
        }
    }
}

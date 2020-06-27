
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FlutterwaveNet
{
    public class FlutterWaveApi
    {
        protected string  _serviceKey;
        public FlutterWaveApi(string ServiceKey)
        {
            _serviceKey = ServiceKey;
        }

        private async Task<TransactionReponse> MakePayment(TransactRequest request)
        {

            string endpoint = "/v3/payments";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.flutterwave.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (!string.IsNullOrWhiteSpace(_serviceKey))
                    // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _serviceKey);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _serviceKey);

                  var payload = JsonConvert.SerializeObject(request);
                   var httpContent = new StringContent(payload, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(endpoint, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TransactionReponse>(content);
                }
                else
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TransactionReponse>(content);
                }
            }
        }

        public TransactionReponse Initialize(TransactRequest request)
        {
           return Task.Run(() => MakePayment(request)).Result;    
        }
    }
}

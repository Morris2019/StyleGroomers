using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace UrbanStyleGroomers.ModelsServer
{
    public class UrbanStyleLoginService
    {
        private static UrbanStyleLoginService _clientInstance;


        public static UrbanStyleLoginService ServiceClientInstance
        {
            get
            {
                if (_clientInstance == null)
                    _clientInstance = new UrbanStyleLoginService();
                return _clientInstance;
            }
        }
        private JsonSerializer _serializer = new JsonSerializer();
        private HttpClient client;

        public UrbanStyleLoginService()
        {
            client = new HttpClient();
            //Change your base address here
            client.BaseAddress = new Uri("https://urbanstyleapi.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<LoginResponse> AuthenticateUserAsync(string email, string password)
        {
            try
            {
                UrbanLoginRequest loginRequestModel = new UrbanLoginRequest()
                {
                    Email = email,
                    Password = password

                };
                var content = new StringContent(JsonConvert.SerializeObject(loginRequestModel), Encoding.UTF8, "application/json");
                //Change your base address tail part here and post it. 
                var response = await client.PostAsync(".api/Users/LoginUsers", content);
                response.EnsureSuccessStatusCode();
                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    var jsoncontent = _serializer.Deserialize<LoginResponse>(json);
                    Preferences.Set("authToken", jsoncontent.Token);
                    return jsoncontent;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace Client
{
    public static class ServiceDiscoverryPatterns
    {
        public static void PointToPointPattern()
        {
            while (true)
            {
                HttpClient apiClient;

                apiClient = new HttpClient
                {
                    BaseAddress = new Uri($"http://localhost:5003")
                };

                apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Console.WriteLine($"Making request to {apiClient.BaseAddress}");

                try
                {
                    var response = apiClient.GetAsync("WeatherForecast").Result;

                    var payload = response.Content.ReadAsStringAsync().Result;

                    if (payload == null)
                        throw new NullReferenceException("services don't work");

                    Console.WriteLine($"Payload: {payload}\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"No response from {apiClient.BaseAddress} couse {ex.Message}");
                }
            }
        }

        public static void LocalRegistryPattern()
        {
            string[] LocalRegistry = new string[] { "http://localhost:5004", "http://localhost:5005", "http://localhost:5006", "http://localhost:5007" };

            while (true)
            {
                foreach (string url in LocalRegistry)
                {
                    HttpClient apiClient;

                    apiClient = new HttpClient
                    {
                        BaseAddress = new Uri(url)
                    };

                    apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    Console.WriteLine($"Making request to {apiClient.BaseAddress}");

                    try
                    {
                        var response = apiClient.GetAsync("WeatherForecast").Result;

                        var payload = response.Content.ReadAsStringAsync().Result;

                        Console.WriteLine($"Payload: {payload}\n");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"No response from {apiClient.BaseAddress} couse {ex.Message}");
                    }

                }
            }
        }

        public static void SelfRegistrationPattern()
        {
            List<SelfRegistrationData> services = new();

            while (true)
            {

                HttpClient apiClient;

                apiClient = new HttpClient
                {
                    BaseAddress = new Uri("http://localhost:5000")
                };

                apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Console.WriteLine($"Making request to {apiClient.BaseAddress}");

                try
                {
                    services = apiClient.GetFromJsonAsync<List<SelfRegistrationData>>("SimpleZooKeeper").Result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"No response from {apiClient.BaseAddress} couse {ex.Message}");
                }

                if (services.Count == 0)
                    throw new NullReferenceException("services don't available");

                apiClient = new HttpClient
                {
                    BaseAddress = new Uri(services[0].Url)
                };

                apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Console.WriteLine($"Making request to {apiClient.BaseAddress}");

                try
                {
                    var response = apiClient.GetAsync("WeatherForecast").Result;

                    var payload = response.Content.ReadAsStringAsync().Result;

                    Console.WriteLine($"Payload: {payload}\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"No response from {apiClient.BaseAddress} couse {ex.Message}");
                }
            }
        }
    }
}

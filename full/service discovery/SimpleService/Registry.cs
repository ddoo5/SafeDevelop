using System.Net.Http.Headers;

namespace SimpleService
{
    public static class RegistryMe
    {
        public static void DoIt(string[] args)
        {
            HttpClient apiClient;

            apiClient = new HttpClient
            {
                BaseAddress = new Uri($"http://localhost:5000")
            };
            SelfRegistrationData selfRegistrationData = new SelfRegistrationData() { Name = ServiceName.Name, Url = args[1], Description = "Test" };

            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = apiClient.PostAsJsonAsync("SimpleZooKeeper", selfRegistrationData).Result;

            var payload = response.Content.ReadAsStringAsync().Result;

            Console.WriteLine(payload);

        }
    }
}

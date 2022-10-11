using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HttpClientSample {
    public class User {

        public string id { get; set; }
        public string email { get; set; }
        public string handle { get; set; }
        public string img_url { get; set; }
    
    }

    class Program {
        static HttpClient client = new HttpClient();

        static void ShowProduct(User product) {
            Console.WriteLine($"Name: {product.handle}\tEmail: {product.email}");
        }

        static async Task<User> GetProductAsync(string path) {
            User user = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode) {
                user = await response.Content.ReadAsAsync<User>();
            }
            return user;
        }

        static void Main() {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync() {
            // Update port # in the following line.
            client.BaseAddress = new Uri("https://api.figma.com/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add("X-Figma-Token", "figd_DmVMdr9PnbfHlubrV0aNwRfIn91_LEUigip2CxxJ");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            try {
                // Create a new product
                User user = new User();

                var url = "/v1/me";
                Console.WriteLine($"Created at {url}");

                // Get the product
                user = await GetProductAsync(url.PathAndQuery);
                ShowProduct(user);

            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}
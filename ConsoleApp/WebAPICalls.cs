using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }
        public bool Discontinued { get; set; }

    }
    public class AuthenticationResponse
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = "";
        public string Token { get; set; } = "";
    }
    internal class WebAPICalls
    {
        static string baseUrl = "http://localhost:5042";
        static string prodUrl = "api/products";
        static string authUrl = "api/accounts";
        static AuthenticationResponse authObj = null!;
         internal static void Test()
        {
            Console.Clear();
            Console.WriteLine("Press a key after the service starts.");
            Console.ReadKey();

            Console.WriteLine("****** SIGN IN ********");
            Console.Write("User Name: "); 
            string userName = Console.ReadLine()!;
            Console.Write("Password: ");
            string password  = Console.ReadLine()!;
            var obj = new { userName = userName, password = password };

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            var objJson = JsonSerializer.Serialize(obj);
            CheckCredentials();

            ListProducts();

            void CheckCredentials()
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    //client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var requestMessage = new HttpRequestMessage(HttpMethod.Post, authUrl);
                    requestMessage.Content = new StringContent(objJson, Encoding.UTF8, "application/json");

                    var response =  client.SendAsync(requestMessage)
                        .GetAwaiter().GetResult();
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        authObj = response.Content.ReadFromJsonAsync<AuthenticationResponse>(options)
                            .GetAwaiter().GetResult()!;
                        Console.WriteLine("{0}, {1}", authObj?.FullName, authObj?.Token);
                    } else
                    {
                        Console.WriteLine("Something went wrong. Try again.");
                    }
                }
            }

            async void ListProducts()
            {
                await Console.Out.WriteLineAsync("Fetching products. Please wait.....");
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress= new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authObj?.Token);
                    var response = client.GetAsync(prodUrl).GetAwaiter().GetResult();
                    Console.WriteLine("Connected.");
                    response.EnsureSuccessStatusCode();
                    var items=response.Content.ReadFromJsonAsync<List<Product>>().GetAwaiter().GetResult(); ;
                    Console.Clear();
                    Console.WriteLine("Items retrieved.");
                    items?.ForEach(c=>Console.WriteLine($"{c.ProductId} {c.ProductName}"));
                }
            }
        }
    }
}

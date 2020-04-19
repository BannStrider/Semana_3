using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

/**
 * 1. ¿Cuál de los siguientes motivos es el mejor para comprobar si se está conectado a Internet antes de ejecutar código de red?
 * Para ofrecer una mejor experiencia del usuario.
 * 
 * 2. Supongamos que se usa una aplicación mientras conduce. Al entrar en un túnel, pierde la conexión con Internet. ¿Qué miembro de la clase Connectivity usaría para detectar el cambio en la conexión de red?
 * El evento ConnectivityChanged.
 */
namespace BookClient.Data
{
    public class BookManager
    {
        const string Url = "{https://bookserver23706.azurewebsites.net}/api/books/";
        private string authorizationKey;

        //Iniciar el servicio
        private async Task<HttpClient> GetClient()
        {
            HttpClient client = new HttpClient();
            if (string.IsNullOrEmpty(authorizationKey))
            {
                authorizationKey = await client.GetStringAsync(Url + "login");
                authorizationKey = JsonConvert.DeserializeObject<string>(authorizationKey);
            }

            client.DefaultRequestHeaders.Add("Authorization", authorizationKey);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        //Get - GET
        public async Task<IEnumerable<Book>> GetAll()
        {
            HttpClient client = await GetClient();
            string result = await client.GetStringAsync(Url);
            return JsonConvert.DeserializeObject<IEnumerable<Book>>(result);
        }

        //Add - POST
        public async Task<Book> Add(string title, string author, string genre)
        {
            Book book = new Book()
            {
                Title = title,
                Authors = new List<string>(new[] { author }),
                ISBN = string.Empty,
                Genre = genre,
                PublishDate = DateTime.Now.Date,
            };

            HttpClient client = await GetClient();
            var response = await client.PostAsync(Url,
                new StringContent(
                    JsonConvert.SerializeObject(book),
                    Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Book>(
                await response.Content.ReadAsStringAsync());
        }

        //Update - PUT
        public async Task Update(Book book)
        {
            HttpClient client = await GetClient();
            await client.PutAsync(Url + "/" + book.ISBN,
                new StringContent(
                    JsonConvert.SerializeObject(book),
                    Encoding.UTF8, "application/json"));
        }

        //Delete - DELETE
        public async Task Delete(string isbn)
        {
            HttpClient client = await GetClient();
            await client.DeleteAsync(Url + isbn);
        }
    }
}


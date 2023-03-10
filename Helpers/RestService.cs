using booklook.Models;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace booklook.Helpers {
    public class RestService {
        readonly HttpClient _client;
        readonly JsonSerializerOptions _serializerOptions;

        public string BasePath = "http://192.168.0.12:5001";
        public string Endpoint = "/api/books";
        public string ApiUrl => BasePath + Endpoint;

        public RestService() {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        /// <summary>
        ///     POST a book to the local server
        /// </summary>
        /// <param name="book"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostBook(Book book, string location = "?") {
            var values = new Dictionary<string, string> {
              { "title", book.Title},
              { "authors", book.Authors},
              { "imageSource", book.ImageSource},
              { "isbn13", book.Isbn13},
              { "isbn10", book.Isbn10},
              { "releaseDate", book.ReleaseDate},
              { "bookLink", book.BookLink},
              { "location", location }
            };

            StringContent request = new(JsonSerializer.Serialize(values), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(ApiUrl, request);
            Debug.WriteLine(response);
            return response;
        }

        /// <summary>
        ///     GET a book from google's api
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns>
        ///     Deserialized response body or null if not succesful
        /// </returns>
        public async Task<GoogleBookResponse> GetBook(string isbn) {
            Uri uri = new(string.Format("https://www.googleapis.com/books/v1/volumes?q=isbn:{0}", isbn));

            HttpResponseMessage response = await _client.GetAsync(uri);

            if (!response.IsSuccessStatusCode) {
                return null;
            }

            string content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GoogleBookResponse>(content, _serializerOptions);
        }
    }
}

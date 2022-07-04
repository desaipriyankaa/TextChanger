using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TextChanger.Model;

namespace TextChanger.Services
{
    public class ComicProcessor
    {
        public static async Task<ComicModel> LoadComic(int comicNumber = 0)
        {
            string url;
            if (comicNumber > 0)
            {
                url = $"https://xkcd.com/{comicNumber}/info.0.json";
            }
            else
            {
                url = "https://xkcd.com/info.0.json";
            }

            using var response = await ApiService.ApiClient!.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var comicAsString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ComicModel>(comicAsString)!;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}

using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;


public static class CrossApi
    {
        static HttpClient client = new HttpClient();
        /// <summary>
        /// دریافت اطلاعات از طریق http 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="Url"></param>
        /// <param name="methode"> def = Post </param>
        /// <returns></returns>
        public static async Task<T> GetDataFromHttpClientAsync<T>(object value, string Url, ApiMethode methode = ApiMethode.Post)
        {
            T Data = default(T);
            try
            {
                switch (methode)
                {
                    case ApiMethode.Post:
                        var result = await client.PostAsJsonAsync(Url, value);
                        result.EnsureSuccessStatusCode();
                        Data = await result.Content.ReadAsAsync<T>();
                        break;
                    case ApiMethode.Get:
                        var results = await client.GetStringAsync(Url);
                        Data = JsonConvert.DeserializeObject<T>(results);
                        break;
                }
                return Data;
            }
            catch (System.Exception ex)
            {

                //TODO : Log Ex
                return Data;
            }
        }


    }

    /// <summary>
    /// متد ارتباط http
    /// </summary>
    public enum ApiMethode
    {

        Post,
        Get
    }


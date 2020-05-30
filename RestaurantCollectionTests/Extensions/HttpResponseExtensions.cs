using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RestaurantCollectionTests.Extensions
{
    public static class HttpResponseExtensions
    {
        /// <summary>
        /// Reads JSON body to typed object
        /// </summary>
        public static async Task<T> ReadBody<T>(this HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}

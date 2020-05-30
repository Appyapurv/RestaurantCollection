using System;
using Newtonsoft.Json;

namespace RestaurantCollection.WebApi.DTO.Forms
{
    /// <summary>
    /// CreateRestaurantRequest
    /// </summary>

    public class CreateForm
    {
        ///// <summary>
        ///// Average Rating of the restaurant
        ///// </summary>
        [JsonProperty("rating")]
        public string Rating { get; set; }
        /// <summary>
        /// City in which restaurant is located
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }
        /// <summary>
        /// Name of the restaurant
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        ///// <summary>
        ///// EstimatedCost of the restaurant
        ///// </summary>
        [JsonProperty("cost")]
        public int Cost { get; set; }
        /// <summary>
        /// Total number of reviews
        /// </summary>
        [JsonProperty("votes")]
        public int Votes { get; set; }
    }
}

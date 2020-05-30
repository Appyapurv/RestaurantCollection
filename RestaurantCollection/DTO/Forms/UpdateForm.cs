using System;
using Newtonsoft.Json;

namespace RestaurantCollection.WebApi.DTO.Forms
{
    public class UpdateForm
    {
        ///// <summary>
        ///// Average Rating of the restaurant
        ///// </summary>
        [JsonProperty("rating")]
        public string Rating { get; set; }
        
        /// <summary>
        /// Total number of reviews
        /// </summary>
        [JsonProperty("votes")]
        public int Votes { get; set; }
    }
}

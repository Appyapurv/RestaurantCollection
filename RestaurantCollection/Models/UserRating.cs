using System;
namespace RestaurantCollection.WebApi.Models
{
    public class UserRating
    {
        /// <summary>
        /// Average Rating of the restaurant
        /// </summary>
        public decimal AverageRating { get; set; }
        /// <summary>
        /// Total number of reviews
        /// </summary>
        public int Votes { get; set; }
    }
}

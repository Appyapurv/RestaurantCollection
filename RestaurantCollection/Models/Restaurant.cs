using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestaurantCollection.WebApi.Models
{
    /// <summary>
    /// Restaurant
    /// </summary>
    public class Restaurant
    {
        /// <summary>
        /// Restaurant Id
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// City in which restaurant is located
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Name of the restaurant
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// EstimatedCost of the restaurant
        /// </summary>
        public int EstimatedCost { get; set; }
        /// <summary>
        /// Average Rating of the restaurant
        /// </summary>
        public string AverageRating { get; set; }
        /// <summary>
        /// Total number of reviews
        /// </summary>
        public int Votes { get; set; }
    }
}

using System;
namespace RestaurantCollection.WebApi.Models
{
    public class RestaurantQueryModel
    {
        /// <summary>
        /// City of the restaurant (or null)
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Id of the restaurant (or null)
        /// </summary>
        public int Id { get; set; }
    }
}

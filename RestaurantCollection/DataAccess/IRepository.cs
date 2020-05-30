using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using RestaurantCollection.WebApi.Models;

namespace RestaurantCollection.WebApi.DataAccess
{
    /// <summary>
    /// IRepository interface
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Gets all restaurants
        /// </summary>
        /// <returns>Gets all restaurants</returns>
        Task<List<Restaurant>> GetRestaurants();

        /// <summary>
        /// Gets all restaurants sorted as per the rating
        /// </summary>
        /// <returns>Gets all restaurants</returns>
        Task<List<Restaurant>> GetRestaurantsSorted();

        /// <summary>
        /// Add new restaurant
        /// </summary>
        /// <param name="restaurant">Restaurant to add</param>
        /// <returns>Added restaurant</returns>
        Task<Restaurant> AddRestaurant(Restaurant restaurant);

        /// <summary>
        /// Gets filtered list of restaurants
        /// </summary>
        /// <param name="query">Query for filtering</param>
        /// <returns>Filtered list of restaurants</returns>
        Task<List<Restaurant>> GetRestaurants(RestaurantQueryModel query);

        /// <summary>
        /// Saves updated restaurant
        /// </summary>
        /// <param name="restaurant">Restaurant to save</param>
        /// <returns>Saved restaurant</returns>
        Task<Restaurant> UpdateRestaurant(Restaurant restaurant);

        /// <summary>
        /// Deletes restaurant by id
        /// </summary>
        /// <param name="restaurant">restaurant details</param>
        /// <returns>Status iof the delete request</returns>
        Task<Restaurant> DeleteRestaurant(Restaurant restaurant);
    }
}

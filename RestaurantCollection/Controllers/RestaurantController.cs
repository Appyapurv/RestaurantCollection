using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantCollection.WebApi.DataAccess;
using RestaurantCollection.WebApi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantCollection.WebApi.Controllers
{

    [Route("api/restaurant")]
    [ApiController]
    public class ResturentController : ControllerBase
    {
        private readonly IRepository _repository;
        public ResturentController(IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Create Resturant 
        /// </summary>
        /// <param name="restaurant">restaurant</param>
        /// <returns>201 Status code & resturent Modek</returns>
        [HttpPost]
        public async Task<IActionResult> AddResturent([FromBody] Restaurant restaurant)
        {
            if (ModelValidation(restaurant))
            {
                return new BadRequestObjectResult("Name, city, AverageCost should not be empty");
            }
            var result = await _repository.AddRestaurant(restaurant);
            if (result == null)
            {
                return new BadRequestResult();
            }
            return Created(string.Empty, result);
        }

        /// <summary>
        /// Get All the list from Resturent table
        /// </summary>
        /// <returns> 200 status code & list of resturents</returns>
        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repository.GetRestaurants();
            if (result == null)
            {
                return new NotFoundResult();
            }
            return Ok((result));
        }

        /// <summary>
        /// Get by Id or Get by City
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="city">city</param>
        /// <returns>200 status code && Resturent object</returns>
        [HttpGet("query")]
        public async Task<IActionResult> GetById(int? id, string city)
        {

            var queryModel = new RestaurantQueryModel();
            if (id.HasValue)
            {
                queryModel.Id = id.Value;
            }
            queryModel.City = city;
            var result = await _repository.GetRestaurants(queryModel);
            if (result == null)
            {
                return new NotFoundResult();
            }
            return Ok((result));
        }

        /// <summary>
        /// update the resturent details
        /// </summary>
        /// <param name="resturentId">resturentId</param>
        /// <param name="restaurant">restaurant</param>
        /// <returns>204 status code</returns>
        [HttpPut("{resturentId:int}")]
        public async Task<IActionResult> update(int resturentId, [FromBody]Restaurant restaurant)
        {
            var queryModel = new RestaurantQueryModel
            {
                Id = resturentId
            };

            var result = await _repository.GetRestaurants(queryModel);
            if (result == null)
            {
                return new BadRequestResult();
            }
            var result1 = await _repository.UpdateRestaurant(restaurant);
            if (result1 == null)
            {
                return new BadRequestResult();
            }
            return this.StatusCode(StatusCodes.Status204NoContent);
        }
        /// <summary>
        /// Delete a restaurant by id
        /// </summary>
        /// <param name="resturentId">resturentId</param>
        /// <returns>204 status code</returns>
        [HttpDelete("{resturentId:int}")]
        public async Task<IActionResult> DeleteById(int resturentId)
        {

            var queryModel = new RestaurantQueryModel
            {
                Id = resturentId
            };

            var restaurants = await _repository.GetRestaurants(queryModel);
            if (restaurants == null)
            {
                return new NotFoundResult();
            }
            var result = await _repository.DeleteRestaurant(restaurants.FirstOrDefault());
            if (result == null)
            {
                return new NotFoundResult();
            }
            return this.StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// sort based on rating
        /// </summary>
        /// <returns>return sorted list based on averageRating</returns>
        [HttpGet("sort")]
        public async Task<IActionResult> Sort()
        {
            var result = await _repository.GetRestaurants();
            if (result == null)
            {
                return new NotFoundResult();
            }
            return Ok((result.OrderBy(o => o.AverageRating)));
        }

        private bool ModelValidation(Restaurant restaurant)
        {
            if (string.IsNullOrWhiteSpace(restaurant.Name) && string.IsNullOrWhiteSpace(restaurant.City) && string.IsNullOrWhiteSpace(restaurant.AverageRating))
            {
                return true;
            }
            return false;
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using RestaurantCollection.WebApi.Models;

namespace RestaurantCollection.WebApi.DataAccess
{
    public class RestaurantCollectionContext : DbContext
    {
        public RestaurantCollectionContext(DbContextOptions<RestaurantCollectionContext> options)
            : base(options)
        { }

        public DbSet<Restaurant> Restaurants { get; set; }
    }
}

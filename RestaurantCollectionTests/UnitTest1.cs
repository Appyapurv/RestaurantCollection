using System;
using Xunit;
using RestaurantCollection.WebApi.DataAccess;
using RestaurantCollection.WebApi.DTO.Forms;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Microsoft.AspNetCore.TestHost;
using System.Text;
using FluentAssertions;

// SUITE NAME - restaurants
// DESCRIPTION - Restaurants controller requests
namespace RestaurantCollectionTests
{
    public class UnitTest1: IDisposable
    {
        private TestServer _server;

        public HttpClient Client { get; private set; }

        public UnitTest1()
        {
            SetUpClient();
        }

        public void Dispose()
        {

        }

        public async Task SeedData()
        {

            // Create entry with id 1 
            var createForm0 = GenerateCreateForm("Miami", "Big Brewskey", "4.8", 500, 1500);
            var response0 = await Client.PostAsync("/api/restaurant", new StringContent(JsonConvert.SerializeObject(createForm0), Encoding.UTF8, "application/json"));

            // Create entry with id 2 
            var createForm1 = GenerateCreateForm("Florida", "Social", "4.7", 400, 1600);
            var response1 = await Client.PostAsync("/api/restaurant", new StringContent(JsonConvert.SerializeObject(createForm1), Encoding.UTF8, "application/json"));

            // Create entry with id 3 
            var createForm2 = GenerateCreateForm("Miami", "Social", "4.2", 50, 1000);
            var response2 = await Client.PostAsync("/api/restaurant", new StringContent(JsonConvert.SerializeObject(createForm2), Encoding.UTF8, "application/json"));

            // Create entry with id 4 
            var createForm3 = GenerateCreateForm("Florida", "CCD", "3.8", 200, 1000);
            var response3 = await Client.PostAsync("/api/restaurant", new StringContent(JsonConvert.SerializeObject(createForm3), Encoding.UTF8, "application/json"));

            // Create entry with id 5
            var createForm4 = GenerateCreateForm("Miami", "CCD", "4.1", 100, 1100);
            var response4 = await Client.PostAsync("/api/restaurant", new StringContent(JsonConvert.SerializeObject(createForm4), Encoding.UTF8, "application/json"));

        }

        // TEST NAME - CreateRestaurant
        // TEST DESCRIPTION - A new entry should be created
        [Fact]
        public async Task TestCase0()
        {
            await SeedData();

            // Create entry with id 6
            var createForm0 = GenerateCreateForm("Florida", "Little Italy", "4.4", 200, 1200);
            var response0 = await Client.PostAsync("/api/restaurant", new StringContent(JsonConvert.SerializeObject(createForm0), Encoding.UTF8, "application/json"));
            response0.StatusCode.Should().BeEquivalentTo(201);

            // Create entry with id 7
            var createForm1 = GenerateCreateForm("Miami", "Circus", "3.5", 1200, 700);
            var response1 = await Client.PostAsync("/api/restaurant", new StringContent(JsonConvert.SerializeObject(createForm1), Encoding.UTF8, "application/json"));
            response1.StatusCode.Should().BeEquivalentTo(201);
            

            var realData1 = JsonConvert.DeserializeObject(response1.Content.ReadAsStringAsync().Result);
            var expectedData1 = JsonConvert.DeserializeObject("{\"city\":\"Miami\",\"votes\":1200,\"estimatedCost\":700,\"id\":7,\"name\":\"Circus\",\"averageRating\":\"3.5\"}");
            realData1.Should().BeEquivalentTo(expectedData1);
        }

        // TEST NAME - GetRestaurants
        // TEST DESCRIPTION - It finds all the restaurants
        [Fact]
        public async Task TestCase1()
        {
            await SeedData();

            // Get All restaurants 
            var response0 = await Client.GetAsync("/api/restaurant");
            response0.StatusCode.Should().BeEquivalentTo(200);
            var realData0 = JsonConvert.DeserializeObject(response0.Content.ReadAsStringAsync().Result);
            var expectedData0 = JsonConvert.DeserializeObject("[{\"id\":1,\"city\":\"Miami\",\"name\":\"Big Brewskey\",\"estimatedCost\":1500,\"averageRating\":\"4.8\",\"votes\":500},{\"id\":2,\"city\":\"Florida\",\"name\":\"Social\",\"estimatedCost\":1600,\"averageRating\":\"4.7\",\"votes\":400},{\"id\":3,\"city\":\"Miami\",\"name\":\"Social\",\"estimatedCost\":1000,\"averageRating\":\"4.2\",\"votes\":50},{\"id\":4,\"city\":\"Florida\",\"name\":\"CCD\",\"estimatedCost\":1000,\"averageRating\":\"3.8\",\"votes\":200},{\"id\":5,\"city\":\"Miami\",\"name\":\"CCD\",\"estimatedCost\":1100,\"averageRating\":\"4.1\",\"votes\":100}]");
            realData0.Should().BeEquivalentTo(expectedData0);

        }

        // TEST NAME - getSingleEntryById
        // TEST DESCRIPTION - It finds single restaurant by ID
        [Fact]
        public async Task TestCase2()
        {
            await SeedData();

            // Get Single restaurant By ID 
            var response0 = await Client.GetAsync("api/restaurant/query?id=5");
            response0.StatusCode.Should().BeEquivalentTo(200);
            var realData0 = JsonConvert.DeserializeObject(response0.Content.ReadAsStringAsync().Result);
            var expectedData0 = JsonConvert.DeserializeObject("[{\"id\":5,\"city\":\"Miami\",\"name\":\"CCD\",\"estimatedCost\":1100,\"averageRating\":\"4.1\",\"votes\":100}]");
            realData0.Should().Equals(expectedData0);

            // Get Single restaurant By ID 
            var response1 = await Client.GetAsync("/api/restaurant/query?id=9");
            response1.StatusCode.Should().BeEquivalentTo(200);
            var realData1 = JsonConvert.DeserializeObject(response1.Content.ReadAsStringAsync().Result);
            realData1.Should().Equals("[]");

        }

        // TEST NAME - getEntriesByCity
        // TEST DESCRIPTION - It finds restaurants by city
        [Fact]
        public async Task TestCase3()
        {
            await SeedData();

            // Get restaurants by city
            var response0 = await Client.GetAsync("api/restaurant/query?city=Miami");
            response0.StatusCode.Should().BeEquivalentTo(200);
            var realData0 = JsonConvert.DeserializeObject(response0.Content.ReadAsStringAsync().Result);
            var expectedData0 = JsonConvert.DeserializeObject("[{\"id\":1,\"city\":\"Miami\",\"name\":\"Big Brewskey\",\"estimatedCost\":1500,\"averageRating\":\"4.8\",\"votes\":500},{\"id\":3,\"city\":\"Miami\",\"name\":\"Social\",\"estimatedCost\":1000,\"averageRating\":\"4.2\",\"votes\":50},{\"id\":5,\"city\":\"Miami\",\"name\":\"CCD\",\"estimatedCost\":1100,\"averageRating\":\"4.1\",\"votes\":100}]");
            realData0.Should().BeEquivalentTo(expectedData0);

            // Get restaurants by city
            var response1 = await Client.GetAsync("api/restaurant/query?city=Mumbai");
            response1.StatusCode.Should().BeEquivalentTo(200);
            var realData1 = JsonConvert.DeserializeObject(response1.Content.ReadAsStringAsync().Result);
            realData1.Should().Equals("[]");

        }

        //// TEST NAME - checkNonExistentApi
        //// TEST DESCRIPTION - It should check if an API exists
        [Fact]
        public async Task TestCase4()
        {
            await SeedData();

            // Return with 404 if no API path exists 
            var response0 = await Client.GetAsync("/api/restaurant/id/123");
            response0.StatusCode.Should().BeEquivalentTo(404);

            // Return with 405 if API path exists but called with different method
            var response1 = await Client.GetAsync("/api/restaurant/123");
            response1.StatusCode.Should().BeEquivalentTo(405);

        }

        // TEST NAME - getSortedRestaurantsByRating
        // TEST DESCRIPTION - It finds restaurants sorted by rating
        [Fact]
        public async Task TestCase5()
        {
            await SeedData();

            // Get restaurants sorted by rating
            var response0 = await Client.GetAsync("api/restaurant/sort");
            response0.StatusCode.Should().BeEquivalentTo(200);
            var realData0 = JsonConvert.DeserializeObject(response0.Content.ReadAsStringAsync().Result);
            var expectedData0 = JsonConvert.DeserializeObject("[{\"id\":1,\"city\":\"Miami\",\"name\":\"Big Brewskey\",\"estimatedCost\":1500,\"averageRating\":\"4.8\",\"votes\":500},{\"id\":2,\"city\":\"Florida\",\"name\":\"Social\",\"estimatedCost\":1600,\"averageRating\":\"4.7\",\"votes\":400},{\"id\":3,\"city\":\"Miami\",\"name\":\"Social\",\"estimatedCost\":1000,\"averageRating\":\"4.2\",\"votes\":50},{\"id\":5,\"city\":\"Miami\",\"name\":\"CCD\",\"estimatedCost\":1100,\"averageRating\":\"4.1\",\"votes\":100},{\"id\":4,\"city\":\"Florida\",\"name\":\"CCD\",\"estimatedCost\":1000,\"averageRating\":\"3.8\",\"votes\":200}]");
            realData0.Should().BeEquivalentTo(expectedData0);
        }



        // TEST NAME - updateEntry
        // TEST DESCRIPTION - Update restaurant details
        [Fact]
        public async Task TestCase6()
        {
            await SeedData();

            // Return with 204 if restaurant is updated 
            var body0 = JsonConvert.DeserializeObject("{\"rating\":\"5\",\"votes\":700}");
            var response0 = await Client.PutAsync("/api/restaurant/3", new StringContent(JsonConvert.SerializeObject(body0), Encoding.UTF8, "application/json"));
            response0.StatusCode.Should().Be(204);
        }

        // TEST NAME - getUpdatedEntryById
        // TEST DESCRIPTION - Updated restaurant details
        [Fact]
        public async Task TestCase7()
        {
            await SeedData();
            // Return with 204 if restaurant is updated 
            var body0 = JsonConvert.DeserializeObject("{\"rating\":\"5\",\"votes\":700}");
            var response0 = await Client.PutAsync("/api/restaurant/3", new StringContent(JsonConvert.SerializeObject(body0), Encoding.UTF8, "application/json"));
            response0.StatusCode.Should().Be(204);

            // Check the details of above restaurant 
            var response1 = await Client.GetAsync("/api/restaurant/query?id=3");
            response1.StatusCode.Should().BeEquivalentTo(200);
            var realData1 = JsonConvert.DeserializeObject(response1.Content.ReadAsStringAsync().Result);
            var expectedData1 = JsonConvert.DeserializeObject("[{\"id\":3,\"city\":\"Miami\",\"name\":\"Social\",\"estimatedCost\":1000,\"averageRating\":\"5\",\"votes\":700}]");
            realData1.Should().Equals(expectedData1);
        }

        // TEST NAME - deleteEntry
        // TEST DESCRIPTION - Delete a restaurant by id
        [Fact]
        public async Task TestCase8()
        {
            await SeedData();

            // Return with 204 if restaurant is deleted
            var response0 = await Client.DeleteAsync("/api/restaurant/3");
            response0.StatusCode.Should().Be(204);

            // Return with 404 if restaurant does not exist
            var response1 = await Client.GetAsync("api/restaurant/query?id=3");
            response1.StatusCode.Should().BeEquivalentTo(200);
            var realData1 = JsonConvert.DeserializeObject(response1.Content.ReadAsStringAsync().Result);
            realData1.Should().Equals("[]");
        }

        private CreateForm GenerateCreateForm(string city, string name, string rating, int votes, int cost)
        {
            return new CreateForm()
            {
                City = city,
                Name = name,
                Rating = rating,
                Votes = votes,
                Cost = cost,
                
            };
        }

        private void SetUpClient()
        {

            var builder = new WebHostBuilder()
                .UseStartup<RestaurantCollection.Startup>()
                .ConfigureServices(services =>
                {
                    var context = new RestaurantCollectionContext(new DbContextOptionsBuilder<RestaurantCollectionContext>()
                        .UseSqlite("DataSource=:memory:")
                        .EnableSensitiveDataLogging()
                        .Options);

                    services.RemoveAll(typeof(RestaurantCollectionContext));
                    services.AddSingleton(context);

                    context.Database.OpenConnection();
                    context.Database.EnsureCreated();

                    context.SaveChanges();

                    // Clear local context cache
                    foreach (var entity in context.ChangeTracker.Entries().ToList())
                    {
                        entity.State = EntityState.Detached;
                    }
                });

            _server = new TestServer(builder);

            Client = _server.CreateClient();
        }
    }
}

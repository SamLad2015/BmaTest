using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BmaTestApi.Dtos;
using BmaTestApi.Services;
using BmaTestApi.v1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace BmaTestApi.Tests
{
    public class RestaurantControllerTest
    {
        private RestaurantListingController _controller;
        private IRestaurantService _service;
        
        public RestaurantControllerTest()
        {
            _service = new RestaurantServiceFake();
            _controller = new RestaurantListingController(_service);
        }
        
        [Fact]
        public void GetAll_WhenCalled_ReturnsOkResult()
        {
            var okResult = _controller.GetAllItems(ApiVersion.Default, new RestaurantFilterDto
            {
                Name = ""
            });
            Assert.IsType<OkObjectResult>(okResult);
        }
        
        [Fact]
        public async Task GetAll_WhenCalled_ReturnAllRestaurants()
        {
            var result = _service.GetAll(new RestaurantFilterDto
            {
                Name = ""
            });
            Assert.Equal(3, result.Result.Count());
        }
        
        [Fact]
        public async Task GetAll_WithFilter_WhenCalled_ReturnFilteredRestaurants()
        {
            var result = _service.GetAll(new RestaurantFilterDto
            {
                Name = "test 3",
                FamilyFriendly = true
            });
            Assert.Equal(2, result.Result.Count());
            Assert.Equal("test 33", result.Result.LastOrDefault().Name);
        }
        
        [Fact]
        public async Task GetAll_WithFilterTags_WhenCalled_ReturnFilteredRestaurants()
        {
            var result = _service.GetAll(new RestaurantFilterDto
            {
                CuisineTagIds = "2,3"
            });
            Assert.Equal(2, result.Result.Count());
            Assert.Equal("test 1", result.Result.FirstOrDefault().Name);
        }
        
        [Fact]
        public async Task AddRestaurant_WhenPosted_WithRestaurant_ReturnOkStatus()
        {
            _service.AddRestaurant(new RestaurantRequestDto()
            {
                Name = "test 4",
                Address = "test address for 4",
                FamilyFriendly = true,
                VeganOptions = false,
                Rating = 5,
                CuisineTagIds = new List<int> {1, 3}
            });
            var result = _service.GetAll(new RestaurantFilterDto
            {
                Name = ""
            });
            Assert.Equal("test 4", result.Result.FirstOrDefault(r => r.Name=="test 4").Name);
        }
        
        [Fact]
        public async Task UpdateRestaurant_WhenPosted_WithRestaurant_ReturnOkStatus()
        {
            _service.UpdateRestaurant(3, new RestaurantRequestDto()
            {
                Name = "test  changed",
                Address = "new address for 3",
                FamilyFriendly = true,
                VeganOptions = false,
                Rating = 4,
                CuisineTagIds = new List<int> {4, 3}
            });
            var result = _service.GetAll(new RestaurantFilterDto
            {
                Name = "changed"
            });
            Assert.Equal("test  changed", result.Result.FirstOrDefault().Name);
        }
        
        [Fact]
        public async Task DeleteRestaurant_WhenDeleted_WithRestaurantId_ReturnOkStatus()
        {
            var prevResult = _service.GetAll(new RestaurantFilterDto
            {
                Name = ""
            });
            Assert.Equal(3, prevResult.Result.Count());
            
            _service.DeleteRestaurant(1);
            var result = _service.GetAll(new RestaurantFilterDto
            {
                Name = ""
            });
            Assert.Equal(3, result.Result.Count());
        }
    }
}
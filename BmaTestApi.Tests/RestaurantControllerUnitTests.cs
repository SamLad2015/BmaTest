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
    }
}
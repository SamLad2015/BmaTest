using System;
using System.Linq;
using AutoMapper;
using BmaTestApi.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using BmaTestApi.Repositories;
using System.Collections.Generic;
using BmaTestApi.Entities;
using BmaTestApi.Models;
using BmaTestApi.Helpers;
using BmaTestApi.Services;

namespace BmaTestApi.v1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("api/[controller]")]
    public class RestaurantListingController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantListingController(
            IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet(Name = nameof(GetAllItems))]
        public ActionResult GetAllItems(ApiVersion version, [FromQuery] QueryParameters queryParameters)
        {
            var restaurants = _restaurantService.GetAll(queryParameters);
            
            return Ok(restaurants);
        }

        [HttpPost(Name = nameof(AddItem))]
        public ActionResult<RestaurantDto> AddItem(ApiVersion version, [FromBody] RestaurantRequestDto createDto)
        {
            try
            {
                if (createDto == null)
                {
                    return BadRequest();
                }
                
                _restaurantService.AddRestaurant(createDto);

                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        

        [HttpPut]
        [Route("{id:int}", Name = nameof(UpdateItem))]
        public ActionResult<RestaurantRequestDto> UpdateItem(int id, [FromBody]RestaurantRequestDto updateDto)
        {
            if (updateDto == null)
            {
                return BadRequest();
            }
            
            _restaurantService.UpdateRestaurant(id ,updateDto);

            return Ok();
        }
        
        [HttpDelete]
        [Route("{id:int}", Name = nameof(DeleteItem))]
        public ActionResult<RestaurantRequestDto> DeleteItem(int id)
        {
            _restaurantService.DeleteRestaurant(id);

            return Ok();
        }
    }
}

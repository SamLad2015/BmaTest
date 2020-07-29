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

namespace BmaTestApi.v1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("api/[controller]")]
    public class RestaurantListingController : ControllerBase
    {
        private readonly IRestaurantRepository _testRepository;
        private readonly IMapper _mapper;

        public RestaurantListingController(
            IRestaurantRepository testRepository,
            IMapper mapper)
        {
            _testRepository = testRepository;
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(GetAllItems))]
        public ActionResult GetAllItems(ApiVersion version, [FromQuery] QueryParameters queryParameters)
        {
            IList<RestaurantEntity> testEntities = _testRepository.GetAll(queryParameters).ToList();
            
            return Ok(_mapper.Map<IList<RestaurantDto>>(testEntities));
        }

        [HttpGet]
        [Route("{id:int}", Name = nameof(GetSingleItem))]
        public ActionResult GetSingleItem(ApiVersion version, int id)
        {
            RestaurantEntity item = _testRepository.GetSingle(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost(Name = nameof(AddItem))]
        public ActionResult<RestaurantDto> AddItem(ApiVersion version, [FromBody] RestaurantRequestDto createDto)
        {
            if (createDto == null)
            {
                return BadRequest();
            }
            
            RestaurantEntity toAdd = _mapper.Map<RestaurantEntity>(createDto);

            _testRepository.Add(toAdd);

            if (!_testRepository.Save())
            {
                throw new Exception("Creating a item failed on save.");
            }

            RestaurantEntity item = _testRepository.GetSingle(toAdd.Id);
            return Ok(_mapper.Map<IList<RestaurantDto>>(item));
        }
        

        [HttpPut]
        [Route("{id:int}", Name = nameof(UpdateItem))]
        public ActionResult<RestaurantRequestDto> UpdateItem(int id, [FromBody]RestaurantRequestDto updateDto)
        {
            if (updateDto == null)
            {
                return BadRequest();
            }

            var existingItem = _testRepository.GetSingle(id);

            if (existingItem == null)
            {
                return NotFound();
            }
            _mapper.Map(updateDto, existingItem);

            _testRepository.Update(existingItem);

            if (!_testRepository.Save())
            {
                throw new Exception("Updating a item failed on save.");
            }

            return Ok(_mapper.Map<RestaurantRequestDto>(existingItem));
        }
        
        [HttpDelete]
        [Route("{id:int}", Name = nameof(DeleteItem))]
        public ActionResult<RestaurantRequestDto> DeleteItem(int id, [FromBody]int deleteDtoId)
        {
            var existingItem = _testRepository.GetSingle(deleteDtoId);

            _testRepository.Delete(existingItem);

            if (!_testRepository.Save())
            {
                throw new Exception("Deleting a item failed on save.");
            }

            return Ok(_mapper.Map<RestaurantRequestDto>(existingItem));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BmaTestApi.Dtos;
using BmaTestApi.Entities;
using BmaTestApi.Models;
using BmaTestApi.Repositories;

namespace BmaTestApi.Services
{
    public class RestaurantService: IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        public RestaurantService(IRestaurantRepository testRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _restaurantRepository = testRepository;
        }

        public async Task<IList<RestaurantDto>> GetAll(RestaurantFilterDto queryParameters)
        {
            return _mapper.Map<IList<RestaurantDto>>((await _restaurantRepository.GetAll(queryParameters)));
        }

        public void AddRestaurant(RestaurantRequestDto requestDto)
        {
            RestaurantEntity toAdd = new RestaurantEntity
            {
                Name = requestDto.Name,
                Address = requestDto.Address,
                FamilyFriendly = requestDto.FamilyFriendly,
                VeganOptions = requestDto.VeganOptions,
                Rating = requestDto.Rating
            };

            _restaurantRepository.Add(toAdd);

            foreach (int id in requestDto.CuisineTagIds)
            {
                _restaurantRepository.AddCuisineTag(new RestaurantCuisineEntity
                {
                    CuisineId = id,
                    RestaurantId = toAdd.Id
                });
            }

            if (!_restaurantRepository.Save())
            {
                throw new Exception("Creating a item failed on save.");
            }
        }
        
        public void UpdateRestaurant(int restaurantId, RestaurantRequestDto requestDto)
        {
            var existingItem = _restaurantRepository.GetSingle(restaurantId);
            
            _mapper.Map(requestDto, existingItem.Result);

            var existingTags = _restaurantRepository.GetRestaurantTags(restaurantId);

            foreach (var tag in existingTags.Result)
            {
                _restaurantRepository.RemoveCuisineTag(tag);
            }
            
            foreach (int id in requestDto.CuisineTagIds)
            {
                _restaurantRepository.AddCuisineTag(new RestaurantCuisineEntity
                {
                    CuisineId = id,
                    RestaurantId = restaurantId
                });
            }
            
            _restaurantRepository.Update(existingItem.Result);

            if (!_restaurantRepository.Save())
            {
                throw new Exception("Updating a item failed on save.");
            }
        }
        
        public void DeleteRestaurant(int restaurantId)
        {
            var existingItem = _restaurantRepository.GetSingle(restaurantId);
            
            var existingTags = _restaurantRepository.GetRestaurantTags(restaurantId);

            foreach (var tag in existingTags.Result)
            {
                _restaurantRepository.RemoveCuisineTag(tag);
            }

            _restaurantRepository.Delete(existingItem.Result);

            if (!_restaurantRepository.Save())
            {
                throw new Exception("Deleting a item failed on save.");
            }
        }
    }
}
using System.Collections.Generic;
using BmaTestApi.Dtos;
using BmaTestApi.Models;

namespace BmaTestApi.Services
{
    public interface IRestaurantService
    {
        IList<RestaurantDto> GetAll(QueryParameters queryParameters);
        void AddRestaurant(RestaurantRequestDto requestDto);
        void UpdateRestaurant(int id, RestaurantRequestDto requestDto);
        void DeleteRestaurant(int restaurantId);
    }
}
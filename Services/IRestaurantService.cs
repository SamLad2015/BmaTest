using System.Collections.Generic;
using System.Threading.Tasks;
using BmaTestApi.Dtos;
using BmaTestApi.Models;

namespace BmaTestApi.Services
{
    public interface IRestaurantService
    {
        Task<IList<RestaurantDto>> GetAll(RestaurantFilterDto queryParameters);
        void AddRestaurant(RestaurantRequestDto requestDto);
        void UpdateRestaurant(int id, RestaurantRequestDto requestDto);
        void DeleteRestaurant(int restaurantId);
    }
}
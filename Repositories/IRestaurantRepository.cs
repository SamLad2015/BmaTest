using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BmaTestApi.Dtos;
using BmaTestApi.Entities;
using BmaTestApi.Models;

namespace BmaTestApi.Repositories
{
    public interface IRestaurantRepository
    {
        Task<RestaurantEntity> GetSingle(int id);
        Task<IList<RestaurantCuisineEntity>> GetRestaurantTags(int id);
        void Add(RestaurantEntity item);
        void AddCuisineTag(RestaurantCuisineEntity tag);
        void Update(RestaurantEntity item);
        void Delete(RestaurantEntity item);
        void RemoveCuisineTag(RestaurantCuisineEntity tag);
        Task<IList<RestaurantEntity>> GetAll(RestaurantFilterDto queryParameters);
        bool Save();
    }
}

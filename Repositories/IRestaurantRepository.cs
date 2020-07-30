using System.Collections.Generic;
using System.Linq;
using BmaTestApi.Entities;
using BmaTestApi.Models;

namespace BmaTestApi.Repositories
{
    public interface IRestaurantRepository
    {
        RestaurantEntity GetSingle(int id);
        IList<RestaurantCuisineEntity> GetRestaurantTags(int id);
        void Add(RestaurantEntity item);
        void AddCuisineTag(RestaurantCuisineEntity tag);
        void Update(RestaurantEntity item);
        void Delete(RestaurantEntity item);
        void RemoveCuisineTag(RestaurantCuisineEntity tag);
        IQueryable<RestaurantEntity> GetAll(QueryParameters queryParameters);
        bool Save();
    }
}

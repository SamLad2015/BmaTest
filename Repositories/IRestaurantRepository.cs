using System.Collections.Generic;
using System.Linq;
using BmaTestApi.Entities;
using BmaTestApi.Models;

namespace BmaTestApi.Repositories
{
    public interface IRestaurantRepository
    {
        RestaurantEntity GetSingle(int id);
        void Add(RestaurantEntity item);
        void Update(RestaurantEntity item);
        void Delete(RestaurantEntity item);
        IQueryable<RestaurantEntity> GetAll(QueryParameters queryParameters);
        bool Save();
    }
}

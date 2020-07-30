using BmaTestApi.Entities;
using BmaTestApi.Helpers;
using BmaTestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace BmaTestApi.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly TestDbContext _TestDbContext;

        public RestaurantRepository(TestDbContext TestDbContext)
        {
            _TestDbContext = TestDbContext;
        }

        public RestaurantEntity GetSingle(int id)
        {
            return _TestDbContext.RestaurantEntities.FirstOrDefault(x => x.Id == id);
        }
        
        public IList<RestaurantCuisineEntity> GetRestaurantTags(int id)
        {
            return _TestDbContext.RestaurantCuisineEntities.Where(t => t.RestaurantId == id).ToList();
        }

        public void Add(RestaurantEntity item)
        {
            _TestDbContext.RestaurantEntities.Add(item);
        }

        public void AddCuisineTag(RestaurantCuisineEntity tag)
        {
            _TestDbContext.RestaurantCuisineEntities.Add(tag);
        }

        public void Update(RestaurantEntity item)
        {
            _TestDbContext.RestaurantEntities.Update(item);
        }
        
        public void Delete(RestaurantEntity item)
        {
            _TestDbContext.RestaurantEntities.Remove(item);
        }

        public void RemoveCuisineTag(RestaurantCuisineEntity tag)
        {
            _TestDbContext.RestaurantCuisineEntities.Remove(tag);
        }

        public IQueryable<RestaurantEntity> GetAll(QueryParameters queryParameters)
        {
            IQueryable<RestaurantEntity> allItems = 
                _TestDbContext.RestaurantEntities
                    .Include(r => r.Cuisine)
                    .ThenInclude(cuisine => cuisine.CuisineEntity)
                    .OrderBy(queryParameters.OrderBy);

            return allItems
                .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                .Take(queryParameters.PageCount);
        }

        public bool Save()
        {
            return (_TestDbContext.SaveChanges() >= 0);
        }
    }
}

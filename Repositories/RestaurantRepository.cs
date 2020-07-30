using System;
using BmaTestApi.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BmaTestApi.Dtos;
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

        public async Task<RestaurantEntity> GetSingle(int id)
        {
            return await _TestDbContext.RestaurantEntities.FirstOrDefaultAsync(x => x.Id == id);
        }
        
        public async Task<IList<RestaurantCuisineEntity>> GetRestaurantTags(int id)
        {
            return await _TestDbContext.RestaurantCuisineEntities.Where(t => t.RestaurantId == id)
                .ToListAsync();
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

        public async Task<IList<RestaurantEntity>> GetAll(RestaurantFilterDto queryParameters)
        {
            var allItems =
                _TestDbContext.RestaurantEntities
                    .Include(r => r.Cuisine)
                    .ThenInclude(cuisine => cuisine.CuisineEntity)
                    .Where(r => queryParameters.FamilyFriendly.HasValue
                        ? r.FamilyFriendly == queryParameters.FamilyFriendly.Value
                        : !queryParameters.FamilyFriendly.HasValue
                    )
                    .Where(r => queryParameters.VeganOptions.HasValue
                        ? r.VeganOptions == queryParameters.VeganOptions.Value
                        : !queryParameters.VeganOptions.HasValue
                    )
                    .Where(r => !string.IsNullOrWhiteSpace(queryParameters.Name)
                        ? r.Name.ToLower().Contains(queryParameters.Name.ToLower())
                        : string.IsNullOrWhiteSpace(queryParameters.Name)
                    );

            if (!string.IsNullOrWhiteSpace(queryParameters.CuisineTagIds))
            {
                var ids = StringToIntList(queryParameters.CuisineTagIds).ToList();
                allItems = allItems.Where(r => r.Cuisine.Any(c =>
                    ids.IndexOf(c.CuisineId) > -1));
            }

            return await allItems.ToListAsync();;
        }

        public bool Save()
        {
            return (_TestDbContext.SaveChanges() >= 0);
        }
        
        private static IEnumerable<int> StringToIntList(string str) {
            if (String.IsNullOrEmpty(str))
                yield break;

            foreach(var s in str.Split(',')) {
                int num;
                if (int.TryParse(s, out num))
                    yield return num;
            }
        }
    }
}

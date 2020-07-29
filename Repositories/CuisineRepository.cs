using BmaTestApi.Entities;
using BmaTestApi.Helpers;
using BmaTestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace BmaTestApi.Repositories
{
    public class CuisineRepository : ICuisineRepository
    {
        private readonly TestDbContext _TestDbContext;

        public CuisineRepository(TestDbContext TestDbContext)
        {
            _TestDbContext = TestDbContext;
        }

        public IEnumerable<CuisineEntity> GetAll()
        {
            return _TestDbContext.CuisineEntities.OrderBy(c => c.Name);
        }
    }
}

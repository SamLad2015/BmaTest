using BmaTestApi.Entities;
using BmaTestApi.Helpers;
using BmaTestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BmaTestApi.Repositories
{
    public class CuisineRepository : ICuisineRepository
    {
        private readonly TestDbContext _TestDbContext;

        public CuisineRepository(TestDbContext TestDbContext)
        {
            _TestDbContext = TestDbContext;
        }

        public async Task<IList<CuisineEntity>> GetAll()
        {
            return await _TestDbContext.CuisineEntities.OrderBy(c => c.Name).ToListAsync();
        }
    }
}

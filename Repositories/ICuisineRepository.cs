using System.Collections.Generic;
using System.Linq;
using BmaTestApi.Entities;
using BmaTestApi.Models;

namespace BmaTestApi.Repositories
{
    public interface ICuisineRepository
    {
        IEnumerable<CuisineEntity> GetAll();
    }
}

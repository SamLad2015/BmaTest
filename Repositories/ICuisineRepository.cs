using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BmaTestApi.Entities;
using BmaTestApi.Models;

namespace BmaTestApi.Repositories
{
    public interface ICuisineRepository
    { 
        Task<IList<CuisineEntity>> GetAll();
    }
}

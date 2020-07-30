using System.Collections.Generic;
using System.Threading.Tasks;
using BmaTestApi.Entities;

namespace BmaTestApi.Services
{
    public interface ICuisineService
    {
        Task<IList<CuisineEntity>> GetAllCuisineTags();
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BmaTestApi.Entities;
using BmaTestApi.Repositories;

namespace BmaTestApi.Services
{
    public class CuisineService: ICuisineService
    {
        private ICuisineRepository _cuisineRepository;
        public CuisineService(ICuisineRepository cuisineRepository)
        {
            _cuisineRepository = cuisineRepository;
        }
        public async Task<IList<CuisineEntity>> GetAllCuisineTags()
        {
            return await _cuisineRepository.GetAll();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
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
        public IList<CuisineEntity> GetAllCuisineTags()
        {
            List<CuisineEntity> testEntities = _cuisineRepository.GetAll().ToList();
            return testEntities;
        }
    }
}
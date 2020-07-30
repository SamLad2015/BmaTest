using System.Collections.Generic;
using BmaTestApi.Entities;

namespace BmaTestApi.Services
{
    public interface ICuisineService
    {
        IList<CuisineEntity> GetAllCuisineTags();
    }
}
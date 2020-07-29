using System.Collections.Generic;

namespace BmaTestApi.Entities
{
    public class CuisineEntity: BaseEntity
    {
        public string Name { get; set; }
        public IList<RestaurantCuisineEntity> Cuisine { get; set; }
    }
}
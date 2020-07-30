using System.Collections.Generic;

namespace BmaTestApi.Entities
{
    public class RestaurantEntity: BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public IList<RestaurantCuisineEntity> Cuisine { get; set; }
        public bool FamilyFriendly { get; set; }
        public bool VeganOptions { get; set; }
        public double Rating { get; set; }
    }
}

using System.Collections.Generic;

namespace BmaTestApi.Dtos
{
    public class RestaurantFilterDto
    {
        public string Name { get; set; }
        public bool? FamilyFriendly { get; set; }
        public bool? VeganOptions { get; set; }
        public string CuisineTagIds { get; set; }
    }
}
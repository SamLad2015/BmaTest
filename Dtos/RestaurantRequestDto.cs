using System;
using System.Collections.Generic;
using BmaTestApi.Entities;

namespace BmaTestApi.Dtos
{
    public class RestaurantRequestDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public IList<int> CuisineTagIds { get; set; }
        public bool FamilyFriendly { get; set; }
        public bool VeganOptions { get; set; }
        public float Rating { get; set; }
    }
}

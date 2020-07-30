using System.Collections.Generic;
using BmaTestApi.Entities;

namespace BmaTestApi.Dtos
{
    public class RestaurantDto: RestaurantRequestDto
    {
        public int Id { get; set; }
        public IList<string> Cuisine { get; set; }
    }
}
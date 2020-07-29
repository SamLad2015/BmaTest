using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace BmaTestApi.Entities
{
    public class RestaurantCuisineEntity: BaseEntity
    {
        public int CuisineId { get; set; }
        public int RestaurantId { get; set; }
        [JsonIgnore] 
        [IgnoreDataMember] 
        public RestaurantEntity RestaurantEntity { get; set; }
        [JsonIgnore] 
        [IgnoreDataMember] 
        public CuisineEntity CuisineEntity { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BmaTestApi.Dtos;
using BmaTestApi.Services;

namespace BmaTestApi.Tests
{
    public class RestaurantServiceFake: IRestaurantService
    {
        private IList<RestaurantDto> _restaurants;
        public RestaurantServiceFake()
        {
            _restaurants = new List<RestaurantDto>()
            {
                new RestaurantDto()
                {
                    Id = 1,
                    Name = "test 1",
                    Address = "Address for test 1",
                    Cuisine = new List<string>() {"tag 1", "tag 2", "tag 3"},
                    Rating = 1,
                    FamilyFriendly = true,
                    VeganOptions = false,
                    CuisineTagIds = new List<int>() {1, 2, 3}
                },
                new RestaurantDto()
                {
                    Id = 2,
                    Name = "test 3",
                    Address = "Address for test 2",
                    Cuisine = new List<string>() {"tag 1", "tag 4"},
                    Rating = 3,
                    FamilyFriendly = true,
                    VeganOptions = true,
                    CuisineTagIds = new List<int>() {1, 4}
                },
                new RestaurantDto()
                {
                    Id = 3,
                    Name = "test 33",
                    Address = "Address for test 3",
                    Cuisine = new List<string>() {"tag 2", "tag 3"},
                    Rating = 5,
                    FamilyFriendly = true,
                    VeganOptions = false,
                    CuisineTagIds = new List<int>() {2, 3}
                }
            };
        }

        public async Task<IList<RestaurantDto>> GetAll(RestaurantFilterDto q)
        {
            await Task.Delay(500);
            if (!string.IsNullOrWhiteSpace(q.Name))
            {
                _restaurants = _restaurants.Where(r => r.Name.Contains(q.Name)).ToList();
            }
            if (q.FamilyFriendly.HasValue)
            {
                _restaurants = _restaurants.Where(r => r.FamilyFriendly == q.FamilyFriendly.Value).ToList();
            }
            if (!string.IsNullOrWhiteSpace(q.CuisineTagIds))
            {
                var tagIds = StringToIntList(q.CuisineTagIds).ToList();
                _restaurants = _restaurants.Where(r => 
                    r.CuisineTagIds.Any(t => tagIds.IndexOf(t) > -1))
                    .ToList();
            }
            return _restaurants;
        }

        public async void AddRestaurant(RestaurantRequestDto requestDto)
        {
            await Task.Delay(200);
            var request = new RestaurantDto
            {
                Id = 4,
                Name = requestDto.Name,
                Address = requestDto.Address,
                FamilyFriendly = requestDto.FamilyFriendly,
                VeganOptions = requestDto.VeganOptions,
                Rating = requestDto.Rating,
                CuisineTagIds = requestDto.CuisineTagIds,
                Cuisine = new List<string> {"test 1", "test 3"}
            };
            _restaurants.Add(request);
        }

        public async void UpdateRestaurant(int id, RestaurantRequestDto requestDto)
        {
            var request = new RestaurantDto
            {
                Id = id,
                Name = requestDto.Name,
                Address = requestDto.Address,
                FamilyFriendly = requestDto.FamilyFriendly,
                VeganOptions = requestDto.VeganOptions,
                Rating = requestDto.Rating,
                CuisineTagIds = requestDto.CuisineTagIds,
                Cuisine = new List<string> {"test 1", "test 3"}
            };
            _restaurants.Remove(_restaurants.FirstOrDefault(r => r.Id == id));
            _restaurants.Add(request);
        }

        public async void DeleteRestaurant(int id)
        {
            await Task.Delay(500);
            _restaurants.Remove(_restaurants.FirstOrDefault(r => r.Id == id));
        }
        
        private static IEnumerable<int> StringToIntList(string str) {
            if (String.IsNullOrEmpty(str))
                yield break;

            foreach(var s in str.Split(',')) {
                int num;
                if (int.TryParse(s, out num))
                    yield return num;
            }
        }
    }
}
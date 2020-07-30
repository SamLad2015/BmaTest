using System;
using System.Linq;
using AutoMapper;
using BmaTestApi.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using BmaTestApi.Repositories;
using System.Collections.Generic;
using BmaTestApi.Entities;
using BmaTestApi.Models;
using BmaTestApi.Helpers;
using BmaTestApi.Services;

namespace BmaTestApi.v1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("api/[controller]")]
    public class CuisineController : ControllerBase
    {
        private readonly ICuisineService _cuisineService;

        public CuisineController(
            ICuisineService cuisineService)
        {
            _cuisineService = cuisineService;
        }

        [HttpGet(Name = nameof(GetAll))]
        public ActionResult GetAll(ApiVersion version)
        {
            var cuisineTags = _cuisineService.GetAllCuisineTags();
            
            return Ok(cuisineTags);
        }
    }
}

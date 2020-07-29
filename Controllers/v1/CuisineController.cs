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

namespace BmaTestApi.v1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("api/[controller]")]
    public class CuisineController : ControllerBase
    {
        private readonly ICuisineRepository _testRepository;

        public CuisineController(
            ICuisineRepository testRepository)
        {
            _testRepository = testRepository;
        }

        [HttpGet(Name = nameof(GetAll))]
        public ActionResult GetAll(ApiVersion version)
        {
            List<CuisineEntity> testEntities = _testRepository.GetAll().ToList();
            
            return Ok(testEntities);
        }
    }
}

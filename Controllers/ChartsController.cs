﻿using Microsoft.AspNetCore.Mvc;
using Pet_Store_Api.DTOs;
using Pet_Store_Api.Models.Interfaces;
using System.Security.Cryptography.X509Certificates;

namespace Pet_Store_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : Controller
    {
        private readonly IChartService _chartService;

        public ChartsController(IChartService chartService)
        {
            _chartService = chartService;
        }

        [HttpGet("SpeciesAmountForStores")]
        public async Task<IActionResult> GetSpeciesAmountForStores([FromQuery] int[] ids)
        {

            var speciesAmountForStores = await _chartService.GetSpeciesAmountForStores(ids);

            return Ok(speciesAmountForStores);
        }

        [HttpGet("CompareSpeciesAmountForStores")]
        public async Task<IActionResult> GetCompareSpeciesAmountForStores([FromQuery] int[] ids)
        {
            var compareSpeciesAmountForStores = await _chartService.GetCompareSpeciesAmountForStores(ids);

            return Ok(compareSpeciesAmountForStores);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrossCutting.Identity.Jwt.Contracts;
using CrossCutting.Identity.Jwt.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceHost.Models;

namespace ServiceHost.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IJwtService _jwtService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger , IJwtService jwtService)
        {
            _logger = logger;
            this._jwtService = jwtService;
        }


        [HttpGet]
        [Route("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult> GetToken()
        {
            var testUser = new User
            {
                Id = Guid.NewGuid(),
                UserName = "admin",
                Password = "1q2w3e4r5t6y",
                FullName = "Yaser balaghi",
                Gender = true // as male
            };

            return Content(await _jwtService.GenerateAsync(testUser));
        }


        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
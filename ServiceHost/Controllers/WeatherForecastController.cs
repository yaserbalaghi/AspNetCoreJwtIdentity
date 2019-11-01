using System.Threading.Tasks;
using CrossCutting.Identity.Jwt.Contracts;
using CrossCutting.Identity.Jwt.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ServiceHost.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public WeatherForecastController(IJwtService jwtService)
        {
            this._jwtService = jwtService;
        } 


        [HttpGet]
        [Route("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult> GetToken()
        {
            var testUser = new User
            {
                UserName = "admin",
                Password = "1q2w3e4r5t6y",
                FullName = "Yaser balaghi",
                Gender = true // as male 
            };

            return Content(await _jwtService.GenerateAsync(testUser));
        }


        [HttpGet]
        public ActionResult Get()
        {
            return Content("Welcome, you are authorized.");
        }
    }
}

using System.Threading.Tasks;
using CrossCutting.Identity.Jwt.Contracts;
using CrossCutting.Identity.Jwt.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ServiceHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public UserController(IJwtService jwtService)
        {
            this._jwtService = jwtService;
        }

        [HttpGet]
        [Route("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult> GetToken()
        {
            var fakeUser = new User
            {
                UserName = "admin",
                Password = "1q2w3e4r5t6y",
                FullName = "Yaser balaghi",
                Gender = true, // as male 
                Roles =
                {
                    new Role
                    {
                        Id = 1,
                        Name = "ADMIN"
                    },
                    new Role
                    {
                        Id = 2,
                        Name = "WRITER"
                    }
                }
            };

            return Content(await _jwtService.GenerateAsync(fakeUser));
        }

        [HttpGet]
        [Authorize]
        public ActionResult NormalUser() //OK
        {
            return Content("Welcome Normal User, you are authorized.");
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        [Route("[action]")]
        public ActionResult AdminUser() //OK
        {
            return Content("Welcome Admin User, you are authorized.");
        }

        [HttpGet]
        [Authorize(Roles = "WRITER")]
        [Route("[action]")]
        public ActionResult WriterUser() //OK
        {
            return Content("Welcome Writer User, you are authorized.");
        }

        [HttpGet]
        [Authorize(Roles = "SUPER-ADMIN")]
        [Route("[action]")]
        public ActionResult SuperAdminUser() //forbidden (403 status code) for fake use
        {
            return Content("Welcome Super Admin User, you are authorized.");
        }
    }
}

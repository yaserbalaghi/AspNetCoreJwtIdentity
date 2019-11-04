using System;
using System.Threading.Tasks;
using CrossCutting.Identity.Jwt.Contracts;
using CrossCutting.Identity.Jwt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly JwtUserService _userService;
        private readonly ITokenBuilder _tokenBuilder;

        public UserController(JwtUserService userService, ITokenBuilder tokenBuilder)
        {
            _userService = userService;
            _tokenBuilder = tokenBuilder;
        }

        [AllowAnonymous]
        [HttpGet]
        /* username=admin & password=123456 */
        public async Task<ActionResult> GetToken(String username, String password)
        {
            var user = await _userService.GetAsync(username, password);
            if (user == null) return Content("نام کاربری و یا کلمه عبور نادرست است");
            return Content(await _tokenBuilder.GenerateTokenAsync(user));
        }

        //   |  |
        //   |  |  Use Postman : 
        //   |  |  First login and get a token for the user (admin) by calling 'GetToken' Action and send parameters to it,
        //   |  |  then use the token you received to call the following actions...
        //   |  |   
        //   \  /
        //    \/ 


        [HttpGet]
        [Authorize]
        public ActionResult NormalUser() //OK
        {
            return Content("Welcome normal user, you are authorized.");
        }

        [HttpGet]
        [Authorize(Roles = "SUPERADMIN, ADMIN")]
        public ActionResult AdminUser() //OK
        {
            return Content("Welcome admin user, you are authorized.");
        }

        [HttpGet]
        [Authorize(Roles = "SUPERADMIN, ADMIN, WRITER")]
        public ActionResult WriterUser() //OK
        {
            return Content("Welcome writer user, you are authorized.");
        }

        [HttpGet]
        [Authorize(Roles = "SUPERADMIN")]
        public ActionResult SuperAdminUser() //forbidden (403 status code) for fake use
        {
            return Content("Welcome super admin user, you are authorized.");
        }
    }
}

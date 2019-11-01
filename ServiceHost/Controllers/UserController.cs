using System;
using System.Threading;
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
        private readonly IJwtIdentityService _identityService;
        private readonly IJwtIdentityRepository _identityRepository;

        public UserController(IJwtIdentityService identityService, IJwtIdentityRepository identityRepository)
        {
            this._identityService = identityService;
            this._identityRepository = identityRepository;
        }

        [HttpGet]
        [Route("[action]")]
        [AllowAnonymous]                        /*uname=admin&pwd=123456*/
        public async Task<ActionResult> GetToken(String uname, String pwd, CancellationToken cancellationToken)
        {
            var user = await _identityRepository.Get(uname, pwd, cancellationToken);
            if (user == null)
                return NotFound("Username or password is incorrect");

            return Content(await _identityService.GenerateTokenAsync(user));
        }

        //   |  |
        //   |  |  First login and get a token for the user (admin) by calling GetToken Action and send parameters to it,
        //   |  |  then call the following actions...
        //   |  |   
        //   \  /
        //    \/ 

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

using System;
using System.Threading;
using System.Threading.Tasks;
using CrossCutting.Identity.Jwt.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

            return Content(_identityService.GenerateToken(user));
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
        [Route("[action]")]
        public ActionResult AdminUser() //OK
        {
            return Content("Welcome admin user, you are authorized.");
        }

        [HttpGet]
        [Authorize(Roles = "SUPERADMIN, ADMIN, WRITER")]
        [Route("[action]")]
        public ActionResult WriterUser() //OK
        {
            return Content("Welcome writer user, you are authorized.");
        }

        [HttpGet]
        [Authorize(Roles = "SUPERADMIN")]
        [Route("[action]")]
        public ActionResult SuperAdminUser() //forbidden (403 status code) for fake use
        {
            return Content("Welcome super admin user, you are authorized.");
        }
    }
}

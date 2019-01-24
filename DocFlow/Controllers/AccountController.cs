using System.Linq;
using System.Threading.Tasks;
using DocFlow.BusinessLayer.Interfaces;
using DocFlow.Data;
using DocFlow.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IAuthentication _authentication;

        public AccountController(
                DocFlowCotext context,
                IAuthentication authentication
            ) : base(context)
        {
            _authentication = authentication;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserModel userModel)
        {   
            if (_context.Users.Any(x => x.UserName == userModel.UserName))
            {
                string token = await _authentication.AuthorizationUser(userModel.UserName, userModel.Password);
                return Json(new { access_token = token });
            }
            return NotFound(userModel);
        }
    }
}
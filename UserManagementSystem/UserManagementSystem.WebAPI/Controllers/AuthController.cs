using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using UserManagementSystem.Business.Interfaces;
using UserManagementSystem.Business.Models;

namespace UserManagementSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISecurityService _securityService;

        public AuthController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> LogIn([FromBody] LoginModel logInModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _securityService.ValidateEmailAndPassword(logInModel.Email, logInModel.Password);
                if (user == null)
                    return Unauthorized("Unknown username or password");

                if (user.IsBlackListed)
                {
                    ModelState.AddModelError(nameof(user.Email), "Your profile is not active");

                    return BadRequest(ModelState);
                }

                var token = await _securityService.GenerateToken(user);

                return Ok(token);
            }
            else
            {
                ModelState.AddModelError(nameof(logInModel), "Email and password are required");
                return BadRequest(ModelState);
            }
        }
    }
}

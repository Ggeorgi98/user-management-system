using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using UserManagementSystem.Business.Infrastructure;
using UserManagementSystem.Business.Interfaces;
using UserManagementSystem.Business.Models;
using UserManagementSystem.WebAPI.Infrastructure;

namespace UserManagementSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ILoggedUserHelper _loggedUserHelper;
        public UsersController(IUsersService usersService, ILoggedUserHelper loggedUserHelper)
        {
            _usersService = usersService;
            _usersService.ValidationDictionary = new ValidationDictionary(ModelState);
            _loggedUserHelper = loggedUserHelper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetUserById(Guid id)
        {
            var response = _usersService.GetResponseModel(x => x.Id == id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(PagedListModel<UserResponseModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetPagedAllUsers([FromQuery]UserRequestListModel requestModel)
        {
            var result = _usersService.GetPagedUsers(requestModel);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("register")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
        [AllowAnonymous]
        public IActionResult CreateUser([FromBody]UserCreateModel model)
        {
            if (_usersService.GetResponseModel(x => x.Email == model.Email) != null)
            {
                ModelState.AddModelError(nameof(model.Email), "Email exists");

                return BadRequest(ModelState);
            }

            model.IsBlackListed = false;

            var user = _usersService.Insert(model);

            if (!_usersService.Save())
            {
                ModelState.AddModelError(nameof(user), "Failed to save");
                return BadRequest(ModelState);
            }

            return Created(Request.Path.Value, user.Id);
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [Authorize]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserModel model)
        {
            var loggedUserId = await _loggedUserHelper.GetLoggedUserIdAsync(HttpContext);
            var loggedUserRole = await _loggedUserHelper.GetLoggedUserRoleAsync(HttpContext);

            if (Guid.Parse(loggedUserId) != id && loggedUserRole != UserRoleConstants.AdminRole)
            {
                return Forbid();
            }

            if (!await _usersService.DoesEntityExistAsync(id))
                return NotFound("There is no user with this id");

            var user = _usersService.Update(id, model);

            if (!_usersService.ValidationDictionary.IsValid())
            {
                return BadRequest(_usersService.ValidationDictionary.GetModelState());
            }

            if (!_usersService.Save())
            {
                ModelState.AddModelError(nameof(user), "Failed to save");
                return BadRequest(ModelState);
            }

            return NoContent();
        }        

        [HttpPost("changePassword")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Authorize]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            var loggedUserId = await _loggedUserHelper.GetLoggedUserIdAsync(HttpContext);
            var loggedUserRole = await _loggedUserHelper.GetLoggedUserRoleAsync(HttpContext);

            if (Guid.Parse(loggedUserId) != model.UserID && loggedUserRole != UserRoleConstants.AdminRole)
            {
                return Forbid();
            }

            if (!await _usersService.DoesEntityExistAsync(model.UserID))
            {
                return NotFound("There is no user with this id");
            }

            if (!_usersService.ChangePassword(model.UserID, model))
            {
                return BadRequest(_usersService.ValidationDictionary.GetModelState());
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [Authorize(Roles = UserRoleConstants.AdminRole)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await _usersService.DoesEntityExistAsync(id))
                return NotFound("There is no user with this id");

            if (!_usersService.Delete(id))
            {
                ModelState.AddModelError(nameof(id), $"There was an error while trying to delete the user with id - {id}");
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
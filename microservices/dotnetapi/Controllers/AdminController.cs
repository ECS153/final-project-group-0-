using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using dotnetapi.Models.Users;
using dotnetapi.Helpers;
using dotnetapi.Services;

namespace WebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private IUserService _userService;
        private readonly AppSettings _AppSettings;

        public AdminController(IUserService userService, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _AppSettings = appSettings.Value;
        }

        [HttpPost("register")]
        public IActionResult RegisterAdmin([FromBody]UserCreateModel model)
        {
            try
            {
                _userService.Create(model, "Admin");
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { title = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user =  _userService.GetById(id);
            if (user == null) {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UserUpdateModel model)
        {
            model.Id = id;
            try {
                _userService.Update(model);
                return Ok();
            }
            catch (AppException ex) {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }
    }
}

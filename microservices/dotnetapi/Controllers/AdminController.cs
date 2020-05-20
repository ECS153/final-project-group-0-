using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

using dotnetapi.Services;
using dotnetapi.Entities;
using dotnetapi.Models.Users;
using dotnetapi.Helpers;

namespace WebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _AppSettings;

        public AdminController(IUserService userService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _AppSettings = appSettings.Value;
        }

        [HttpPost("registerAdmin")]
        public IActionResult RegisterAdmin([FromBody]RegisterModel model)
        {
            var user = _mapper.Map<User>(model);
            try
            {
                user.Role = "Admin";
                _userService.Create(user, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
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
            if (user == null)
                return NotFound();
            var model = _mapper.Map<ViewModel>(user);

            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UpdateModel model)
        {
            var user = _mapper.Map<User>(model);
            user.Id = id;
            try {
                _userService.Update(user, model.Password);
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

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

using dotnetapi.Services;
using dotnetapi.Entities;
using dotnetapi.Models.Users;
using dotnetapi.Helpers;

using dotnetapi.Models.Credentials;


namespace dotnetapi.Controllers 
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CredentialController : ControllerBase
    {
        private ICredentialService _service;
        private IMapper _mapper;
        public CredentialController(ICredentialService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        
        [HttpPost("new")]
        public IActionResult Register(CredentialCreateModel model) 
        {
            int userId = int.Parse(User.Identity.Name);             

            try {
                _service.Create(model, userId);
                return Ok();
            }
            catch (AppException e) {
                return BadRequest(new {message = e.Message});
            }
        }

        [HttpGet]
        public IActionResult Read(CredentialReadModel model)
        {
            int userId = int.Parse(User.Identity.Name);
            
            List<Credential> credentials = _service.Read(model, userId);
            
            return Ok(credentials);
        }

        [HttpPost]
        public IActionResult Update(CredentialUpdateModel model)
        {
            int userId = int.Parse(User.Identity.Name);
            try {
                _service.Update(model, userId);
                return Ok();
            }
            catch (AppException e) {
                return BadRequest(new {message = e.Message});
            }
        }

        [HttpDelete]
        public IActionResult Delete(CredentialDeleteModel model)
        {
            int userId = int.Parse(User.Identity.Name);
            try {
                _service.Delete(model, userId);
                return Ok();
            }
            catch (AppException e) {
                return BadRequest(new {message = e.Message});
            }
        }
    }


}
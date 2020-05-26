using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using dotnetapi.Helpers;
using dotnetapi.Models.Credentials;
using dotnetapi.Services;

namespace dotnetapi.Controllers 
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CredentialController : ControllerBase
    {
        private ICredentialService _service;
        public CredentialController(ICredentialService service)
        {
            _service = service;
        }
        
        [HttpPost("new")]
        public IActionResult Create([FromBody]CredentialCreateModel model) 
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
        public IActionResult Read([FromQuery]CredentialReadModel model)
        {
            int userId = int.Parse(User.Identity.Name); 
            List<CredentialReadModel> credentials = _service.Read(model, userId);
            
            return Ok(credentials);
        }

        [HttpPost]
        public IActionResult Update([FromBody]CredentialUpdateModel model)
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
        public IActionResult Delete([FromBody]CredentialDeleteModel model)
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
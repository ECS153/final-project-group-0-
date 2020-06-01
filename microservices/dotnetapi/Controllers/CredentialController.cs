using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using dotnetapi.Entities;
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
        private IMapper _mapper;
        
        public CredentialController(ICredentialService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        
        [HttpPost("new")]
        public IActionResult Create([FromBody]CredentialCreateModel model) 
        {
            int userId = int.Parse(User.Identity.Name);             
            try {
                _service.Create(_mapper.Map<Credential>(model), userId);
                return Ok();
            }
            catch (AppException e) {
                return BadRequest(new {Title = e.Message});
            }
        }

        [HttpGet]
        public IActionResult Read([FromQuery]CredentialReadModel model)
        {
            int userId = int.Parse(User.Identity.Name); 
            var credentials = _mapper.Map<List<CredentialReadModel>>(_service.Read(_mapper.Map<Credential>(model), userId));
        
            return Ok(credentials);
        }

        [HttpPost]
        public IActionResult Update([FromBody]CredentialUpdateModel model)
        {
            int userId = int.Parse(User.Identity.Name);
            try {
                _service.Update(_mapper.Map<Credential>(model), userId);
                return Ok();
            }
            catch (AppException e) {
                return BadRequest(new {Title = e.Message});
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromBody]CredentialDeleteModel model)
        {
            int userId = int.Parse(User.Identity.Name);
            try {
                _service.Delete(_mapper.Map<Credential>(model), userId);
                return Ok();
            }
            catch (AppException e) {
                return BadRequest(new {Title = e.Message});
            }
        }
    }


}


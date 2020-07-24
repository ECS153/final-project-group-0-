using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

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
        private ICredentialService _credService;
        private IUserService _userService;
        private IMapper _mapper;

        public CredentialController(ICredentialService credService, IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _credService = credService;
            _mapper = mapper;
        }
        
        [HttpPost("new")]
        public IActionResult New([FromBody]CredentialCreateModel model) 
        {
            User user = _userService.Read(int.Parse(User.Identity.Name));   
            Credential cred = _mapper.Map<Credential>(model);
            cred.UserId = user.Id;   
            try {
                cred.ValueHash = Encrypt(model.Value, user.PublicCredKey);
                _credService.Create(cred);
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
            Credential cred = _mapper.Map<Credential>(model);
            cred.UserId = userId;
            var credentials = _mapper.Map<List<CredentialReadModel>>(_credService.Read(cred));
            return Ok(credentials);
        }

        [HttpPost]
        public IActionResult Update([FromBody]CredentialUpdateModel model)
        {
            int userId = int.Parse(User.Identity.Name);
            Credential cred = _mapper.Map<Credential>(model);
            cred.UserId = userId;
            try {
                _credService.Update(cred);
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
            Credential cred = _mapper.Map<Credential>(model);
            cred.UserId = userId;
            try {
                _credService.Delete(cred);
                return Ok();
            }
            catch (AppException e) {
                return BadRequest(new {Title = e.Message});
            }
        }

        private static string Encrypt(string textToEncrypt, string publicKeyString)
        {
            var bytesToEncrypt = Encoding.UTF8.GetBytes(textToEncrypt);

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try {               
                    rsa.FromXmlString(publicKeyString.ToString());
                    var encryptedData = rsa.Encrypt(bytesToEncrypt, true);
                    var base64Encrypted = Convert.ToBase64String(encryptedData);
                    return base64Encrypted;
                } finally {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }
    }
}


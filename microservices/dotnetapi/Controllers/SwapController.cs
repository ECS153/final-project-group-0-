using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Security.Cryptography;

using dotnetapi.Entities;
using dotnetapi.Services;
using dotnetapi.Helpers;
using dotnetapi.Models.Requests;

namespace dotnetapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    
    public class SwapController : ControllerBase
    {
        private ISwapService _swapService;
        private ICredentialService _credService;
        private IMapper _mapper;
    
        public SwapController(ISwapService swapService, ICredentialService credService, IMapper mapper)
        {
            _swapService = swapService;
            _credService = credService;
            _mapper = mapper;
        }
        
        ////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////// Request Methods //////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////
        [HttpPost("new")]
        public IActionResult New([FromBody] SubmitRequestModel model)
        {
            int userId = int.Parse(User.Identity.Name);
            var ReqSwap = _mapper.Map<RequestSwap>(model);
            
            ReqSwap.UserId = userId; 
            ReqSwap.Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            
            _swapService.Enqueue(ReqSwap);
            
            return Ok();
        }

        [HttpGet]
        public IActionResult GetHead()
        {
            var userId = int.Parse(User.Identity.Name);
            var reqSwap = _swapService.Front(userId);
            return Ok(_mapper.Map<ReadSwapModel>(reqSwap));
        }

        [HttpDelete]
        public IActionResult Pop()
        {
            var userId = int.Parse(User.Identity.Name);
            try {
                _swapService.Dequeue(userId);
                return Ok();
            }
            catch(AppException e) {
                return BadRequest(new { Title = e.Message });
            }
        }

        [HttpPost]
        public IActionResult Submit([FromBody]SubmitSwapModel model)
        {
            int userId = int.Parse(User.Identity.Name);

            // Grab the top request Swap as well as the credential the user wants to use
            RequestSwap reqSwap = _swapService.Front(userId);
            if (reqSwap == null) {
                return BadRequest(new {Title = "User does not have any pending request Swaps"});
            }

            Credential cred = new Credential();
            cred.Id = model.CredentialId;
            cred.UserId = userId;
            cred.Domain = reqSwap.Domain;
            cred = _credService.Read(cred)[0];
            if (cred == null) {
                return BadRequest(new {Title = "User is not allowed to use this credential on this domain"});
            }

            try {
                String valueHash = Decrypt(cred.ValueHash, model.PrivateKey);
                _swapService.Swap(userId, valueHash);
                return Ok();
            }
            catch (AppException e) {
                return BadRequest(new { Title = e.Message });

            // If private key is not correct, this exception will trigger
            } catch(FormatException e) {
                return BadRequest(new { Title = e.Message });
            } catch(Exception e) {
                return BadRequest(new {Title = e.Message});
            }
        }


        private static string Decrypt(string textToDecrypt, string privateKeyString)
        {
            var bytesToDescrypt = Encoding.UTF8.GetBytes(textToDecrypt);

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    // server decrypting data with private key                    
                    rsa.FromXmlString(privateKeyString);

                    var resultBytes = Convert.FromBase64String(textToDecrypt);
                    var decryptedBytes = rsa.Decrypt(resultBytes, true);
                    var decryptedData = Encoding.UTF8.GetString(decryptedBytes);
                    return decryptedData.ToString();
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

    }
}

using Assignment2.Data;
using Assignment2.Dtos;
using Assignment2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Assignment2.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IWebAPIRepo _repository;

        public UserController(IWebAPIRepo repository)
        {
            _repository = repository;
        }

        [Authorize]
        [Authorize(AuthenticationSchemes = "MyAuthentication")]
        [Authorize(Policy = "UserOnly")]
        [HttpGet("GetVersionA")]
        public ActionResult<string> GetApiVersion()
        {
            return Ok("v1");
        }


        [HttpPost("Register")]
        public ActionResult<string> RegisterUser(UserInDto user)
        {
            User u = new User { UserName = user.UserName, Password = user.Password, Address = user.Address };
            string response = _repository.RegisterUser(u);
            return Ok(response);
        }
    }
}

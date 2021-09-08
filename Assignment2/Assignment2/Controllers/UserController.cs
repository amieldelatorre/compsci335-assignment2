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

        [Authorize]
        [Authorize(AuthenticationSchemes = "MyAuthentication")]
        [Authorize(Policy = "UserOnly")]
        [HttpPost("PurchaseItem")]
        public ActionResult<OrderOutDto> PurchaseItems(OrderInDto order)
        {
            ClaimsIdentity ci = HttpContext.User.Identities.FirstOrDefault();
            Claim c = ci.FindFirst("UserName");
            string userName = c.Value;
            Order o = new Order { UserName = userName, ProductID = order.ProductID, Quantity = order.Quantity };
            Order addedOrder = _repository.AddOrder(o);

            OrderOutDto oOut = new OrderOutDto
            {
                Id = addedOrder.Id,
                UserName = addedOrder.UserName,
                ProductID = addedOrder.ProductID,
                Quantity = addedOrder.Quantity
            };

            return Ok(oOut);
        }

        [Authorize]
        [Authorize(AuthenticationSchemes = "MyAuthentication")]
        [Authorize(Policy = "UserOnly")]
        [HttpGet("PurchaseSingleItem/{productId}")]
        public ActionResult<OrderOutDto> PurchaseSingleItem(int productId)
        {
            ClaimsIdentity ci = HttpContext.User.Identities.FirstOrDefault();
            Claim c = ci.FindFirst("UserName");
            string userName = c.Value;
            Order o = new Order { UserName = userName, ProductID = productId, Quantity = 1 };
            Order addedOrder = _repository.AddOrder(o);

            OrderOutDto oOut = new OrderOutDto
            {
                Id = addedOrder.Id,
                UserName = addedOrder.UserName,
                ProductID = addedOrder.ProductID,
                Quantity = addedOrder.Quantity
            };

            return Ok(oOut);
        }
    }
}

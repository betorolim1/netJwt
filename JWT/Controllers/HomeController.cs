using JWT.Models;
using JWT.Repositories;
using JWT.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Controllers
{
    [Route("v1/account")]
    public class HomeController : Controller
    {
        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult<dynamic> Authenticate([FromBody] User userModel)
        {
            var user = UserRepository.Get(userModel.UserName, userModel.Password);

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);

            user.Password = "";

            return new
            {
                user = user,
                token = token
            };
        }

        [HttpGet("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet("authenticated")]
        [Authorize]
        public string Authenticated() => $"Authenticado - {User.Identity.Name}";

        [HttpGet("employee")]
        [Authorize(Roles = "employee,manager")]
        public string Employee() => "Funcionário";

        [HttpGet("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Gerente";
    }
}

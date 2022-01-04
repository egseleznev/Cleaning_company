using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LRSeleznev.Data.Models;
using LRSeleznev.Authorization;
using Json;

namespace LRSeleznev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        // тестовые данные вместо использования базы данных
        private List<Person> people = new List<Person>
        {
            new Person {Login="admin@gmail.com", Password="58f6546c3b16d9aa2a57d1014cabb298", Role = "admin" },
            new Person { Login="qwerty@gmail.com", Password="1515afe0c57c17502fffe54205666b64", Role = "user" },
            new Person {Login="admin",Password="827ccb0eea8a706c4c34a16891f84e7b",Role = "admin"}
        };

        [HttpPost("token")]
        public IActionResult Token(Person person)
        {

            var identity = GetIdentity(person.Login, person.Password);
            if (identity == null)
            {
                return BadRequest(new { errorText = person.Login + person.Password });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt
            };

            return Json(response);
        }


        private ClaimsIdentity GetIdentity(string login, string password)
        {
            Person person = people.FirstOrDefault(x => x.Login == login && x.Password == password);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}
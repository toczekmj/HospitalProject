using HospitalProject.Models;
using HospitalProject.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using TaskAuthenticationAuthorization.Models;

namespace TaskAuthenticationAuthorization.Controllers
{

    public class AccountController : Controller
    {
        private IAccountRepository accRepo;
        private IConfiguration configuration;

        public AccountController(IAccountRepository _accRepo, IConfiguration _configuration)
        {
            accRepo = _accRepo;
            configuration = _configuration;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            if (ModelState.IsValid)
            {
                var t = accRepo.CheckLogin(user.Email, user.Password);
                if (t == null)
                {
                    ModelState.AddModelError("Password", "Incorrect email and/or password");
                }
                else
                {
                    var res = accRepo.Login(t.Email, user.Password);
                    await Authenticate(res);




                    return RedirectToAction("Index", "Home");
                }
            }
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (!accRepo.Check(user.Email))
                {
                    ModelState.AddModelError(string.Empty, "Email is already in use");
                }
                else
                {

                    accRepo.Add(user);
                    await Authenticate(user);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(user);
        }





        private async Task Authenticate(User user)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                //new Claim(ClaimsIdentity.DefaultRoleClaimType, "default")
            };

            if(user.Role == "admin")
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, "admin"));
            }

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        //private void CreateHashPass(string password, out byte[] hashPassword, out byte[] saltPassword)
        //{
        //    using (var hmac = new HMACSHA512())
        //    {
        //        saltPassword = hmac.Key;
        //        hashPassword = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //    }
        //}

        //private bool VerifyPasswordHash(string password, byte[] hash, byte[] salt)
        //{
        //    using (var hmac = new HMACSHA512(salt))
        //    {
        //        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //        return computedHash.SequenceEqual(hash);
        //    }
        //}

        //private string CreateToken(User user)
        //{
        //    List<Claim> claims = new List<Claim>()
        //    {
        //        new Claim(ClaimTypes.Email, user.Email)
        //    };
        //    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
        //        configuration.GetSection("AppSettings:Token").Value));
        //    var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        //    var token = new JwtSecurityToken(
        //        claims: claims,
        //        expires: DateTime.Now.AddMinutes(1),
        //        signingCredentials: cred
        //        );
        //    var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        //    return jwt;
        //}
    }
}

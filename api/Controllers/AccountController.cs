using api.Dtos.Account;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _usermanager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> usermanager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _usermanager = usermanager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if(!ModelState.IsValid) { return BadRequest(ModelState); }

            var user = await _usermanager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName);
            if(user == null) { return Unauthorized("Invalid Username"); }

            var result = _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if(!result.IsCompletedSuccessfully) { return Unauthorized("Username not found and/or password is incorrect"); } 

            return Ok(
                new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user),
                }
            );
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto register)
        {
            try 
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var appUser = new AppUser
                {
                    UserName = register.Username,
                    Email = register.Email,
                };

                var createdUser = await _usermanager.CreateAsync(appUser, register.Password);

                if(createdUser.Succeeded)
                {
                    var roleResult = await _usermanager.AddToRoleAsync(appUser, "User");
                    if(roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto
                            {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Token = _tokenService.CreateToken(appUser),
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            } 
            catch (Exception e)
            {
                    return StatusCode(500, e);
            }
        }

    }
}
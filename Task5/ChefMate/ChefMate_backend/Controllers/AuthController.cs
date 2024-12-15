using ChefMate_backend.Controllers.RequestModels;
using ChefMate_backend.Models;
using ChefMate_backend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChefMate_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly RoleManager<ChefMateRole> _roleManager;
        private readonly UserManager<ChefMateUser> _userManager;
        private readonly SignInManager<ChefMateUser> _signInManager;
        private readonly JwtTokenService _jwtTokenService;

        public AuthController(
            RoleManager<ChefMateRole> roleManager,
            UserManager<ChefMateUser> userManager,
            SignInManager<ChefMateUser> signInManager,
            JwtTokenService jwtTokenService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
        }

        [HttpGet("init-roles")]
        public async Task <IActionResult> InitRoles()
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new ChefMateRole { Name = "Admin" });
            }

            if (!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(new ChefMateRole { Name = "User" });
            }

            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ChefMateUser
            {
                UserName = request.Email,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _userManager.AddToRoleAsync(user, "User");
            return Ok(new { Message = "User registered successfully." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!signInResult.Succeeded)
            {
                return Unauthorized("Invalid credentials.");
            }

            var token = await _jwtTokenService.GenerateTokenAsync(user);

            return Ok(new
            {
                Token = token,
            });
        }
    }
}

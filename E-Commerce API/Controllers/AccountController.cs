using E_commerc_Servers.Services.DTO.AccountDTO;
using E_commerce_Core.DTO.AccountDTO;
using E_commerce_Core.Entityes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<User> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        // ================= Register =================
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _userManager.FindByEmailAsync(registerDTO.Email) != null)
                return BadRequest(new { message = "Email already exists" });

            if (await _userManager.FindByNameAsync(registerDTO.UserName) != null)
                return BadRequest(new { message = "Username already exists" });

            User user = new()
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (result.Succeeded)
            {
                if (await _roleManager.RoleExistsAsync("Customer"))
                    await _userManager.AddToRoleAsync(user, "Customer");

                return Ok(new { message = "User registered successfully" });
            }

            return BadRequest(result.Errors);
        }

        // ================= Login =================
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return Unauthorized(new { message = "Invalid username or password" });

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        // ================= Get All Users =================
        [HttpGet("users")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllUsers(int page = 1, int pageSize = 10)
        {
            var users = _userManager.Users
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new responsAllUserDTO
                {
                    id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email
                }).ToList();

            return Ok(users);
        }


        // ================= Get User By Id =================
        [HttpGet("users/{Email}")]
        [Authorize(Roles ="Admin")]
        
        public async Task<IActionResult> GetUserById(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(new getUserByIdDTO
            {
                id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            });
        }

        // ================= Get User Roles =================
        [HttpGet("users/rolesBy/{email}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetUserRoles(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound(new { message = "User not found" });

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Any())
                return NotFound(new { message = "User has no roles" });

            return Ok(new { roles });
        }

        // ================= Add Role To User =================
        [HttpPost("roles/addTOUser")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> AddRoleToUser( [FromBody]addRoleDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
                return NotFound(new { message = "User not found" });

            if (!await _roleManager.RoleExistsAsync(model.RoleName))
                return BadRequest(new { message = "Role does not exist" });

            var result = await _userManager.AddToRoleAsync(user, model.RoleName);
            if (result.Succeeded)
                return Ok(new { message = "Role added successfully" });

            return BadRequest(result.Errors);
        }

        // ================= Delete User =================
        [HttpDelete("users/{Email}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteUser(string Email) 
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
                return NotFound(new { message = "User not found" });

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                return Ok(new { message = "User deleted successfully" });

            return BadRequest(result.Errors);
        }
        [HttpPut("Updateusers/{Email}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(string Email, [FromBody]UpdateUserDTO updateUserDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
                return NotFound(new { message = "User not found" });
            user.UserName = updateUserDto.UserName;
            user.Email = updateUserDto.Email;
            user.PhoneNumber = updateUserDto.PhoneNumber;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return Ok(new { message = "User updated successfully" });
            return BadRequest(result.Errors);
        }
        [HttpDelete("User/role/{Email}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserRole(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if(user == null) return NotFound(new { message = "user Not Found " } );
           var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Any()) return NotFound(new { message = "User has no roles" });
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (result.Succeeded)
                return Ok(new { message = $"User roles deleted successfully: {string.Join(",",roles)} " });
            return BadRequest(new { Error=result.Errors.Select(e=>e.Description)});

        }
    }
}

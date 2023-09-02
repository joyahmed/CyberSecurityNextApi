using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CyberSecurityNextApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IAuthRepository _authRepo;
        public UserController(IAuthRepository authRepo)
        {

           _authRepo = authRepo;

        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> GetUsers()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);

            return Ok(await _authRepo.GetAllUsers());
        }


        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register (UserRegisterDto request) 
        {

            int? userId = null;

            var nameIdentifierClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (nameIdentifierClaim != null && int.TryParse(nameIdentifierClaim.Value, out int parsedUserId))
            {
                userId = parsedUserId; 
            }

            var response = await _authRepo.Register(
                new User { Name = request.Name, Email = request.Email, Role = request.Role, IsActive = request.IsActive, CreatedBy = userId }, request.Password
            );

            if(!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login (UserLoginDto request) 
        {
            var response = await _authRepo.Login(request.Email, request.Name, request.Password);

            if(!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> UpdateUser(UpdateUserDto updatedUser)
        {

            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
           

            var response = await _authRepo.UpdateUser(userId, updatedUser);

            if (response.Data is null) return NotFound(response);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> GetUser(int id)
        {
            return Ok(await _authRepo.GetUserById(id));
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> DeleteUser(int id)
        {
            var response = await _authRepo.DeleteUser(id);

            if (response.Data is null)  return NotFound(response);

            return Ok(response);
        }



    }
}

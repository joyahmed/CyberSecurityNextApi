
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CyberSecurityNextApi.Data
{
    public class AuthRepository : IAuthRepository
    {

        private readonly DataContext _context;
        private readonly IConfiguration _configuration; // for accessing Token in AppSettings
        private readonly IMapper _mapper;

        public AuthRepository(DataContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> GetAllUsers()
        {

            var response = new ServiceResponse<List<GetUserDto>>();

            var users = await _context.Users.ToListAsync();


            response.Data = users.Select(u => _mapper.Map<GetUserDto>(u)).ToList();

            return response;
        }



        public async Task<ServiceResponse<GetUserDto>> GetUserById(int id)
        {
            var response = new ServiceResponse<GetUserDto>();

            var user = await _context.Users.SingleOrDefaultAsync(c => c.Id == id);

            response.Data = _mapper.Map<GetUserDto>(user);

            return response;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id)
        {
            var response = new ServiceResponse<List<GetUserDto>>();

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id) ?? throw new Exception("User with Id '{id} not found.");

                _context.Users.Remove(user);

                await _context.SaveChangesAsync();

                response.Data = await _context.Users.Select(u => _mapper.Map<GetUserDto>(u)).ToListAsync();
            }
            catch (Exception ex)
            {

                response.Success = false;
                response.Message = ex.Message;
            }

            return response;

        }

        public async Task<ServiceResponse<string>> Login(string email, string name, string password)
        {
            var response = new ServiceResponse<string>();


            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email || u.Name == name);



            if(user is null) {
                response.Success = false;
                response.Message = "User not found.";
            }

            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PassworSalt))
            {
                response.Success = false;
                response.Message = "Wrong password.";

            }
            else
            {
                response.Data = CreateToken(user);
                response.Message = $"Successfully logged in {user.Name}";
            }

            return response;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            var response = new ServiceResponse<int>
            {
                Data = user.Id
            };


            if (await UserExists(user.Email))
            {
                response.Success = false;
                response.Message = "User already exists.";
                return response;
            }

            CreatepasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PassworSalt = passwordSalt;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return response;
        }

        public async Task<ServiceResponse<GetUserDto>> UpdateUser(int? userId, UpdateUserDto updatedUser)
        {
            var response = new ServiceResponse<GetUserDto>();

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == updatedUser.Id);

                if(user is null)
                {
                    throw new Exception("User with Id '{updatedUser.Id}' not found");
                }

                _mapper.Map(updatedUser, user);

                user.Name = updatedUser.Name;
                user.Email = updatedUser.Email;
                user.Role = updatedUser.Role;
                user.IsActive = updatedUser.IsActive;

                CreatepasswordHash(updatedUser.Password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PassworSalt = passwordSalt;


                user.UpdatedBy = userId;

                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetUserDto>(user);

            }
            catch (Exception ex)
            {

                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }


        public async Task<bool> UserExists(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email.ToLower());
        }

        private void CreatepasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPasswordHash(string password, byte[] passwordhash, byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(passwordhash);
        }


        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            };

            var appSettingsToken = _configuration.GetSection("AppSettings:Token").Value ?? throw new Exception("AppSettings Token is null!");

            SymmetricSecurityKey key = new(System.Text.Encoding.UTF8.GetBytes(appSettingsToken));

            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


    }
}

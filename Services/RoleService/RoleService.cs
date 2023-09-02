//namespace CyberSecurityNextApi.Services.RoleService
//{
//    public class RoleService : IRoleRepository
//    {
//        private readonly DataContext _context;

//        public RoleService(DataContext context)
//        {
//            _context = context;
//        }
//        public async Task<Role> CreateRoleAsync(Role role)
//        {
//            _context.Roles.Add(role);
//            await _context.SaveChangesAsync();
//            return role;
//        }

//        public async Task<bool> DeleteRoleAsync(int id)
//        {
//            var roleToDelete = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
//            if (roleToDelete == null)
//                return false;

//            _context.Roles.Remove(roleToDelete);
//            await _context.SaveChangesAsync();
//            return true;
//        }

//        public async Task<IEnumerable<Role>> GetAllRolesAsync()
//        {
//            return await _context.Roles.ToListAsync();
//        }

//        public async Task<Role> GetRoleByIdAsync(int id)
//        {
//            return await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
//        }

//        public async Task<Role> UpdateRoleAsync(int id, Role role)
//        {
//            var existingRole = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
//            if (existingRole == null)
//                return null;

//            existingRole.Name = role.Name; // Update other properties as needed

//            await _context.SaveChangesAsync();
//            return existingRole;
//        }
//    }
//}

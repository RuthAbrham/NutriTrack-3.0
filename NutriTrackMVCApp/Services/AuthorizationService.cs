using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace NutriTrackMVCApp.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthorizationService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        // Check if a user is an admin
        public async Task<bool> IsAdmin(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user != null && await _userManager.IsInRoleAsync(user, "Admin");
        }

        // Check if a user has the specified role
        public async Task<bool> IsAuthorized(string userId, string requiredRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user != null && await _userManager.IsInRoleAsync(user, requiredRole);
        }
    }
}

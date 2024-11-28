using System.Threading.Tasks;

namespace NutriTrackMVCApp.Services
{
    public interface IAuthorizationService
    {
        Task<bool> IsAdmin(string userId); // Checks if a user is an admin
        Task<bool> IsAuthorized(string userId, string requiredRole); // Checks for a specific role
    }
}

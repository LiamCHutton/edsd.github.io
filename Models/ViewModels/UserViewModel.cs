using Microsoft.AspNetCore.Identity;

namespace EDStationDatabase.Models.ViewModels
{
    public class UserViewModel
    {
        public IEnumerable<User> Users { get; set; } = null!;
        public IEnumerable<IdentityRole> Roles { get; set; } = null!;
    }
}

namespace SportsStore.Data
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    public static class IdentitySeed
    {
        private const string AdminName = "Admin";

        private const string AdminPassword = "Secret123$";

        public static async Task EnsurePopulated(UserManager<IdentityUser> userManager)
        {
            IdentityUser user = await userManager.FindByIdAsync(AdminName);

            if (user == null)
            {
                user = new IdentityUser(AdminName);
                await userManager.CreateAsync(user, AdminPassword);
            }
        }
    }
}
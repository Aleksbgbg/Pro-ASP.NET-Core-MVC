namespace SportsStore.Data
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public static class IdentitySeed
    {
        private const string AdminName = "Admin";

        private const string AdminPassword = "Secret123$";

        public static async Task EnsurePopulated(IApplicationBuilder app)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.CreateScope())
            {
                UserManager<IdentityUser> userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                IdentityUser user = await userManager.FindByIdAsync(AdminName);

                if (user == null)
                {
                    user = new IdentityUser(AdminName);
                    await userManager.CreateAsync(user, AdminPassword);
                }
            }
        }
    }
}
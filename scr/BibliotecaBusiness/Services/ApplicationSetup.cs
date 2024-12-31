using Microsoft.AspNetCore.Identity;


namespace BibliotecaBusiness.Services
{
    public static class ApplicationSetup
    {

        public static async Task Setup(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            await CreateDefaultRolesAsync(roleManager);
            await CreateDefaultUserAsync(userManager);
        }


        private static async Task CreateDefaultRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            IdentityRole? superAdminRole = await roleManager.FindByNameAsync("GestorAdmin");
            if (superAdminRole == null)
            {
                await roleManager.CreateAsync(new IdentityRole("GestorAdmin"));
            }

            IdentityRole? gestorRole = await roleManager.FindByNameAsync("Gestor");
            if (superAdminRole == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Gestor"));
            }

            IdentityRole? subordinadoRole = await roleManager.FindByNameAsync("Subordinado");
            if (subordinadoRole == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Subordinado"));
            }
        }

        private static async Task CreateDefaultUserAsync(UserManager<IdentityUser> userManager)
        {
            string userEmail = "oportunidades@smn.com.br";

            IdentityUser? user = await userManager.FindByNameAsync(userEmail);

            if (user == null) 
            {
                var identityUser = new IdentityUser()
                {
                    UserName = "GestorOperacional",
                    Email = userEmail,
                };

                IdentityResult result = await userManager.CreateAsync(identityUser, "teste123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(identityUser, "GestorAdmin");
                }
            }
        }
    }
}

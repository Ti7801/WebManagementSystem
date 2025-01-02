using Microsoft.AspNetCore.Identity;


namespace BibliotecaBusiness.Services
{
    public static class ApplicationSetup
    {

        public static async Task Setup(RoleManager<IdentityRole<Guid>> roleManager, UserManager<IdentityUser<Guid>> userManager)
        {
            await CreateDefaultRolesAsync(roleManager);
            await CreateDefaultUserAsync(userManager);
        }


        private static async Task CreateDefaultRolesAsync(RoleManager<IdentityRole<Guid>> roleManager)
        {
            IdentityRole<Guid>? superAdminRole = await roleManager.FindByNameAsync("GestorAdmin");
            if (superAdminRole == null)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>("GestorAdmin"));
            }

            IdentityRole<Guid>? gestorRole = await roleManager.FindByNameAsync("Gestor");
            if (superAdminRole == null)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>("Gestor"));
            }

            IdentityRole<Guid>? subordinadoRole = await roleManager.FindByNameAsync("Subordinado");
            if (subordinadoRole == null)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>("Subordinado"));
            }
        }

        private static async Task CreateDefaultUserAsync(UserManager<IdentityUser<Guid>> userManager)
        {
            string userEmail = "oportunidades@smn.com.br";

            IdentityUser<Guid>? user = await userManager.FindByNameAsync(userEmail);

            if (user == null) 
            {
                var identityUser = new IdentityUser<Guid>()
                {
                    Id = Guid.NewGuid(),
                    UserName = "GestorOperacional",
                    Email = userEmail,
                    EmailConfirmed = true
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

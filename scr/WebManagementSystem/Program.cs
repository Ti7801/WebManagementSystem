using BibliotecaBusiness.Abstractions;
using BibliotecaBusiness.Services;
using BibliotecaData.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configura��o do banco de dados

builder.Services.AddDbContext<AppDbContext>(
    (DbContextOptionsBuilder optionsBuilder) =>
    {
        string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);
    },
    ServiceLifetime.Scoped
);

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<UsuarioService>();

builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
builder.Services.AddScoped<TarefaService>();


// Adicionar o Identity com a inje��o de depend�ncia
builder.Services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>(options =>
{
    // Configura��es de senha
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;

    // Configura��es de bloqueio de conta
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // Configura��es de cookie de login
    options.SignIn.RequireConfirmedAccount = false; // Definir como true se voc� quiser confirma��o de conta por e-mail

    // Configura��es a identidade para permitir espa�os no UserName
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ "; // Adicionando o espa�o
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders(); // Para recupera��o de senha e confirma��o de e-mail


// Configura��o do middleware de cookies
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/usuario/login"; // Define a rota de login
    options.LogoutPath = "/usuario/logout"; // Define a rota de logout (opcional)
    options.AccessDeniedPath = "/usuario/accessdenied"; // Define a rota para acesso negado (opcional)

    // Outras configura��es de cookies
    options.Cookie.Name = "WebManagementCookie";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;
    options.Cookie.SameSite = SameSiteMode.None;
});


// Configura��o MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Ativar autentica��o
app.UseAuthorization(); // Ativar autoriza��o

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


using (var scope = app.Services.CreateScope())
{
    var rolesManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser<Guid>>>();
    await ApplicationSetup.Setup(rolesManager, userManager);
}

await app.RunAsync();


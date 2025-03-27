using ControleDePresenca.Models;
using ControleDePresenca.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ControleDePresenca.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("BancoPresenca")
    ?? throw new InvalidOperationException("Connection string 'BancoPresenca' not found.");

// Configurar Context e AuthDbContext para usar BancoPresenca
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(connectionString)); // Usa o mesmo banco


builder.Services.AddIdentity<ControleDePresencaUser, IdentityRole>(options =>
    options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AuthDbContext>()  
    .AddDefaultUI()
    .AddDefaultTokenProviders();

// Adiciona suporte a Controllers e Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
});

var app = builder.Build();

// Inicializando as roles e o usuário administrador
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<ControleDePresencaUser>>();
    await SeedData.Initialize(services, userManager);  // Chama a inicialização de roles e usuário de forma assíncrona
}


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
//Identity Precisa muito disso !!!1!!! não apagar
app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();

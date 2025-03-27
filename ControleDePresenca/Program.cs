using ControleDePresenca.Models;
using ControleDePresenca.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ControleDePresenca.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

// Pegando apenas UMA string de conexão
var connectionString = builder.Configuration.GetConnectionString("BancoPresenca")
    ?? throw new InvalidOperationException("Connection string 'BancoPresenca' not found.");

// Configurar **AuthDbContext** para usar BancoPresenca
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BancoPresenca")));

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BancoPresenca"))); // Usa o mesmo banco!


// Configurar **Context** também com BancoPresenca
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(connectionString));

// Configuração do Identity
builder.Services.AddDefaultIdentity<ControleDePresencaUser>(options =>
    options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AuthDbContext>();  // O Identity será armazenado no BancoPresenca!

// Adiciona suporte a Controllers e Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
});

var app = builder.Build();

// Configuração do pipeline de requisições
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); // Identity precisa disso!
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

// Popula o banco de dados (caso necessário)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<Context>();
    SeedData.EnsurePopulated(services);
}

app.Run();

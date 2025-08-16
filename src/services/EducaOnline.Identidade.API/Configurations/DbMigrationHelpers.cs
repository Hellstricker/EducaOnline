using EducaOnline.Core.Enums;
using EducaOnline.Identidade.API.Data;
using EducaOnline.WebAPI.Core.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EducaOnline.Identidade.API.Configurations
{
    public class DbMigrationHelpers
    {
        public static async Task EnsureSeedData(WebApplication app)
        {
            var services = app.Services.CreateScope().ServiceProvider;
            await EnsureSeedData(services);
        }

        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var passwordHash = scope.ServiceProvider.GetRequiredService<IPasswordHasher<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var ssoContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await DbHealthChecker.TestConnection(ssoContext);

            if (env.IsDevelopment() || env.IsEnvironment("Docker"))
                await ssoContext.Database.EnsureCreatedAsync();


            #region adm
            //var usuarioDb = userManager.FindByEmailAsync("admin@educaonline.com.br").Result;
            //if (usuarioDb == null)
            //{
            //    var identityUser = new IdentityUser("admin@educaonline.com.br");
            //    identityUser.Email = "admin@educaonline.com.br";

            //    var result = userManager.CreateAsync(identityUser).Result;

            //    if (result.Succeeded)
            //    {
            //        var admininstrador = new Administrador(Guid.Parse(identityUser.Id), "Educa online ADM", "admin@educaonline.com.br");
            //        appContext.Set<Administrador>().Add(admininstrador);
            //        appContext.SaveChanges();

            //        if (admininstrador != null)
            //        {
            //            CreateRoles(roleManager).Wait();

            //            userManager.AddToRoleAsync(identityUser, PerfilUsuarioEnum.ADM.ToString()).Wait();
            //        }


            //        var hash = passwordHash.HashPassword(identityUser, "Teste@123");
            //        identityUser.SecurityStamp = Guid.NewGuid().ToString();
            //        identityUser.PasswordHash = hash;
            //        userManager.UpdateAsync(identityUser).Wait();
            //    }
            //}
            #endregion
        }
    }
}

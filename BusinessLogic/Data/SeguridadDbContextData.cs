using Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Data
{
    public class SeguridadDbContextData
    {
        public static async Task SeedUsersAsync(UserManager<Usuario> userManager, 
            RoleManager<IdentityRole> roleManager)
        {

            if (!userManager.Users.Any())
            {
                var usuarios = new List<Usuario> { 
                    new Usuario{                    
                    Nombres = "Byron",
                    Apellidos = "Samayoa",
                    UserName = "PapiAurelio",
                    Email = "byrondereyes@gmail.com",
                    Direccion = new Direccion
                        {
                            direccionCalle = "Calle el calvario",
                            Ciudad = "Masaya",
                            Departamento = "Masaya"
                        } 
                    },
                    new Usuario
                    {
                    Nombres = "Rey",
                    Apellidos = "Eduardo",
                    UserName = "reduhq",
                    Email = "reduhq@gmail.com",
                    Direccion = new Direccion
                        {
                            direccionCalle = "Por la UNI",
                            Ciudad = "Managua",
                            Departamento = "Managua"
                        }

                    }
                };


                await userManager.CreateAsync(usuarios[0], "Aurelio444*");
                await userManager.CreateAsync(usuarios[1], "Reduhq4444*");


                if (!roleManager.Roles.Any())
                {
                    var role = new IdentityRole
                    {
                        Name = "Administrador"
                    };

                    await roleManager.CreateAsync(role);
                }

                await userManager.AddToRoleAsync(usuarios[0], "Administrador");
                await userManager.AddToRoleAsync(usuarios[1], "Administrador");
            }

        }
    }
}

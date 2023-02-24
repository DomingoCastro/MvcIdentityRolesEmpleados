using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MvcIdentityRolesEmpleados.Models;
using MvcIdentityRolesEmpleados.Repositories;
using System.Security.Claims;

namespace MvcIdentityRolesEmpleados.Controllers
{
    public class ManagerController : Controller
    {
        private RepositoryEmpleado repo;
        public ManagerController(RepositoryEmpleado repo)
        {
            this.repo = repo;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            Empleado empleado = this.repo.ExisteEmpleado(username, int.Parse(password));
            if (empleado != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme,
                    ClaimTypes.Name, ClaimTypes.Role);
                Claim claimApellido = new Claim(ClaimTypes.Name, empleado.Apellido);
                identity.AddClaim(claimApellido);
                Claim claimId = new Claim(ClaimTypes.NameIdentifier, empleado.IdEmpleado.ToString());
                identity.AddClaim(claimId);
                Claim claimoficio = new Claim(ClaimTypes.Role, empleado.Oficio);
                identity.AddClaim(claimoficio);
                Claim claimSalario = new Claim("Sueldo", empleado.Salario.ToString());
                identity.AddClaim(claimSalario);
                Claim claimDept = new Claim("Departamento", empleado.Departamento.ToString());
                identity.AddClaim(claimDept);
                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);

                return RedirectToAction("PerfilEmpleado", "Empleados");
            }
            else
            {
                ViewData["MENSAJE"] = "Credenciales incorrectas";
            }
            return View();
        }
        public IActionResult ErrorAcceso()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Empleados");
        }
    }
}

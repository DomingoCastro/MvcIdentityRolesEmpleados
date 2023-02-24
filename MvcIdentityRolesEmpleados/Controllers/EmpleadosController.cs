using Microsoft.AspNetCore.Mvc;
using MvcIdentityRolesEmpleados.Filters;
using MvcIdentityRolesEmpleados.Models;
using MvcIdentityRolesEmpleados.Repositories;

namespace MvcIdentityRolesEmpleados.Controllers
{
    public class EmpleadosController : Controller
    {
        private RepositoryEmpleado repo;
        public EmpleadosController(RepositoryEmpleado repo)
        {
            this.repo = repo;
        }


        public IActionResult Index()
        {
            List<Empleado> empleados = this.repo.GetEmpleados();
            return View(empleados);
        }

        public IActionResult Details(int id)
        {
            Empleado empleado = this.repo.FindEmpleado(id);
            return View(empleado);
        }
        [AuthorizeEmpleados]
        public IActionResult PerfilEmpleado()
        {
            return View();
        }
        [AuthorizeEmpleados]
        public IActionResult CompisCurro()
        {
            //DEBEMOS BUSCAR EL CLAIM CON EL DATO DEL DEPARTAMENTO
            string dato = HttpContext.User.FindFirst("Departamento").Value;
            int iddepartamento = int.Parse(dato);
            List<Empleado> empleados = this.repo.GetEmpleadosDepartamento(iddepartamento);
            return View(empleados);
        }
    }
}

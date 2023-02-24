using Microsoft.EntityFrameworkCore;
using MvcIdentityRolesEmpleados.Models;

namespace MvcIdentityRolesEmpleados.Data
{
    public class EmpleadosContext: DbContext
    {
        public EmpleadosContext(DbContextOptions<EmpleadosContext> options) : base(options) { }
        public DbSet<Empleado> Empleados { get; set; }
    }
}

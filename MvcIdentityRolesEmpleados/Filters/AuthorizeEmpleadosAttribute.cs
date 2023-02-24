using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MvcIdentityRolesEmpleados.Filters
{
    public class AuthorizeEmpleadosAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user.Identity.IsAuthenticated == false )
            {
                context.Result = this.GetRoute("Manager", "Login");
            }
            else
            {
                //CONTROLLER QUE A REALIZADO LA LLAMADA
                //string controller = context.RouteData.Values["controller"].ToString();
                //string action = context.RouteData.Values["action"].ToString();
                ////HACEMOS UN IF Y SI ES EMPLEADO Y COMPIS CURRO...
                //if(user.IsInRole("DIRECTOR") == false && controller == "Empleados" && action == "CompisCurro")
                //{
                //    context.Result = this.GetRoute("Manager", "ErrorAcceso");
                //}
                //VALIDAMOS EL ROLE CON SU OFICIO
                if(user.IsInRole("DIRECTOR") == false && user.IsInRole("PRESIDENTE") == false)
                {
                    context.Result = this.GetRoute("Manager", "ErrorAcceso");
                }
            }
        }
        //CREAMOS UN METODO DE AYUDA POR SI REDIRECCIONAMOS A MAS SITIOS ADEMAS DE LOGIN
        private RedirectToRouteResult GetRoute(string controlador, string vista)
        {
            RouteValueDictionary ruta = new RouteValueDictionary(new
            {
                controller = controlador,
                action = vista
            });
            return new RedirectToRouteResult(ruta);
        }
    }
}

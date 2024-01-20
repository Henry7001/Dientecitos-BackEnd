using Dientecitos_BackEnd.Datos;
using Dientecitos_BackEnd.Entidades;
using Dientecitos_BackEnd.Middleware.Exceptions.BadRequest;
using Dientecitos_BackEnd.Middleware.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Dientecitos_BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : Controller
    {

        [HttpPost("/Dientecitos/Usuario")]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(Usuario), 200)]
        public ActionResult<Usuario> Insertar(
            [FromBody] NuevoUsuario registro,
            [FromQuery, Required] RolesEnum Rol
        )
        {
            
            MapeoDatosUsuario DatosUsuario = new();

            Validator.ValidateObject(registro, new ValidationContext(registro), true);

            bool validar = registro.ValidarCedula();

            if (!validar) { throw new InvalidFieldException("Ingrese una cédula válida de Ecuador.");  }

            Usuario response = DatosUsuario.GrabarUsuario(registro, Rol);

            return Ok(response);
            
        }
    }
}

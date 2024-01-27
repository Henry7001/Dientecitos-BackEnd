using Dientecitos_BackEnd.Datos;
using Dientecitos_BackEnd.Entidades;
using Dientecitos_BackEnd.Middleware.Exceptions.BadRequest;
using Dientecitos_BackEnd.Middleware.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Dientecitos_BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : Controller
    {
        readonly MapeoDatosUsuario DatosUsuario = new();

        [HttpPost("/Dientecitos/Usuario")]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(Usuario), 200)]
        public ActionResult<Usuario> Insertar(
            [FromBody] NuevoUsuario registro,
            [FromQuery, Required] RolesEnum Rol
        )
        {
            Validator.ValidateObject(registro, new ValidationContext(registro), true);
            bool validar = registro.ValidarCedula();
            if (!validar) { throw new InvalidFieldException("Ingrese una cédula válida de Ecuador.");  }
            Usuario response = DatosUsuario.GrabarUsuario(registro, Rol);
            return Ok(response);
            
        }

        [HttpPut("/Dientecitos/Usuario")]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(Usuario), 200)]
        public ActionResult<Usuario> Actualizar(
            [FromBody] Usuario registro
        )
        {
            Validator.ValidateObject(registro, new ValidationContext(registro), true);
            return Ok(DatosUsuario.ActualizarUsuario(registro));
        }

        [HttpDelete("/Dientecitos/Usuario/{id}")]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(MessageResponse), 200)]
        public ActionResult<MessageResponse> Eliminar(
            [FromRoute, Required] int id
        )
        {
            return Ok(DatosUsuario.EliminarUsuario(id));
        }

        [HttpGet("/Dientecitos/Usuario/{id}")]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(Usuario), 200)]
        public ActionResult<Usuario> ObtenerPorId(
            [FromRoute, Required] int id
        )
        {
            return Ok(DatosUsuario.GetUsuarioByID(id));
        }

        [HttpPost("/Dientecitos/Login")]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(Usuario), 200)]
        public ActionResult<Usuario> Login(
            [FromBody, Required] Login login
        )
        {
            Validator.ValidateObject(login, new ValidationContext(login), true);
            bool validar = login.ValidarCedula();
            if (!validar) { throw new InvalidFieldException("Ingrese una cédula válida de Ecuador."); }
            return Ok(DatosUsuario.Login(login));
        }
    }
}

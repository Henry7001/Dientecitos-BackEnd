using Dientecitos_BackEnd.Datos;
using Dientecitos_BackEnd.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;


namespace Dientecitos_BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpPost("/Dientecitos/Usuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Usuario> Insertar(
            [BindRequired][FromBody] NuevoUsuario registro,
            [BindRequired][FromQuery] RolesEnum Rol
        )
        {
            MapeoDatosUsuario DatosUsuario = new();

            Usuario response= DatosUsuario.GrabarUsuario(registro, Rol);

            return response;
        }
    }
}

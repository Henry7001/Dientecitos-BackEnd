using Dientecitos_BackEnd.Datos;
using Dientecitos_BackEnd.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


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
            try
            {
                MapeoDatosUsuario DatosUsuario = new();

                bool validar = registro.Validar();

                if (!validar) throw new Exception("Verifique la información a ingresar");

                Usuario response = DatosUsuario.GrabarUsuario(registro, Rol);

                return response;
            }
            catch (Exception e)
            {
                Error Error = new()
                {
                    error= e.Message,
                };
                return BadRequest(JsonConvert.SerializeObject(Error));
            }
        }
    }
}

using Dientecitos_BackEnd.Datos;
using Dientecitos_BackEnd.Entidades;
using Dientecitos_BackEnd.Middleware.Exceptions.BadRequest;
using Dientecitos_BackEnd.Middleware.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Dientecitos_BackEnd.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TipoTratamientoController : ControllerBase
    {

        MapeoDatosTipoTratamiento datosTipoTratamiento;

        public TipoTratamientoController()
        {
            datosTipoTratamiento = new();
        }


        [HttpPost("/Dientecitos/TipoTratamiento")]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(TipoTratamiento), 200)] 
        public ActionResult<TipoTratamiento> Insertar(
            [FromBody] TipoTratamientoDAO tipoTratamientoDAO
        )
        {
            try
            {

                Validator.ValidateObject(tipoTratamientoDAO, new ValidationContext(tipoTratamientoDAO), true);

                tipoTratamientoDAO.ValidarIngreso();

                TipoTratamiento response = datosTipoTratamiento.GrabarTipoTratamiento(tipoTratamientoDAO, 0);

                return Ok(response);

            }
            catch (Exception)
            {
                throw;                
            }            

        }



        [HttpPut("/Dientecitos/TipoTratamiento/{Id}")]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(TipoTratamiento), 200)]
        public ActionResult<TipoTratamiento> Actualizar(
            [FromBody] TipoTratamientoDAO tipoTratamientoDAO,
            [FromRoute, Required] int Id
        )
        {
            try
            {

                if (Id <= 0) { throw new InvalidIdException(); }

                Validator.ValidateObject(tipoTratamientoDAO, new ValidationContext(tipoTratamientoDAO), true);

                tipoTratamientoDAO.ValidarActualizacion();

                TipoTratamiento response = datosTipoTratamiento.GrabarTipoTratamiento(tipoTratamientoDAO, Id);

                return Ok(response);

            }
            catch (Exception)
            {
                throw;
            }

        }



        
        [HttpGet("/Dientecitos/TipoTratamiento/{Id}")]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(TipoTratamiento), 200)]
        public ActionResult<TipoTratamiento> ConsultarPorId(
            [FromRoute, Required] int Id
        )
        {
            try
            {

                if (Id <= 0) { throw new InvalidIdException(); }

                List<TipoTratamiento> response = datosTipoTratamiento.ConsultarTipoTratamiento(Id);

                return Ok(response[0]);

            }
            catch (Exception)
            {
                throw;
            }

        }




        [HttpGet("/Dientecitos/TipoTratamiento")]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(List<TipoTratamiento>), 200)]
        public ActionResult<List<TipoTratamiento>> ConsultarTodos()
        {
            try
            {

                List<TipoTratamiento> response = datosTipoTratamiento.ConsultarTipoTratamiento(0);

                return Ok(response);

            }
            catch (Exception)
            {
                throw;
            }

        }




        [HttpDelete("/Dientecitos/TipoTratamiento/{Id}")]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(MessageResponse), 200)]
        public ActionResult<MessageResponse> Eliminar(
            [FromRoute, Required] int Id
        )
        {
            try
            {

                if (Id <= 0) { throw new InvalidIdException(); }

                MessageResponse response = datosTipoTratamiento.EliminarTipoTratamiento(Id);

                return Ok(response);

            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}

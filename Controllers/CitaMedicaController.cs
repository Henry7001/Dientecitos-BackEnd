using Dientecitos_BackEnd.Datos;
using Dientecitos_BackEnd.Entidades;
using Dientecitos_BackEnd.Middleware.Exceptions.BadRequest;
using Dientecitos_BackEnd.Middleware.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Dientecitos_BackEnd.Controllers
{
    public class CitaMedicaController : ControllerBase
    {

        MapeoDatosCitaMedica datosCitaMedica;

        public CitaMedicaController()
        {
            datosCitaMedica = new();
        }


        [HttpPost("/Dientecitos/CitaMedica")]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(CitaMedica), 200)]
        public ActionResult<CitaMedica> Insertar(
            [FromBody] CitaMedicaDAO CitaMedicaDAO
        )
        {
            try
            {

                Validator.ValidateObject(CitaMedicaDAO, new ValidationContext(CitaMedicaDAO), true);

                CitaMedicaDAO.ValidarIngreso();

                CitaMedica response = datosCitaMedica.GrabarCitaMedica(CitaMedicaDAO, 0);

                return Ok(response);

            }
            catch (Exception)
            {
                throw;
            }

        }



        [HttpPut("/Dientecitos/CitaMedica/{Id}")]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(CitaMedica), 200)]
        public ActionResult<CitaMedica> Actualizar(
            [FromBody] CitaMedicaDAO CitaMedicaDAO,
            [FromRoute, Required] int Id
        )
        {
            try
            {

                if (Id <= 0) { throw new InvalidIdException(); }

                Validator.ValidateObject(CitaMedicaDAO, new ValidationContext(CitaMedicaDAO), true);

                CitaMedicaDAO.ValidarActualizacion();

                CitaMedica response = datosCitaMedica.GrabarCitaMedica(CitaMedicaDAO, Id);

                return Ok(response);

            }
            catch (Exception)
            {
                throw;
            }

        }




        [HttpGet("/Dientecitos/CitaMedica/{Id}")]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(CitaMedica), 200)]
        public ActionResult<CitaMedica> ConsultarPorId(
            [FromRoute, Required] int Id
        )
        {
            try
            {

                if (Id <= 0) { throw new InvalidIdException(); }

                List<CitaMedica> response = datosCitaMedica.ConsultarCitaMedica(Id, null, null, null, null, null);

                return Ok(response[0]);

            }
            catch (Exception)
            {
                throw;
            }

        }



        [HttpGet("/Dientecitos/CitaMedica")]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(List<CitaMedica>), 200)]
        public ActionResult<List<CitaMedica>> ConsultarTodos(
            [FromQuery] int? TipoTratamientoID,
            [FromQuery] int? MedicoID,
            [FromQuery] int? PacienteID,
            [FromQuery] DateTimeOffset? Fecha,
            [FromQuery] string? Estado
        )
        {
            try
            {

                if (TipoTratamientoID != null && TipoTratamientoID <= 0) 
                { 
                    throw new InvalidFieldException("El ID del tipo tratamiento debe ser mayor a 0."); 
                }
                
                if (MedicoID != null && MedicoID <= 0) 
                { 
                    throw new InvalidFieldException("El ID del medico debe ser mayor a 0."); 
                }
                
                if (PacienteID != null && PacienteID <= 0) 
                { 
                    throw new InvalidFieldException("El ID del paciente debe ser mayor a 0."); 
                }

                List<CitaMedica> response = datosCitaMedica.ConsultarCitaMedica(0, TipoTratamientoID, MedicoID, PacienteID, Fecha, Estado);

                return Ok(response);

            }
            catch (Exception)
            {
                throw;
            }

        }




        [HttpDelete("/Dientecitos/CitaMedica/{Id}")]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(MessageResponse), 200)]
        public ActionResult<MessageResponse> Cancelar(
            [FromRoute, Required] int Id
        )
        {
            try
            {

                if (Id <= 0) { throw new InvalidIdException(); }

                MessageResponse response = datosCitaMedica.CancelarCitaMedica(Id);

                return Ok(response);

            }
            catch (Exception)
            {
                throw;
            }

        }


    }
}

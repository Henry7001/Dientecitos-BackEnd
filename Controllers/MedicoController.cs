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
    public class MedicoController : ControllerBase
    {

        MapeoDatosMedico datosMedico;

        public MedicoController()
        {
            datosMedico = new();
        }


        [HttpPost("/Dientecitos/Medico")]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(Medico), 200)]
        public ActionResult<Medico> Insertar(
            [FromBody] MedicoDAO MedicoDAO
        )
        {
            try
            {

                Validator.ValidateObject(MedicoDAO, new ValidationContext(MedicoDAO), true);

                MedicoDAO.ValidarIngreso();

                Medico response = datosMedico.GrabarMedico(MedicoDAO, 0);

                return Ok(response);

            }
            catch (Exception)
            {
                throw;
            }

        }



        [HttpPut("/Dientecitos/Medico/{Id}")]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(Medico), 200)]
        public ActionResult<Medico> Actualizar(
            [FromBody] MedicoDAO MedicoDAO,
            [FromRoute, Required] int Id
        )
        {
            try
            {

                if (Id <= 0) { throw new InvalidIdException(); }

                Validator.ValidateObject(MedicoDAO, new ValidationContext(MedicoDAO), true);

                MedicoDAO.ValidarActualizacion();

                Medico response = datosMedico.GrabarMedico(MedicoDAO, Id);

                return Ok(response);

            }
            catch (Exception)
            {
                throw;
            }

        }




        [HttpGet("/Dientecitos/Medico/{Id}")]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(Medico), 200)]
        public ActionResult<Medico> ConsultarPorId(
            [FromRoute, Required] int Id
        )
        {
            try
            {

                if (Id <= 0) { throw new InvalidIdException(); }

                List<Medico> response = datosMedico.ConsultarMedico(Id, "");

                return Ok(response[0]);

            }
            catch (Exception)
            {
                throw;
            }

        }



        [HttpGet("/Dientecitos/Medico/Filtro")]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(Medico), 200)]
        public ActionResult<Medico> ConsultarPorCedula(
            [FromQuery, Required] string Cedula
        )
        {
            try
            {

                if (string.IsNullOrWhiteSpace(Cedula)) { throw new InvalidFieldException("Cedula es requerida."); }

                List<Medico> response = datosMedico.ConsultarMedico(-1, Cedula);

                return Ok(response[0]);

            }
            catch (Exception)
            {
                throw;
            }

        }




        [HttpGet("/Dientecitos/Medico")]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(List<Medico>), 200)]
        public ActionResult<List<Medico>> ConsultarTodos()
        {
            try
            {

                List<Medico> response = datosMedico.ConsultarMedico(0, "");

                return Ok(response);

            }
            catch (Exception)
            {
                throw;
            }

        }




        [HttpDelete("/Dientecitos/Medico/{Id}")]
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

                MessageResponse response = datosMedico.EliminarMedico(Id);

                return Ok(response);

            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}

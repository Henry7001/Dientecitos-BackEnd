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
    public class PacientesController : ControllerBase
    {

        MapeoDatosPaciente datosPaciente;

        public PacientesController()
        {
            datosPaciente = new();
        }


        [HttpPost("/Dientecitos/Paciente")]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(Paciente), 200)]
        public ActionResult<Paciente> Insertar(
            [FromBody] PacienteDAO PacienteDAO
        )
        {
            try
            {

                Validator.ValidateObject(PacienteDAO, new ValidationContext(PacienteDAO), true);

                PacienteDAO.ValidarIngreso();

                Paciente response = datosPaciente.GrabarPaciente(PacienteDAO, 0);

                return Ok(response);

            }
            catch (Exception)
            {
                throw;
            }

        }



        [HttpPut("/Dientecitos/Paciente/{Id}")]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(Paciente), 200)]
        public ActionResult<Paciente> Actualizar(
            [FromBody] PacienteDAO PacienteDAO,
            [FromRoute, Required] int Id
        )
        {
            try
            {

                if (Id <= 0) { throw new InvalidIdException(); }

                Validator.ValidateObject(PacienteDAO, new ValidationContext(PacienteDAO), true);

                PacienteDAO.ValidarActualizacion();

                Paciente response = datosPaciente.GrabarPaciente(PacienteDAO, Id);

                return Ok(response);

            }
            catch (Exception)
            {
                throw;
            }

        }




        [HttpGet("/Dientecitos/Paciente/{Id}")]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(Paciente), 200)]
        public ActionResult<Paciente> ConsultarPorId(
            [FromRoute, Required] int Id
        )
        {
            try
            {

                if (Id <= 0) { throw new InvalidIdException(); }

                List<Paciente> response = datosPaciente.ConsultarPaciente(Id, "");

                return Ok(response[0]);

            }
            catch (Exception)
            {
                throw;
            }

        }



        [HttpGet("/Dientecitos/Paciente/Filtro")]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(Paciente), 200)]
        public ActionResult<Paciente> ConsultarPorCedula(
            [FromQuery, Required] string Cedula
        )
        {
            try
            {

                if (string.IsNullOrWhiteSpace(Cedula)) { throw new InvalidFieldException("Cedula es requerida."); }

                List<Paciente> response = datosPaciente.ConsultarPaciente(-1, Cedula);

                return Ok(response[0]);

            }
            catch (Exception)
            {
                throw;
            }

        }




        [HttpGet("/Dientecitos/Paciente")]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(List<Paciente>), 200)]
        public ActionResult<List<Paciente>> ConsultarTodos()
        {
            try
            {

                List<Paciente> response = datosPaciente.ConsultarPaciente(0, "");

                return Ok(response);

            }
            catch (Exception)
            {
                throw;
            }

        }




        [HttpDelete("/Dientecitos/Paciente/{Id}")]
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

                MessageResponse response = datosPaciente.EliminarPaciente(Id);

                return Ok(response);

            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}

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
    public class HorarioLaboralController : ControllerBase
    {

        MapeoDatosHorarioLaboral datosHorarioLaboral;

        public HorarioLaboralController()
        {
            datosHorarioLaboral = new();
        }


        [HttpPost("/Dientecitos/HorarioLaboral")]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(HorarioLaboral), 200)]
        public ActionResult<HorarioLaboral> Insertar(
            [FromBody] HorarioLaboralDAO HorarioLaboralDAO
        )
        {
            try
            {

                Validator.ValidateObject(HorarioLaboralDAO, new ValidationContext(HorarioLaboralDAO), true);

                HorarioLaboralDAO.ValidarIngreso();

                HorarioLaboral response = datosHorarioLaboral.GrabarHorarioLaboral(HorarioLaboralDAO, 0);

                return Ok(response);

            }
            catch (Exception)
            {
                throw;
            }

        }



        [HttpPut("/Dientecitos/HorarioLaboral/{Id}")]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(HorarioLaboral), 200)]
        public ActionResult<HorarioLaboral> Actualizar(
            [FromBody] HorarioLaboralDAO HorarioLaboralDAO,
            [FromRoute, Required] int Id
        )
        {
            try
            {

                if (Id <= 0) { throw new InvalidIdException(); }

                Validator.ValidateObject(HorarioLaboralDAO, new ValidationContext(HorarioLaboralDAO), true);

                HorarioLaboralDAO.ValidarActualizacion();

                HorarioLaboral response = datosHorarioLaboral.GrabarHorarioLaboral(HorarioLaboralDAO, Id);

                return Ok(response);

            }
            catch (Exception)
            {
                throw;
            }

        }




        [HttpGet("/Dientecitos/HorarioLaboral/{Id}")]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(HorarioLaboral), 200)]
        public ActionResult<HorarioLaboral> ConsultarPorId(
            [FromRoute, Required] int Id
        )
        {
            try
            {

                if (Id <= 0) { throw new InvalidIdException(); }

                List<HorarioLaboral> response = datosHorarioLaboral.ConsultarHorarioLaboral(Id, false);

                return Ok(response[0]);

            }
            catch (Exception)
            {
                throw;
            }

        }



        [HttpGet("/Dientecitos/HorarioLaboral/Filtro")]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(List<HorarioLaboral>), 200)]
        public ActionResult<List<HorarioLaboral>> ConsultarPorMedico(
            [FromQuery, Required] int MedicoID
        )
        {
            try
            {

                if (MedicoID <= 0) { throw new InvalidFieldException("El ID del medico es requerido"); }

                List<HorarioLaboral> response = datosHorarioLaboral.ConsultarHorarioLaboral(MedicoID, true);

                return Ok(response);

            }
            catch (Exception)
            {
                throw;
            }

        }




        [HttpGet("/Dientecitos/HorarioLaboral")]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(List<HorarioLaboral>), 200)]
        public ActionResult<List<HorarioLaboral>> ConsultarTodos()
        {
            try
            {

                List<HorarioLaboral> response = datosHorarioLaboral.ConsultarHorarioLaboral(0, false);

                return Ok(response);

            }
            catch (Exception)
            {
                throw;
            }

        }




        [HttpDelete("/Dientecitos/HorarioLaboral/{Id}")]
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

                MessageResponse response = datosHorarioLaboral.EliminarHorarioLaboral(Id);

                return Ok(response);

            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}

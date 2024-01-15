using Dientecitos_BackEnd.Middleware.Exceptions.BadRequest;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Dientecitos_BackEnd.Entidades
{

    public class CitaMedica : IDisposable
    {

        [JsonProperty("citaMedicaID")]
        public int CitaMedicaID { get; set; }


        [JsonProperty("tipoTratamiento")]
        public virtual TipoTratamiento? TipoTratamiento { get; set; }


        [JsonProperty("paciente")]
        public virtual Paciente? Paciente { get; set; }


        [JsonProperty("medico")]
        public virtual Medico? Medico { get; set; }


        [JsonProperty("fechaHoraCita")]
        public DateTime FechaHoraCita { get; set; }


        [JsonProperty("observaciones")]
        public string? Observaciones { get; set; }


        [JsonProperty("diagnostico")]
        public string? Diagnostico { get; set; }


        [JsonProperty("estado")]
        public string? Estado { get; set; }



        private bool disposed = false;




        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }



        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    CitaMedicaID = 0;
                    TipoTratamiento = null;
                    Paciente = null;
                    Medico = null;
                    FechaHoraCita = DateTime.MinValue;
                    Observaciones = null;
                    Diagnostico = null;
                    Estado = null;
                }
                disposed = true;
            }
        }



        ~CitaMedica()
        {
            Dispose(false);
        }

    }



    public class CitaMedicaDAO : IDisposable
    {


        [JsonProperty("tipoTratamientoID")]
        public int? TipoTratamientoID { get; set; }


        [JsonProperty("pacienteID")]
        public int? PacienteID { get; set; }


        [JsonProperty("medicoID")]
        public int? MedicoID { get; set; }


        [JsonProperty("fechaHoraCita")]
        public DateTime? FechaHoraCita { get; set; }


        [JsonProperty("observaciones")]
        [StringLength(200, ErrorMessage = "El campo observaciones debe contener máximo 200 caracteres.")]
        public string? Observaciones { get; set; }


        [JsonProperty("diagnostico")]
        [StringLength(200, ErrorMessage = "El diagnostico debe contener máximo 200 caracteres.")]
        public string? Diagnostico { get; set; }


        [JsonProperty("estado")]
        public string? Estado { get; set; }



        private bool disposed = false;



        public void ValidarIngreso()
        {

            if (MedicoID == null || MedicoID <= 0)
            {
                throw new InvalidFieldException("El ID del medico asociado es obligatorio.");
            }


            if (PacienteID == null || PacienteID <= 0)
            {
                throw new InvalidFieldException("El ID del paciente asociado es obligatorio.");
            }


            if (TipoTratamientoID == null || TipoTratamientoID <= 0)
            {
                throw new InvalidFieldException("El ID del tipo de tratamiento asociado es obligatorio.");
            }


            if (FechaHoraCita == null || FechaHoraCita <= DateTime.Today)
            {
                throw new InvalidFieldException("La fecha y hora es obligatoria y debe ser mayor a la fecha actual.");
            }

        }



        public void ValidarActualizacion()
        {


            if (MedicoID != null && MedicoID <= 0)
            {
                throw new InvalidFieldException("El ID del medico asociado debe ser mayor a 0.");
            }


            if (PacienteID != null && PacienteID <= 0)
            {
                throw new InvalidFieldException("El ID del paciente asociado debe ser mayor a 0.");
            }


            if (TipoTratamientoID != null && TipoTratamientoID <= 0)
            {
                throw new InvalidFieldException("El ID del tipo de tratamiento asociado debe ser mayor a 0.");
            }


            if (FechaHoraCita != null && FechaHoraCita <= DateTime.Today)
            {
                throw new InvalidFieldException("La fecha y hora es debe ser mayor a la fecha actual.");
            }


            if (Observaciones != null && Observaciones.Trim().Length < 5)
            {
                throw new InvalidFieldException("Las observaciones deben contener al menos 5 caracteres.");
            }


            if (Diagnostico != null && Diagnostico.Trim().Length < 10)
            {
                throw new InvalidFieldException("El diagnostico debe contener al menos 10 caracteres.");
            }


            if (Diagnostico != null && Diagnostico.Trim().Length < 10)
            {
                throw new InvalidFieldException("El diagnostico debe contener al menos 10 caracteres.");
            }


            ValidarEstadoCita(Estado ?? "");

        }


        public static void ValidarEstadoCita(string Estado)
        {
            if (!string.IsNullOrWhiteSpace(Estado) &&
                (!Estado.Equals("Pendiente", StringComparison.OrdinalIgnoreCase) &&
                !Estado.Equals("Finalizada", StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidFieldException("El estado debe ser 'Pendiente' o 'Finalizada'.");
            }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }



        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    TipoTratamientoID = null;
                    PacienteID = null;
                    MedicoID = null;
                    FechaHoraCita = DateTime.MinValue;
                    Observaciones = null;
                    Diagnostico = null;
                    Estado = null;
                }
                disposed = true;
            }
        }



        ~CitaMedicaDAO()
        {
            Dispose(false);
        }

    }


}

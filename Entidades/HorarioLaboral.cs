using Dientecitos_BackEnd.Middleware.Exceptions.BadRequest;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Dientecitos_BackEnd.Entidades
{
    public class HorarioLaboral : IDisposable
    {

        [JsonProperty("horarioID")]
        public int HorarioID { get; set; }


        [JsonProperty("diaSemana")]
        public string? DiaSemana { get; set; }


        [JsonProperty("horaInicio")]
        public TimeSpan? HoraInicio { get; set; }


        [JsonProperty("horaFin")]
        public TimeSpan? HoraFin { get; set; }


        [JsonProperty("medico")]
        public virtual Medico? Medico { get; set; }


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
                    HorarioID = 0;
                    Medico = null;
                    DiaSemana = null;
                    Estado = null;
                }
                disposed = true;
            }
        }



        ~HorarioLaboral()
        {
            Dispose(false);
        }


    }



    public class HorarioLaboralDAO : IDisposable
    {

        [JsonProperty("medicoID")]
        public int? MedicoID { get; set; }


        [JsonProperty("diaSemana")]
        [StringLength(10, ErrorMessage = "El dia de la semana debe contener máximo 10 caracteres.")]
        public string? DiaSemana { get; set; }


        [JsonProperty("horaInicio")]
        public TimeSpan? HoraInicio { get; set; }


        [JsonProperty("horaFin")]
        public TimeSpan? HoraFin { get; set; }


        [JsonProperty("estado")]
        public string? Estado { get; set; }


        private bool disposed = false;



        public void ValidarIngreso()
        {

            if (MedicoID == null || MedicoID <= 0)
            {
                throw new InvalidFieldException("El ID del medico asociado es obligatorio.");
            }
            
            if (string.IsNullOrWhiteSpace(DiaSemana))
            {
                throw new InvalidFieldException("El dia de la semana es obligatorio.");
            }

            ValidarDiaSemana();

        }



        public void ValidarActualizacion()
        {

            if (MedicoID != null && MedicoID <= 0)
            {
                throw new InvalidFieldException("El ID del medico asociado debe ser mayor a 0.");
            }

            ValidarDiaSemana();

            Utils.Utils.ValidarEstado(Estado ?? "");

        }


        private void ValidarDiaSemana()
        {

            HashSet<string> ValidDiasSemana = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "lunes", "martes", "miércoles", "jueves", "viernes", "sábado", "domingo"
            };


            if (DiaSemana != null && !ValidDiasSemana.Contains(DiaSemana))
            {
                throw new InvalidFieldException("El día de la semana no es válido.");
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
                    MedicoID = 0;
                    DiaSemana = null;
                    Estado = null;
                }
                disposed = true;
            }
        }



        ~HorarioLaboralDAO()
        {
            Dispose(false);
        }


    }

}

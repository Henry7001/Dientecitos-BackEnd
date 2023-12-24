using Newtonsoft.Json;

namespace Dientecitos_BackEnd.Entidades
{
    public class CitaMedica : IDisposable
    {
        [JsonProperty("citaMedicaID")]
        public int CitaMedicaID { get; set; }
        [JsonProperty("tipoTratamientoID")]
        public int TipoTratamientoID { get; set; }
        [JsonProperty("pacienteID")]
        public int PacienteID { get; set; }
        [JsonProperty("medicoID")]
        public int MedicoID { get; set; }
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
                    TipoTratamientoID = 0;
                    PacienteID = 0;
                    MedicoID = 0;
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
}

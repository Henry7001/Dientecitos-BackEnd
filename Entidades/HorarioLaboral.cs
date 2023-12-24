using Newtonsoft.Json;

namespace Dientecitos_BackEnd.Entidades
{
    public class HorarioLaboral : IDisposable
    {
        [JsonProperty("horarioID")]
        public int HorarioID { get; set; }
        [JsonProperty("medicoID")]
        public int MedicoID { get; set; }
        [JsonProperty("diaSemana")]
        public string? DiaSemana { get; set; }
        [JsonProperty("horaInicio")]
        public TimeSpan HoraInicio { get; set; }
        [JsonProperty("horaFin")]
        public TimeSpan HoraFin { get; set; }

        [JsonIgnore]
        public virtual Medico Medico { get; set; }

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
                    MedicoID = 0;
                    DiaSemana = null;
                }
                disposed = true;
            }
        }

        ~HorarioLaboral()
        {
            Dispose(false);
        }
    }
}

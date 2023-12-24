using Newtonsoft.Json;

namespace Dientecitos_BackEnd.Entidades
{
    public class Paciente : IDisposable
    {
        [JsonProperty("pacienteID")]
        public int PacienteID { get; set; }
        [JsonProperty("usuarioID")]
        public int UsuarioID { get; set; }
        [JsonProperty("direccion")]
        public string? Direccion { get; set; }
        [JsonProperty("numeroContacto")]
        public string? NumeroContacto { get; set; }

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
                    PacienteID = 0;
                    UsuarioID = 0;
                    Direccion = null;
                    NumeroContacto = null;
                }
                disposed = true;
            }
        }

        ~Paciente()
        {
            Dispose(false);
        }
    }
}

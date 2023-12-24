using Newtonsoft.Json;

namespace Dientecitos_BackEnd.Entidades
{
    public class Medico : IDisposable
    {
        [JsonProperty("medicoID")]
        public int MedicoID { get; set; }
        [JsonProperty("usuarioID")]
        public int UsuarioID { get; set; }
        [JsonProperty("especialidad")]
        public string? Especialidad { get; set; }

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
                    MedicoID = 0;
                    UsuarioID = 0;
                    Especialidad = null;
                }
                disposed = true;
            }
        }

        ~Medico()
        {
            Dispose(false);
        }
    }
}

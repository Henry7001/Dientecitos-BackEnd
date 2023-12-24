using Newtonsoft.Json;

namespace Dientecitos_BackEnd.Entidades
{
    public class PersonalAdministrativo : IDisposable
    {
        [JsonProperty("usuarioID")]
        public int UsuarioID { get; set; }
        [JsonProperty("rolAdicional")]
        public string? RolAdicional { get; set; }

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
                    UsuarioID = 0;
                    RolAdicional = null;
                }
                disposed = true;
            }
        }

        ~PersonalAdministrativo()
        {
            Dispose(false);
        }
    }
}

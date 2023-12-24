using Newtonsoft.Json;

namespace Dientecitos_BackEnd.Entidades
{
    public class Usuario : IDisposable
    {
        [JsonProperty("usuarioID")]
        public int UsuarioID { get; set; }
        [JsonProperty("cedula")]
        public string? Cedula { get; set; }
        [JsonProperty("nombre")]
        public string? Nombre { get; set; }
        [JsonProperty("telefono")]
        public string? Telefono { get; set; }
        [JsonProperty("rol")]
        public string? Rol { get; set; }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    UsuarioID = 0;
                    Cedula = null;
                    Nombre = null;
                    Telefono = null;
                    Rol = null;
                }
                disposed = true;
            }
        }

        ~Usuario()
        {
            Dispose(false);
        }

    }

    public class NuevoUsuario : IDisposable
    {
        [JsonProperty("cedula")]
        public string? Cedula { get; set; }
        [JsonProperty("nombre")]
        public string? Nombre { get; set; }
        [JsonProperty("telefono")]
        public string? Telefono { get; set; }
        [JsonProperty("contraseña")]
        public string? Contraseña { get; set; }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Cedula = null;
                    Nombre = null;
                    Telefono = null;
                    Contraseña = null;
                }
                disposed = true;
            }
        }

        ~NuevoUsuario()
        {
            Dispose(false);
        }

    }
}
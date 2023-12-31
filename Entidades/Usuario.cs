using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

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

        public bool Validar()
        {
            return ValidarCedula() && 
                Nombre != null && Nombre?.Length == 50 &&
                Contraseña != null && Contraseña?.Length > 8 &&
                Telefono != null && Telefono?.Length == 10;
        }

        private Boolean ValidarCedula()
        {
            return Cedula != null &&
               Cedula.Length > 0 &&
               Cedula.Length == 10 &&
               int.Parse(Cedula[..2]) >= 1 &&
               int.Parse(Cedula[..2]) <= 24 &&
               int.Parse(Cedula.Substring(2, 1)) < 6 &&
               int.Parse(Cedula[..9]) % 11 == int.Parse(Cedula[9..]);
        }

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
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "La cédula no puede ser nula.")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "La cédula debe contener 10 dígitos numéricos.")]
        public string Cedula { get; set; }


        [JsonProperty("nombre")]
        [Required(ErrorMessage = "El nombre no puede ser nulo.")]
        [StringLength(50, ErrorMessage = "El nombre deben contener máximo 50 caracteres.")]
        public string Nombre { get; set; }


        [JsonProperty("telefono")]
        [Required(ErrorMessage = "El teléfono no puede ser nulo.")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "El teléfono debe contener 10 dígitos numéricos.")]
        public string Telefono { get; set; }


        [JsonProperty("contraseña")]
        [Required(ErrorMessage = "La contraseña no puede ser nula.")]
        [MinLength(8, ErrorMessage = "La contraseña debe contener mínimo 8 caracteres.")]
        [MaxLength(16, ErrorMessage = "La contraseña debe contener máximo 16 caracteres.")]
        public string Contraseña { get; set; }

        private bool disposed = false;

        public bool ValidarCedula()
        {
            int aux = 0, par = 0, impar = 0, verifi;
            for (int i = 0; i < 9; i += 2)
            {
                aux = 2 * int.Parse(Cedula[i].ToString());
                if (aux > 9)
                    aux -= 9;
                par += aux;
            }
            for (int i = 1; i < 9; i += 2)
            {
                impar += int.Parse(Cedula[i].ToString());
            }

            aux = par + impar;
            if (aux % 10 != 0)
            {
                verifi = 10 - (aux % 10);
            }
            else
                verifi = 0;
            if (verifi == int.Parse(Cedula[9].ToString()))
                return true;
            else
                return false;
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
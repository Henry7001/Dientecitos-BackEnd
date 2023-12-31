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

    public class NuevoUsuario //: IDisposable
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

        //private bool disposed = false;

        //public bool Validar()
        //{
        //    return ValidarCedula() && 
        //        Nombre != null && Nombre?.Length == 50 &&
        //        Contraseña != null && Contraseña?.Length > 8 &&
        //        Telefono != null && Telefono?.Length == 10;
        //}

        public bool ValidarCedula()
        {
            if (cedula.Length != 10 || !cedula.All(char.IsDigit))
            {
                return false;
            }

            int[] digitos = cedula.Take(9).Select(c => c - '0').ToArray();
            int digitoVerificador = int.Parse(cedula[9].ToString());

            int sumaPares = digitos.Where((d, i) => i % 2 == 1).Sum();
            int sumaImpares = digitos.Where((d, i) => i % 2 == 0).Sum();
            int total = sumaPares + sumaImpares * 3;
            int residuo = total % 10;
            int digitoCalculado = (residuo == 0) ? 0 : 10 - residuo;

            return digitoCalculado == digitoVerificador;
        }

        //public void Dispose()
        //{
        //    Dispose(true);
        //    System.GC.SuppressFinalize(this);
        //}

        //public void Dispose(bool disposing)
        //{
        //    if (!disposed)
        //    {
        //        if (disposing)
        //        {
        //            Cedula = null;
        //            Nombre = null;
        //            Telefono = null;
        //            Contraseña = null;
        //        }
        //        disposed = true;
        //    }
        //}

        //~NuevoUsuario()
        //{
        //    Dispose(false);
        //}

    }
}
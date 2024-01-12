using Dientecitos_BackEnd.Middleware.Exceptions.BadRequest;

namespace Dientecitos_BackEnd.Utils
{
    public class Utils
    {


        public static void ValidarEstado(string Estado)
        {
            if (!string.IsNullOrWhiteSpace(Estado) &&
                (!Estado.Equals("A", StringComparison.OrdinalIgnoreCase) &&
                !Estado.Equals("I", StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidFieldException("El Estado debe ser A ó I.");
            }
        }


    }
}

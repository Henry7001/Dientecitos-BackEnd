namespace Dientecitos_BackEnd.Middleware.Exceptions.BadRequest
{
    public class ExistingObjectException : BadRequestException
    {
        public ExistingObjectException(): base("El registro ya existe en la Base de Datos.")
        {
        }

        public ExistingObjectException(string mensaje) : base("El registro ya existe en la Base de Datos. " + mensaje)
        {
        }
    }
}

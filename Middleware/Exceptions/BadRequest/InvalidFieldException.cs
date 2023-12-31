namespace Dientecitos_BackEnd.Middleware.Exceptions.BadRequest
{
    public class InvalidFieldException : BadRequestException
    {
        public InvalidFieldException() : base("Campo no válido.")
        {
        }

        public InvalidFieldException(string mensaje) : base("Campo no válido. " + mensaje)
        {
        }
    }
}

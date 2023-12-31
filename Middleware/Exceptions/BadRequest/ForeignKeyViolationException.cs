namespace Dientecitos_BackEnd.Middleware.Exceptions.BadRequest
{
    public class ForeignKeyViolationException : BadRequestException
    {
        public ForeignKeyViolationException() : base("La clave foránea hace referencia a un registro inexistente en la base de datos.")
        {
        }

        public ForeignKeyViolationException(string mensaje) : base("La clave foránea hace referencia a un registro inexistente en la base de datos. " + mensaje)
        {
        }
    }
}

using System.Text.Json.Serialization;

namespace Dientecitos_BackEnd.Entidades
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RolesEnum
    {
        Paciente,
        Medico,
        Administrativo
    }
}
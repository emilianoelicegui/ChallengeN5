using ChallengeN5.Repositories.Models;

namespace ChallengeN5.Repositories.Dto
{
    public class PermissionDto
    {
        public int Id { get; set; }
        public string NombreEmpleado { get; set; } = null!;
        public string ApellidoEmpleado { get; set; } = null!;
        public int TipoPermiso { get; set; }
        public DateTime FechaPermiso { get; set; }
    }
}

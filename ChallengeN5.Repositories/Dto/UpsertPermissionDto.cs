using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeN5.Repositories.Dto
{
    public class UpsertPermissionDto
    {
        public string NombreEmpleado { get; set; } = null!;
        public string ApellidoEmpleado { get; set; } = null!;
        public int TipoPermiso { get; set; }
        public DateTime FechaPermiso { get; set; }
    }
}

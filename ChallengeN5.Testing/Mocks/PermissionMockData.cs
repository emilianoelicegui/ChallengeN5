using ChallengeN5.Repositories.Dto;
using ChallengeN5.Repositories.Models;

namespace ChallengeN5.Testing.Mocks
{
    public class PermissionMockData
    {
        public static UpsertPermissionDto NewRequest()
        {
            return new UpsertPermissionDto
            {
                NombreEmpleado = "Emiliano",
                ApellidoEmpleado = "Elicegui",
                FechaPermiso = DateTime.Today,
                TipoPermiso = 1
            };
        }

        public static async Task<IEnumerable<Permission>> GetEmptyAsync()
        {
            return new List<Permission>();
        }

        public static async Task<IEnumerable<Permission>> GetAllAsync()
        {
            return new List<Permission>{
                new Permission
                {
                    Id = 1,
                    NombreEmpleado = "Emi",
                    ApellidoEmpleado = "Elicegui",
                    FechaPermiso = DateTime.Today,
                    TipoPermiso = 1 
                },
                new Permission
                {
                    Id = 2,
                    NombreEmpleado = "El pocha",
                    ApellidoEmpleado = "Roberti",
                    FechaPermiso = DateTime.Today,
                    TipoPermiso = 1
                }
            };
        }

        public static async Task<IEnumerable<PermissionDto>> GetAllDtoAsync()
        {
            return new List<PermissionDto>{
                new PermissionDto
                {
                    Id = 1,
                    NombreEmpleado = "Emi",
                    ApellidoEmpleado = "Elicegui",
                    FechaPermiso = DateTime.Today,
                    TipoPermisoNavigation = new PermissionTypeDto
                    {
                        Id = 1,
                        Descripcion = "Administrador"
                    }
                },
                new PermissionDto
                {
                    Id = 2,
                    NombreEmpleado = "El pocha",
                    ApellidoEmpleado = "Roberti",
                    FechaPermiso = DateTime.Today,
                    TipoPermisoNavigation = new PermissionTypeDto
                    {
                        Id = 1,
                        Descripcion = "Empleado"
                    }
                }
            };
        }
    }
}

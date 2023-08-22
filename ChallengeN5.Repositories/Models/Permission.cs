using System;
using System.Collections.Generic;

namespace ChallengeN5.Repositories.Models;

public partial class Permission
{
    /// <summary>
    /// Unique ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Employee Forename
    /// </summary>
    public string NombreEmpleado { get; set; } = null!;

    /// <summary>
    /// Employee Surname
    /// </summary>
    public string ApellidoEmpleado { get; set; } = null!;

    /// <summary>
    /// Permission Type
    /// </summary>
    public int TipoPermiso { get; set; }

    /// <summary>
    /// Permission granted on Date
    /// </summary>
    public DateTime FechaPermiso { get; set; }

    public virtual PermissionType TipoPermisoNavigation { get; set; } = null!;
}

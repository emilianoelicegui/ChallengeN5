using System;
using System.Collections.Generic;

namespace ChallengeN5.Repositories.Models;

public partial class PermissionType
{
    /// <summary>
    /// Unique ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Permission description
    /// </summary>
    public string? Descripcion { get; set; }

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}

using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? Cedula { get; set; }

    public string? NombreCompleto { get; set; }

    public int? IdRol { get; set; }

    public string? Correo { get; set; }

    public string? Contrasenia { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual Rol? IdRolNavigation { get; set; }
}

namespace BackendApi.DTOs
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }

        public string? Cedula { get; set; }

        public string? NombreCompleto { get; set; }

        public int? IdRol { get; set; }
        public string? NombreRol { get; set; }

        public string? Correo { get; set; }

        public string? Contrasenia { get; set; }
        public string? FechaCreacion { get; set; }
    }
}

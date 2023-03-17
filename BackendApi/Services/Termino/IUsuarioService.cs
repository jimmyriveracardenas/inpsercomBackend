using BackendApi.Models;

namespace BackendApi.Services.Termino
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> GetList();
        Task<Usuario> Get(int idUsuario);
        Task<Usuario> Add(Usuario modelo);
        Task<bool> Update(Usuario modelo);
        Task<bool> Delete(Usuario modelo);

    }
}

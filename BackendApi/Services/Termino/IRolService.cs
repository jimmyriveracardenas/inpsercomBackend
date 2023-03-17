using BackendApi.Models;

namespace BackendApi.Services.Termino
{
    public interface IRolService
    {
        Task<List<Rol>> GetList();
    }
}

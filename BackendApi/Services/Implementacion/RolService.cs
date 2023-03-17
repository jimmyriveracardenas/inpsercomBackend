using Microsoft.EntityFrameworkCore;
using BackendApi.Models;
using BackendApi.Services.Termino;

namespace BackendApi.Services.Implementacion
{
    public class RolService : IRolService
    {
        private DbusuarioContext _dbContext;

        public RolService(DbusuarioContext dbContext)
        {
            _dbContext = dbContext; 
        }

        public async Task<List<Rol>> GetList()
        {
            try
            {
                List<Rol> lista = new List<Rol>();
                lista = await _dbContext.Rols.ToListAsync();

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

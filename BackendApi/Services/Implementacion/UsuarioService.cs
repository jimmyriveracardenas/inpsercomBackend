using Microsoft.EntityFrameworkCore;
using BackendApi.Models;
using BackendApi.Services.Termino;

namespace BackendApi.Services.Implementacion
{
    public class UsuarioService:IUsuarioService
    {
        private DbusuarioContext _dbContext;

        public UsuarioService(DbusuarioContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Usuario>> GetList()
        {
            try
            {
                List<Usuario> lista = new List<Usuario>();
                lista = await _dbContext.Usuarios.Include(rl => rl.IdRolNavigation).ToListAsync();

                return lista;   
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Usuario> Get(int idUsuario)
        {
            try
            {
                Usuario? encontrado = new Usuario();
                encontrado = await _dbContext.Usuarios.Include(rl => rl.IdRolNavigation)
                            .Where(e => e.IdUsuario == idUsuario).FirstOrDefaultAsync();

                return encontrado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Usuario> Add(Usuario modelo)
        {
            try
            {
                _dbContext.Usuarios.Add(modelo);
                await _dbContext.SaveChangesAsync();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(Usuario modelo)
        {
            try
            {
                _dbContext.Usuarios.Update(modelo);
                await _dbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(Usuario modelo)
        {
            try
            {
                _dbContext.Usuarios.Remove(modelo);
                await _dbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        

        
    }
}

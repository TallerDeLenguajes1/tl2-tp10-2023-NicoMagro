using tl2_tp10_2023_NicoMagro.Models;

namespace tl2_tp10_2023_NicoMagro.Repositories
{
    public interface IUsuarioRepository
    {
        public List<Usuario> GetAll();
        public void Create(Usuario user);
        public void Update(int id, Usuario user);
        public Usuario GetById(int id);
        public void Remove(int id);
    }
}
using TP9.Clases;

namespace TP9.Repositorios
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
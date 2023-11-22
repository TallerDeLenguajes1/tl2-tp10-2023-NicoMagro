using TP9.Clases;

namespace TP9.Repositorios
{
    public interface ITareaRepository
    {
        public void Create(int idTablero, Tarea task);
        public void Update(int id, Tarea task);
        public List<Tarea> GetAll();
        public Tarea GetById(int id);
        public List<Tarea> GetByUsuario(int idUsuario);
        public List<Tarea> GetByTablero(int idTablero);
        public void Remove(int id);
        public void AssignUserTask(int idUsuario, int idTarea);
    }
}
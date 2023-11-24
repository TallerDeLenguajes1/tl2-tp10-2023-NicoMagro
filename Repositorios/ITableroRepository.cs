using tl2_tp10_2023_NicoMagro.Models;

namespace tl2_tp10_2023_NicoMagro.Repositories
{
    public interface ITableroRepository
    {
        public void Create(Tablero tablero);
        public void Update(int id, Tablero tablero);
        public Tablero GetById(int id);
        public List<Tablero> GetAll();
        public void Remove(int id);
    }
}
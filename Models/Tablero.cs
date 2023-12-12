using tl2_tp10_2023_NicoMagro.ViewModels.Tableros;

namespace tl2_tp10_2023_NicoMagro.Models
{
    public class Tablero
    {
        public int Id { get; set; }
        public int IdUsuarioPropietario { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public Tablero()
        {

        }

        public Tablero(CrearTableroViewModel vm)
        {
            this.IdUsuarioPropietario = vm.IdUsuarioPropietario;
            this.Nombre = vm.Nombre;
            this.Descripcion = vm.Descripcion;
        }

        public Tablero(ModificarTableroViewModel vm)
        {
            this.IdUsuarioPropietario = vm.IdUsuarioPropietario;
            this.Nombre = vm.Nombre;
            this.Descripcion = vm.Descripcion;
        }
    }
}
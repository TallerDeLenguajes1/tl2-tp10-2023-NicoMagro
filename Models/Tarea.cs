using tl2_tp10_2023_NicoMagro.ViewModels.Tareas;

namespace tl2_tp10_2023_NicoMagro.Models
{
    public class Tarea
    {
        public int Id { get; set; }
        public int Id_tablero { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Color { get; set; }
        public EstadoTarea Estado { get; set; }
        public int IdUsuarioAsignado { get; set; }

        public Tarea() { }

        public Tarea(CrearTareaViewModel vm)
        {
            this.Id_tablero = vm.IdTablero;
            this.Nombre = vm.Nombre;
            this.Descripcion = vm.Descripcion;
            this.Color = vm.Color;
            this.Estado = vm.Estado;
            this.IdUsuarioAsignado = vm.IdUsuarioAsignado;
        }

        public Tarea(ModificarTareaViewModel vm)
        {
            this.Id_tablero = vm.IdTablero;
            this.Nombre = vm.Nombre;
            this.Descripcion = vm.Descripcion;
            this.Color = vm.Color;
            this.Estado = vm.Estado;
            this.IdUsuarioAsignado = vm.IdUsuarioAsignado;
        }
    }

    public enum EstadoTarea
    {
        Ideas = 1,
        ToDo = 2,
        Doing = 3,
        Review = 4,
        Done = 5
    }
}
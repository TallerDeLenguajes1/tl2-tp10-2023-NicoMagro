using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_NicoMagro.Models;


namespace tl2_tp10_2023_NicoMagro.ViewModels.Tareas
{
    public class CrearTareaViewModel
    {
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Id del tablero")]
        public int IdTablero { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(100, ErrorMessage = "El nombre de la tarea no puede contener más de 100 caracteres")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(200, ErrorMessage = "La descripción de la tarea no puede contener más de 200 caracteres")]
        public string Descripcion { get; set; }

        [Display(Name = "Color")]
        [StringLength(15, ErrorMessage = "El color de la tarea no puede contener más de 15 caracteres")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Estado")]
        public EstadoTarea Estado { get; set; }

        [Display(Name = "Id del usuario asignado")]
        public int IdUsuarioAsignado { get; set; }


        public CrearTareaViewModel()
        {

        }

        public CrearTareaViewModel(Tarea tarea)
        {
            this.IdTablero = tarea.Id_tablero;
            this.Nombre = tarea.Nombre;
            this.Descripcion = tarea.Descripcion;
            this.Color = tarea.Color;
            this.Estado = tarea.Estado;
            this.IdUsuarioAsignado = tarea.IdUsuarioAsignado;
        }
    }
}
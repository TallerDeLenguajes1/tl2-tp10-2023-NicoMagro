using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_NicoMagro.Models;

namespace tl2_tp10_2023_NicoMagro.ViewModels.Tableros
{
    public class CrearTableroViewModel
    {
        [Display(Name = "Id del usuario propietario")]
        public int IdUsuarioPropietario { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(100, ErrorMessage = "El nombre del tablero no puede contener mas de 100 caracteres")]
        [Display(Name = "Nombre del tablero")]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(200, ErrorMessage = "La descripción del tablero no puede contener mas de 200 caracteres")]
        public string Descripcion { get; set; }


        public CrearTableroViewModel()
        {

        }

        public CrearTableroViewModel(Tablero tablero)
        {
            this.IdUsuarioPropietario = tablero.IdUsuarioPropietario;
            this.Nombre = tablero.Nombre;
            this.Descripcion = tablero.Descripcion;
        }
    }
}
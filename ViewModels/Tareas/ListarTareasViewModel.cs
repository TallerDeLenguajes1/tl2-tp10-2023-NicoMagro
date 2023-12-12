using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_NicoMagro.Models;

namespace tl2_tp10_2023_NicoMagro.ViewModels.Tareas
{
    public class ListarTareasViewModel
    {
        public List<Tarea> ListadoTareas { get; set; }


        public ListarTareasViewModel()
        {

        }

        public ListarTareasViewModel(List<Tarea> tareas)
        {
            this.ListadoTareas = tareas;
        }
    }
}
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_NicoMagro.Models;

namespace tl2_tp10_2023_NicoMagro.ViewModels.Tableros
{
    public class ListarTablerosViewModel
    {
        public List<Tablero> ListadoTableros { get; set; }


        public ListarTablerosViewModel()
        {

        }

        public ListarTablerosViewModel(List<Tablero> tableros)
        {
            this.ListadoTableros = tableros;
        }
    }
}
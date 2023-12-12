using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_NicoMagro.Models;

namespace tl2_tp10_2023_NicoMagro.ViewModels.Usuarios
{
    public class ListarUsuariosViewModel
    {
        public List<Usuario> ListadoUsuarios { get; set; }


        public ListarUsuariosViewModel()
        {

        }

        public ListarUsuariosViewModel(List<Usuario> usuarios)
        {
            this.ListadoUsuarios = usuarios;
        }
    }
}
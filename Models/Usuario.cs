using tl2_tp10_2023_NicoMagro.ViewModels.Usuarios;

namespace tl2_tp10_2023_NicoMagro.Models
{
    public enum Rol
    {
        Administrador = 0,
        Operador = 1
    }

    public class Usuario
    {
        private int id;
        private string nombre;
        private string password;
        private Rol rol;

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Password { get => password; set => password = value; }
        public Rol RolUsuario { get => rol; set => rol = value; }

        public bool EsAdministrador => RolUsuario == Rol.Administrador;
        public bool EsOperador => RolUsuario == Rol.Operador;

        public Usuario()
        {

        }

        public Usuario(CrearUsuarioViewModel vm)
        {
            this.Nombre = vm.Nombre;
            this.Password = vm.Password;
            this.RolUsuario = vm.RolUsuario;
        }

        public Usuario(ModificarUsuarioViewModel vm)
        {
            this.Id = vm.Id;
            this.Nombre = vm.Nombre;
            this.Password = vm.Password;
            this.RolUsuario = vm.RolUsuario;
        }
    }

}
namespace TP9.Clases
{
    public class Tarea
    {
        public int Id { get; set; }
        public int Id_tablero { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Color { get; set; }
        public EstadoTarea Estado { get; set; }
        public int? IdUsuarioAsignado { get; set; }
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
using TP9.Clases;
using Microsoft.Data.Sqlite;

namespace TP9.Repositorios
{
    public class TareaRepository : ITareaRepository
    {
        private string cadenaConexion = "Data Source=DB/Kanban.db;Cache=Shared";

        public void Create(int idTablero, Tarea task)
        {
            var query = $"INSERT INTO Tarea (Id_tablero, Nombre, Estado, Descripcion, Color, Id_usuario_asignado)  VALUES (@IdTablero, @Nombre, @Estado, @Descripcion, @Color, @IdUsuarioAsignado)";
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {

                connection.Open();
                var command = new SqliteCommand(query, connection);

                //command.Parameters.Add(new SqliteParameter("@Id", task.Id));
                command.Parameters.Add(new SqliteParameter("@IdTablero", idTablero));
                command.Parameters.Add(new SqliteParameter("@Nombre", task.Nombre));
                command.Parameters.Add(new SqliteParameter("@Estado", task.Estado));
                command.Parameters.Add(new SqliteParameter("@Descripcion", task.Descripcion));
                command.Parameters.Add(new SqliteParameter("@Color", task.Color));
                command.Parameters.Add(new SqliteParameter("@IdUsuarioAsignado", task.IdUsuarioAsignado));

                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Update(int id, Tarea task)
        {
            var query = "UPDATE Tarea SET Id_tablero = @Id_tablero, Nombre = @NombreTarea, Estado = @Estado, Descripcion = @Descripcion, Color = @Color, Id_usuario_asignado = @IdUsuario WHERE Id = @Id";

            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                var command = new SqliteCommand(query, connection);

                command.Parameters.Add(new SqliteParameter("@Id", id));
                command.Parameters.Add(new SqliteParameter("@Id_tablero", task.Id_tablero));
                command.Parameters.Add(new SqliteParameter("@NombreTarea", task.Nombre));
                command.Parameters.Add(new SqliteParameter("@Estado", task.Estado));
                command.Parameters.Add(new SqliteParameter("@Descripcion", task.Descripcion));
                command.Parameters.Add(new SqliteParameter("@Color", task.Color));
                command.Parameters.Add(new SqliteParameter("@IdUsuario", task.IdUsuarioAsignado));

                connection.Open();
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public List<Tarea> GetAll()
        {
            var queryString = @"SELECT * FROM Tarea;";
            List<Tarea> tasks = new List<Tarea>();
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                connection.Open();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var task = new Tarea();
                        task.Id = Convert.ToInt32(reader["Id"]);
                        task.Id_tablero = Convert.ToInt32(reader["Id_tablero"]);
                        task.Nombre = reader["Nombre"].ToString();
                        task.Estado = (EstadoTarea)Convert.ToInt32(reader["Estado"]);
                        task.Descripcion = reader["Descripcion"].ToString();
                        task.Color = reader["Color"].ToString();
                        task.IdUsuarioAsignado = Convert.ToInt32(reader["Id_usuario_asignado"]);

                        tasks.Add(task);
                    }
                }
                connection.Close();
            }
            return tasks;
        }

        public Tarea GetById(int id)
        {
            var query = "SELECT * FROM Tarea WHERE Id = @Id";
            var task = new Tarea();

            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                var command = new SqliteCommand(query, connection);

                command.Parameters.Add(new SqliteParameter("@Id", id));
                connection.Open();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        task.Id = id;
                        task.Id_tablero = Convert.ToInt32(reader["Id_tablero"]);
                        task.Nombre = reader["Nombre"].ToString();
                        task.Estado = (EstadoTarea)Convert.ToInt32(reader["Estado"]);
                        task.Descripcion = reader["Descripcion"].ToString();
                        task.Color = reader["Color"].ToString();
                        task.IdUsuarioAsignado = Convert.ToInt32(reader["Id_usuario_asignado"]);
                    }
                }

                connection.Close();
            }
            return task;
        }

        public List<Tarea> GetByUsuario(int idUsuario)
        {
            var queryString = @"SELECT Id, Id_tablero, Nombre, Estado, Descripcion, Color, Id_usuario_asignado FROM Tarea WHERE Id_usuario_asignado = @idUsuario;";
            List<Tarea> Tareas = new List<Tarea>();
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                connection.Open();

                command.Parameters.Add(new SqliteParameter("@idUsuario", idUsuario));

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var task = new Tarea();
                        task.Id = Convert.ToInt32(reader["Id"]);
                        task.Id_tablero = Convert.ToInt32(reader["Id_tablero"]);
                        task.Nombre = reader["Nombre"].ToString();
                        task.Estado = (EstadoTarea)Convert.ToInt32(reader["Estado"]);
                        task.Descripcion = reader["Descripcion"].ToString();
                        task.Color = reader["Color"].ToString();
                        task.IdUsuarioAsignado = Convert.ToInt32(reader["Id_usuario_asignado"]);

                        Tareas.Add(task);
                    }
                }
                connection.Close();
            }
            return Tareas;
        }

        public List<Tarea> GetByTablero(int idTablero)
        {
            var queryString = @"SELECT * FROM Tarea WHERE Id_tablero = @idTablero;";
            List<Tarea> Tareas = new List<Tarea>();
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.Add(new SqliteParameter("@idTablero", idTablero));
                connection.Open();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var task = new Tarea();
                        task.Id = Convert.ToInt32(reader["Id"]);
                        task.Id_tablero = Convert.ToInt32(reader["Id_tablero"]);
                        task.Nombre = reader["Nombre"].ToString();
                        task.Estado = (EstadoTarea)Convert.ToInt32(reader["Estado"]);
                        task.Descripcion = reader["Descripcion"].ToString();
                        task.Color = reader["Color"].ToString();
                        task.IdUsuarioAsignado = Convert.ToInt32(reader["Id_usuario_asignado"]);

                        Tareas.Add(task);
                    }
                }
                connection.Close();
            }
            return Tareas;
        }

        public void Remove(int id)
        {
            var query = "DELETE FROM Tarea WHERE Id = @Id";

            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SqliteCommand(query, connection);

                command.Parameters.Add(new SqliteParameter("@Id", id));
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void AssignUserTask(int idUsuario, int idTarea)
        {
            var query = "UPDATE Tarea SET Id_usuario_asignado = @idUsuario WHERE Id = idTarea";
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SqliteCommand(query, connection);

                command.Parameters.Add(new SqliteParameter("@idUsuario", idUsuario));
                command.Parameters.Add(new SqliteParameter("@idTarea", idTarea));

                command.ExecuteNonQuery();

                connection.Close();
            }

        }
    }
}
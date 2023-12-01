using tl2_tp10_2023_NicoMagro.Models;
using Microsoft.Data.Sqlite;

namespace tl2_tp10_2023_NicoMagro.Repositories
{
    public class TableroRepository : ITableroRepository
    {
        private string cadenaConexion = "Data Source=BD/Kanban.db;Cache=Shared";

        public void Create(Tablero tablero)
        {
            var query = $"INSERT INTO Tablero (Id_usuario_propietario, Nombre, Descripcion) VALUES (@Id_usuario_propietario, @Nombre, @Descripcion)";
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {

                connection.Open();
                var command = new SqliteCommand(query, connection);

                //command.Parameters.Add(new SqliteParameter("@Id", tablero.Id));
                command.Parameters.Add(new SqliteParameter("@Id_usuario_propietario", tablero.IdUsuarioPropietario));
                command.Parameters.Add(new SqliteParameter("@Nombre", tablero.Nombre));
                command.Parameters.Add(new SqliteParameter("@Descripcion", tablero.Descripcion));

                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Update(int id, Tablero tablero)
        {
            var query = "UPDATE Tablero SET Id_usuario_propietario = @Id_usuario, Nombre = @Nombre_tablero, Descripcion = @Descripcion WHERE Id = @Id";

            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SqliteCommand(query, connection);

                command.Parameters.Add(new SqliteParameter("@Id", id));
                command.Parameters.Add(new SqliteParameter("@Id_usuario", tablero.IdUsuarioPropietario));
                command.Parameters.Add(new SqliteParameter("@Nombre_tablero", tablero.Nombre));
                command.Parameters.Add(new SqliteParameter("@Descripcion", tablero.Descripcion));
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public List<Tablero> GetAll()
        {
            var queryString = @"SELECT * FROM Tablero;";
            List<Tablero> Tableros = new List<Tablero>();
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                connection.Open();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tablero = new Tablero();
                        tablero.Id = Convert.ToInt32(reader["Id"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["Id_usuario_propietario"]);
                        tablero.Nombre = reader["Nombre"].ToString();
                        tablero.Descripcion = reader["Descripcion"].ToString();
                        Tableros.Add(tablero);
                    }
                }
                connection.Close();
            }
            return Tableros;
        }

        public Tablero GetById(int id)
        {
            var query = "SELECT * FROM Tablero WHERE Id = @Id";
            var tablero = new Tablero();

            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                var command = new SqliteCommand(query, connection);
                connection.Open();

                command.Parameters.Add(new SqliteParameter("@Id", id));

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tablero.Id = Convert.ToInt32(reader["Id"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["Id_usuario_propietario"]);
                        tablero.Nombre = reader["Nombre"].ToString();
                        tablero.Descripcion = reader["Descripcion"].ToString();
                    }
                }

                connection.Close();
            }
            return tablero;
        }

        public void Remove(int id)
        {

            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = $"DELETE FROM Tablero WHERE id = @idTablero;";
                command.Parameters.Add(new SqliteParameter("@idTablero", id));

                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
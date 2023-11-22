using Microsoft.AspNetCore.DataProtection.Repositories;
using TP9.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace TP9.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private string cadenaConexion = "Data Source=DB/Kanban.db;Cache=Shared";

        public void Create(Usuario user)
        {
            var query = $"INSERT INTO Usuario (Nombre_de_usuario) VALUES (@Nombre)";
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {


                var command = new SqliteCommand(query, connection);

                //command.Parameters.Add(new SQLiteParameter("@Id", user.Id));
                command.Parameters.Add(new SqliteParameter("@Nombre", user.Nombre));
                connection.Open();
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Update(int id, Usuario user)
        {
            var query = "UPDATE Usuario SET Nombre_de_usuario = @Nombre WHERE Id = @Id";

            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SqliteCommand(query, connection);

                command.Parameters.Add(new SqliteParameter("@Id", id));
                command.Parameters.Add(new SqliteParameter("@Nombre", user.Nombre));
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public List<Usuario> GetAll()
        {
            var queryString = @"SELECT * FROM Usuario;";
            List<Usuario> Usuarios = new List<Usuario>();
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                connection.Open();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new Usuario();
                        user.Id = Convert.ToInt32(reader["Id"]);
                        user.Nombre = reader["Nombre_de_usuario"].ToString();
                        Usuarios.Add(user);
                    }
                }
                connection.Close();
            }
            return Usuarios;
        }

        public Usuario GetById(int id)
        {
            var query = "SELECT * FROM Usuario WHERE Id = @Id";
            var user = new Usuario();

            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                var command = new SqliteCommand(query, connection);
                command.Parameters.Add(new SqliteParameter("@Id", id));

                connection.Open();


                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user.Id = id;
                        user.Nombre = reader["Nombre_de_usuario"].ToString();
                    }
                }

                connection.Close();
            }
            return user;
        }

        public void Remove(int id)
        {
            var query = "DELETE FROM Usuario WHERE Id = @Id";

            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SqliteCommand(query, connection);

                command.Parameters.Add(new SqliteParameter("@Id", id));
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

    }
}
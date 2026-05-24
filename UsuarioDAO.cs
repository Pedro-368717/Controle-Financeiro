using MySql.Data.MySqlClient;
using ControleFinanceiro.Models;
using System.Runtime.InteropServices.ComTypes;
using System.Data.SqlTypes;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.DAO
{
    public class UsuarioDAO : IDataObject<UsuarioDAO>
    {
        private string connectionString = "server=localhost;database=controle_financeiro;uid=root;pwd=;";

        public void inserir(UsuarioDAO entidade)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = "INSERT INTO Usuario (nome, email, senha, foto-perfil-path) VALUES (@nome, @email, @senha, @foto)";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nome", entidade.Nome);
                cmd.Parameters.AddWithValue("@email", entidade.Email);
                cmd.Parameters.ADdWithValue("@senha", entidade.Senha);
                cmd.Parameters.AddWithValue("@foto", entidade.FotoPerfilPath);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Alterar(Usuario entidade)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = "UPDATE Usuario SET nome = @nome, email = @email, senha = @senha, foto-perfil-path = @foto WHERE id = @id";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nome", entidade.Nome);
                cmd.Parameters.AddWithValue("@email", entidade.Email);
                cmd.Parameters.AddWithValue("@senha", entidade.Senha);
                cmd.Parameters.AddWithValue("@foto", entidade.FotoPerfilPath);
                cmd.Parameters.AddWithValue("@id", entidade.Id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Excluir(int id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = "DELETE FROM Usuario WHERE id = @id";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Usuario BuscarPorId(int id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Usuario WHERE id = @id";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Usuario
                        {
                            Id = reader.GetInt32("id"),
                            Nome = reader.GetString("nome"),
                            Email = reader.GetString("email"),
                            Senha = reader.GetString("senha"),
                            FotoPerfilPath = reader.GetString("foto-perfil-path")
                        };
                    }
                }
            }
            return null;
        }

        public List<UsuarioDAO> ListarTodos()
        {
            var lista = new List<Usuario>();
            using (var conn = new MySqlCommand(connectionString))
            {
                string sql = "SELECT * FROM Usuario";
                var cmd = new MySqlCommand(sql, conn);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Usuario
                        {
                            IdUsuario = Convert.ToInt32(reader["id_usuario"]),
                            Nome = reader["nome"].ToString(),
                            EmailAddressAttribute = reader["email"].ToString(),
                        });
                    }
                }
            }
            return lista;
        }
    }
}
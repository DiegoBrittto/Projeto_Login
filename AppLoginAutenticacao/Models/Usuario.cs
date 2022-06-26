using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;

namespace AppLoginAutenticacao.Models
{
    public class Usuario
    {
        public int Usuarios { get; set; }
        [Required]
        [MaxLength(50)]

        public string UsuNome { get; set; }
        [Required]
        [MaxLength(50)]

        public string Login { get; set; }
        [Required]
        [MaxLength(100)]

        public string Senha { get; set; }

        MySqlConnection conexao = new MySqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
        MySqlCommand comando = new MySqlCommand();

        public void InsertUsuario(Usuario usuario)
        {
          
            conexao.Open();
            comando.Connection = conexao;
            comando.CommandText = "call spInsertUsuario(@vUsuNome, @vLogin, @vSenha);";
            comando.Parameters.Add("@vUsuNome", MySqlDbType.VarChar).Value = usuario.UsuNome;
            comando.Parameters.Add("@vLogin", MySqlDbType.VarChar).Value = usuario.Login;
            comando.Parameters.Add("@vSenha", MySqlDbType.VarChar).Value = usuario.Senha;
            comando.ExecuteNonQuery();
            conexao.Close(); 
        }

        public string SelectLogin(string vLogin)
        {
            conexao.Open();
            comando.CommandText = "call spSelectLogin(@Login);";
            comando.Parameters.Add("@Login", MySqlDbType.VarChar).Value = vLogin;
            comando.Connection = conexao;
            string Login = (string)comando.ExecuteScalar();
            conexao.Close();
            if (Login == null)
                Login = "";
            return Login;

        }

        public Usuario SelectUsuario(string vLogin)
        {
            conexao.Open();
            comando.CommandText = "call spSelectUsuario(@Login);";
            comando.Parameters.Add("@Login", MySqlDbType.VarChar).Value = vLogin;
            comando.Connection = conexao;
            var readUsuario = comando.ExecuteReader();
            var TempUsuario = new Usuario();

            readUsuario.Read();

                TempUsuario.Usuarios = int.Parse(readUsuario["Usuario"].ToString());
                TempUsuario.UsuNome = readUsuario["UsuNome"].ToString();
                TempUsuario.Login = readUsuario["Login"].ToString();
                TempUsuario.Senha = readUsuario["Senha"].ToString();
            
            readUsuario.Close();
            conexao.Close();
            return TempUsuario;
        }

        public void UpdateSenha(Usuario usuario)
        {
            conexao.Open();
            comando.CommandText = "call spUpdateSenha(@Login, @Senha);";
            comando.Parameters.Add("@Login", MySqlDbType.VarChar).Value = usuario.Login;
            comando.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = usuario.Senha;
            comando.Connection = conexao;
            comando.ExecuteNonQuery();
            conexao.Close();
        }

        
    }
}
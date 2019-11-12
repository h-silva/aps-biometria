using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using ProjetoBiometria.MODEL;
using System.Windows.Forms;
using NITGEN.SDK.NBioBSP;

namespace ProjetoBiometria.DAO
{
    class UserDAO
    {
        bool result = false;

        public bool insertUser(User user)
        {

            NpgsqlCommand comandoSQL = new NpgsqlCommand();

            comandoSQL.CommandText = "INSERT INTO usuariod (usuario,senha,nomeCompleto,email,imagem,digital, nivelacesso, format, datalength, datatype, length, purpose, quality, reserved, version) " +
                                     "VALUES (@usuario,@senha,@nomeCompleto,@email,@imagem,@digital, @nivelacesso, @format, @dataLength, @dataype, @length, @purpose, @quality, @reserved, @version)";

            comandoSQL.Parameters.AddWithValue("@usuario", user.Usuario);
            comandoSQL.Parameters.AddWithValue("@senha", user.Senha);
            comandoSQL.Parameters.AddWithValue("@nomeCompleto", user.NomeCompleto);
            comandoSQL.Parameters.AddWithValue("@email", user.Email);
            comandoSQL.Parameters.AddWithValue("@imagem", user.Imagem);
            comandoSQL.Parameters.AddWithValue("@digital", user.Digital);
            comandoSQL.Parameters.AddWithValue("@nivelacesso", user.NivelAcesso);
            comandoSQL.Parameters.AddWithValue("@format", user.Format);
            comandoSQL.Parameters.AddWithValue("@dataLength", user.DataLength);
            comandoSQL.Parameters.AddWithValue("@dataype", user.DataType);
            comandoSQL.Parameters.AddWithValue("@length", user.Length);
            comandoSQL.Parameters.AddWithValue("@purpose", user.Purpose);
            comandoSQL.Parameters.AddWithValue("@quality", user.Quality);
            comandoSQL.Parameters.AddWithValue("@reserved", user.Reserved);
            comandoSQL.Parameters.AddWithValue("@version", user.Version);

            try
            {
                Connection.CRUD(comandoSQL);
                result = true;
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show($"Erro ao inserir, contate um admin", "Erro" + ex, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }

        public User selectUser(string valueID)
        {
            User user = new User();

            NpgsqlCommand comandoSQL = new NpgsqlCommand();

            comandoSQL.CommandText = "SELECT codigo, usuario, senha, nomecompleto, email, nivelacesso, imagem, digital, format, datalength, datatype, length, purpose, quality, reserved, version FROM usuariod where usuario = @usuario";

            comandoSQL.Parameters.AddWithValue("@usuario", valueID);


            NpgsqlDataReader dr = null;

            try
            {
                dr = Connection.Select(comandoSQL);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            try
            {
                if (dr.HasRows)
                {

                    while (dr.Read())
                    {
                        user.Codigo = Convert.ToInt32(dr[0].ToString());
                        user.Usuario = dr[1].ToString();
                        user.Senha = dr[2].ToString();
                        user.NomeCompleto = dr[3].ToString();
                        user.Email = dr[4].ToString();
                        user.NivelAcesso = Convert.ToInt32(dr[5].ToString());
                        user.Imagem = (byte[])dr[6];
                        user.Digital = (byte[])dr[7];
                        user.Format = Convert.ToInt32(dr[8].ToString());
                        user.DataLength = Convert.ToInt32(dr[9].ToString());
                        user.DataType = Convert.ToInt32(dr[10].ToString());
                        user.Length = Convert.ToInt32(dr[11].ToString());
                        user.Purpose = Convert.ToInt32(dr[12].ToString());
                        user.Quality = Convert.ToInt32(dr[13].ToString());
                        user.Reserved = Convert.ToInt32(dr[14].ToString());
                        user.Version = Convert.ToInt32(dr[15].ToString());

                    }
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Erro com a comunicação de dados", ex.ToString());
            }
            return user;
        }

        public User selectUserByPassword(string valueID)
        {
            User user = new User();

            NpgsqlCommand comandoSQL = new NpgsqlCommand();

            comandoSQL.CommandText = "SELECT codigo, usuario, senha, nomecompleto, email, nivelacesso, imagem, digital, format, datalength, datatype, length, purpose, quality, reserved, version FROM usuariod where usuario = @usuario";

            comandoSQL.Parameters.AddWithValue("@usuario", valueID);


            NpgsqlDataReader dr = null;

            try
            {
                dr = Connection.Select(comandoSQL);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            try
            {
                if (dr.HasRows)
                {

                    while (dr.Read())
                    {
                        user.Codigo = Convert.ToInt32(dr[0].ToString());
                        user.Usuario = dr[1].ToString();
                        user.Senha = dr[2].ToString();
                        user.NomeCompleto = dr[3].ToString();
                        user.Email = dr[4].ToString();
                        user.NivelAcesso = Convert.ToInt32(dr[5].ToString());
                        user.Imagem = (byte[])dr[6];
                        user.Digital = (byte[])dr[7];
                        user.Format = Convert.ToInt32(dr[8].ToString());
                        user.DataLength = Convert.ToInt32(dr[9].ToString());
                        user.DataType = Convert.ToInt32(dr[10].ToString());
                        user.Length = Convert.ToInt32(dr[11].ToString());
                        user.Purpose = Convert.ToInt32(dr[12].ToString());
                        user.Quality = Convert.ToInt32(dr[13].ToString());
                        user.Reserved = Convert.ToInt32(dr[14].ToString());
                        user.Version = Convert.ToInt32(dr[15].ToString());

                    }
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Erro com a comunicação de dados", ex.ToString());
            }
            return user;
        }

        public bool verifyUser(string valueID)
        {

            NpgsqlCommand comandoSQL = new NpgsqlCommand();

            comandoSQL.CommandText = "SELECT 1 FROM usuariod C WHERE C.usuario = @usuario";


            comandoSQL.Parameters.AddWithValue("@usuario", valueID);

            NpgsqlDataReader dr = null;

            try
            {
                dr = Connection.Select(comandoSQL);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            try
            {
                if (dr.HasRows)
                {
                    return true;
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Erro com a comunicação de dados",ex.ToString());
            }
            return false;
        }

        public bool verifyAdmin()
        {

            NpgsqlCommand comandoSQL = new NpgsqlCommand();

            comandoSQL.CommandText = "SELECT 1 FROM usuariod C WHERE C.codigo > 0";

            NpgsqlDataReader dr = null;

            try
            {
                dr = Connection.Select(comandoSQL);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            try
            {
                if (dr.HasRows)
                {
                    return true;
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Erro com a comunicação de dados", ex.ToString());
            }
            return false;
        }
    }
}

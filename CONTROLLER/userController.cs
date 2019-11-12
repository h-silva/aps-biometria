using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoBiometria.DAO;
using ProjetoBiometria.MODEL;
using BCrypt.Net;
using System.Windows.Forms;

namespace ProjetoBiometria.CONTROLLER
{
    class UserController
    {
        string passwordHash;
        
        public UserController()
        {

        }
        
        public void insertUser(User user)
        {
            UserDAO UserDAO = new UserDAO();
            string hash = getHash(user.Senha);
            user.Senha = hash;
            UserDAO.insertUser(user);

        }

        public User selectUser(string valueID)
        {
           
            UserDAO UserDAO = new UserDAO();
            User user = UserDAO.selectUser(valueID);

            if (user.Usuario == null)
            {
                MessageBox.Show("Usuário não encontrado!", "Projeto Biometria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            return user;
        }

        public User selectUserByPassword(string username, string password)
        {

            UserDAO UserDAO = new UserDAO();
            User user = UserDAO.selectUser(username);

            if (user.Usuario == username && verifyHash(password,user.Senha) == true)
            {
                return user;
            }
            else
            {
                MessageBox.Show("Usuário ou Senha inválido!", "Projeto Biometria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

           
        }

        public bool verifyUser(string valueID)
        {
            bool verify = false;
            UserDAO userDAO = new UserDAO();

            verify = userDAO.verifyUser(valueID);

            if (verify)
            {
                MessageBox.Show("Usuário já existente!", "Projeto Biometria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return verify;
        }

        public bool verifyAdmin()
        {
            UserDAO userDAO = new UserDAO();
            
            return userDAO.verifyAdmin(); 
        }

        public string getHash(string password)
        {
            passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            return passwordHash;
        }

        public bool verifyHash(string senha, string senhaDB)
        {
            return BCrypt.Net.BCrypt.Verify(senha, senhaDB);
        }

        public int getLevel(string valueID)
        {
            switch (valueID)
            {
                case "Membro":
                    return 1;
                    break;
                case "Diretor":
                    return 2;
                    break;
                case "Ministro":
                    return 3;
                    break;
                default: return 0;

            }
        }

        public string setLevel(int valueID)
        {
            switch (valueID)
            {
                case 1: 
                    return "Membro";
                    break;
                case 2:
                    return "Diretor";
                    break;
                case 3:
                    return "Ministro";
                    break;
                default: return "";

            }
        }
    }
}

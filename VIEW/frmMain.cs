using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjetoBiometria.MODEL;

namespace ProjetoBiometria.VIEW
{
    public partial class frmMain : Form
    {
        User user;
         public frmMain(User param)
        {       

            InitializeComponent();
            user = param;
            lblNomeCompleto.Text = user.NomeCompleto;
            lblEmail.Text = user.Email;
            lblNivelAcesso.Text = user.NivelAcesso.ToString();
            lblUsuario.Text = user.Usuario;
           
            if (user.NivelAcesso != 3)
            {
                tsUsuario.Enabled = false;
            }
        }

        private void tsUsuario_Click(object sender, EventArgs e)
        {
            frmRegister reg = new frmRegister();
            reg.ShowDialog();
        }
    }
}

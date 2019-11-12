using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NITGEN.SDK.NBioBSP;
using ProjetoBiometria.MODEL;
using ProjetoBiometria.CONTROLLER;
using Npgsql;

namespace ProjetoBiometria.VIEW
{
    public partial class frmLogin : Form
    {
        NBioAPI m_NBioAPI;
        User user;
        UserController userC;
        public frmLogin()
        {
            InitializeComponent();
            m_NBioAPI = new NBioAPI();

            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtUsername.Text == "")
            {
                MessageBox.Show("Favor, preencher o campo de usuário", "Projeto Biometria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
            }
            else if(txtPassword.Text == "")
            {
                MessageBox.Show("Favor, preencher o campo de senha", "Projeto Biometria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
            }
            else
            {
                userC = new UserController();
                user = userC.selectUserByPassword(txtUsername.Text, txtPassword.Text);

                    if (user != null)
                    {

                        MessageBox.Show("Verificação bem Sucedida!!", "Projeto Biometria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        this.ShowIcon = false;
                        this.ShowInTaskbar = false;
                        frmMain main = new frmMain(user);
                        main.ShowDialog();
                        this.ShowIcon = true;
                        this.ShowInTaskbar = true;
                        this.Show();
                    }         
            }
            
        }

        private void btnBiometria_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "")
            {
                MessageBox.Show("Favor, preencher o campo de usuário", "Projeto Biometria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
            }
            else
            {
                uint reta = m_NBioAPI.OpenDevice(NBioAPI.Type.DEVICE_ID.AUTO);

                user = new User();
                userC = new UserController();
                user = userC.selectUser(txtUsername.Text);
                bool result;
                if (user != null)
                {
                    NBioAPI.Type.HFIR bNewFIR = new NBioAPI.Type.HFIR();

                    NBioAPI.Type.FIR m_biFIR = new NBioAPI.Type.FIR();
                    NBioAPI.Type.FIR_PAYLOAD myPayload = new NBioAPI.Type.FIR_PAYLOAD();

                    m_biFIR.Format = (uint)user.Format;
                    m_biFIR.Data = user.Digital;
                    m_biFIR.Header.DataLength = (uint)user.DataLength;
                    m_biFIR.Header.DataType = (ushort)user.DataType;
                    m_biFIR.Header.Length = (uint)user.Length;
                    m_biFIR.Header.Purpose = (ushort)user.Purpose;
                    m_biFIR.Header.Quality = (ushort)user.Quality;
                    m_biFIR.Header.Reserved = (uint)user.Reserved;
                    m_biFIR.Header.Version = (ushort)user.Version;


                    uint ret = m_NBioAPI.Verify(m_biFIR, out result, myPayload);

                    if (result)
                    {

                        MessageBox.Show("Verificação bem Sucedida!!", "Projeto Biometria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        this.ShowIcon = false;
                        this.ShowInTaskbar = false;
                        frmMain main = new frmMain(user);
                        main.ShowDialog();
                        this.ShowIcon = true;
                        this.ShowInTaskbar = true;
                        this.Show();
                    }
                    else
                    {
                        MessageBox.Show("Verificação Incorreta! Tente novamente", "´Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                m_NBioAPI.CloseDevice(NBioAPI.Type.DEVICE_ID.AUTO);
            }
        }
    }
}

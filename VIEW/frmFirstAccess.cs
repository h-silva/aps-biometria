using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DirectX.Capture;
using System.Drawing.Imaging;
using ProjetoBiometria.CONTROLLER;
using ProjetoBiometria.MODEL;
using NITGEN.SDK.NBioBSP;
using NBioBSPCOMLib;
using Npgsql;



namespace ProjetoBiometria.VIEW
{
    public partial class frmFirstAccess : Form
    {
        NBioAPI.Type.FIR m_biFIR;
        bool existLogin = false;
        UserController userC;
        User user;
        NBioAPI m_NBioAPI;

        public NBioBSPCOMLib.IExtraction objExtraction;



        public frmFirstAccess()
        {
            InitializeComponent();
            m_biFIR = null;
        }

        private void btnColetarDigital_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
             userC = new UserController();
             user = new User();

            if (txtUsuario.Text == "")
            {
                MessageBox.Show("Favor, preencher o campo de usuário", "Projeto Biometria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Focus();
            }
            else if (txtNome.Text == "")
            {
                MessageBox.Show("Favor, preencher o campo de nome", "Projeto Biometria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
            }
            else if (txtEmail.Text == "")
            {
                MessageBox.Show("Favor, preencher o campo de email", "Projeto Biometria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
            }
            else if (txtSenha.Text == "")
            {
                MessageBox.Show("Favor, preencher o campo de senha", "Projeto Biometria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSenha.Focus();
            }
            else
            {
                    NBioAPI.Type.FIR heFIR = new NBioAPI.Type.FIR();

                    m_NBioAPI = new NBioAPI();
                    NBioAPI.Type.HFIR NewFIR;

                    uint reta = m_NBioAPI.OpenDevice(NBioAPI.Type.DEVICE_ID.AUTO);

                    // m_NBioAPI.Enroll(out NewFIR,  null);
                    uint ret = m_NBioAPI.Capture(NBioAPI.Type.FIR_PURPOSE.VERIFY, out NewFIR, NBioAPI.Type.TIMEOUT.DEFAULT, null, null);
                    if (ret == NBioAPI.Error.NONE)
                    {
                        if (NewFIR != null)
                        {
                            m_NBioAPI.GetFIRFromHandle(NewFIR, out m_biFIR);

                            user.NomeCompleto = txtNome.Text;
                            user.Usuario = txtUsuario.Text;
                            user.Senha = txtSenha.Text;
                            user.Email = txtEmail.Text;
                            user.NivelAcesso = 3;
                            user.Digital = m_biFIR.Data;
                            user.Imagem = m_biFIR.Data;
                            user.Format = Convert.ToInt32(m_biFIR.Format.ToString());
                            user.DataLength = Convert.ToInt32(m_biFIR.Header.DataLength.ToString());
                            user.DataType = Convert.ToInt32(m_biFIR.Header.DataType.ToString());
                            user.Purpose = Convert.ToInt32(m_biFIR.Header.Purpose.ToString());
                            user.Length = Convert.ToInt32(m_biFIR.Header.Length.ToString());
                            user.Quality = Convert.ToInt32(m_biFIR.Header.Quality.ToString());
                            user.Reserved = Convert.ToInt32(m_biFIR.Header.Reserved.ToString());
                            user.Version = Convert.ToInt32(m_biFIR.Header.Version.ToString());

                            userC.insertUser(user);

                            MessageBox.Show("Usuário Administrador cadastrado com sucesso!", "Projeto Biometria", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            frmLogin login = new frmLogin();
                            this.Hide();
                            this.ShowIcon = false;
                            this.ShowInTaskbar = false;
                            login.ShowDialog();
                            Application.Exit();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Erro ao cadastrar digital, tente novamente!", "Projeto Biometria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    m_NBioAPI.CloseDevice(NBioAPI.Type.DEVICE_ID.AUTO);
                }
        }

        private void frmFirstAccess_Load(object sender, EventArgs e)
        {
            userC = new UserController();
            existLogin = userC.verifyAdmin();

           if (existLogin)
            {
                frmLogin login = new frmLogin();
                this.Hide();
                this.ShowIcon = false;
                this.ShowInTaskbar = false;
                login.ShowDialog();
                Application.Exit();
            }
            

        }
    }
}

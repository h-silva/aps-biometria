using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using DirectX.Capture;
using ProjetoBiometria.CONTROLLER;

namespace ProjetoBiometria.VIEW
{
    public partial class frmRegister : Form
    {
        List<AccessLevel> dataSource;
        public DirectX.Capture.Filter Camera;
        public DirectX.Capture.Capture CaptureInfo;
        public DirectX.Capture.Filters CamContainer;
        Image capturaImagem;
        public string caminhoImagemSalva = null;

        public frmRegister()
        {

            InitializeComponent();
        }

        private void btnLigarCamera_Click(object sender, EventArgs e)
        {
            CamContainer = new DirectX.Capture.Filters();

            try
            {
                int no_of_cam = CamContainer.VideoInputDevices.Count;
                for (int i = 0; i < no_of_cam; i++)
                {
                    try
                    {
                        // obtém o dispositivo de entrada do vídeo
                        Camera = CamContainer.VideoInputDevices[i];
                        // inicializa a Captura usando o dispositivo
                        CaptureInfo = new DirectX.Capture.Capture(Camera, null);
                        // Define a janela de visualização do vídeo
                        CaptureInfo.PreviewWindow = pcCamera;
                        // Capturando o tratamento de evento
                        CaptureInfo.FrameCaptureComplete += AtualizaImagem;
                        // Captura o frame do dispositivo
                        CaptureInfo.CaptureFrame();
                        // Se o dispositivo foi encontrado e inicializado então sai sem checar o resto
                        break;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        public void AtualizaImagem(PictureBox frame)
        {
            try
            {
                capturaImagem = frame.Image;
                pcCamera.Image = capturaImagem;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro " + ex.Message);
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {

        }

        private void frmRegister_Load(object sender, EventArgs e)
        {

            
            dataSource = new List<AccessLevel>();
            dataSource.Add(new AccessLevel() { Name = "Membro", Value = "Membro" });
            dataSource.Add(new AccessLevel() { Name = "Diretor", Value = "Diretor" });
            dataSource.Add(new AccessLevel() { Name = "Ministro", Value = "Ministro" });
            this.cbNivelAcesso.DataSource = dataSource;
            this.cbNivelAcesso.DisplayMember = "Name";
            this.cbNivelAcesso.ValueMember = "Value";

        }
    }
}

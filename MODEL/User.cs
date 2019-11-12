using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NITGEN.SDK.NBioBSP;

namespace ProjetoBiometria.MODEL
{
   public  class User
    {
        private int codigo;
        private string usuario;
        private string senha;
        private string nomeCompleto;
        private string email;
        private int nivelAcesso;
        private string dataNascimento;
        private byte[] imagem;
        private byte[] digital;
        private int dataLength;
        private int format;
        private int dataType;
        private int length;
        private int purpose;
        private int quality;
        private int reserved;
        private int version;
        public  User(){
            }

        public int Codigo { get => codigo; set => codigo = value; }
        public string Usuario { get => usuario; set => usuario = value; }
        public string Senha { get => senha; set => senha = value; }
        public string NomeCompleto { get => nomeCompleto; set => nomeCompleto = value; }
        public string Email { get => email; set => email = value; }
        public int NivelAcesso { get => nivelAcesso; set => nivelAcesso = value; }
        public string DataNascimento { get => dataNascimento; set => dataNascimento = value; }
        public byte[] Imagem { get => imagem; set => imagem = value; }
        public byte[] Digital { get => digital; set => digital = value; }
        public int DataLength { get => dataLength; set => dataLength = value; }
        public int DataType { get => dataType; set => dataType = value; }
        public int Length { get => length; set => length = value; }
        public int Purpose { get => purpose; set => purpose = value; }
        public int Quality { get => quality; set => quality = value; }
        public int Reserved { get => reserved; set => reserved = value; }
        public int Version { get => version; set => version = value; }
        public int Format { get => format; set => format = value; }
    }
}

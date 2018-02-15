using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Runtime.Serialization;

namespace EntidadesCompartidas
{

    [DataContract]
    public class Empresa
    {
        private int codigo;
        private int rut;
        private string dirFiscal;
        private string telefono;
        private List<TipoContrato> listaContratos;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public int Rut
        {
            get { return rut; }
            set { rut = value; }
        }

        public string DirFiscal
        {
            get { return dirFiscal; }
            set { dirFiscal = value; }
        }

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }
        public List<TipoContrato> ListaContratos
        {
            get { return listaContratos; }
            set { listaContratos = value; }
        }

        public Empresa()
        { }

        public Empresa(int pCodigo, int pRut, string pDirFiscal, string pTelefono, List<TipoContrato> pLista)
        {
            Codigo = pCodigo;
            Rut = pRut;
            DirFiscal = pDirFiscal;
            Telefono = pTelefono;
            ListaContratos = pLista;
        }


    }
}

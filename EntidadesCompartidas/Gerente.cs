using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Runtime.Serialization;

namespace EntidadesCompartidas
{
    [DataContract]
    public class Gerente : Usuario
    {
        private string correo;

        public string Correo
        {
            get { return correo; }
            set { correo = value; }
        }


        public Gerente()
        { }


        public Gerente(int pCedula, string pNomUsu, string pPass, string pNomComp, string pCorreo)
            : base(pCedula, pNomUsu, pPass, pNomComp)
        {
            Correo = pCorreo;
        }

    }
}

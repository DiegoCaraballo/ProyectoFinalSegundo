using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Runtime.Serialization;

using System.Text.RegularExpressions;

namespace EntidadesCompartidas
{
    [DataContract]
    public class Gerente : Usuario
    {
        private string correo;

        [DataMember]
        public string Correo
        {
            get { return correo; }
            set
            {
                if (Regex.IsMatch(value,
                  @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                  @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$")
                   )
                    correo = value;
                else
                    throw new Exception("El formato del correo no es correcto");
            }
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logica
{
    public class FabricaLogica
    {
        public static IUusuarioLogica GetLogicaUsuario()
        {
            return (UsuarioLogica.GetInstancia());
        }

        public static IPagoLogica GetLogicaPago()
        {
            return (PagoLogica.GetInstancia());
        }

        public static IEmpresaLogica GetLogicaEmpresa()
        {
            return (EmpresaLogica.GetInstancia());
        }

        public static ITipoContratoLogica GetLogicaTipoContrato()
        {
            return (TipoContratoLogica.GetInstancia());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia
{
    internal class EmpresaPersistencia: IEmpresaPersistencia
    {
        #region SINGLETON
        private static EmpresaPersistencia instancia = null;
        private EmpresaPersistencia() { }
        public static EmpresaPersistencia GetInstancia()
        {
            if (instancia == null)
                instancia = new EmpresaPersistencia();
            return instancia;
        }
        #endregion

        #region Operaciones

        public Empresa BuscarEmpresa(int codEmp)
        { 
            SqlConnection cnn = new SqlConnection(Conexion.Cnn);

            Empresa unaEmpresa = null;

            SqlCommand cmd = new SqlCommand("BuscarEmpresa", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@codEmpresa", codEmp);

            try
            {
                cnn.Open();
                SqlDataReader lector = cmd.ExecuteReader();
                if (lector.HasRows)
                {
                    lector.Read();
                    unaEmpresa = new Empresa((int)lector["codEmpresa"], (int)lector["rut"], (string)lector["dirFiscal"], (string)lector["telefono"]);                 
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
            return unaEmpresa;
        }        
        
        #endregion
    }
}

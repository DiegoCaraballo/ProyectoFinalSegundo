using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia
{
    internal class TipoContratoPersistencia: ITipoContratoPersistencia
    {
        #region SINGLETON
        private static TipoContratoPersistencia instancia = null;
        private TipoContratoPersistencia() { }
        public static TipoContratoPersistencia GetInstancia()
        {
            if (instancia == null)
                instancia = new TipoContratoPersistencia();
            return instancia;
        }
        #endregion

        #region Operaciones

        public TipoContrato BuscarContrato(int codEmp, int codTipoContrato)
        {
            SqlConnection cnn = new SqlConnection(Conexion.Cnn);

            TipoContrato unContrato = null;

            SqlCommand cmd = new SqlCommand("BuscarContrato", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@codEmp", codEmp);
            cmd.Parameters.AddWithValue("@codContrato", codTipoContrato);

            try
            {
                cnn.Open();
                SqlDataReader lector = cmd.ExecuteReader();
                if (lector.HasRows)
                {
                    lector.Read();
                    unContrato = new TipoContrato((Empresa)lector["codEmp"], (int)lector["codContrato"], (string)lector["nombre"]);
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
            return unContrato;
        }

        #endregion
    }
}

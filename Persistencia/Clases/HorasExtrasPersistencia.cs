using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia
{
    internal class HorasExtrasPersistencia : IHorasExtrasPersistencia
    {
        #region SINGLETON

        private static HorasExtrasPersistencia instancia = null;
        private HorasExtrasPersistencia() { }
        public static HorasExtrasPersistencia GetInstancia()
        {
            if (instancia == null)
                instancia = new HorasExtrasPersistencia();
            return instancia;
        }

        #endregion

        #region OPERACIONES

        //Guardar horas extras de un cajero
        public void GuardarHorasExtras(HorasExtras horasExtras)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand comando = new SqlCommand("AgregarHorasExtras",conexion);
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@fecha", horasExtras.Fecha);
            comando.Parameters.AddWithValue("@minutos", horasExtras.Minutos);
            comando.Parameters.AddWithValue("@cedula", horasExtras.Cajero.Cedula);

            SqlParameter retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;
            comando.Parameters.Add(retorno);

            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
                //TODO - Ver devoluciones
                if ((int)retorno.Value == -1)
                    throw new Exception("Cajero No Existe");
                if ((int)retorno.Value == -2)
                    throw new Exception("El cajero esta inactivo");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }

        #endregion
    }
}

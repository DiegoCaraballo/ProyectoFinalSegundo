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
        public void GuardarHorasExtras(DateTime pFecha, int pMinutos, Usuario pCajero)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand comando = new SqlCommand("AltaHorasExtras");
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@fecha", pFecha);
            comando.Parameters.AddWithValue("@minutos", pMinutos);
            comando.Parameters.AddWithValue("@cedula", pCajero.Cedula);

            SqlParameter retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;
            comando.Parameters.Add(retorno);

            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
                //TODO - Ver devoluciones
                if ((int)retorno.Value == -1)
                    throw new Exception("La cedula ingresada ya existe en el sistema.");
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

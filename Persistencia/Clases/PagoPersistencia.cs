using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia
{
    internal class PagoPersistencia : IPagoPersistencia
    {
        #region SINGLETON

        private static PagoPersistencia instancia = null;
        private PagoPersistencia() { }
        public static PagoPersistencia GetInstancia()
        {
            if (instancia == null)
                instancia = new PagoPersistencia();
            return instancia;
        }

        #endregion

        #region Operaciones

        public void AltaPago(Pago unPago)
        {
 
        }

        public void BajaPago(Pago unPago)
        {

        }

        public void ModificarPago(Pago unPago)
        {

        }

        public Pago BuscarPago(int numeroInt)
        {
            SqlConnection cnn = new SqlConnection(Conexion.Cnn);

            Pago unPago = null;

            SqlCommand cmd = new SqlCommand("BuscarPago", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@numeroInt", numeroInt);

            try
            {
                cnn.Open();
                SqlDataReader lector = cmd.ExecuteReader();
                if (lector.HasRows)
                {
                    int _numeroInt = (int)lector["numeroInt"];
                    DateTime _fecha = (DateTime)lector["fecha"];
                    int _montoTotal = (int)lector["montoTotal"];
                    int _cedulaCajero = (int)lector["cedulaCajero"];
                    //TODO - Ver como agregar tipo contrato y facturas en el SP
                    TipoContrato _unTipoContrato = (TipoContrato)lector[""];
                    unPago = new Pago(_numeroInt, _fecha, _montoTotal, CajeroPersistencia.GetInstancia().BuscarCajero(_cedulaCajero));
                }
                lector.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
            return unPago;
        }
        #endregion
    }
}

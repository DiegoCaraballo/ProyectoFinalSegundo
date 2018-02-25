using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia
{
    internal class FacturaPersistencia
    {
        internal static void AgregarFactura(int pIdPago, int pCodContrato, int pCodEmp, int pMonto, int pCodCli, DateTime pFechaVto, SqlTransaction pTransaccion)
        {
            SqlCommand comando = new SqlCommand("AltaFactura", pTransaccion.Connection);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@idPago", pIdPago);
            comando.Parameters.AddWithValue("@codContrato", pCodContrato);
            comando.Parameters.AddWithValue("@codEmp", pCodEmp);
            comando.Parameters.AddWithValue("@monto", pMonto);
            comando.Parameters.AddWithValue("@codCli", pCodCli);
            comando.Parameters.AddWithValue("@fechaVto", pFechaVto);
            SqlParameter ParmRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            ParmRetorno.Direction = ParameterDirection.ReturnValue;
            comando.Parameters.Add(ParmRetorno);

            try
            {
                //determino que debo trabajar con l misma transaccion
                comando.Transaction = pTransaccion;

                //ejecuto comando
                comando.ExecuteNonQuery();

                //verifico si hay errores
                int retorno = Convert.ToInt32(ParmRetorno.Value);
                if (retorno == -1)
                    throw new Exception("La factura ya fue ingresada");
                else if (retorno == -2)
                    throw new Exception("Error no especificado");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static List<Factura> CargoFactura(int pIdPago, int pCodContrato, int pCodEmp, int pMonto, int pCodCli, DateTime pFechaVto, SqlTransaction pTransaccion)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn);
            SqlCommand comando = new SqlCommand("CargarFacturaDeUnPago", _cnn);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@idPago", pIdPago);
            comando.Parameters.AddWithValue("@codContrato", pCodContrato);
            comando.Parameters.AddWithValue("@codEmp", pCodEmp);
            comando.Parameters.AddWithValue("@monto", pMonto);
            comando.Parameters.AddWithValue("@codCli", pCodCli);
            comando.Parameters.AddWithValue("@fechaVto", pFechaVto);

            List<Factura> _ListaFacturas = new List<Factura>();

            try
            {
                _cnn.Open();

                SqlDataReader _lector = comando.ExecuteReader();

                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        //TODO - Ver como cargar las facturas
                        //_ListaFacturas.Add((Factura)_lector["servicio"]);
                    }
                }
                _lector.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _cnn.Close();
            }

            return _ListaFacturas;
        }
    }
}

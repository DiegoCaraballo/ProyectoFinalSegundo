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
                    throw new Exception("La factura ya existe");
                else if (retorno == -2)
                    throw new Exception("La empresa no existe");
                else if (retorno == -3)
                    throw new Exception("El tipo de contrato no existe");
                else if (retorno == -4)
                    throw new Exception("Error en la base de datos");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static List<Factura> CargoFactura(int pIdPago, int codEmp, int codTipoContrato,Usuario usuLogueado)
        {

            Conexion con = new Conexion();
            SqlConnection _cnn = new SqlConnection(con.cnnUsu(usuLogueado));


            SqlCommand comando = new SqlCommand("CargarFacturaDeUnPago", _cnn);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@idPago", pIdPago);

            List<Factura> _ListaFacturas = new List<Factura>();

            try
            {
                _cnn.Open();

                SqlDataReader _lector = comando.ExecuteReader();

                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {

                        Factura fac = new Factura();

                        fac.FechaVto = (DateTime)_lector["fechaVto"];
                        fac.CodCli = (int)_lector["codCli"];
                        fac.Monto = (int)_lector["monto"];
                        fac.UnTipoContrato = TipoContratoPersistencia.GetInstancia().BuscarContrato(codEmp, codTipoContrato,usuLogueado);

                        _ListaFacturas.Add(fac);
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

        internal static List<Factura> ListarFactura(int pIdPago,Usuario usuLogueado)
        {

            Conexion con = new Conexion();
            SqlConnection cnn = new SqlConnection(con.cnnUsu(usuLogueado));
            SqlCommand cmd = new SqlCommand("CargarFacturaDeUnPago", cnn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idPago", pIdPago);

            List<Factura> _ListaFacturas = new List<Factura>();
            int codCli;
            DateTime fechaVto;
            int monto;
            int codEmpresa;
            int codContrato;
            int idPago;

            try
            {
                cnn.Open();

                SqlDataReader _lector = cmd.ExecuteReader();

                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {


                        idPago = (int)_lector["idPago"];
                        codContrato = (int)_lector["codContrato"];
                        codEmpresa = (int)_lector["codEmp"];
                        monto = (int)_lector["monto"];
                        codCli = Convert.ToInt32(_lector["codCli"]);
                        fechaVto = (DateTime)_lector["fechaVto"];

                        Factura fac = new Factura(monto, codCli, fechaVto, TipoContratoPersistencia.GetInstancia().BuscarContrato(codEmpresa, codContrato,usuLogueado));
                        _ListaFacturas.Add(fac);
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
                cnn.Close();
            }

            return _ListaFacturas;
        }

        internal static List<Factura> ListarFacturaTodos(int pIdPago, Usuario usuLogueado)
        {

            Conexion con = new Conexion();
            SqlConnection cnn = new SqlConnection(con.cnnUsu(usuLogueado));
            SqlCommand cmd = new SqlCommand("CargarFacturaDeUnPago", cnn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idPago", pIdPago);

            List<Factura> _ListaFacturas = new List<Factura>();
            int codCli;
            DateTime fechaVto;
            int monto;
            int codEmpresa;
            int codContrato;
            int idPago;

            try
            {
                cnn.Open();

                SqlDataReader _lector = cmd.ExecuteReader();

                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {


                        idPago = (int)_lector["idPago"];
                        codContrato = (int)_lector["codContrato"];
                        codEmpresa = (int)_lector["codEmp"];
                        monto = (int)_lector["monto"];
                        codCli = Convert.ToInt32(_lector["codCli"]);
                        fechaVto = (DateTime)_lector["fechaVto"];

                        Factura fac = new Factura(monto, codCli, fechaVto, TipoContratoPersistencia.GetInstancia().BuscarContratoTodos(codEmpresa, codContrato, usuLogueado));
                        _ListaFacturas.Add(fac);
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
                cnn.Close();
            }

            return _ListaFacturas;
        }
    }
}

﻿using System;
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

        //Altar pago
        public void AltaPago(Pago unPago, Usuario usuLogueado)
        {
            Conexion con = new Conexion();
            SqlConnection cnn = new SqlConnection(con.cnnUsu(usuLogueado));

            SqlCommand oComando = new SqlCommand("AltaPago", cnn);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _Fecha = new SqlParameter("@fecha", unPago.Fecha);
            SqlParameter _MontoTotal = new SqlParameter("@montoTotal", unPago.MontoTotal);
            SqlParameter _CedulaCajero = new SqlParameter("@cedulaCajero", unPago.UsuCajero.Cedula);
            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            oComando.Parameters.Add(_Fecha);
            oComando.Parameters.Add(_MontoTotal);
            oComando.Parameters.Add(_CedulaCajero);
            oComando.Parameters.Add("@numInterno", SqlDbType.Int).Direction = ParameterDirection.Output;
            oComando.Parameters.Add(_Retorno);

            int afectados = -1;
            SqlTransaction _miTransaccion = null;

            try
            {
                //conecto a la bd
                cnn.Open();

                //determino que voy a trabajar en una unica transaccion
                _miTransaccion = cnn.BeginTransaction();

                //ejecuto comando de alta del servicio en la transaccion
                oComando.Transaction = _miTransaccion;
                oComando.ExecuteNonQuery();

                //verifico si hay errores
                afectados = (int)oComando.Parameters["@Retorno"].Value;
                if (afectados == -1)
                    throw new Exception("No existe el cajero");
                else if (afectados == -3)
                    throw new Exception("El Cajero no está activo");
                else if (afectados == -2)
                    throw new Exception("Error en la base de datos al insertar pago");

                //Obtengo id de pago
                int idPago = Convert.ToInt32(oComando.Parameters["@numInterno"].Value.ToString());

                //si llego hasta aca es xq pude dar de alta el pago

                //genero alta para las facturas
                foreach (Factura unaFactura in unPago.LasFacturas)
                {
                    FacturaPersistencia.AgregarFactura(idPago, unaFactura.UnTipoContrato.CodContrato, unaFactura.UnTipoContrato.UnaEmp.Codigo, unaFactura.Monto, unaFactura.CodCli, unaFactura.FechaVto, _miTransaccion);
                }

                //si llegue aca es xq no hubo problemas con las facturas
                _miTransaccion.Commit();
            }

            catch (Exception ex)
            {
                _miTransaccion.Rollback();
                throw ex;
            }

            finally
            {
                cnn.Close();
            }
        }

        //Buscar pago de una factura
        public DateTime PagoDeUnaFactura(int codContrato, int codEmp, int monto, int codCli, DateTime fecha)
        {
            SqlConnection cnn = new SqlConnection(Conexion.Cnn);

            DateTime fechaPago = new DateTime();

            SqlCommand cmd = new SqlCommand("pagoDeUnaFactura", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@codContra", codContrato);
            cmd.Parameters.AddWithValue("@codEmp", codEmp);
            cmd.Parameters.AddWithValue("@monto", monto);
            cmd.Parameters.AddWithValue("@codCli", codCli);
            cmd.Parameters.AddWithValue("@fecha", fecha);

            try
            {
                cnn.Open();
                SqlDataReader lector = cmd.ExecuteReader();
                if (lector.HasRows)
                {
                    lector.Read();
                    fechaPago = (DateTime)lector["fecha"];
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
            return fechaPago;
        }

        //Listar Pagos
        public List<Pago> listar(Usuario usuLogueado)
        {
            Conexion con = new Conexion();
            SqlConnection cnn = new SqlConnection(con.cnnUsu(usuLogueado));

            SqlCommand cmd = new SqlCommand("ListarPagos", cnn);
            SqlDataReader lector;

            List<Pago> lista = new List<Pago>();
            int idPago;
            DateTime fecha;
            // Cajero unCajero;
            int monto;
            int codCajero;

            try
            {
                cnn.Open();
                lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    idPago = (int)lector["numeroInt"];
                    fecha = (DateTime)lector["fecha"];
                    codCajero = (int)lector["cedulaCajero"];
                    monto = (int)lector["montoTotal"];

                    Pago unPago = new Pago(idPago, fecha, monto, CajeroPersistencia.GetInstancia().BuscarCajeroTodos(codCajero,usuLogueado), FacturaPersistencia.ListarFacturaTodos(idPago,usuLogueado));
                    lista.Add(unPago);
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

            return lista;

        }
        #endregion
    }
}

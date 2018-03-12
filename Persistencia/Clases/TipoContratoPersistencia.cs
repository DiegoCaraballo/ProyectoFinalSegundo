using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia
{
    internal class TipoContratoPersistencia : ITipoContratoPersistencia
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
                    //TODO - Funciona, pero ver si esta bien
                    Empresa emp = new Empresa();
                    emp = EmpresaPersistencia.GetInstancia().BuscarEmpresa((int)lector["codEmp"]);
                    unContrato = new TipoContrato(emp, (int)lector["codContrato"], (string)lector["nombre"]);
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

        public void AltaTipoContrato(TipoContrato unTipoContrato, Usuario usuLogueado)
        {
            Conexion con = new Conexion();
            SqlConnection cnn = new SqlConnection(con.cnnUsu(usuLogueado));

            SqlCommand cmd = new SqlCommand("AltaTipoContrato", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@codContrato", unTipoContrato.CodContrato);
            cmd.Parameters.AddWithValue("@codEmp", unTipoContrato.UnaEmp.Codigo);
            cmd.Parameters.AddWithValue("@nombre", unTipoContrato.Nombre);

            SqlParameter retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(retorno);

            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
                if ((int)retorno.Value == -1)
                    throw new Exception("No existe la empresa");
                else if ((int)retorno.Value == -2)
                    throw new Exception("La empresa esta inactiva");
                else if ((int)retorno.Value == -3)
                    throw new Exception("Ya existe el Tipo de Contrato");
                else if ((int)retorno.Value == -4)
                    throw new Exception("Error en la base de datos");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cnn.Close();
            }

        }

        public void BajaTipoContrato(TipoContrato unTipoContrato, Usuario usuLogueado)
        {
            Conexion con = new Conexion();
            SqlConnection cnn = new SqlConnection(con.cnnUsu(usuLogueado));

            SqlCommand cmd = new SqlCommand("BajaTipoContrato", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@codContrato", unTipoContrato.CodContrato);
            cmd.Parameters.AddWithValue("@codEmp", unTipoContrato.UnaEmp.Codigo);

            SqlParameter retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(retorno);

            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
                if ((int)retorno.Value == -1)
                    throw new Exception("No existe el Tipo de Contrato");
                else if ((int)retorno.Value == -2)
                    throw new Exception("Error en la base de datos");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public void ModificarTipoContrato(TipoContrato unTipoContrato, Usuario usuLogueado)
        {
            Conexion con = new Conexion();
            SqlConnection cnn = new SqlConnection(con.cnnUsu(usuLogueado));

            SqlCommand cmd = new SqlCommand("ModificarTipoContrato", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@codContrato", unTipoContrato.CodContrato);
            cmd.Parameters.AddWithValue("@codEmp", unTipoContrato.UnaEmp.Codigo);
            cmd.Parameters.AddWithValue("@nombre", unTipoContrato.Nombre);

            SqlParameter retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(retorno);

            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
                if ((int)retorno.Value == -1)
                    throw new Exception("El Tipo de Contrato no está activo");
                else if ((int)retorno.Value == -2)
                    throw new Exception("Error en la base de datos");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }


        //TODO- Ver si tenemos que agregar otro buscar contratos--- En web no se precisa usuario pero para el ABM si
        public List<TipoContrato> ListarContratos()
        {
            SqlConnection cnn = new SqlConnection(Conexion.Cnn);
            SqlCommand cmd = new SqlCommand("ListarContratos", cnn);

            cmd.CommandType = CommandType.StoredProcedure;

            List<TipoContrato> Listar = new List<TipoContrato>();
            int codContrato;
            int codEmp;
            string nombre;

            try
            {
                cnn.Open();

                SqlDataReader _lector = cmd.ExecuteReader();

                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        codContrato = (int)_lector["codContrato"];
                        codEmp = (int)_lector["codEmp"];
                        nombre = (string)_lector["nombre"];
                        TipoContrato tc = new TipoContrato(EmpresaPersistencia.GetInstancia().BuscarEmpresa(codEmp), codContrato, nombre);
                        Listar.Add(tc);
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

            return Listar;
        }

        #endregion
    }
}

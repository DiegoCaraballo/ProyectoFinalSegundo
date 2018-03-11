using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia
{
    internal class EmpresaPersistencia : IEmpresaPersistencia
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
                    unaEmpresa = new Empresa((int)lector["codEmpresa"], (string)lector["rut"], (string)lector["dirFiscal"], (string)lector["telefono"]);
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

        public void AltaEmpresa(Empresa unaEmpresa, Usuario usuLogueado)
        {
            Conexion con = new Conexion();
            SqlConnection cnn = new SqlConnection(con.cnnUsu(usuLogueado));

            SqlCommand cmd = new SqlCommand("AltaEmpresa", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@codEmp", unaEmpresa.Codigo);
            cmd.Parameters.AddWithValue("@rut", unaEmpresa.Rut);
            cmd.Parameters.AddWithValue("@direccion", unaEmpresa.DirFiscal);
            cmd.Parameters.AddWithValue("@telefono", unaEmpresa.Telefono);


            SqlParameter retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(retorno);

            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
                if ((int)retorno.Value == -1)
                    throw new Exception("La Empresa ya existe");
                else if ((int)retorno.Value == -2)
                    throw new Exception("Error en dar de alta la empresa");

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

        public void BajaEmpresa(Empresa unaEmpresa, Usuario usuLogueado)
        {
            Conexion con = new Conexion();
            SqlConnection cnn = new SqlConnection(con.cnnUsu(usuLogueado));

            SqlCommand cmd = new SqlCommand("BajaEmpresa", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@codEmp", unaEmpresa.Codigo);

            SqlParameter retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(retorno);

            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
                if ((int)retorno.Value == -1)
                    throw new Exception("La empresa no existe");
                if ((int)retorno.Value == -2)
                    throw new Exception("Error al desactivar Tipo de Contrato y/o Empresa");
                if ((int)retorno.Value == -3)
                    throw new Exception("Error al eliminar Tipo de Contrato y/o Empresa");

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

        public void ModificarEmpresa(Empresa unaEmpresa, Usuario usuLogueado)
        {
            Conexion con = new Conexion();
            SqlConnection cnn = new SqlConnection(con.cnnUsu(usuLogueado));

            SqlCommand cmd = new SqlCommand("ModificarEmpresa", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@codEmp", unaEmpresa.Codigo);
            cmd.Parameters.AddWithValue("@rut", unaEmpresa.Rut);
            cmd.Parameters.AddWithValue("@direccion", unaEmpresa.DirFiscal);
            cmd.Parameters.AddWithValue("@telefono", unaEmpresa.Telefono);


            SqlParameter retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(retorno);

            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
                if ((int)retorno.Value == -1)
                    throw new Exception("La Empresa no existe");
                else if ((int)retorno.Value == -2)
                    throw new Exception("La empresa esta inactiva");
                else if ((int)retorno.Value == -3)
                    throw new Exception("Errero en la base de datos");

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

        public List<Empresa> ListarEmpresas()
        {
            List<Empresa> lista = new List<Empresa>();

            int codigo;
            string rut;
            string dirFiscal;
            string telefono;

            SqlConnection oConexion = new SqlConnection(Conexion.Cnn);
            SqlCommand oComando = new SqlCommand("ListarEmpresas", oConexion);
            SqlDataReader oReader;

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();

                while (oReader.Read())
                {
                    codigo = (int)oReader["codEmpresa"];
                    rut = (string)oReader["rut"];
                    dirFiscal = (string)oReader["dirFiscal"];
                    telefono = (string)oReader["telefono"];

                    Empresa unaEmpresa = new Empresa(codigo, rut, dirFiscal, telefono);

                    lista.Add(unaEmpresa);
                }
                oReader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas con la base de datos: " + ex.Message);
            }
            finally
            {
                oConexion.Close();
            }

            return lista;
        }

        #endregion
    }
}

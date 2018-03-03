using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia
{
    internal class CajeroPersistencia : ICajeroPersistencia
    {
        #region SINGLETON

        private static CajeroPersistencia instancia = null;
        private CajeroPersistencia() { }
        public static CajeroPersistencia GetInstancia()
        {
            if (instancia == null)
                instancia = new CajeroPersistencia();
            return instancia;
        }

        #endregion

        #region Operaciones

        public void AltaCajero(Cajero unCajero)
        {
            SqlConnection cnn = new SqlConnection(Conexion.Cnn);

            SqlCommand cmd = new SqlCommand("AltaCajero", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@cedula", unCajero.Cedula);
            cmd.Parameters.AddWithValue("@nomUsu", unCajero.NomUsu);
            cmd.Parameters.AddWithValue("@pass", unCajero.Pass);
            cmd.Parameters.AddWithValue("@nomCompleto", unCajero.NomCompleto);

            cmd.Parameters.AddWithValue("@horaini", unCajero.HoranIni);
            cmd.Parameters.AddWithValue("@horaFin", unCajero.HoranFin);

            SqlParameter retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(retorno);

            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
                if ((int)retorno.Value == -1)
                    throw new Exception("La cedula ingresada ya existe en el sistema.");
                if ((int)retorno.Value == -2)
                    throw new Exception("La cedula ingresada ya existe como Gerente");
                if ((int)retorno.Value == -3 || (int)retorno.Value == -4)
                    throw new Exception("Error al ingresar el usuario");
                if ((int)retorno.Value == -5)
                    throw new Exception("Error al crear el usuario de SQL");
                if ((int)retorno.Value == -6)
                    throw new Exception("Error al crear el usuario de Base de datos");

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

        public void BajaCajero(Cajero unCajero)
        {
            SqlConnection cnn = new SqlConnection(Conexion.Cnn);

            SqlCommand cmd = new SqlCommand("BajaCajero", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@cedula", unCajero.Cedula);

            SqlParameter retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(retorno);

            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
                if ((int)retorno.Value == -1)
                    throw new Exception("La cedula no existe.");
                if ((int)retorno.Value == -2)
                    throw new Exception("La cedula pertenece a un Usuario tipo Gerente");
                if ((int)retorno.Value == -3)
                    throw new Exception("Error al eliminar el usuario de acceso a SQL");
                if ((int)retorno.Value == -4)
                    throw new Exception("Error al eliminar el usuario de acceso a la BD");
                if ((int)retorno.Value == -5 || (int)retorno.Value == -6)
                    throw new Exception("Error al eliminar el Cajero");
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

        public void ModificarCajero(Cajero unCajero)
        {
            SqlConnection cnn = new SqlConnection(Conexion.Cnn);

            SqlCommand cmd = new SqlCommand("ModificarCajero", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@cedula", unCajero.Cedula);
            cmd.Parameters.AddWithValue("@nomUsu", unCajero.NomUsu);
            cmd.Parameters.AddWithValue("@pass", unCajero.Pass);
            cmd.Parameters.AddWithValue("@nomCompleto", unCajero.NomCompleto);

            cmd.Parameters.AddWithValue("@horaini", unCajero.HoranIni);
            cmd.Parameters.AddWithValue("@horaFin", unCajero.HoranFin);

            SqlParameter retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(retorno);

            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
                if ((int)retorno.Value == -1)
                    throw new Exception("La cedula no existe.");
                if ((int)retorno.Value == -2)
                    throw new Exception("La cedula ingresada ya existe como Gerente");
                if ((int)retorno.Value == -3)
                    throw new Exception("Error al crear el usuario de SQL");
                if ((int)retorno.Value == -4)
                    throw new Exception("Error al crear el usuario de Base de datos");
                if ((int)retorno.Value == -5 || (int)retorno.Value == -6)
                    throw new Exception("Error al ingresar el usuario");
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

        public void CambioPass(Cajero unCajero)
        {
            SqlConnection cnn = new SqlConnection(Conexion.Cnn);

            SqlCommand cmd = new SqlCommand("CambioPass", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@cedula", unCajero.Cedula);
            cmd.Parameters.AddWithValue("@pass", unCajero.Pass);

            SqlParameter retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(retorno);

            try
            {
                //TODO - Cuando resulta el tema de la BD veo los errores que se pueden devolver
                cnn.Open();
                cmd.ExecuteNonQuery();
                if ((int)retorno.Value == -1)
                    throw new Exception("La cedula no existe.");

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

        public Cajero BuscarCajero(int cedula)
        {
            SqlConnection cnn = new SqlConnection(Conexion.Cnn);

            Cajero unCajero = null;

            SqlCommand cmd = new SqlCommand("BuscarCajero", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@cedula", cedula);

            try
            {
                cnn.Open();
                SqlDataReader lector = cmd.ExecuteReader();
                if (lector.HasRows)
                {
                    lector.Read();
                    //TODO - ver tema de fechas... en BD es time y aca es DATE TIME

                    unCajero = new Cajero((int)lector["cedula"], (string)lector["nomUsu"], (string)lector["pass"], (string)lector["nomCompleto"], Convert.ToDateTime(lector["horaIni"]), Convert.ToDateTime(lector["horaFin"]));
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
            return unCajero;
        }


        public Cajero LogueoCajero(string nomUsu)
        {
            SqlConnection cnn = new SqlConnection(Conexion.Cnn);

            Cajero unCajero = null;

            SqlCommand cmd = new SqlCommand("LogueoCajero", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@nomUsu", nomUsu);

            try
            {
                cnn.Open();
                SqlDataReader lector = cmd.ExecuteReader();
                if (lector.HasRows)
                {
                    lector.Read();
                    //TODO - ver tema de fechas... en BD es time y aca es DATE TIME

                    unCajero = new Cajero((int)lector["cedula"], (string)lector["nomUsu"], (string)lector["pass"], (string)lector["nomCompleto"], Convert.ToDateTime(lector["horaIni"]), Convert.ToDateTime(lector["horaFin"]));
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
            return unCajero;
        }



        
        #endregion


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia
{
    internal class GerentePersistencia : IGerentePersistencia
    {
        #region SINGLETON

        private static GerentePersistencia instancia = null;
        private GerentePersistencia() { }
        public static GerentePersistencia GetInstancia()
        {
            if (instancia == null)
                instancia = new GerentePersistencia();
            return instancia;
        }

        #endregion


        public void AltaGerente(Gerente unGerente)
        {
            SqlConnection cnn = new SqlConnection(Conexion.Cnn);

            SqlCommand cmd = new SqlCommand("AltaGerente", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@cedula", unGerente.Cedula);
            cmd.Parameters.AddWithValue("@nomUsu", unGerente.NomUsu);
            cmd.Parameters.AddWithValue("@pass", unGerente.Pass);
            cmd.Parameters.AddWithValue("@nomCompleto", unGerente.NomCompleto);

            cmd.Parameters.AddWithValue("@correo", unGerente.Correo);

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

        public void CambioPass(Gerente unGerente)
        {
            SqlConnection cnn = new SqlConnection(Conexion.Cnn);

            SqlCommand cmd = new SqlCommand("CambioPass", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@cedula", unGerente.Cedula);
            cmd.Parameters.AddWithValue("@pass", unGerente.Pass);

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

        public Gerente LogueoGerente(string nomUsu)
        {
            SqlConnection cnn = new SqlConnection(Conexion.Cnn);

            Gerente unGerente = null;

            SqlCommand cmd = new SqlCommand("LogueoGerente", cnn);
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

                    unGerente = new Gerente((int)lector["cedula"], (string)lector["nomUsu"], (string)lector["pass"], (string)lector["nomCompleto"],(string)lector["correo"]);
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
            return unGerente;
        }


    }
}

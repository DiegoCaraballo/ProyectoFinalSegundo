﻿using System;
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

        #region OPERACIONES
        //Altar usuario Gerente
        public void AltaGerente(Gerente unGerente, Usuario usuLogueado)
        {
            Conexion con = new Conexion();
            SqlConnection cnn = new SqlConnection(con.cnnUsu(usuLogueado));

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
                    throw new Exception("Error al crear usuario");
                if ((int)retorno.Value == -5)
                    throw new Exception("Error al crear el usuario de Servidor");
                if ((int)retorno.Value == -6)
                    throw new Exception("Error al crear el usuario de Base de datos");
                if ((int)retorno.Value == -7 || (int)retorno.Value == -8)
                    throw new Exception("Error al asignar permisos");

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

        //Cambiar Contraseña
        public void CambioPass(Usuario unGerente, Usuario usuLogueado)
        {
            Conexion con = new Conexion();
            SqlConnection cnn = new SqlConnection(con.cnnUsu(usuLogueado));

            SqlCommand cmd = new SqlCommand("CambioPass", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@cedula", unGerente.Cedula);
            cmd.Parameters.AddWithValue("@antiguaPass", usuLogueado.Pass);
            cmd.Parameters.AddWithValue("@nuevaPass", unGerente.Pass);
            cmd.Parameters.AddWithValue("@nomUsu", usuLogueado.NomUsu);

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
                    throw new Exception("La cedula pertenece a un usuario inactivo.");
                if ((int)retorno.Value == -3)
                    throw new Exception("Error al actualizar el usuario");
                if ((int)retorno.Value == -4)
                    throw new Exception("Error al actualizar contraseña en servidor");

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
        
        //Logueo Gerente
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
                    unGerente =
                        new Gerente((int)lector["cedula"], (string)lector["nomUsu"], (string)lector["pass"], (string)lector["nomCompleto"], (string)lector["correo"]);
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

        //Buscar usuario Gerente
        public Gerente BuscarGerente(int cedula,Usuario usuLogueado)
        {
            Conexion con = new Conexion();
            SqlConnection cnn = new SqlConnection(con.cnnUsu(usuLogueado));

            Gerente unGerente = null;

            SqlCommand cmd = new SqlCommand("BuscarGerente", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@cedula", cedula);

            try
            {
                cnn.Open();
                SqlDataReader lector = cmd.ExecuteReader();
                if (lector.HasRows)
                {
                    lector.Read();
                    unGerente = new Gerente((int)lector["cedula"], (string)lector["nomUsu"], (string)lector["pass"], (string)lector["nomCompleto"], (string)lector["Correo"]);
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
        #endregion
    }
}

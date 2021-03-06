﻿using System;
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

        //Altar un cajero
        public void AltaCajero(Cajero unCajero, Usuario usuLogueado)
        {
            Conexion con = new Conexion();
            SqlConnection cnn = new SqlConnection(con.cnnUsu(usuLogueado));

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
                    throw new Exception("La cedula ingresada pertenece a un cajero");
                if ((int)retorno.Value == -3 || (int)retorno.Value == -4)
                    throw new Exception("Error al actualizar usuario inactivo");
                if ((int)retorno.Value == -5)
                    throw new Exception("Error al crear el usuario de servidor");
                if ((int)retorno.Value == -6)
                    throw new Exception("Error al crear el usuario de Base de datos");
                if ((int)retorno.Value == -7)
                    throw new Exception("Error al asignar permisos");
                if ((int)retorno.Value == -8)
                    throw new Exception("Error al crear el usuario");

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

        //Dar de baja un cajero
        public void BajaCajero(Cajero unCajero, Usuario usuLogueado)
        {
            Conexion con = new Conexion();
            SqlConnection cnn = new SqlConnection(con.cnnUsu(usuLogueado));

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
                    throw new Exception("Error al eliminar horas extras del usuario");
                if ((int)retorno.Value == -4)
                    throw new Exception("Error al eliminar el usuario de acceso al servidor");
                if ((int)retorno.Value == -5)
                    throw new Exception("Error al eliminar el usuario de acceso a Base de datos ");
                if ((int)retorno.Value == -6)
                    throw new Exception("Error al dar de baja el usuario");

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

        //Modificar un cajero
        public void ModificarCajero(Cajero unCajero, Usuario usuLogueado)
        {
            Conexion con = new Conexion();
            SqlConnection cnn = new SqlConnection(con.cnnUsu(usuLogueado));

            SqlCommand cmd = new SqlCommand("ModificarCajero", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@cedula", unCajero.Cedula);
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
                    throw new Exception("Error al modificar usuario");
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

        //Cambiar contraseña
        public void CambioPass(Usuario unCajero, Usuario usuLogueado)
        {
            Conexion con = new Conexion();
            SqlConnection cnn = new SqlConnection(con.cnnUsu(usuLogueado));

            SqlCommand cmd = new SqlCommand("CambioPass", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@cedula", unCajero.Cedula);
            cmd.Parameters.AddWithValue("@nuevaPass", unCajero.Pass);
            cmd.Parameters.AddWithValue("@antiguaPass", usuLogueado.Pass);
            cmd.Parameters.AddWithValue("@nomUsu", unCajero.Pass);

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

        //Buscar cajero
        public Cajero BuscarCajero(int cedula,Usuario usuLogueado)
        {
            Conexion con = new Conexion();
            SqlConnection cnn = new SqlConnection(con.cnnUsu(usuLogueado));

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

        //Buscar cajero para el servicio de horas extras
        public Cajero BuscarCajeroServicioWin(int cedula)
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

        //Buscar cajero por más que esté desactivado
        public Cajero BuscarCajeroTodos(int cedula, Usuario usuLogueado)
        {
            Conexion con = new Conexion();
            SqlConnection cnn = new SqlConnection(con.cnnUsu(usuLogueado));

            Cajero unCajero = null;

            SqlCommand cmd = new SqlCommand("BuscarCajeroTodos", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@cedula", cedula);

            try
            {
                cnn.Open();
                SqlDataReader lector = cmd.ExecuteReader();
                if (lector.HasRows)
                {
                    lector.Read();
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

        //Login de cajero
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

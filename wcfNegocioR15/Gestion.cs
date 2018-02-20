using System;
using System.Collections.Generic;
using System.Data;
using MGestionR.app_code;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

namespace MGestionR
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Gestion : IGestion
    {

        public DataTable Datos;

        public EntUsuario usuariovalido(string pusr, string ppws)
        {
            AbdM lobjabd = new AbdM();
            string password = String.Empty;
            EntUsuario usuario = new EntUsuario();
            try
            {
                lobjabd.Consultar("EXEC paconlogin '" + pusr + "'");

                if (lobjabd.Datos.Rows.Count == 0)
                    throw new ApplicationException("El usuario no existe.");

                password = lobjabd.Datos.Rows[0]["pwd"].ToString().Trim();

                if (ppws != password)
                    throw new ApplicationException("La contraseña no es correcta.");

                usuario.codigo_error = 0;
                usuario.cadena_error = "";
                usuario.id_usuario = Convert.ToInt32(lobjabd.Datos.Rows[0][0]);
                usuario.usuario = lobjabd.Datos.Rows[0][1].ToString().Trim();
                usuario.contrasena = lobjabd.Datos.Rows[0][2].ToString().Trim();
                usuario.nombre = lobjabd.Datos.Rows[0][3].ToString().Trim().ToUpper();
                usuario.perfil = lobjabd.Datos.Rows[0][4].ToString().Trim();
                usuario.correo = lobjabd.Datos.Rows[0][5].ToString().Trim();
            }
            catch (ApplicationException aex)
            {
                usuario.codigo_error = -1;
                usuario.cadena_error = aex.Message.ToString();
            }
            catch (Exception ex)
            {
                int code = ex.HResult;
                usuario.codigo_error = code;
                usuario.cadena_error = ex.Message.ToString();
            }
            return usuario;
        }

        public EntCatalogos Acciones(string id_usuario)
        {
            AbdM lobjabd = new AbdM();
            bool lbolConexion = false;
            EntCatalogos Catalogos = new EntCatalogos();
            try
            {
                lobjabd.Consultar("EXEC PA_CON_ACCIONES " + id_usuario);

                if (lobjabd.Datos.Rows.Count == 0)
                    throw new ApplicationException("No existen acciones.");

                Catalogos.Catalogo = new Dictionary<int, string>();
                foreach (DataRow rw in lobjabd.Datos.Rows)
                    Catalogos.Catalogo.Add(Convert.ToInt32(rw[0]), rw[1].ToString().Trim().ToUpper());
                Catalogos.codigo_error = 0;
                Catalogos.cadena_error = "";

            }
            catch (ApplicationException aex)
            {
                Catalogos.codigo_error = -1;
                Catalogos.cadena_error = aex.Message.ToString();
            }
            catch (Exception ex)
            {
                int code = ex.HResult;
                Catalogos.codigo_error = code;
                Catalogos.cadena_error = ex.Message.ToString();
            }
            finally
            {
                if (lbolConexion)
                    lobjabd.Desconectar();
            }
            return Catalogos;
        }

        public EntRespuesta GuardaBd(string id_accion, string acciones, 
            string observaciones, string fecha, string latitud, string longitud, string foto)
        {

            EntRespuesta Respuesta = new EntRespuesta();
            AbdM lobjabd = new AbdM();
            SqlConnection con = new SqlConnection(lobjabd.GenerarCadena());
            SqlCommand cmd = new SqlCommand("pa_ins_accion", con);
            Dictionary<string, SqlDbType> paramsql = new Dictionary<string, SqlDbType>();
            
            try
            {

                byte[] lobjImagen = Convert.FromBase64String(foto);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_accion", SqlDbType.Int).Value = Convert.ToInt32(id_accion);
                cmd.Parameters.AddWithValue("@num_acciones", SqlDbType.Int).Value = Convert.ToInt64(acciones);
                cmd.Parameters.AddWithValue("@observaciones", SqlDbType.VarChar).Value = observaciones;
                cmd.Parameters.AddWithValue("@fecha_dm", SqlDbType.DateTime).Value = Convert.ToDateTime(fecha);
                cmd.Parameters.AddWithValue("@foto", SqlDbType.VarBinary).Value = lobjImagen;
                cmd.Parameters.AddWithValue("@latitud", SqlDbType.Decimal).Value = Convert.ToDecimal(latitud);
                cmd.Parameters.AddWithValue("@longitud", SqlDbType.Decimal).Value = Convert.ToDecimal(longitud);

                using (SqlDataAdapter DA = new SqlDataAdapter(cmd))
                {
                    using (DataSet DS = new DataSet())
                    {
                        DA.Fill(DS);
                        Respuesta.cadena_error = DS.Tables[0].Rows[0][0].ToString();
                    }
                }
                Respuesta.codigo_error = 0;
                
                
            }
            catch (ApplicationException aex)
            {
                Respuesta.codigo_error = -1;
                Respuesta.cadena_error = aex.Message.ToString();
            }
            catch (Exception ex)
            {
                int code = ex.HResult;
                Respuesta.codigo_error = code;
                Respuesta.cadena_error = ex.Message.ToString();
            }
            return Respuesta;
        }
    }
}

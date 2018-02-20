using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGestionR.app_code;
using System.Data.SqlClient;
using System.Web;
using System.Data;

namespace MGestionR
{
    public class GestionPost : IGestionPost
    {
        //public EntRespuesta GuardaBd(EntGuarda guarda)
        //{

        //    EntRespuesta Respuesta = new EntRespuesta();
        //    AbdM lobjabd = new AbdM();
        //    SqlConnection con = new SqlConnection(lobjabd.GenerarCadena());
        //    SqlCommand cmd = new SqlCommand("pa_ins_accion", con);
        //    Dictionary<string, SqlDbType> paramsql = new Dictionary<string, SqlDbType>();

        //    try
        //    {

        //        byte[] lobjImagen = Convert.FromBase64String(guarda.foto);

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@id_accion", SqlDbType.Int).Value = Convert.ToInt32(guarda.id_accion);
        //        cmd.Parameters.AddWithValue("@num_acciones", SqlDbType.Int).Value = Convert.ToInt64(guarda.acciones);
        //        cmd.Parameters.AddWithValue("@observaciones", SqlDbType.VarChar).Value = guarda.observaciones;
        //        cmd.Parameters.AddWithValue("@fecha_dm", SqlDbType.DateTime).Value = Convert.ToDateTime(guarda.fecha);
        //        cmd.Parameters.AddWithValue("@foto", SqlDbType.VarBinary).Value = lobjImagen;
        //        cmd.Parameters.AddWithValue("@latitud", SqlDbType.Decimal).Value = Convert.ToDecimal(guarda.latitud);
        //        cmd.Parameters.AddWithValue("@longitud", SqlDbType.Decimal).Value = Convert.ToDecimal(guarda.longitud);

        //        using (SqlDataAdapter DA = new SqlDataAdapter(cmd))
        //        {
        //            using (DataSet DS = new DataSet())
        //            {
        //                DA.Fill(DS);
        //                Respuesta.cadena_error = DS.Tables[0].Rows[0][0].ToString();
        //            }
        //        }
        //        Respuesta.codigo_error = 0;

        //    }
        //    catch (ApplicationException aex)
        //    {
        //        Respuesta.codigo_error = -1;
        //        Respuesta.cadena_error = aex.Message.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        int code = ex.HResult;
        //        Respuesta.codigo_error = code;
        //        Respuesta.cadena_error = ex.Message.ToString();
        //    }
        //    return Respuesta;
        //}


        public EntRespuesta GuardaBd2(string pid_accion, string pnum_acciones, string pobservaciones, string pfecha_dm, string platitud, string plongitud, byte[] pfoto)
        {

            EntRespuesta Respuesta = new EntRespuesta();
            AbdM lobjabd = new AbdM();
            SqlConnection con = new SqlConnection(lobjabd.GenerarCadena());
            SqlCommand cmd = new SqlCommand("pa_ins_accion", con);
            Dictionary<string, SqlDbType> paramsql = new Dictionary<string, SqlDbType>();

            try
            {

                byte[] lobjImagen = pfoto; //Convert.FromBase64String(pfoto);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_accion", SqlDbType.Int).Value = Convert.ToInt32(pid_accion);
                cmd.Parameters.AddWithValue("@num_acciones", SqlDbType.Int).Value = Convert.ToInt32(pnum_acciones);
                cmd.Parameters.AddWithValue("@observaciones", SqlDbType.VarChar).Value = pobservaciones;
                cmd.Parameters.AddWithValue("@fecha_dm", SqlDbType.DateTime).Value = Convert.ToDateTime(pfecha_dm);
                cmd.Parameters.AddWithValue("@foto", SqlDbType.VarBinary).Value = lobjImagen;
                cmd.Parameters.AddWithValue("@latitud", SqlDbType.Decimal).Value = Convert.ToDecimal(platitud);
                cmd.Parameters.AddWithValue("@longitud", SqlDbType.Decimal).Value = Convert.ToDecimal(plongitud);

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

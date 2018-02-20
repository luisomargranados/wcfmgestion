using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;


namespace MGestionR
{
    class AbdM : IDisposable
    {
        private bool disposing;
        private SqlConnection Conn;
        private SqlTransaction Tran;
        public DataTable Datos;
        public bool Conectar(string pstrCadenaConexion)
        {
            try
            {
                if (this.Conn == null)
                    this.Conn = new SqlConnection(pstrCadenaConexion);
                if (this.Conn.State != ConnectionState.Open)
                    this.Conn.Open();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Desconectar()
        {
            try
            {
                if (this.Conn != null)
                    this.Conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IniciarTransaccion()
        {
            try
            {
                if (this.Conn == null)
                    throw new ApplicationException("El objeto de coneccion esta vacio");
                if (this.Conn.State == ConnectionState.Closed || this.Conn.State == ConnectionState.Broken)
                    throw new ApplicationException("La coneccion con la BD esta rota o cerrada");
                this.Tran = this.Conn.BeginTransaction("Transaction");
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool TerminaTransaccion()
        {
            try
            {
                if (this.Conn == null)
                    throw new ApplicationException("El objeto de coneccion esta vacio");
                if (this.Conn.State == ConnectionState.Closed || this.Conn.State == ConnectionState.Broken)
                    throw new ApplicationException("La coneccion con la BD esta rota o cerrada");
                if (this.Tran == null)
                    throw new ApplicationException("El objeto de transaccion esta vacio");
                this.Tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ReversaTransaccion()
        {
            try
            {
                if (this.Conn == null)
                    throw new ApplicationException("El objeto de coneccion esta vacio");
                if (this.Conn.State == ConnectionState.Closed || this.Conn.State == ConnectionState.Broken)
                    throw new ApplicationException("La coneccion con la BD esta rota o cerrada");
                if (this.Tran == null)
                    throw new ApplicationException("El objeto de transaccion esta vacio");
                ((DbTransaction)this.Tran).Rollback();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int EjecutarNQ(string pstrCadena)
        {
            try
            {
                using (SqlCommand command = this.Conn.CreateCommand())
                {
                    command.Connection = this.Conn;
                    command.Transaction = this.Tran;
                    command.CommandText = pstrCadena;
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string EjecutarSC(string pstrCadena)
        {
            try
            {
                using (SqlCommand command = this.Conn.CreateCommand())
                {
                    command.Connection = this.Conn;
                    command.Transaction = this.Tran;
                    command.CommandText = pstrCadena;
                    return Convert.ToString(command.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet EjecutarDS(string pstrCadena)
        {
            try
            {
                if (this.Conn == null)
                    throw new ApplicationException("El objeto de coneccion esta vacio");
                if (this.Conn.State == ConnectionState.Closed || this.Conn.State == ConnectionState.Broken)
                    throw new ApplicationException("La coneccion con la BD esta rota o cerrada");
                using (SqlCommand command = this.Conn.CreateCommand())
                {
                    command.Connection = this.Conn;
                    command.Transaction = this.Tran;
                    command.CommandText = pstrCadena;
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                    {
                        sqlDataAdapter.SelectCommand = command;
                        using (DataSet dataSet = new DataSet(pstrCadena))
                        {
                            ((DataAdapter)sqlDataAdapter).Fill(dataSet);
                            return dataSet;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet EjecutarSP(Dictionary<string, object> pobjParametros)
        {
            try
            {
                if (this.Conn == null)
                    throw new ApplicationException("El objeto de coneccion esta vacio");
                if (this.Conn.State == ConnectionState.Closed || this.Conn.State == ConnectionState.Broken)
                    throw new ApplicationException("La coneccion con la BD esta rota o cerrada");
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(this.GeneraCommand(pobjParametros)))
                {
                    using (DataSet dataSet = new DataSet())
                    {
                        ((DataAdapter)sqlDataAdapter).Fill(dataSet);
                        return dataSet;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet EjecutarCompracion(SqlCommand pobjCommand)
        {
            try
            {
                if (this.Conn == null)
                    throw new ApplicationException("El objeto de coneccion esta vacio");
                if (this.Conn.State == ConnectionState.Closed || this.Conn.State == ConnectionState.Broken)
                    throw new ApplicationException("La coneccion con la BD esta rota o cerrada");
                pobjCommand.Connection = this.Conn;
                pobjCommand.CommandType = CommandType.StoredProcedure;

                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(pobjCommand))
                {
                    using (DataSet dataSet = new DataSet())
                    {
                        ((DataAdapter)sqlDataAdapter).Fill(dataSet);
                        return dataSet;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet EjecutarSPSQL(Dictionary<string, object> pobjParametros)
        {
            try
            {
                if (this.Conn == null)
                    throw new ApplicationException("El objeto de coneccion esta vacio");
                if (this.Conn.State == ConnectionState.Closed || this.Conn.State == ConnectionState.Broken)
                    throw new ApplicationException("La coneccion con la BD esta rota o cerrada");
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(this.GeneraSQlCommand(pobjParametros)))
                {
                    using (DataSet dataSet = new DataSet())
                    {
                        ((DataAdapter)sqlDataAdapter).Fill(dataSet);
                        return dataSet;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private SqlCommand GeneraCommand(Dictionary<string, object> pobjParametros)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand((string)pobjParametros["stored"], this.Conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Transaction = this.Tran;
                Dictionary<string, DbType> dictionary = (Dictionary<string, DbType>)pobjParametros["Parameters"];
                foreach (KeyValuePair<string, DbType> keyValuePair in dictionary)
                {
                    sqlCommand.Parameters.AddWithValue(keyValuePair.Key.Split('|')[0], (object)keyValuePair.Value);
                    sqlCommand.Parameters[keyValuePair.Key.Split('|')[0]].Value = (object)keyValuePair.Key.Split('|')[1];
                }
                if (pobjParametros.ContainsKey("timeout"))
                    sqlCommand.CommandTimeout = (int)pobjParametros["timeout"];
                dictionary.Clear();
                return sqlCommand;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private SqlCommand GeneraSQlCommand(Dictionary<string, object> pobjParametros)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand((string)pobjParametros["stored"], this.Conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Transaction = this.Tran;
                SqlParameterCollection Parameters = (SqlParameterCollection)pobjParametros["Parameters"];
                foreach (KeyValuePair<string, SqlDbType> keyValuePair in Parameters)
                {
                    sqlCommand.Parameters.AddWithValue(keyValuePair.Key.Split('|')[0], (object)keyValuePair.Value);
                    sqlCommand.Parameters[keyValuePair.Key.Split('|')[0]].Value = (object)keyValuePair.Key.Split('|')[1];
                }
                if (pobjParametros.ContainsKey("timeout"))
                    sqlCommand.CommandTimeout = (int)pobjParametros["timeout"];

                return sqlCommand;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GenerarCadena()
        {
            string CadenaConeccion = string.Empty;

            try
            {
                CadenaConeccion = ConfigurationManager.ConnectionStrings["cnnsql"].ConnectionString;
                if (string.IsNullOrEmpty(CadenaConeccion))
                {
                    throw new ApplicationException("No existe la cadena de conexion en el archivo de configuración");
                }
                return CadenaConeccion;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IDataReader EjecutarDR(string pstrCadena)
        {
            try
            {
                using (SqlCommand command = this.Conn.CreateCommand())
                {
                    command.Connection = this.Conn;
                    command.Transaction = this.Tran;
                    command.CommandText = pstrCadena;
                    return (IDataReader)command.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize((object)this);
        }

        protected virtual void Dispose(bool b)
        {
            if (this.disposing)
                return;
            this.disposing = true;
            if (this.Conn != null)
            {
                this.Conn.Dispose();
                this.Conn = (SqlConnection)null;
            }
            if (this.Tran == null)
                return;
            this.Tran.Dispose();
            this.Tran = (SqlTransaction)null;
        }

        public bool Consultar(string pstrsentencia)
        {
            bool lbolmetodo = true;
            bool lbolconectado = true;
            AbdM Abd = new AbdM();
            try
            {
                lbolconectado = Abd.Conectar(GenerarCadena());
                Datos = Abd.EjecutarDS(pstrsentencia).Tables[0];
                return lbolmetodo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (lbolconectado)
                {
                    Abd.Desconectar();
                }
                Abd.Dispose();
            }
        }
    }
}

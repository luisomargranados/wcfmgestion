using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGestionR
{
    [DataContract]
    public class EntUsuario : IDisposable
    {
        private bool disposing;

        [DataMember]
        public int codigo_error
        {
            get;
            set;
        }

        [DataMember]
        public string cadena_error
        {
            get;
            set;
        }

        [DataMember]
        public int id_usuario
        {
            get;
            set;
        }

        [DataMember]
        public string usuario
        {
            get;
            set;
        }

        [DataMember]
        public string contrasena
        {
            get;
            set;
        }

        [DataMember]
        public string nombre
        {
            get;
            set;
        }

        [DataMember]
        public string correo
        {
            get;
            set;
        }

        [DataMember]
        public string perfil
        {
            get;
            set;
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool b)
        {
            if (!disposing)
            {
                disposing = true;
            }
        }
    }
}
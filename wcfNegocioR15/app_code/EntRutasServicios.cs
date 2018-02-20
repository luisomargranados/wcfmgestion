using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGestionR.app_code
{
    public class EntRutasServicios : IDisposable
    {
        private bool disposing;

        [DataMember]
        public int id_ruta_catalogo
        {
            get;
            set;
        }

        [DataMember]
        public int idrutas_servicios
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
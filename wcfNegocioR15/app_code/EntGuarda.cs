using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGestionR.app_code
{
    public class EntGuarda : IDisposable
    {
        private bool disposing;
        [DataMember]
        public string id_accion { get; set; }

        [DataMember]
        public string acciones { get; set; }

        [DataMember]
        public string observaciones { get; set; }

        [DataMember]
        public string fecha { get; set; }

        [DataMember]
        public string latitud { get; set; }

        [DataMember]
        public string longitud { get; set; }

        [DataMember]
        public string foto { get; set; }

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
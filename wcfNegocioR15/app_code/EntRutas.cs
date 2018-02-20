﻿using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGestionR.app_code
{
    public class EntRutas : IDisposable
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
        public List<EntRutasServicios> RutasServicios
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
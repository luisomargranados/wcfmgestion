using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using MGestionR.app_code;

namespace MGestionR
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGestionPost" in both code and config file together.
    [ServiceContract]
    public interface IGestionPost
    {
        //[OperationContract]
        //EntRespuesta GuardaBd(EntGuarda guarda);

        [OperationContract]
        //[WebInvoke(Method = "POST",
        //            ResponseFormat = WebMessageFormat.Json,
        //            RequestFormat = WebMessageFormat.Json,
        //            BodyStyle = WebMessageBodyStyle.Wrapped,
        //            UriTemplate = "GuardaBd2?id_accion={pid_accion}&num_acciones={pnum_acciones}&observaciones={pobservaciones}&fecha_dm={pfecha_dm}&latitud={platitud}&longitud={plongitud}&foto={pfoto}")]
        EntRespuesta GuardaBd2(string pid_accion, string pnum_acciones, string pobservaciones, string pfecha_dm, string platitud, string plongitud, byte[] pfoto);
    }
}

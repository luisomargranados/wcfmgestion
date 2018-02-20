using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using MGestionR.app_code;

namespace MGestionR
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IGestion
    {

        [OperationContract]
        [WebInvoke(Method = "GET",
                    ResponseFormat = WebMessageFormat.Json,
                    BodyStyle = WebMessageBodyStyle.Wrapped,
                    UriTemplate = "usuariovalido?usr={pusr}&pws={ppws}")]
        EntUsuario usuariovalido(string pusr, string ppws);

        [OperationContract]
        [WebInvoke(Method = "GET",
                    ResponseFormat = WebMessageFormat.Json,
                    BodyStyle = WebMessageBodyStyle.Wrapped,
                    UriTemplate = "acciones?id={id_usuario}")]
        EntCatalogos Acciones(string id_usuario);

        [OperationContract]
        [WebInvoke(Method = "GET",
                    ResponseFormat = WebMessageFormat.Json,
                    BodyStyle = WebMessageBodyStyle.Wrapped,
                    UriTemplate = "guardabd?id={id_accion}&ac={acciones}&obs={observaciones}&fec={fecha}&lat={latitud}&lon={longitud}&foto={foto}")]
        EntRespuesta GuardaBd(string id_accion, string acciones, string observaciones, 
            string fecha, string latitud, string longitud, string foto);

    }



}

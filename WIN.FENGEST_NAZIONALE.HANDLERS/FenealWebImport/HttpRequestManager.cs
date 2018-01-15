using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealwebImportExport;


namespace WIN.FENGEST_NAZIONALE.HANDLERS.FenealWebImport
{
    public class HttpRequestManager
    {
       // private String url = "http://localhost:8080/importquote/";
        public void SendQuotetofenealweb(RiepilogoQuotaDTO dto, string url)
        {



            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.CookieContainer = new CookieContainer();
            //imposto il timeout a sei minuti
            httpWebRequest.Timeout = 360000;

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var json =  JsonConvert.SerializeObject(dto, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    //DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc
            });

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }


        public void NotiFyFenealWebOfNewExport(string url)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
           
            httpWebRequest.Method = "GET";
            httpWebRequest.CookieContainer = new CookieContainer();
            httpWebRequest.Timeout = 360000;
            
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }
    }
}

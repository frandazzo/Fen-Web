using System;
using System.Collections.Generic;
using System.Text;
using System.Messaging;
using WIN.TECHNICAL.MIDDLEWARE.QueueHandlers;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;
using System.Xml;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.ImportHandler
{
    public class TraceFromQueueRetriever: MessageReceiver
    {

        public TraceFromQueueRetriever(MessageQueue queue)
            : base(queue, true)
        {

        }

        protected override void ProcessMessage()
        {
            Message message = this.gotMessage;

            ExportTrace t;
            LiberiTrace t1 = null;

            try
            {
                //il serializzatore nel caso non trovi un oggetto consono
                //lancia una eccezione xml

                Log("Tento il recupero del messaggio come traccia IQA");
                t = message.Body as ExportTrace;



                if (t == null)
                {
                    Log("Recupero del messaggio come traccia IQA non andato a buon fine");
                    Log("Tento il recupero del messaggio come traccia Liberi");
                    t1 = message.Body as LiberiTrace;
                    Log("Recupero del messaggio come traccia Liberi: OK");
                }
                else
                {
                    Log("Recupero del messaggio come traccia IQA: OK");
                }


                    
            }
            catch (Exception)
            {
                Log("Recupero del messaggio come traccia LIBERI non andato a buon fine");
                t = null;
            } 
            

            if (t == null && t1 == null)
                throw new Exception("Oggetto sconosciuto impossibile da deserializzare");
        }

        protected override void SetFormatter()
        {
            this.incomingQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(ExportTrace), typeof(LiberiTrace) });
        }


        public ExportTrace Trace
        {
            get
            {
                try
                {
                    if (this.gotMessage != null)
                        return this.gotMessage.Body as ExportTrace;
                    return null;
                }
                catch (XmlException)
                {
                    return null;
                } 
            }
        }

        public LiberiTrace LiberiTrace
        {
            get
            {
                try
                {
                    if (this.gotMessage != null)
                        return this.gotMessage.Body as LiberiTrace;
                    return null;
                }
                catch (XmlException)
                {
                    return null;
                }
            }
        }
    }
}

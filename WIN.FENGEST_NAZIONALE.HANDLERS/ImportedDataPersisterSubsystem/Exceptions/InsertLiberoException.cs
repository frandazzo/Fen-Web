using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.FENGEST_NAZIONALE.DOMAIN.Workers;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.ImportedDataPersisterSubsystem.Exceptions
{
    public class InsertLiberoException: Exception
    {
        private Libero _worker;
        private ExceptionType _type;

        public InsertLiberoException(Libero worker, ExceptionType type, Exception innerException)
            : base("", innerException)
        {
            _worker = worker;
            _type = type;

        }


        public override string Message
        {
            get
            {
                string op;
                if (_type == ExceptionType.InsertElement)
                    op = " CREAZIONE ";
                op = " MODIFICA-AGGIORNAMENTO ";

                string worker = "";
                string province = "";

                worker = _worker.CompleteName;

              
                province = _worker.Provincia.Descrizione;

                string baseMessage = "Errore nella" + op + "del lavoratore " + worker + " nella provincia di " + province;

                StringBuilder b = new StringBuilder();
                b.AppendLine(baseMessage);

                Exception inner = base.InnerException;

                b.AppendLine("ECCEZIONE BASE: " + inner.Message);

                if (inner.InnerException != null)
                {
                    string err1 = inner.InnerException.Message;
                    b.AppendLine("ECCEZIONE INTERNA: " + err1);
                }

                return b.ToString();
            }
        }
    }
}

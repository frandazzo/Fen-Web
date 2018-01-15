using System;
using System.Collections.Generic;
using System.Text;
using WIN.TECHNICAL.PERSISTENCE;
using WIN.FENGEST_NAZIONALE.DOMAIN.Workers;
using WIN.BASEREUSE;
using System.Collections;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.DeleteImportDataHandler
{
    public class ImportRemover
    {
        public static void RemoveImportData(IPersistenceFacade f, Export export)
        {
            //ICommand docRemover = new DocumentRemoveCommand(f, export.Id);
            //ICommand iscRemover = new SubscriptionRemoveCommand(f, export.Id);
            //ICommand expRemover = new ExportRemoveCommand(f, export.Id);


            string docRemoverString = string.Format("Delete from documenti where Id_Importazione = {0}", export.Id);
            string iscRemoverString = string.Format("Delete from iscrizioni where Id_Importazione =  {0}", export.Id);
            string expRemoverString = string.Format("Delete from importazioni where id = {0}", export.Id);

            IList<string> queries = new List<string>();

            queries.Add(docRemoverString);
            queries.Add(iscRemoverString);
            queries.Add(expRemoverString);


            //IList l = new ArrayList();
            ////ordino i comandi da effettuare rispenttando i vincoli di integrità referenziale.
            //l.Add(docRemover);
            //l.Add(iscRemover);
            //l.Add(expRemover);
            //eseguo
            f.ExecuteQueryList(queries);
            //f.BeginTransaction();
            ////f.ExecuteCommandList(l);
            //f.CommitTransaction();
        }
    }
}

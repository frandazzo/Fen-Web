using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using WIN.FENGEST_NAZIONALE.HANDLERS.Financial.HtmlRenderer;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;

namespace FenealgestWEB.WebServices.DTOWrappers
{
    public class HtmlTransformer
    {

        


        public string TransformToHtml(QueryResultDTO dto)
        {
            string result = "";

            if (!dto.IsResultValid)
                return dto.Error;

            //action questo punto posso creare la tabella che conterrà i risultati

            int numberOfElements = GetNumberOf(dto.Workers);



            if (numberOfElements == 0)
                return "Nessun risultato trovato";


            StringBuilder b = new StringBuilder();
            //inserisco l'intestazione
            b.Append(TabellaHtmlRenderer.CreaRigaConListaColonne(CreaIntestazione()));
            //aggiungo i contenuti
            int i = 0;
            foreach (WorkerDTO item in dto.Workers)
            {
                if (!string.IsNullOrEmpty(item.Surname))
                {
                    i++;
                    b.Append(TabellaHtmlRenderer.CreaRigaConListaColonne(CreaContenuti(item, i)));
                }
            }

            result = TabellaHtmlRenderer.CreaTabella(b.ToString());

            

            return result;
        }

        private int GetNumberOf(WorkerDTO[] workerDTOs)
        {
            int i = 0;

            foreach (WorkerDTO item in workerDTOs)
            {
                if (!string.IsNullOrEmpty(item.Surname))
                    i++;
            }

            return i;
        }

        private string[] CreaContenuti(WorkerDTO item, int index)
        {
            string lav = CreaContenutiLavoratore(item);
            string subs = CreaContenutiIscrizioni(item.Subscriptions);

            return new string[] { index.ToString() , lav, subs };
        }

        private string CreaContenutiIscrizioni(SubscriptionDTO[] subscriptionDTOs)
        {
            if ((subscriptionDTOs.Length == 0))
                return "Nessuna iscrizione trovata;";

            StringBuilder b = new StringBuilder();

            foreach (SubscriptionDTO item in subscriptionDTOs)
            {
                b.Append(string.Format("{0} <br/>", CreateSubstriptionInfo(item)));
            }

            return b.ToString();
        }

        private string CreateSubstriptionInfo(SubscriptionDTO item)
        {
            StringBuilder b = new StringBuilder();

            string provincia = "<b> Prov.: </b>{0};";
            string sem = "<b> Sem.: </b>{1}";
            string set = "<b> Settore.: </b>{2}";
            string ente = "<b>Ente: </b>{3};";
            string az = string.IsNullOrEmpty(item.EmployCompany) ? "{4}" : "<b>Azienda: </b>{4};";
            string iva = string.IsNullOrEmpty(item.FiscalCode) ? "{5}" : "<b>P. iva: </b>{5};";
            string liv = string.IsNullOrEmpty(item.Level) ? "{6}": "<b>Liv.: </b>{6};";
            string quo = (item.Quota > 0.01) ? "<b>Quota: </b>{7};" : "{7}";
            string cont = string.IsNullOrEmpty(item.Contract) ? "{8}":"<b>Contratto: </b>{8};" ;

            string semester = String.Format("{0}-{1}", item.Semester, item.Year);

            string desc = provincia + sem + set + ente + az + iva + liv + quo + cont;

            b.Append(string.Format(desc, 
                item.Province,
                semester,
                item.Sector,
                item.Entity,
                item.EmployCompany,
                item.FiscalCode,
                item.Level,
                item.Quota,
                item.Contract
            ));

            return b.ToString();
        }

        private string CreaContenutiLavoratore(WorkerDTO item)
        {
            string result = "";

            StringBuilder b = new StringBuilder();

            b.Append(string.Format("<b>Cognome: </b> {0} <br/>", item.Surname));
            b.Append(string.Format("<b>Nome: </b> {0} <br/>", item.Name));
            b.Append(string.Format("<b>Fiscale: </b> {0} <br/>", item.Fiscalcode));
            b.Append(string.Format("<b>Data nascita: </b> {0} <br/>", item.BirthDate.ToShortDateString()));
            b.Append(string.Format("<b>Nazione: </b> {0} <br/>", item.Nationality));
            b.Append(string.Format("<b>Comune nascita: </b> {0} <br/>", item.BirthPlace));
            b.Append(string.Format("<b>Comune residenza: </b> {0} <br/>", item.LivingPlace));
            b.Append(string.Format("<b>Indirizzo: </b> {0} <br/>", item.Address));
            b.Append(string.Format("<b>Cap: </b> {0} <br/>", item.Cap));
            b.Append(string.Format("<b>Telefono: </b> {0} <br/>", item.Phone));

            result = b.ToString();

            return result;
        }

        private string[] CreaIntestazione()
        {
            string numero = "<b>Numero</b>";
            string lav = "<b>Lavoratore</b>";
            string sub = "<b>Iscrizioni</b>";

            return new string[] { numero ,lav, sub};
        }

    }
}
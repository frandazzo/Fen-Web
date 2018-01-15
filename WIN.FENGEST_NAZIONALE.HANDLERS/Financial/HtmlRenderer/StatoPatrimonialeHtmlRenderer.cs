using System;
using System.Collections.Generic;
using System.Text;
using BilancioFenealgest.DomainLayer;
using WIN.BILANCIO.DomainLayer;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.Financial.HtmlRenderer
{
    public class StatoPatrimonialeHtmlRenderer
    {
        private StatoPatrimoniale _statoPatrimoniale;

        public string Polizze { get; private set; }
        public string Immobili { get; private set; }
        public string Auto { get; private set; }
        public string Depositi { get; private set; }

        public string Mobili { get; private set; }
        public string Accantonamenti { get; private set; }
        public string Chiusure { get; private set; }






        public StatoPatrimonialeHtmlRenderer(StatoPatrimoniale sp)
        {
            _statoPatrimoniale = sp;
            Polizze = "";
            Immobili = "";
            Auto = "";
            Depositi = "";


            Mobili = "";
            Accantonamenti = "";
            Chiusure = "";
        }

        public void Render()
        {
            if (_statoPatrimoniale == null)
                return;

            RenderAuto();
            RenderImmobili();
            RenderDepositi();
            RenderPolizze();



            RenderMobili();
            RenderAccantonamenti();
            RenderChiusure();
        }

        private void RenderChiusure()
        {
            if (_statoPatrimoniale.Chiusure.Count == 0)
                return;

            CreaTabellaChiusure();
        }

        private void CreaTabellaChiusure()
        {
            StringBuilder b = new StringBuilder();
            //inserisco l'intestazione
            b.Append(TabellaHtmlRenderer.CreaRigaConListaColonne(CreaIntestazioneChiusure()));
            //aggiungo i contenuti
            foreach (Chiusura item in _statoPatrimoniale.Chiusure)
            {
                b.Append(TabellaHtmlRenderer.CreaRigaConListaColonne(CreaContenutiChiusure(item)));
            }

            Chiusure = TabellaHtmlRenderer.CreaTabella(b.ToString());
        }

        private string[] CreaContenutiChiusure(Chiusura item)
        {
            string Anno = item.Anno;
            string Avanzo = item.Avanzo;
            string Disavanzo = item.Disavanzo;
            return new string[] { Anno, Avanzo, Disavanzo };
        }

        private string[] CreaIntestazioneChiusure()
        {
            string primacolonna = "<b> Anno </b>";
            string secondacolonna = "<b> Avanzo </b>";
            string terzacolonna = "<b> Disavanzo </b>";

            return new string[] { primacolonna, secondacolonna, terzacolonna };
        }

        private void RenderAccantonamenti()
        {
            if (_statoPatrimoniale.AccantonamentiTFR.Count == 0)
                return;

            CreaTabellaAccantonamenti();
        }

        private void CreaTabellaAccantonamenti()
        {
            StringBuilder b = new StringBuilder();
            //inserisco l'intestazione
            b.Append(TabellaHtmlRenderer.CreaRigaConListaColonne(CreaIntestazioneAccantonamenti()));
            //aggiungo i contenuti
            foreach (AccantonamentoTFR item in _statoPatrimoniale.AccantonamentiTFR)
            {
                b.Append(TabellaHtmlRenderer.CreaRigaConListaColonne(CreaContenutiAccantonamenti(item)));
            }

            Accantonamenti = TabellaHtmlRenderer.CreaTabella(b.ToString());
        }

        private string[] CreaContenutiAccantonamenti(AccantonamentoTFR item)
        {
            string Anno = item.Anno;
            string Importo = item.Importo;
            string Note = item.Note;
            return new string[] { Anno, Importo, Note };
        }

        private string[] CreaIntestazioneAccantonamenti()
        {
            string primacolonna = "<b> Anno </b>";
            string secondacolonna = "<b> Importo </b>";
            string terzacolonna = "<b> Note </b>";

            return new string[] { primacolonna, secondacolonna, terzacolonna };
        }

        private void RenderMobili()
        {
            if (_statoPatrimoniale.Mobili.Count == 0)
                return;

            CreaTabellaMobili();
        }

        private void CreaTabellaMobili()
        {
            StringBuilder b = new StringBuilder();
            //inserisco l'intestazione
            b.Append(TabellaHtmlRenderer.CreaRigaConListaColonne(CreaIntestazioneMobili()));
            //aggiungo i contenuti
            foreach (Mobile item in _statoPatrimoniale.Mobili)
            {
                b.Append(TabellaHtmlRenderer.CreaRigaConListaColonne(CreaContenutiMobile(item)));
            }

            Mobili = TabellaHtmlRenderer.CreaTabella(b.ToString());
        }

        private string[] CreaContenutiMobile(Mobile item)
        {
            string Numero = item.Numero;
            string Descrizione = item.Descrizione;
            return new string[] { Numero, Descrizione };
        }


        private void RenderAuto()
        {
            if (_statoPatrimoniale.Autos.Count == 0)
                return;

            CreaTabellaAuto();

        }

        private void RenderDepositi()
        {
            if (_statoPatrimoniale.Depositi.Count == 0)
                return;


            CreaTabellaDepositi();

        }


        private void RenderImmobili()
        {
            if (_statoPatrimoniale.Immobili.Count == 0)
                return;

            CreaTabellaImmobili();
        }
        private void RenderPolizze()
        {
            if (_statoPatrimoniale.Polizze.Count == 0)
                return;

       
            CreaTabellaPolizze();
         
        }


        private void CreaTabellaAuto()
        {
            StringBuilder b = new StringBuilder();
            //inserisco l'intestazione
            b.Append(TabellaHtmlRenderer.CreaRigaConListaColonne(CreaIntestazioneAuto()));
            //aggiungo i contenuti
            foreach (Auto item in _statoPatrimoniale.Autos)
            {
                b.Append(TabellaHtmlRenderer.CreaRigaConListaColonne( CreaContenutiAuto(item)));
            }

            Auto = TabellaHtmlRenderer.CreaTabella(b.ToString());

        }

        private  string[] CreaContenutiAuto(Auto item)
        {
            string tipoMezzo = item.TipoMezzo;
            string intestazione = item.Intestazione;
            return new string[] { tipoMezzo, intestazione };
        }

        private string[] CreaIntestazioneAuto()
        {

            string primacolonna = "<b> Tipo mezzo </b>";
            string secondacolonna = "<b> Intestazione </b>";

            return new string[] { primacolonna, secondacolonna };
            
        }

        private string[] CreaIntestazioneMobili()
        {

            string primacolonna = "<b> Numero </b>";
            string secondacolonna = "<b> Descrizione </b>";

            return new string[] { primacolonna, secondacolonna };

        }



        private void CreaTabellaDepositi()
        {
            StringBuilder b = new StringBuilder();
            //inserisco l'intestazione
            b.Append(TabellaHtmlRenderer.CreaRigaConListaColonne(CreaIntestazioneDepositi()));
            //aggiungo i contenuti
            foreach (Deposito item in _statoPatrimoniale.Depositi)
            {
                b.Append(TabellaHtmlRenderer.CreaRigaConListaColonne(CreaContenutiDepositi(item)));
            }

            Depositi = TabellaHtmlRenderer.CreaTabella(b.ToString());
        }

        private  string[] CreaContenutiDepositi(Deposito item)
        {
            string banca = item.Denominazione;
            string conto = item.ContoCorrente;
            string intestazione = item.IntestazioneConto;
            string tipo = item.Tipo;
            string firmaAmm = item.IsFirmaAmministrativoDepositata.ToString();
            string firmaResp = item.IsFirmaResponsabileDepositata.ToString();
            return new string[] { banca, conto, intestazione, tipo, firmaAmm, firmaResp };
        }

        private string[] CreaIntestazioneDepositi()
        {

           

            string banca = "<b> Banca o società </b>";
            string conto = "<b> conto corrente </b>";
            string intestazione = "<b> Intestazione </b>";
            string tipo = "<b> Tipo conto </b>";
            string firmaAmm = "<b> Firma amm. </b>";
            string firmaResp = "<b> Firma resp. </b>";
            return new string[] { banca, conto, intestazione, tipo, firmaAmm, firmaResp };

        }


        private void CreaTabellaPolizze()
        {
            StringBuilder b = new StringBuilder();
            //inserisco l'intestazione
            b.Append(TabellaHtmlRenderer.CreaRigaConListaColonne(CreaIntestazionePolizze()));
            //aggiungo i contenuti
            foreach (Polizza item in _statoPatrimoniale.Polizze)
            {
                b.Append(TabellaHtmlRenderer.CreaRigaConListaColonne(CreaContenutiPolizze(item)));
            }

            Polizze = TabellaHtmlRenderer.CreaTabella(b.ToString());
        }

        private  string[] CreaContenutiPolizze(Polizza item)
        {
            string denominazione = item.DenominazioneCompagnia;
            string tipo = item.Tipo;
            string numPol = item.NumeroPolizza;
            string denopmSoc = item.DenominazioneSocieta;

            return new string[] { denominazione, tipo, numPol, denopmSoc };
        }

        private string[] CreaIntestazionePolizze()
        {



            string denominazione = "<b> Denominazione compagnìa </b>";
            string tipo = "<b> Tipo </b>";
            string numPol = "<b> Num. Polizza </b>";
            string denopmSoc = "<b> Denominazione società </b>";

            return new string[] { denominazione, tipo, numPol, denopmSoc };

        }




        private void CreaTabellaImmobili()
        {
            StringBuilder b = new StringBuilder();
            //inserisco l'intestazione
            b.Append(TabellaHtmlRenderer.CreaRigaConListaColonne(CreaIntestazioneImmobili()));
            //aggiungo i contenuti
            foreach (Immobile item in _statoPatrimoniale.Immobili)
            {
                b.Append(TabellaHtmlRenderer.CreaRigaConListaColonne(CreaContenutiImmobili(item)));
            }

            Immobili = TabellaHtmlRenderer.CreaTabella(b.ToString());
        }

        private  string[] CreaContenutiImmobili(Immobile item)
        {
            string ubicazione = item.Ubicazione;
            string via = item.Via ;
            string numciv = item.NumCivico ;
            string patita = item.PartitaCatastale ;
            string data = "";
            if (item.DataAcquisto != DateTime.MinValue)
                data = item.DataAcquisto.ToShortDateString();
            string intestazione = item.IntestazioneProprieta;
            string utilizzo = item.UtilizzataDa;
            return new string[] { ubicazione, via, numciv, patita, data, intestazione, utilizzo };

        }

        private string[] CreaIntestazioneImmobili()
        {



            string ubicazione = "<b> Ubicazione </b>";
            string via = "<b> Via </b>";
            string numciv = "<b> Num. civ. </b>";
            string patita = "<b> Part. catastale </b>";
            string data = "<b> Data acquisto </b>";
            string intestazione = "<b> Intestazione </b>";
            string utilizzo = "<b> Utilizzata da </b>";

            return new string[] { ubicazione, via, numciv, patita, data, intestazione, utilizzo };

        }



    }
}

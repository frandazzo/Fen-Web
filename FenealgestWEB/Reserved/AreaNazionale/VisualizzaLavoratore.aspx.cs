using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using FenealgestWEB.Utility;
using WIN.FENGEST_NAZIONALE.DOMAIN.Workers;
using WIN.TECHNICAL.PERSISTENCE;
using WIN.BASEREUSE;
using WIN.FENGEST_NAZIONALE.HANDLERS;
using WIN.FENGEST_NAZIONALE.DOMAIN.Security;

namespace FenealgestWEB.Reserved.AreaNazionale
{
    public partial class VisualizzaLavoratore : BaseReservedArea
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //esegue la verifica che un utetne è loggato;
            //inizializza le info base della pagina insieme con 
            //l'inizializzaizone del menu per l'utente
            InitializeBasePage();

            ////se è una pagina che richiede una autorizzazione
            ////posso decommentare il codice che segue
            if (!IsPostBack)
            {
                CheckUserIsAuthorized();
            }

            //inserire qui altro codice di inizializzaizone della pagina
            InitializePage();
        }

        protected override string AuthorizedFunction
        {
            //Quale devo inserire delle due? potrei provenire da entrambe
            get
            {
                if (Request.QueryString["isnat"].Equals("True"))
                    return "Visualizza storico lavoratore liv nazionale";
                else
                    return "Visualizza storico lavoratore liv regionale";
            }
        }

        protected override string SitePagePath
        {
            get { return "Home -> Area riservata -> Visualizza lavoratore"; }
        }

        //protected override Menu PageMenu
        //{
        //    get { return ((ReservedArea)this.Master).MainMenu; }
        //}


        protected override DevExpress.Web.ASPxMenu.ASPxMenu DevPageMenu
        {
            get { return ((FenealgestwebNew)this.Master).MainMenu; }
        }

        protected override DevExpress.Web.ASPxTreeView.ASPxTreeView DevPageTree
        {
            get { return null; }
        }

        protected override void LoadPageInfo()
        {
            //string tempSubstring;
            ////imposto tutte le info necessarie alla creazione di tutte le informazioni della pagina
            //Label lbl = ((ReservedArea)this.Master).UtenteLabelPro;

            //tempSubstring = base.CurrentUser;
            //if (!tempSubstring.Equals("") || tempSubstring != null)
            //    if (tempSubstring.Length > 20)
            //        tempSubstring = tempSubstring.Substring(0, 19) + "...";
            //lbl.Text = tempSubstring;


            //Label lbl1 = ((ReservedArea)this.Master).StrutturaLabelPro;
            //lbl1.Text = base.UserStructure;

            //Label lbl2 = ((ReservedArea)this.Master).HomeLabelPro;

            //tempSubstring = SitePagePath;
            //if (!tempSubstring.Equals("") || tempSubstring != null)
            //    if (tempSubstring.Length > 20)
            //        tempSubstring = tempSubstring.Substring(0, 19) + "...";
            //lbl2.Text = tempSubstring;

            //Label lbl3 = ((ReservedArea)this.Master).DataLabelPro;
            //lbl3.Text = DateTime.Now.ToShortDateString();

            //Label lbl4 = ((ReservedArea)this.Master).OraLabelPro;
            //lbl4.Text = DateTime.Now.ToShortTimeString();

            //Label lbl5 = ((ReservedArea)this.Master).ScadenzaLabelPro;
            //lbl5.Text = base.UserDecayDataPassword.ToShortDateString();

            //Label lbl6 = ((ReservedArea)this.Master).RegioneLabelPro;

            //tempSubstring = base.UserRegion;
            //if (!tempSubstring.Equals("") || tempSubstring != null)
            //    if (tempSubstring.Length > 15)
            //        tempSubstring = tempSubstring.Substring(0, 14) + "...";
            //lbl6.Text = tempSubstring;

            //Label lbl7 = ((ReservedArea)this.Master).ProvinciaLabelPro;

            //tempSubstring = base.UserProvice;
            //if (!tempSubstring.Equals("") || tempSubstring != null)
            //    if (tempSubstring.Length > 15)
            //        tempSubstring = tempSubstring.Substring(0, 14) + "...";
            //lbl7.Text = tempSubstring;


            //LoadContextImage();
        }

        protected override string FunctionHeaderTitle
        {
            get { return "Visualizza lavoratore"; }
        }

        protected override void InitializePage()
        {
            //Page.Title = FunctionHeaderTitle;
            if (!this.IsPostBack)
            {
                //lblTitolo.Text = FunctionHeaderTitle;
                InitializeGrid();

                if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
                    LoadWorker(Request.QueryString["Id"], Request.QueryString["isnat"]);
            }
        }

        private void InitializeGrid()
        {
            grdIscrizioni.Columns.Clear();

            //Aggiungiamo le colonne che ci interessano per le iscrizioni
            BoundField ProvinciaDoc = new BoundField();
            ProvinciaDoc.DataField = ("Province");
            ProvinciaDoc.HeaderText = "Provincia";
            //ProvinciaDoc.ItemStyle.CssClass = "grdLavProvincia";

            BoundField RegioneDoc = new BoundField();
            RegioneDoc.DataField = ("Regione");
            RegioneDoc.HeaderText = "Regione";

            BoundField Sector = new BoundField();
            Sector.DataField = ("Sector");
            Sector.HeaderText = "Settore";

            BoundField Entity = new BoundField();
            Entity.DataField = ("Entity");
            Entity.HeaderText = "Ente bilaterale";

            BoundField EmployCompany = new BoundField();
            EmployCompany.DataField = ("EmployCompany");
            EmployCompany.HeaderText = "Azienda";

            BoundField Level = new BoundField();
            Level.DataField = ("Level");
            Level.HeaderText = "Livello";

            BoundField Quota = new BoundField();
            Quota.DataField = ("Quota");
            Quota.HeaderText = "Quota";

            //BoundField PeriodType = new BoundField();
            //PeriodType.DataField = ("PeriodType");
            //PeriodType.HeaderText = "Tipo periodo";

            BoundField Period = new BoundField();
            Period.DataField = ("PeriodNumber");
            Period.HeaderText = "Periodo";

            BoundField Anno = new BoundField();
            Anno.DataField = ("PeriodYear");
            Anno.HeaderText = "Anno";

            //BoundField DataInizio = new BoundField();
            //DataInizio.DataField = ("InitialDate");
            //DataInizio.DataFormatString = "{0:d}";
            //DataInizio.HeaderText = "Data inizio";

            //BoundField DataFine = new BoundField();
            //DataFine.DataField = ("EndDate");
            //DataFine.DataFormatString = "{0:d}";
            //DataFine.HeaderText = "Data fine";

            BoundField Contract = new BoundField();
            Contract.DataField = ("Contract");
            Contract.HeaderText = "Contratto";

            grdIscrizioni.Columns.Add(RegioneDoc);
            grdIscrizioni.Columns.Add(ProvinciaDoc);
            grdIscrizioni.Columns.Add(Sector);
            grdIscrizioni.Columns.Add(Entity);
            grdIscrizioni.Columns.Add(EmployCompany);
            grdIscrizioni.Columns.Add(Contract);
            grdIscrizioni.Columns.Add(Level);
            grdIscrizioni.Columns.Add(Period);
            grdIscrizioni.Columns.Add(Anno);
            grdIscrizioni.Columns.Add(Quota);

            //grdIscrizioni.Columns.Add(PeriodType);
            //grdIscrizioni.Columns.Add(DataInizio);
            //grdIscrizioni.Columns.Add(DataFine);
            
            grdIscrizioni.AutoGenerateColumns = false;
            grdIscrizioni.Width = new Unit(1000);

            grdDocumenti.Columns.Clear();

            //Aggiungiamo le colonne che ci interessano per i documenti
            BoundField ProvinciaDoc1 = new BoundField();
            ProvinciaDoc1.DataField = ("Province");
            ProvinciaDoc1.HeaderText = "Provincia";

            BoundField RegioneDoc1 = new BoundField();
            RegioneDoc1.DataField = ("Regione");
            RegioneDoc1.HeaderText = "Regione";

            BoundField DocumentDate = new BoundField();
            DocumentDate.DataField = ("DocumentDate");
            DocumentDate.DataFormatString = "{0:d}";
            DocumentDate.HeaderText = "Data documento";

            BoundField DocumentType = new BoundField();
            DocumentType.DataField = ("DocumentType");
            DocumentType.HeaderText = "Tipo documento";

            BoundField State = new BoundField();
            State.DataField = ("State");
            State.HeaderText = "Stato";

            BoundField Notes = new BoundField();
            Notes.DataField = ("Notes");
            Notes.HeaderText = "Note";

            grdDocumenti.Columns.Add(RegioneDoc1);
            grdDocumenti.Columns.Add(ProvinciaDoc1);
            grdDocumenti.Columns.Add(DocumentDate);
            grdDocumenti.Columns.Add(DocumentType);
            grdDocumenti.Columns.Add(State);
            grdDocumenti.Columns.Add(Notes);

            grdDocumenti.AutoGenerateColumns = false;
            grdDocumenti.Width = new Unit(1000);
        }

        private void LoadWorker(string id, string isnat)
        {
            IPersistenceFacade p = SessionServiceManager.PersistentService;
            GeoLocationFacade f = SessionServiceManager.GeoHandlerService;

            WorkerHandler w = new WorkerHandler(p, f);
            w.LoadById(System.Convert.ToInt32(id));
            if (w.Found)
            {
                if (isnat.Equals("True"))
                    w.LoadWorkerWithSusbscriptionsAndDocuments(new RegioneNulla());
                else
                {
                    Regione r = ((Utente)SessionServiceManager.SecurityService.CurrentUser).Appartenenza.Regione;
                    w.LoadWorkerWithSusbscriptionsAndDocuments(r);
                }


                LoadObjectProperties(w);
                return;
            }
            throw new Exception("Id del lavoratore non valido!");
        }

        private void LoadObjectProperties(WorkerHandler wh)
        {
            Worker w = wh.CurrentWorker;
            lblCognome.Text = w.Cognome;
            lblNome.Text = w.Nome;
            lblCodFisc.Text = w.CodiceFiscale;
            lblDataNasc.Text = w.DataNascita.ToShortDateString();
            lblNazione.Text = w.Nazionalita.Descrizione;
            lblProvinciaNasc.Text = w.ProvinciaNascita.Descrizione;
            lblComuneNasc.Text = w.ComuneNascita.Descrizione;
            lblSesso.Text = w.Sesso.ToString();
            lblComuneRes.Text = w.ComuneResidenza.Descrizione;
            lblProvinciaRes.Text = w.Residenza.Provincia.Descrizione;
            lblCap.Text = w.Residenza.Cap;
            lblIndirizzo.Text = w.Residenza.Via;
            lblTelefono.Text = w.Comunicazione.Cellulare1;
            lblDataMod.Text = w.DataModifica.ToShortDateString();
            lblProvMod.Text = w.ModificatoDa;

            IList subscrip = wh.CurrentWorker.Subscriptions as IList;
            if (subscrip.Count > 0)
            {
                //pnlIscrizioni.Visible = true;
                grdIscrizioni.DataSource = subscrip;
                grdIscrizioni.DataBind();
                grdIscrizioni.Visible = true;
            }

            IList docum = w.Documents as IList;
            if (docum.Count > 0)
            {
                //pnlDocumenti.Visible = true;
                grdDocumenti.DataSource = docum;
                grdDocumenti.DataBind();
                grdDocumenti.Visible = true;
            }
        }

        protected override Image ImageHeader
        {
            get { return null; }
        }

        //protected override Image BkgrTable
        //{
        //    get { return ((ReservedArea)this.Master).BgkImage; }
        //}
    }
}
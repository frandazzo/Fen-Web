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
using WIN.FENGEST_NAZIONALE.DOMAIN.Public;
using WIN.TECHNICAL.PERSISTENCE;
using System.IO;
using System.Web.Configuration;
using WIN.BASEREUSE;
using WIN.FENGEST_NAZIONALE.DOMAIN.Security;


namespace FenealgestWEB.Reserved.AreaAmministrativa
{
    public partial class GestioneDownload : BaseReservedArea
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
                CheckUserIsAuthorized();


            //inserire qui altro codice di inizializzaizone della pagina
            InitializePage();
        }

        protected override string AuthorizedFunction
        {
            get { return "Gestione download"; }
        }

        protected override string SitePagePath
        {
            get { return "Home -> Area riservata -> Amministrazione -> Visualizza download"; }
        }

        //protected override Menu PageMenu
        //{
        //    get { return ((ReservedArea)this.Master).MainMenu; }
        //}

        protected override DevExpress.Web.ASPxMenu.ASPxMenu DevPageMenu
        {
            get { return ((ReservedArea)this.Master).MainMenu; }
        }
        protected override DevExpress.Web.ASPxTreeView.ASPxTreeView DevPageTree
        {
            get { return ((ReservedArea)this.Master).DevMainTree; }
        }
        protected override void LoadPageInfo()
        {
            string tempSubstring;
            //imposto tutte le info necessarie alla creazione di tutte le informazioni della pagina
            Label lbl = ((ReservedArea)this.Master).UtenteLabelPro;

            tempSubstring = base.CurrentUser;
            if (!tempSubstring.Equals("") || tempSubstring != null)
                if (tempSubstring.Length > 20)
                    tempSubstring = tempSubstring.Substring(0, 19) + "...";
            lbl.Text = tempSubstring;


            Label lbl1 = ((ReservedArea)this.Master).StrutturaLabelPro;
            lbl1.Text = base.UserStructure;

            Label lbl2 = ((ReservedArea)this.Master).HomeLabelPro;

            tempSubstring = SitePagePath;
            if (!tempSubstring.Equals("") || tempSubstring != null)
                if (tempSubstring.Length > 20)
                    tempSubstring = tempSubstring.Substring(0, 19) + "...";
            lbl2.Text = tempSubstring;

            Label lbl3 = ((ReservedArea)this.Master).DataLabelPro;
            lbl3.Text = DateTime.Now.ToShortDateString();

            Label lbl4 = ((ReservedArea)this.Master).OraLabelPro;
            lbl4.Text = DateTime.Now.ToShortTimeString();

            Label lbl5 = ((ReservedArea)this.Master).ScadenzaLabelPro;
            lbl5.Text = base.UserDecayDataPassword.ToShortDateString();

            Label lbl6 = ((ReservedArea)this.Master).RegioneLabelPro;

            tempSubstring = base.UserRegion;
            if (!tempSubstring.Equals("") || tempSubstring != null)
                if (tempSubstring.Length > 15)
                    tempSubstring = tempSubstring.Substring(0, 14) + "...";
            lbl6.Text = tempSubstring;

            Label lbl7 = ((ReservedArea)this.Master).ProvinciaLabelPro;

            tempSubstring = base.UserProvice;
            if (!tempSubstring.Equals("") || tempSubstring != null)
                if (tempSubstring.Length > 15)
                    tempSubstring = tempSubstring.Substring(0, 14) + "...";
            lbl7.Text = tempSubstring;


            LoadContextImage(); 
        }



        protected override string FunctionHeaderTitle
        { 
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
                { return "Aggiorna download"; }
                else
                { return "Crea download"; }
            }
        }

        protected override void InitializePage()
        {
            Page.Title = FunctionHeaderTitle;
            if (!this.IsPostBack)
            {
                lblTitolo.Text = FunctionHeaderTitle;

                SetLblFileDim();
                LoadComboTipo();
                LoadComboRegioni();
                //Verifico se sono in modalità di update
                //e nel caso carico i dati del download da modificare
                if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
                {
                    LoadDownload(Request.QueryString["Id"]);
                    lblPercorsoFile.Visible = true;
                    lblNomeFile.Visible = true;
                    cmdSave.Text = "Aggiorna";
                }
                //altrimenti inizializzo i valori
                else
                {
                    ASPxDateEdit1.Date = DateTime.Now;
                    cmdSave.Text = "Inserisci";
                }
            }
        }

        private void SetLblFileDim()
        {
            string dim = "dim";

            HttpRuntimeSection section = ConfigurationManager.GetSection("system.web/httpRuntime") as HttpRuntimeSection;
            if (section != null)
                dim = section.MaxRequestLength.ToString();
            lblFileDim.Text = "Attenzione: verrà generato un errore per file superiori a " + dim + " kb";

        }

        private void LoadComboRegioni()
        {
            GeoLocationFacade f = SessionServiceManager.GeoHandlerService;
            IList regioni = f.GetListaRegioni();
            cboRegione.Items.Clear();
            cboRegione.Items.Add("");
            foreach (string item in regioni)
            {
                cboRegione.Items.Add(item);
                //cboRegione.Items.Add(new ListItem(item.Descrizione, item.Id.ToString()));
            }

            Utente u = SessionServiceManager.SecurityService.CurrentUser as Utente;
           
            if(u.Appartenenza.Regione == null)
                //si mette sul primo elemento
                cboRegione.SelectedIndex = 0;
            else
                cboRegione.SelectedValue=u.Appartenenza.Regione.Descrizione;

            cboRegione.Enabled = false;
        }

        private void LoadComboTipo()
        {

            IPersistenceFacade p = SessionServiceManager.PersistentService;
            IList d = p.GetAllObjects("TipoDownload");
            cboTipo.Items.Clear();
            foreach (TipoDownload item in d)
            {
                cboTipo.Items.Add( new ListItem(item.Descrizione, item.Id.ToString()));
            }
            //si mette sul primo elemento
            cboTipo.SelectedIndex = 0;  
        }

        private void LoadDownload(string id)
        {
            IPersistenceFacade p = SessionServiceManager.PersistentService;

            Download d = p.GetObject("Download", System.Convert.ToInt32(id)) as Download;

            //imposto l'interfaccia utente
            if (d != null)
            {
                LoadObjectProperties(d);
                return;
            }
            throw new Exception("Id del download non valido!");


        }

        private void LoadObjectProperties(Download d)
        {
            //Valorizzo i campi del form legato al download

            ASPxDateEdit1.Date = d.DataCreazione;
            txtCreatoDa.Text = d.CreatoDa;
            txtDescrizione.Text = d.Descrizione;
            lblPercorsoFile.Text = d.Percorso + " " + d.DimensioneToString();
            cboTipo.SelectedValue = d.Tipo.Id.ToString();
            cboRegione.SelectedValue = d.Regione.Descrizione;
        }

        protected override Image ImageHeader
        {
            get { return ((ReservedArea)this.Master).HeaderImage; }
        }

        //protected override Image BkgrTable
        //{
        //    get { return ((ReservedArea)this.Master).BgkImage; }
        //}

        protected void cmdCreaDownload_Click(object sender, EventArgs e)
        {
            Response.Redirect(LinkHandler.LinkGestioneDownload);
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(1000);
            // otteniamo il path della cartella
            // principale dell'applicazione
            string filePath = WebConfigurationManager.AppSettings["DownloadDirectoryName"];

            if (!System.IO.Directory.Exists(filePath))
            {
                throw new Exception("Directory per il caricamento dei dati non trovata");
            }
            //int fileLen;

            //fileLen = FileUpload1.PostedFile.ContentLength;

            // controlliamo se il controllo FileUpload1
            // contiene un file da caricare
            if (FileUpload1.HasFile)
            {
            //    // se si, aggiorniamo il path del file
                filePath += FileUpload1.FileName;

            //    // salviamo il file nel percorso calcolato
                FileUpload1.SaveAs(filePath);
            }

            ////codice per aggiornare
            IPersistenceFacade p = SessionServiceManager.PersistentService;
            try
            {

                if (cmdSave.Text == "Aggiorna")
                {
                    Download d = p.GetObject("Download", System.Convert.ToInt32(Request.QueryString["Id"])) as Download;

                    if (d != null)
                    {
                        SetBaseData(d);
                        if (FileUpload1.HasFile)
                        {
                            d.Percorso = filePath;
                            lblPercorsoFile.Text = d.Percorso + " " + d.DimensioneToString();
                        }
                        lblPercorsoFile.Visible = true;
                        lblNomeFile.Visible = true;
                        p.UpdateObject("Download", d);
                    }
                }
                else
                {

                    Download d = new Download();
                    SetBaseData(d);
                    d.Percorso = filePath;
                    lblPercorsoFile.Visible = false;
                    lblNomeFile.Visible = false;
                    p.InsertObject("Download", d);
                    Response.Redirect(LinkHandler.ConstructSimpleQueryString(LinkHandler.LinkGestioneDownload, "ID", d.Id.ToString()));
                }

            }
            catch (Exception)
            {

                throw;
            }
            //finally
            //{
            //}
        }

        private void SetBaseData(Download d)
        {
            d.DataCreazione = ASPxDateEdit1.Date;
            d.CreatoDa = txtCreatoDa.Text;
            d.Descrizione = txtDescrizione.Text;

            TipoDownload tipoD = new TipoDownload();
            tipoD.Key = new Key(int.Parse(cboTipo.SelectedValue));
            tipoD.Descrizione = cboTipo.SelectedItem.ToString();
            d.Tipo = tipoD;

            GeoLocationFacade f = SessionServiceManager.GeoHandlerService;
            Regione reg = f.GetGeoHandler().GetRegioneByName(cboRegione.Text);
            if (reg.Id != -1)
                d.Regione = reg;

            //d.Percorso = filePath;// txtPercorso.Text;
        }

        protected void cmdAnnulla_Click(object sender, EventArgs e)
        {
            Response.Redirect(LinkHandler.LinkVisualizzaDownload);
        }

        protected void cmdDeleteDownload_Click(object sender, EventArgs e)
        {
            
            //codice per cancellare un download
            IPersistenceFacade p = SessionServiceManager.PersistentService;

            Download d = p.GetObject("Download", System.Convert.ToInt32(Request.QueryString["Id"])) as Download;

            if (d != null)
            {
                p.DeleteObject("Download", d);
                Response.Redirect(LinkHandler.LinkVisualizzaDownload);
            }
        }
    }
}

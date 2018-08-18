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
using WIN.TECHNICAL.PERSISTENCE;
using FenealgestWEB.Utility;
using System.IO;
using WIN.FENGEST_NAZIONALE.DOMAIN.Public;
using System.Web.Configuration;
using WIN.BASEREUSE;
using WIN.FENGEST_NAZIONALE.DOMAIN.Security;

namespace FenealgestWEB.Reserved.AreaPubblica
{
    public partial class ScaricaMateriali : BaseReservedArea
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
            get { return "Scarica materiali"; }
        }

        protected override string SitePagePath
        {
            get { return "Home -> Area riservata -> Area pubblica -> Scarica materiali"; }
        }

        //protected override Menu PageMenu
        //{
        //    get { return ((ReservedArea)this.Master).MainMenu; }
        //}

        protected override DevExpress.Web.ASPxTreeView.ASPxTreeView DevPageTree
        {
            get { return ((ReservedArea)this.Master).DevMainTree; }
        }
        protected override DevExpress.Web.ASPxMenu.ASPxMenu DevPageMenu
        {
            get { return ((ReservedArea)this.Master).MainMenu; }
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
            get { return "Scarica materiali"; }
        }

        protected override void InitializePage()
        {
            //Page.Title = FunctionHeaderTitle;
            //if (!this.IsPostBack)
            //{
            //    lblTitolo.Text = FunctionHeaderTitle;
            //    LoadComboTipo();
            //    LoadComboRegione();
            //    InitializeGrid();
            //    SetResultVisibility(false, "");
            //}
        }

        //private void LoadComboRegione()
        //{
        //    GeoLocationFacade f = SessionServiceManager.GeoHandlerService;
        //    IList regioni = f.GetListaRegioni();
        //    cboRegione.Items.Clear();
        //    cboRegione.Items.Add("");
        //    foreach (string item in regioni)
        //    {
        //        cboRegione.Items.Add(item);
        //        //cboRegione.Items.Add(new ListItem(item.Descrizione, item.Id.ToString()));
        //    }

        //    Utente u = SessionServiceManager.SecurityService.CurrentUser as Utente;

        //    if (u.Appartenenza.Regione == null)
        //        //si mette sul primo elemento
        //        cboRegione.SelectedIndex = 0;
        //    else
        //        cboRegione.SelectedValue = u.Appartenenza.Regione.Descrizione;

        //    //cboRegione.Enabled = false;
        //}

        //private void LoadComboTipo()
        //{
        //    IPersistenceFacade p = SessionServiceManager.PersistentService;
        //    IList d = p.GetAllObjects("TipoDownload");
        //    cboTipo.Items.Clear();
        //    cboTipo.Items.Add("Tutti");
        //    foreach (TipoDownload item in d)
        //    {
        //        cboTipo.Items.Add(new ListItem(item.Descrizione, item.Id.ToString()));
        //    }
        //    //si mette sul primo elemento
        //    cboTipo.SelectedIndex = 0;
        //}

        //private void InitializeGrid()
        //{
        //    GridView1.Columns.Clear();

        //    //Aggiungiamo le colonne che ci interessano
        //    BoundField data = new BoundField();
        //    data.DataField = ("DataCreazione");
        //    data.DataFormatString = "{0:d}";
        //    data.HeaderText = "Data";

        //    BoundField Descrizione = new BoundField();
        //    Descrizione.DataField = ("Descrizione");
        //    Descrizione.HeaderText = "Descrizione";

        //    BoundField Percorso = new BoundField();
        //    Percorso.DataField = ("Percorso");
        //    Percorso.HeaderText = "Percorso";

        //    BoundField NomeFile = new BoundField();
        //    NomeFile.DataField = ("NomeFile");
        //    NomeFile.HeaderText = "NomeFile";

        //    //BoundField CreatoDa = new BoundField();
        //    //CreatoDa.DataField = ("CreatoDa");
        //    //CreatoDa.HeaderText = "Creato da";

        //    BoundField Tipo = new BoundField();
        //    Tipo.DataField = ("DescrizioneTipo");
        //    Tipo.HeaderText = "Tipo";

        //    BoundField Regione = new BoundField();
        //    Regione.DataField = ("DescrizioneRegione");
        //    Regione.HeaderText = "Regione";

        //    //BoundField Id = new BoundField();
        //    //Id.DataField = ("Id");
        //    //Id.HeaderText = "Id";
        //    //Id.Visible = false;

        //    //ButtonField bfcol = new ButtonField();
        //    //bfcol.HeaderText = "Aggiorna/Modifica";
        //    //bfcol.ButtonType = ButtonType.Link;
        //    //bfcol.CommandName = "select";
        //    //bfcol.Text = "Aggiorna";

        //    ButtonField bf = new ButtonField();
        //    bf.CommandName = "cmd";
        //    bf.HeaderText = "<img src='../../immagini/icon/download32.png' alt='MOD' border='0' />";
        //    bf.Text = "<img src='../../immagini/icon/download16.png' alt='mod'border='0' />";
        //    bf.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        //    bf.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            
        //    GridView1.Columns.Add(data);
        //    GridView1.Columns.Add(Descrizione);
        //    //GridView1.Columns.Add(Percorso);
        //    GridView1.Columns.Add(NomeFile);
        //    //GridView1.Columns.Add(CreatoDa);
        //    //GridView1.Columns.Add(bfcol);
        //    GridView1.Columns.Add(Tipo);
        //    GridView1.Columns.Add(Regione);
        //    GridView1.Columns.Add(bf);

        //    GridView1.DataKeyNames = new string[] { "Id" };

        //    GridView1.AutoGenerateColumns = false;
        //}

        //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "cmd")
        //    {
        //        string riga = e.CommandArgument.ToString();
        //        int i = Convert.ToInt32(riga);
        //        string p = GridView1.Rows[i].Cells[2].Text;
        //        //string path = MapPath("~/soft/" + filename);
        //        string path = WebConfigurationManager.AppSettings["DownloadDirectoryName"] + p;
        //        string filename = Path.GetFileName(path);
        //        byte[] bts = File.ReadAllBytes(path);

        //        Response.Clear();
        //        Response.ClearHeaders();

        //        Response.AddHeader("Content-Type", "Application/octet-stream");
        //        Response.AddHeader("Content-Length", bts.Length.ToString());
        //        Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
        //        Response.BinaryWrite(bts);
        //        Response.Flush();
        //        Response.End();
        //    }
        //}

        protected override Image ImageHeader
        {
            get { return ((ReservedArea)this.Master).HeaderImage; }
        }

        //protected override Image BkgrTable
        //{
        //    get { return ((ReservedArea)this.Master).BgkImage; }
        //}



        //private void SetInterfaceAfterQuery(Query q, IList download)
        //{
        //    GridView1.DataSource = download;
        //    GridView1.DataBind();
        //    SetResultVisibility(true, "");
        //    lblTotalResults.Text = "Sono stati trovati " + q.TotalReturnedRecords + " record!";
        //}

        //private void SetResultVisibility(bool visible, string message)
        //{
        //    if (visible)
        //    {
        //        GridView1.Visible = true;
        //        lblVis.Visible = true;
        //        lnkPrec.Visible = true;
        //        lnkSucc.Visible = true;
        //        pnlGrid.Visible = true;
        //    }
        //    else
        //    {
        //        GridView1.Visible = false;
        //        lblVis.Visible = false;
        //        lnkPrec.Visible = false;
        //        lnkSucc.Visible = false;
        //        lblTotalResults.Text = message;
        //        pnlGrid.Visible = false;
        //    }
        //}

        //protected void lnkPrec_Click(object sender, EventArgs e)
        //{
        //    PaginationQueryHandler q = Session["PaginationQuery"] as PaginationQueryHandler;
        //    if (q == null)
        //        throw new Exception("Query di paginazione non memorizzata nella session");

        //    IList download = q.PreviousPageQuery(SessionServiceManager.PersistentService);

        //    SetInterfaceAfterQuery(q, download);
        //}

        //protected void lnkSucc_Click(object sender, EventArgs e)
        //{
        //    PaginationQueryHandler q = Session["PaginationQuery"] as PaginationQueryHandler;
        //    if (q == null)
        //        throw new Exception("Query di paginazione non memorizzata nella session");

        //    IList download = q.NextPageQuery(SessionServiceManager.PersistentService);

        //    SetInterfaceAfterQuery(q, download);
        //}

        //private void SetInterfaceAfterQuery(PaginationQueryHandler q, IList download)
        //{
        //    GridView1.DataSource = download;
        //    GridView1.DataBind();
        //    SetResultVisibility(true, "");

        //    lblVis.Text = q.ViewedRecordMessage;
        //    lblTotalResults.Text = q.FoundElementMessage;
        //    lnkPrec.Enabled = q.IsPreviousQueryEnabled;
        //    lnkSucc.Enabled = q.IsNextQueryEnabled;
        //}
        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            //int maxQueryResult = Convert.ToInt32(WebConfigurationManager.AppSettings["MaxQueryResult"]);
            //int resMax = TryCastToInt();

            ////seguo la query
            //IPersistenceFacade p = SessionServiceManager.PersistentService;

            //PaginationQueryHandler q = p.CreateNewPaginationQuery("MapperDownload", maxQueryResult, resMax);

            //q.SetTable("Download");

            //CompositeCriteria c = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);

            //if ((chbData.Checked) && (bdpData.IsDate))
            //    if (bdpData.SelectedDate != DateTime.MinValue)
            //        c.AddCriteria(Criteria.DateEqual("Data", bdpData.SelectedDate, p.DBType));


            //if (!string.IsNullOrEmpty(cboTipo.Text))
            //    if (!cboTipo.Text.Equals("Tutti"))
            //    {
            //        c.AddCriteria(Criteria.Equal("Tipo", cboTipo.SelectedValue.ToString()));                    
            //    }

            //int idRegione = GetIdRegione();

            //if (idRegione > 0)
            //    c.AddCriteria(Criteria.Equal("Id_Regione", idRegione.ToString()));

            //q.AddWhereClause(c);

            //AbstractBoolCriteria orderclause = new OrderByCriteria();
            //orderclause.AddCriteria(Criteria.SortCriteria("Data", true));
            //q.AddOrderByClause(orderclause);

            //IList ex = q.ExecuteFirstQuery(p);

            ////imposto l'interfaccia utente
            //if (ex.Count > 0)
            //{
            //    SetInterfaceAfterQuery(q, ex);
            //    //memorizzo la query nella session
            //    Session["PaginationQuery"] = q;
            //    return;
            //}

            //SetResultVisibility(false, "Nessun risultato trovato!");
            ////memorizzo la query nella session
            //Session["PaginationQuery"] = q;
        }

        //private int TryCastToInt()
        //{
        //    string resperPage = txtResultPerPage.Text;

        //    try
        //    {
        //        int res = Convert.ToInt32(resperPage);

        //        if (res > 50)
        //            return 50;
        //        if (res < 10)
        //            return 10;
        //        return res;
        //    }
        //    catch (Exception)
        //    {
        //        return 10;
        //    }

        //}

        //private int GetIdRegione()
        //{
        //    if (string.IsNullOrEmpty(cboRegione.Text))
        //        return -1;

        //    GeoLocationFacade f = SessionServiceManager.GeoHandlerService;

        //    Regione r = f.GetGeoHandler().GetRegioneByName(cboRegione.Text);

        //    return r.Id;

        //}

        //protected void chbData_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chbData.Checked)
        //        bdpData.SelectedDate = DateTime.Now;
        //    else
        //        bdpData.SelectedDate = DateTime.MinValue;
        //}

    }
}
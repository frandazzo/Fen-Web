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
using WIN.FENGEST_NAZIONALE.HANDLERS;
using WIN.BASEREUSE;
using WIN.FENGEST_NAZIONALE.DOMAIN.Security;
using WIN.TECHNICAL.PERSISTENCE;
using System.Collections.Generic;
using WIN.FENGEST_NAZIONALE.DOMAIN.Workers;
using System.Web.Configuration;
using DevExpress.Web;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;
using DevExpress.XtraPrinting;

namespace FenealgestWEB.Reserved.AreaNazionale
{
    public partial class RicercaLavoratori : BaseReservedArea
    {
        private bool _isNationalResearch = false;
       

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Request.QueryString["isnat"].Equals("true"))
                _isNationalResearch = true;
            else
                _isNationalResearch = false;


            ////se è una pagina che richiede una autorizzazione
            ////posso decommentare il codice che segue
            if (!IsPostBack)
            {
                //esegue la verifica che un utetne è loggato;
                //inizializza le info base della pagina insieme con 
                //l'inizializzaizone del menu per l'utente
                InitializeBasePage();


                CheckUserIsAuthorized();




                //inserire qui altro codice di inizializzaizone della pagina
                InitializePage();



            }
            else
            {
                SearchWorkers();
            }

            
        }

        //private string _tipoRicerca = "Ricerca per codice fiscale";





        public int CodiceRicerca
        {
            get
            {
                return (int)ASPxHiddenField1.Get("codicericerca");
            }
            set
            {
                ASPxHiddenField1.Set("codicericerca", value);
            }
        }

        protected string TipoRicerca
        {
            get 
            {
                string ricerca = "";
                Utente u = (Utente)SessionServiceManager.SecurityService.CurrentUser;
                if (u.Appartenenza.Struttura != WIN.FENGEST_NAZIONALE.DOMAIN.StrutturaApparteneza.Feneal_Nazionale)
                {
                    if (!_isNationalResearch)
                    {
                        ricerca = String.Format(" (limita la ricerca delle iscrizioni alla regione {0}) <br /><br />", u.Appartenenza.Regione.Descrizione);
                    }
                }
 
                switch (CodiceRicerca)
                {
                    case 0:
                        return String.Format("<h4>Ricerca per codice fiscale</h4><br />{0}", ricerca);
                    case 1:
                        return String.Format("<h4>Ricerca per dati anagrafici</h4><br />{0}", ricerca);
                    case 2:
                        return String.Format("<h4>Ricerca per azienda</h4><br />{0}", ricerca);
                    default:
                        return String.Format("<h4>Ricerca per codice fiscale</h4><br />{0}", ricerca);
                }

            
            }
            
        }

        private void loadCombos()
        {
            GeoLocationFacade f = SessionServiceManager.GeoHandlerService;

            IList nazioni = f.GetListaNazioni();

            cboNazionalita.Items.Clear();
            cboNazionalita.Items.Add("");
            foreach (string item in nazioni)
            {
                cboNazionalita.Items.Add(item);
            }
            //si mette sul primo elemento
            cboNazionalita.SelectedIndex = 0;


            //prendo l'utente connesso per verificare il tipo di accesso
            Utente u = SessionServiceManager.SecurityService.CurrentUser as Utente;

            cboProvince.Items.Clear();
            IList provincie = new ArrayList();

            if (u.Appartenenza.Struttura == WIN.FENGEST_NAZIONALE.DOMAIN.StrutturaApparteneza.Feneal_Provinciale)
                provincie.Add(u.Appartenenza.Provicnia.Descrizione);
            else if (u.Appartenenza.Struttura == WIN.FENGEST_NAZIONALE.DOMAIN.StrutturaApparteneza.Feneal_Regionale)
            {
                provincie = f.GetGeoHandler().GetNomiProviciePerRegione(u.Appartenenza.Regione.Descrizione);
            }
            else
            {
                cboProvince.Items.Add("");
                provincie = f.GetListaProvincie();
            }

            foreach (string item in provincie)
            {
                cboProvince.Items.Add(item);
            }

            if (u.Appartenenza.Struttura != WIN.FENGEST_NAZIONALE.DOMAIN.StrutturaApparteneza.Feneal_Provinciale)
            { //si mette sul primo elemento
                cboProvince.SelectedIndex = 0;
                if (cboProvince.Text != "")
                    LoadComuni();
            }
            else
            {
                cboProvince.Text = u.Appartenenza.Provicnia.Descrizione;
                LoadComuni();
                cboComune.Text = u.Appartenenza.Provicnia.Descrizione;
               
            }




            cboAnno.Items.Clear();
            cboAnno.Items.Add("");
            for (int i = DateTime.Now.Year; i > DateTime.Now.Year - 20 ; i--)
            {
                cboAnno.Items.Add(i.ToString());
            }
            //si mette sul primo elemento
            cboAnno.SelectedIndex = 0;



            cboTerr.Items.Clear();
            cboTerr.Items.Add("");
            IList provincie1 = f.GetGeoHandler().GetListaNomiProvincie();
            foreach (string item in provincie1)
            {
                cboTerr.Items.Add(item);
            }

                
            
        
        }

        protected override string AuthorizedFunction
        {
            get 
            {
                if (Request.QueryString["isnat"].Equals("true"))
                    return "Visualizza storico lavoratore liv nazionale"; 
                else
                    return "Visualizza storico lavoratore liv regionale";
            }
        }

        protected override string SitePagePath
        {
            get 
            {
                if (Request.QueryString["isnat"].Equals("true"))
                    return "Home -> Area riservata -> Nazionale -> Iscritti -> Ricerca lavoratori";
                else
                    return "Home -> Area riservata -> Regionale -> Ricerca lavoratori";
            }
        }

        //protected override Menu PageMenu
        //{
        //    get { return ((ReservedArea)this.Master).MainMenu; }
        //}
        protected override DevExpress.Web.ASPxTreeView DevPageTree
        {
            get { return null; }
        }

        protected override DevExpress.Web.ASPxMenu DevPageMenu
        {
            get { return ((FenealgestwebNew)this.Master).MainMenu; }
        }

        protected override void LoadPageInfo()
        {
            
        }



        protected override string FunctionHeaderTitle
        {
            get { return "Ricerca lavoratori"; }
        }

        protected override void InitializePage()
        {
            ASPxHiddenField1.Add("codicericerca", 0);
            InitializeGrid();
            //Panel1.Visible = false;
            cboComune.Enabled = false;
            SetResultVisibility(false, "");
            bdpNascita1.Date = DateTime.Now.Date;
            bdpNascita2.Date = DateTime.Now.Date;

            loadCombos();

        }

        private void InitializeGrid()
        {
            //GridView1.Columns.Clear();

            ////Aggiungiamo le colonne che ci interessano
            //BoundField Cognome = new BoundField();
            //Cognome.DataField = ("Cognome");
            //Cognome.HeaderText = "Cognome";

            //BoundField Nome = new BoundField();
            //Nome.DataField = ("Nome");
            //Nome.HeaderText = "Nome";

            //BoundField Nazionalita = new BoundField();
            //Nazionalita.DataField = ("Nazionalita");
            //Nazionalita.HeaderText = "Nazionalita";

            //BoundField Comune = new BoundField();
            //Comune.DataField = ("ComuneResidenza");
            //Comune.HeaderText = "Comune residenza";

            //BoundField Cellulare = new BoundField();
            //Cellulare.DataField = ("Cellulare1");
            //Cellulare.HeaderText = "Cellulare";

            //HyperLinkField hlfield = new HyperLinkField();
            //hlfield.DataNavigateUrlFields = new string[] { "Id" };
            //hlfield.DataNavigateUrlFormatString = LinkHandler.LinkVisualizzaLavoratore + "?ID={0}" + "&isnat=" + _isNationalResearch.ToString();
            //hlfield.HeaderText = "<img src='../../immagini/icon/zoom32.png' alt='MOD' border='0' />";
            //hlfield.Text = "<img src='../../immagini/icon/zoom16.png' alt='mod'border='0' />";
            //hlfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            //hlfield.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

            //GridView1.Columns.Add(Cognome);
            //GridView1.Columns.Add(Nome);
            //GridView1.Columns.Add(Nazionalita);
            //GridView1.Columns.Add(Comune);
            //GridView1.Columns.Add(Cellulare);
            //GridView1.Columns.Add(hlfield);

            //GridView1.DataKeyNames = new string[] { "Id" };


            //GridView1.AutoGenerateColumns = false;
        }

        protected void gvData_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters != string.Empty)
            {
                (sender as ASPxGridView).Columns[e.Parameters].Visible = false;
                (sender as ASPxGridView).Columns[e.Parameters].ShowInCustomizationForm = true;
            }
        }

        private void SetResultVisibility(bool visible, string message)
        {
            //if (visible)
            //{
            //    GridView1.Visible = true;
            //    lblVis.Visible = true;
            //    lnkPrec.Visible = true;
            //    lnkSucc.Visible = true;
            //    //pnlGrid.Visible = true;
            //}
            //else
            //{
            //    GridView1.Visible = false;
            //    lblVis.Visible = false;
            //    lnkPrec.Visible = false;
            //    lnkSucc.Visible = false;
                lblTotalResults.Text = message;
            //    //pnlGrid.Visible = false;
            //}

        }

        protected override Image ImageHeader
        {
            get { return null; }
        }

        //protected override Image BkgrTable
        //{
        //    get { return ((ReservedArea)this.Master).BgkImage; }
        //}

        protected void cmdRicerca_Click(object sender, EventArgs e)
        {
           // SearchWorkers();

        }

        private void SearchWorkers()
        {
            if (CodiceRicerca == 0)
            {

                IPersistenceFacade p = SessionServiceManager.PersistentService;
                GeoLocationFacade f = SessionServiceManager.GeoHandlerService;

                WorkerHandler w = new WorkerHandler(p, f);
                w.LoadByFiscalCode(txtCodFisc.Text);

                if (w.CurrentWorker != null)
                {
                    SetInterfaceAfterQuery(w);
                    return;
                }
                //}
                SetResultVisibility(false, "Nessun risultato trovato!");

            }
            //controllo sul tipo di ricerca scelto avanzata o semplice
            else if (CodiceRicerca == 1)
            //sono nella ricerca avanzata
            {

                bool someParamSet = false;

                int maxQueryResult = Convert.ToInt32(WebConfigurationManager.AppSettings["MaxQueryResult"]);
                int resMax = TryCastToInt();

                //eseguo la query
                IPersistenceFacade p = SessionServiceManager.PersistentService;

                //PaginationQueryHandler q = p.CreateNewPaginationQuery("MapperWorker", maxQueryResult, resMax);

                Query q = p.CreateNewQuery("MapperWorker");

                q.SetTable("lavoratori");


                CompositeCriteria c = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);

                if (!string.IsNullOrEmpty(txtCognome.Text))
                {
                    c.AddCriteria(Criteria.Matches("Cognome", txtCognome.Text, p.DBType));
                    someParamSet = true;
                }


                if (!string.IsNullOrEmpty(txtNome.Text))
                {
                    c.AddCriteria(Criteria.Matches("Nome", txtNome.Text, p.DBType));
                    someParamSet = true;
                }
                    

                //if (!string.IsNullOrEmpty(txtCodFisc.Text))
                //    c.AddCriteria(Criteria.MatchesEqual("CodiceFiscale", txtCodFisc.Text, p.DBType));

                string cfNazione = GetIdNazione();

                //prendo la nazionalità dal codice fiscale
                if (!string.IsNullOrEmpty(cfNazione))
                { 
                    c.AddCriteria(Criteria.MatchesEqual("SUBSTRING(CodiceFiscale FROM -5 FOR 4)", cfNazione, p.DBType));
                    someParamSet = true;
                }
                   

                if (cboSex.Text.Equals("UOMO"))
                {
                    c.AddCriteria(Criteria.LessThan("SUBSTRING(CodiceFiscale FROM -7 FOR 2)", "40"));
                    someParamSet = true;
                }
                else if (cboSex.Text.Equals("DONNA"))
                {
                    c.AddCriteria(Criteria.GreaterEqualThan("SUBSTRING(CodiceFiscale FROM -7 FOR 2)", "40"));
                    someParamSet = true;
                }



                int idProvincia = GetIdProvincia();
                if (idProvincia > 0)
                {
                    c.AddCriteria(Criteria.Equal("Id_Provincia_Residenza", idProvincia.ToString()));
                    someParamSet = true;
                }
                    

                int idComune = GetIdComune();

                if (idComune > 0)
                {
                    c.AddCriteria(Criteria.Equal("Id_Comune_Residenza", idComune.ToString()));
                    someParamSet = true;
                }
                   


                if ((chbNascita.Checked))// && (bdpNascita1.Date))
                    if (bdpNascita1.Date != DateTime.MinValue)
                    {
                        c.AddCriteria(Criteria.DateContained("DataNascita", bdpNascita1.Date, bdpNascita2.Date, p.DBType));
                        someParamSet = true;
                    }
                        

                if (!string.IsNullOrEmpty(cboAnno.Text) || !cboSector.Text.Equals("ALL") || !string.IsNullOrEmpty(cboTerr.Text))
                {
                    CompositeCriteria cc = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);
                    if (!string.IsNullOrEmpty(cboAnno.Text))
                    {
                        cc.AddCriteria(Criteria.Equal("Anno", cboAnno.Text));
                    }
                    if (!cboSector.Text.Equals("ALL"))
                    {
                        cc.AddCriteria(Criteria.MatchesEqual("Settore", cboSector.Text, p.DBType));
                    }
                    int idProvincia1 = GetIdProvincia1();
                    if (idProvincia1 > 0)
                        cc.AddCriteria(Criteria.Equal("Id_Provincia", idProvincia1.ToString()));
                    someParamSet = true;

                    //adesso devo aggiungere la query interna per l'anno e il settore 
                    SubQueryCriteria subqry = new SubQueryCriteria("Iscrizioni", "Id_Lavoratore", true);
                    subqry.AddCriteria(cc);


                    c.AddCriteria(Criteria.INClause("ID", subqry));
                }




                q.AddWhereClause(c);

                if (someParamSet != true)
                    return;

                string ff = q.CreateQuery(p);

                AbstractBoolCriteria orderclause = new OrderByCriteria();
                orderclause.AddCriteria(Criteria.SortCriteria("Cognome", true));
                q.AddOrderByClause(orderclause);
                
                //IList workers = q.ExecuteFirstQuery(p);
                IList workers = q.Execute(p);

                IList workersDto = new ArrayList();
                foreach (Worker item in workers)
                {
                    workersDto.Add(item.ToWorkerDTO());
                }

                SetInterfaceAfterQuery(q, workersDto);
                //imposto l'interfaccia utente
                if (workers.Count > 0)
                {
                    
                    SetResultVisibility(false, "Nessun risultato trovato!");
                }
                
            }
            // sono nella ricerca semplice
            else
            {

                if ((string.IsNullOrEmpty(txtPartitaIva.Text)) && (string.IsNullOrEmpty(txtAzienda.Text)))
                {
                    SetResultVisibility(false, "Impostare almeno uno dei criteri di ricerca (Partita iva  o Azienda)");
                    return;
                }

                if ((txtPartitaIva.Text.Length < 3) && (txtAzienda.Text.Length < 3))
                {
                    SetResultVisibility(false, "I criteri di selezione (Partita iva  o Azienda) devono contenere almeno tre caratteri");
                    return;
                }


                int maxQueryResult = Convert.ToInt32(WebConfigurationManager.AppSettings["MaxQueryResult"]);
                int resMax = TryCastToInt();

                //eseguo la query
                IPersistenceFacade p = SessionServiceManager.PersistentService;

                //PaginationQueryHandler q = p.CreateNewPaginationQuery("MapperWorker", maxQueryResult, resMax);
               
                //q.SetTable("lavoratori");

                Query q = p.CreateNewQuery("MapperWorker");

                CompositeCriteria c = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);



                if ((!string.IsNullOrEmpty(txtPartitaIva.Text)) || (!string.IsNullOrEmpty(txtAzienda.Text)))
                {
                    AbstractBoolCriteria c1 = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);

                    if (!string.IsNullOrEmpty(txtPartitaIva.Text))
                    {
                        c1.AddCriteria(Criteria.Matches("Piva", txtPartitaIva.Text, p.DBType));
                    }

                    if (!string.IsNullOrEmpty(txtAzienda.Text))
                    {
                        c1.AddCriteria(Criteria.Matches("Azienda", txtAzienda.Text, p.DBType));
                    }

                    SubQueryCriteria subqry = new SubQueryCriteria("Iscrizioni", "Id_Lavoratore", true);
                    subqry.AddCriteria(c1);


                    c.AddCriteria(Criteria.INClause("ID", subqry));


                }



                q.AddWhereClause(c);

                AbstractBoolCriteria orderclause = new OrderByCriteria();
                orderclause.AddCriteria(Criteria.SortCriteria("Cognome", true));
                q.AddOrderByClause(orderclause);

                IList workers = q.Execute(p);
                IList workersDto = new ArrayList();
                foreach (Worker item in workers)
                {
                    workersDto.Add(item.ToWorkerDTO());
                }


                //imposto l'interfaccia utente
                if (workersDto.Count > 0)
                {
                    SetInterfaceAfterQuery(q, workersDto);
                    //    //memorizzo la query nella session
                    Session["PaginationQuery"] = q;
                    return;
                }
                SetResultVisibility(false, "Nessun risultato trovato!");



            }
        }

        private void SetInterfaceAfterQuery(Query q, IList workers)
        {
            ASPxGridView1.DataSource = workers;
            ASPxGridView1.KeyFieldName = "Id";
            ASPxGridView1.DataBind();
        }

        private int GetIdProvincia()
        {
            if (string.IsNullOrEmpty(cboProvince.Text))
                return -1;

            GeoLocationFacade f = SessionServiceManager.GeoHandlerService;

            Provincia c = f.GetGeoHandler().GetProvinciaByName(cboProvince.Text);

            return c.Id;
        }

        private int GetIdProvincia1()
        {
            if (string.IsNullOrEmpty(cboTerr.Text))
                return -1;

            GeoLocationFacade f = SessionServiceManager.GeoHandlerService;

            Provincia c = f.GetGeoHandler().GetProvinciaByName(cboTerr.Text);

            return c.Id;
        }

        private int GetIdComune()
        {
            if (string.IsNullOrEmpty(cboComune.Text))
                return -1;

            GeoLocationFacade f = SessionServiceManager.GeoHandlerService;

            Comune c = f.GetGeoHandler().GetComuneByName(cboComune.Text);

            return c.Id;
        }

        private string GetIdNazione()
        {
            if (string.IsNullOrEmpty(cboNazionalita.Text))
                return "";

            GeoLocationFacade f = SessionServiceManager.GeoHandlerService;

            Nazione n = f.GetGeoHandler().GetNazioneByName(cboNazionalita.Text);

            return n.CodiceFiscale;

        }

        private void SetInterfaceAfterQuery(PaginationQueryHandler q, IList workers)
        {
            ASPxGridView1.DataSource = workers;
            ASPxGridView1.KeyFieldName = "Id";
            ASPxGridView1.DataBind();
            //SetResultVisibility(true, "");

            //lblVis.Text = q.ViewedRecordMessage;
            //lblTotalResults.Text = q.FoundElementMessage;
            //lnkPrec.Enabled = q.IsPreviousQueryEnabled;
            //lnkSucc.Enabled = q.IsNextQueryEnabled;
        }

        private void SetInterfaceAfterQuery(WorkerHandler w)
        {

            IList l = new List<WorkerDTO>();
            l.Add(w.CurrentWorker.ToWorkerDTO());
            ASPxGridView1.DataSource = l;
            ASPxGridView1.KeyFieldName = "Id";
            ASPxGridView1.DataBind();
            SetResultVisibility(true, "");

            //lblVis.Text = "Record visualizzati: dal record 1 al record 1.";
            //lblTotalResults.Text = "E' stato trovato 1 record!";
            //lnkPrec.Enabled = false;
            //lnkSucc.Enabled = false;
        }

        protected void cmdAvanzata_Click(object sender, EventArgs e)
        {
           
            //if ((Panel1.Visible == false))
            //{
            //    Panel1.Visible = true;
            //    Panel2.Visible = false;
            //    txtCodFisc.Text = "";
            //    txtCodFisc.Enabled = false;
            //}
            //else
            //{
            //    Panel2.Visible = false;
            //    Panel1.Visible = false;
            //    txtCodFisc.Enabled = true;
            //}
            CodiceRicerca = 1;
        }

        private int TryCastToInt()
        {
            //string resperPage = txtResultPerPage.Text;

            //try
            //{
            //    int res = Convert.ToInt32(resperPage);

            //    if (res > 50)
            //        return 50;
            //    if (res < 10)
            //        return 10;
            //    return res;
            //}
            //catch (Exception)
            //{
            //    return 10;
            //}

            return 10;

        }

        //protected void lnkPrec_Click(object sender, EventArgs e)
        //{
        //    PaginationQueryHandler q = Session["PaginationQuery"] as PaginationQueryHandler;
        //    if (q == null)
        //        throw new Exception("Query di paginazione non memorizzata nella session");

        //    IList workers = q.PreviousPageQuery(SessionServiceManager.PersistentService);

        //    SetInterfaceAfterQuery(q, workers);
        //}

        //protected void lnkSucc_Click(object sender, EventArgs e)
        //{
        //    PaginationQueryHandler q = Session["PaginationQuery"] as PaginationQueryHandler;
        //    if (q == null)
        //        throw new Exception("Query di paginazione non memorizzata nella session");

        //    IList workers = q.NextPageQuery(SessionServiceManager.PersistentService);

        //    SetInterfaceAfterQuery(q, workers);
        //}

        protected void cboProvince_SelectedIndexChanged(object sender, EventArgs e)
        {

           
            LoadComuni();
        }

        private void LoadComuni()
        {
            if (cboProvince.Text.Equals(""))
            {
                cboComune.Items.Clear();
                cboComune.Enabled = false;
            }
            else
            {
                GeoLocationFacade f = SessionServiceManager.GeoHandlerService;
                IList comuni = f.GetListaComuniPerProvincia(cboProvince.Text);
                cboComune.Items.Clear();
                cboComune.Items.Add("");
                foreach (string item in comuni)
                {
                    cboComune.Items.Add(item);
                }
                //si mette sul primo elemento
                cboComune.SelectedIndex = 0;

                cboComune.Enabled = true;
            }
        }

        protected void chbNascita_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //if ((Panel2.Visible == false))
            //{
            //    Panel2.Visible = true;
            //    Panel1.Visible = false;
            //    txtCodFisc.Text = "";
            //    txtCodFisc.Enabled = false;
            //}
            //else
            //{

            //    Panel2.Visible = false;
            //    Panel1.Visible = false;
            //    txtCodFisc.Enabled = true;
            //}
            CodiceRicerca = 2;
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            CodiceRicerca = 0;
           
           
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {

            Utente u = SessionServiceManager.SecurityService.CurrentUser as Utente;

          

            if (u.Appartenenza.Struttura != WIN.FENGEST_NAZIONALE.DOMAIN.StrutturaApparteneza.Feneal_Nazionale)
            {
                return;
            }
               

            ASPxGridViewExporter1.GridViewID = "ASPxGridView1";
           

            
            XlsxExportOptions opt = new XlsxExportOptions();
            opt.SheetName= "Report iscritti";

            ASPxGridViewExporter1.WriteXlsxToResponse(true, opt);
                  
            
        }
    }
}

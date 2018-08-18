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
using WIN.TECHNICAL.PERSISTENCE;
using WIN.FENGEST_NAZIONALE.DOMAIN.Workers;
using System.Web.Configuration;
using WIN.BASEREUSE;
using WIN.FENGEST_NAZIONALE.DOMAIN.Security;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;

namespace FenealgestWEB.Reserved.AreaRegionale
{
    public partial class VisualizzaTracciaImportazione : BaseReservedArea
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
            get { return "Visualizza lista importazioni"; }
        }

        protected override string SitePagePath
        {
            get { return "Home -> Area riservata -> Regionale -> Visulazza lista importazioni"; }
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
            get { return "Visualizza lista importazioni"; }
        }

        protected override void InitializePage()
        {
            //Page.Title = FunctionHeaderTitle;
            if (!this.IsPostBack)
            {
                //lblTitolo.Text = FunctionHeaderTitle;
                
                LoadComboRegione();
                Utente u = SessionServiceManager.SecurityService.CurrentUser as Utente;

                //se l'utente è nazionale setto la list box con tutte le regioni e visualizzo il pulsante ricerca
                //altrimenti setto la listbox con la sua regione e restituisco la lista senza visualizzare il pulsante ricerca

                if (u.IsNationalUser)
                {
                    cboRegione.Enabled = true;
                }
                else
                {
                    cboRegione.SelectedValue = u.Appartenenza.Regione.Descrizione;
                    cboRegione.Enabled = false;
                }

                LoadComboSettore();
                cboSettore.SelectedValue = "EDILE";

                cboSemestre.Items.Clear();
                ListItem li;
                li = new ListItem("1");
                cboSemestre.Items.Add(li);
                li = new ListItem("2");
                cboSemestre.Items.Add(li);
                cboSemestre.Enabled = true;

                cboAnno.Items.Clear();
                for (int i = 1980; i < 2050; i++)
                {
                    li = new ListItem(i.ToString());
                    cboAnno.Items.Add(li);
                }
                cboAnno.SelectedValue = DateTime.Now.Year.ToString();

                InitializeGrid();
                SetResultVisibility(false, "");
            }
        }

        private void LoadComboRegione()
        {
            GeoLocationFacade f = SessionServiceManager.GeoHandlerService;

            //pulisco e aggiungo tutte le voci compresa una prima voce vuota
            cboRegione.Items.Clear();
            foreach (string item in f.GetListaRegioni())
            {
                cboRegione.Items.Add(item);
            }
            //si mette sul primo elemento
            cboRegione.SelectedIndex = 0;
        }

        private void LoadComboSettore()
        {
            ListItem listI = new ListItem();
            listI.Text = "EDILE";
            listI.Value = "EDILE";
            cboSettore.Items.Add(listI);
            listI = new ListItem();
            listI.Text = "IMPIANTI FISSI";
            listI.Value = "IMPIANTI FISSI";
            cboSettore.Items.Add(listI);
            listI = new ListItem();
            listI.Text = "INPS";
            listI.Value = "INPS";
            cboSettore.Items.Add(listI);

        }

        protected override Image ImageHeader
        {
            get { return null; }
        }

        //protected override Image BkgrTable
        //{
        //    get { return ((ReservedArea)this.Master).BgkImage; }
        //}

        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            //eseguo la query
            IPersistenceFacade p = SessionServiceManager.PersistentService;
            
            Query q = p.CreateNewQuery("MapperExport");

            q.SetTable("Importazioni");

            CompositeCriteria c = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);

            //if (!string.IsNullOrEmpty(cboAnno.Text))
            //    c.AddCriteria(Criteria.Matches("Anno", cboAnno.Text, p.DBType));

            //if (!string.IsNullOrEmpty(cboSemestre.Text))
            //    c.AddCriteria(Criteria.Matches("Periodo", cboSemestre.Text, p.DBType));
            
            SubscriptionPeriod s;
            if (cboSettore.Text.Equals("EDILE"))
                s = new SubscriptionPeriod(Convert.ToInt32(cboSemestre.Text), Convert.ToInt32(cboAnno.Text), PeriodType.Semester);
            else
                s = new SubscriptionPeriod(Convert.ToInt32(cboAnno.Text));
            

            //La combo è sempre valorizzata
            //if (!string.IsNullOrEmpty(cboSettore.Text))
            c.AddCriteria(Criteria.MatchesEqual("Settore", cboSettore.Text, p.DBType));
            
            c.AddCriteria(Criteria.DateEqual("DataInizio", s.InitialDate, p.DBType));
            c.AddCriteria(Criteria.DateEqual("DataFine", s.EndDate, p.DBType));


            if (!string.IsNullOrEmpty(cboRegione.Text))
            {
                int id = SessionServiceManager.GeoHandlerService.GetGeoHandler().GetRegioneByName(cboRegione.Text).Id;
                c.AddCriteria(Criteria.Equal("Id_Regione", id.ToString()));
            }
            else
                throw new Exception("L'utente non è associato a nessuna regione");
            
            q.AddWhereClause(c);

            AbstractBoolCriteria orderclause = new OrderByCriteria();
            orderclause.AddCriteria(Criteria.SortCriteria("DataEsportazione", true));
            q.AddOrderByClause(orderclause);

            IList ex = q.Execute(p);

            //imposto l'interfaccia utente
            if (ex.Count > 0)
            {
                SetInterfaceAfterQuery(q, ex);
                //memorizzo la query nella session
                //Session["PaginationQuery"] = q;
                return;
            }

            SetResultVisibility(false, "Nessun risultato trovato!");
            //memorizzo la query nella session
            //Session["PaginationQuery"] = q;
            
        }

        private void SetInterfaceAfterQuery(Query q, IList ex)
        {
            GeoLocationFacade f = SessionServiceManager.GeoHandlerService;

            GridView1.DataSource = ex;
            GridView1.DataBind();
            SetResultVisibility(true, "");
            lblTotalResults.Text = "Sono stati trovati " + q.TotalReturnedRecords + " record!";
            
            //indico le province mancanti
            //recupero tutte le province
            Utente u = SessionServiceManager.SecurityService.CurrentUser as Utente;

            IList prov = f.GetGeoHandler().GetNomiProviciePerRegione(cboRegione.Text);

            foreach(Export expo in ex)
            {
            //scarto quelle esportate

                prov.Remove(expo.Province.Descrizione);
            
            }

            string provMancanti = "";
            bool b = true;
            foreach (String prv in prov)
            {
                //scarto quelle esportate
                if (b)
                {
                    provMancanti = "Province che non hanno ancora provveduto all'esportazione " + prv;
                    b = false;
                }
                else
                    provMancanti = provMancanti + ", " + prv;

            }

            lblProvMancanti.Text = provMancanti;

        }

        private void InitializeGrid()
        {
            GridView1.Columns.Clear();

            //Aggiungiamo le colonne che ci interessano
            BoundField Data = new BoundField();
            Data.DataField = ("exportDate");
            Data.DataFormatString = "{0:d}";
            Data.HeaderText = "Data";

            BoundField Provincia = new BoundField();
            Provincia.DataField = ("Province");
            Provincia.HeaderText = "Provincia";
            
            BoundField DataInizio = new BoundField();
            DataInizio.DataField = ("InitialDate");
            DataInizio.DataFormatString = "{0:d}";
            DataInizio.HeaderText = "DataInizio";

            BoundField DataFine = new BoundField();
            DataFine.DataField = ("EndDate");
            DataFine.DataFormatString = "{0:d}";
            DataFine.HeaderText = "DataFine";
            
            BoundField Settore = new BoundField();
            Settore.DataField = ("Settore");
            Settore.HeaderText = "Settore";
            
            HyperLinkField hlfield = new HyperLinkField();
            hlfield.DataNavigateUrlFields = new string[] { "Id" };
            hlfield.DataNavigateUrlFormatString = LinkHandler.DettagioImportazioni + "?ID={0}";
            hlfield.HeaderText = "<img src='../../immagini/icon/zoom32.png' alt='VIS' border='0' />";
            hlfield.Text = "<img src='../../immagini/icon/zoom16.png' alt='vis' border='0' />";
            hlfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            hlfield.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

            GridView1.Columns.Add(Data);
            GridView1.Columns.Add(Provincia);
            GridView1.Columns.Add(DataInizio);
            GridView1.Columns.Add(DataFine);
            GridView1.Columns.Add(Settore);
            GridView1.Columns.Add(hlfield);
            GridView1.DataKeyNames = new string[] { "Id" };


            GridView1.AutoGenerateColumns = false;
        }




        private void SetResultVisibility(bool visible, string message)
        {
            if (visible)
            {
                GridView1.Visible = true;
                lblProvMancanti.Visible = true;
                //pnlGrid.Visible = true;
            }
            else
            {
                GridView1.Visible = false;
                lblProvMancanti.Visible = false;
                lblTotalResults.Text = message;
                //pnlGrid.Visible = false;
            }

        }

        protected void cboSettore_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSettore.SelectedValue.Equals("EDILE"))
            {
                ListItem li;
                li = new ListItem("");
                cboSemestre.Items.Remove(li);
                cboSemestre.Enabled = true;
            }
            else
            {
                ListItem li;
                li = new ListItem("");
                cboSemestre.Items.Add(li);
                cboSemestre.Text = "";
                cboSemestre.Enabled = false;
            }   
        }
    }
}

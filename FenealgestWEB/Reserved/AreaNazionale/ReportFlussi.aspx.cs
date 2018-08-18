using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FenealgestWEB.Utility;
using WIN.BASEREUSE;
using WIN.FENGEST_NAZIONALE.DOMAIN.Workers;
using WIN.FENGEST_NAZIONALE.HANDLERS;
using WIN.TECHNICAL.PERSISTENCE;

namespace FenealgestWEB.Reserved.AreaNazionale
{
    public partial class ReportFlussi : BaseReservedArea
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


        private void InitializeGridView()
        {
            GridView1.Columns.Clear();

            BoundField Label = new BoundField();
            Label.DataField = ("Region");
            Label.HeaderText = "Regione";

            BoundField Data = new BoundField();
            Data.DataField = ("Province");
            Data.HeaderText = "Provincia";

            BoundField Data1 = new BoundField();
            Data1.DataField = ("MigratedWorkers");
            Data1.HeaderText = "Flusso lavoratori";

            BoundField Data2 = new BoundField();
            Data2.DataField = ("MigratedWorkersPercentage");
            Data2.HeaderText = "Flusso lavoratori (%)";

            GridView1.Columns.Add(Label);
            GridView1.Columns.Add(Data);
            GridView1.Columns.Add(Data1);
            GridView1.Columns.Add(Data2);

            GridView1.AutoGenerateColumns = false;
        }

        protected override string AuthorizedFunction
        {
            get
            {
                return "Report flussi lavoratori";
            }
        }

        protected override string SitePagePath
        {
            get
            {
                return "Home -> Area riservata -> Nazionale -> Report flussi lavoratori";
            }
        }

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
            get { return "Report flussi lavoratori"; }
        }

        protected override void InitializePage()
        {
            //Page.Title = FunctionHeaderTitle;
            if (!this.IsPostBack)
            {
                //lblTitolo.Text = FunctionHeaderTitle;
                loadCombos();
            }
        }



        protected override System.Web.UI.WebControls.Image ImageHeader
        {
            get { return null; }
        }

        //protected override Image BkgrTable
        //{
        //    get { return ((ReservedArea)this.Master).BgkImage; }
        //}

        private void loadCombos()
        {
            GeoLocationFacade f = SessionServiceManager.GeoHandlerService;

            if (base.IsNationalUser)//utente nazionale
            {
                IList regioni = f.GetListaRegioni();

                cboRegioni.Items.Clear();
                cboRegioni.Items.Add("#");
                foreach (string item in regioni)
                {
                    cboRegioni.Items.Add(item);
                }
                //si mette sul primo elemento
                cboRegioni.SelectedIndex = 0;

                cboRegioni.Enabled = true;
                cboProvince.Enabled = true;

            }
            else if (string.IsNullOrEmpty(base.UserProvice)) // utente regionale
            {
                cboRegioni.Items.Clear();
                cboRegioni.Items.Add(base.UserRegion);

                IList province = f.GetGeoHandler().GetNomiProviciePerRegione(cboRegioni.Text);
                cboProvince.Items.Clear();
                cboProvince.Items.Add("#");
                foreach (string item in province)
                {
                    cboProvince.Items.Add(item);
                }
                //si mette sul primo elemento
                cboProvince.SelectedValue = base.UserRegion;

                cboRegioni.Enabled = false;
                cboProvince.Enabled = true;

            }
            else //utente provinciale
            {
                cboRegioni.Items.Clear();
                cboRegioni.Items.Add(base.UserRegion);

                IList province = f.GetGeoHandler().GetNomiProviciePerRegione(cboRegioni.Text);
                cboProvince.Items.Clear();
                cboProvince.Items.Add("#");
                foreach (string item in province)
                {
                    cboProvince.Items.Add(item);
                }
                //si mette sul primo elemento
                cboProvince.SelectedValue = base.UserProvice;

                cboRegioni.Enabled = false;
                cboProvince.Enabled = true;
            }

            int anno;
            for (int i = 0; i <= 30; i++)
            {
                anno = i + 2000;
                cboAnno.Items.Add(new ListItem(anno.ToString(), anno.ToString()));
            }
            cboAnno.SelectedValue = DateTime.Now.Year.ToString();
        }

        protected void cboRegioni_SelectedIndexChanged(object sender, EventArgs e)
        {
            GeoLocationFacade f = SessionServiceManager.GeoHandlerService;

            if (base.IsNationalUser)
            {
                IList province = f.GetGeoHandler().GetNomiProviciePerRegione(cboRegioni.Text);
                cboProvince.Items.Clear();
                cboProvince.Items.Add("#");
                foreach (string item in province)
                {
                    cboProvince.Items.Add(item);
                }
                //si mette sul primo elemento
                cboProvince.SelectedIndex = 0;
            }

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            CaricaLista();
            GridView1.Visible = true;
        }

        private void CaricaLista()
        {

           
            GeoLocationFacade f = SessionServiceManager.GeoHandlerService;
            IPersistenceFacade p = SessionServiceManager.PersistentService;

            ListItem item = cboAnniValutazione.SelectedItem;

            int anniValutazione = Convert.ToInt16(item.Value);
            string regionName = cboRegioni.Text;
            string provinceName = cboProvince.Text;
            Regione region = f.GetGeoHandler().GetRegioneByName(regionName);
            Provincia province = f.GetGeoHandler().GetProvinciaByName(provinceName);
            FlowReporting bh = new FlowReporting(p, f);

            IList<WorkerFlow> listFil;


            listFil = bh.LoadFlows(region.Id, province.Id, int.Parse(cboAnno.Text), anniValutazione);


            if (listFil.Count == 0)
            {
                Label1.Visible = true;
                GridView1.Visible = false;
                GridView1.DataSource = listFil;
                GridView1.DataBind();
            }
            else
            {

                Label1.Visible = false;
                GridView1.Visible = true;
                GridView1.DataSource = listFil;
                GridView1.DataBind();
            }

          
           
        }
       

     
    }
}

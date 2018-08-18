using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FenealgestWEB.Utility;
using WIN.BASEREUSE;
using System.Collections;
using WIN.FENGEST_NAZIONALE.DOMAIN.Financial;
using WIN.FENGEST_NAZIONALE.HANDLERS.Financial;
using WIN.TECHNICAL.PERSISTENCE;
using BilancioFenealgest.DomainLayer;
using WIN.FENGEST_NAZIONALE.HANDLERS.Financial.HtmlRenderer;
using System.Drawing;
using System.Text;

namespace FenealgestWEB.Reserved.AreaNazionale
{
    public partial class Bilancio :  BaseReservedArea
    {

        private bool _found = false;
        private string _intestazioneBilancio = "Nessun bilancio visualizzato.";
        private string _quadratura = "";
        private string _conto = "";



        public string Conto
        {
            get
            {
                return _conto;
            }
        }
        protected string quadratura
        {
            get
            {
                return _quadratura;
            }
        }



        protected bool Found
        {
            get
            {
                return _found;
            }
        }

        protected string IntestazioneBilancio
        {
            get
            {
                return _intestazioneBilancio;
            }
        }

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
            get
            {
                return "Ricerca bilancio";
            }
        }

        protected override string SitePagePath
        {
            get
            {
                return "Home -> Area riservata -> Nazionale -> Visualizza bilancio";
            }
        }

        //protected override Menu PageMenu
        //{
        //    get { return ((ReservedArea)this.Master).MainMenu; }
        //}


        protected override DevExpress.Web.ASPxMenu DevPageMenu
        {
            get { return ((FenealgestwebNew)this.Master).MainMenu; }
        }

        protected override DevExpress.Web.ASPxTreeView DevPageTree
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
            get { return "Visualizzazione bilancio"; }
        }

        protected override void InitializePage()
        {
            //Page.Title = FunctionHeaderTitle;
            if (!this.IsPostBack)
            {
                //lblTitolo.Text = FunctionHeaderTitle;
                //Panel1.Visible = false;
                //Panel2.Visible = false;
                //Panel3.Visible = false;
                //lblNotify.Visible = false;
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

            IList regioni = f.GetListaRegioni();

            cboRegioni.Items.Clear();
            //cboRegioni.Items.Add("");
            foreach (string item in regioni)
            {
                cboRegioni.Items.Add(item);
            }
            //si mette sul primo elemento
            cboRegioni.SelectedIndex = 0;

            IList province = f.GetGeoHandler().GetNomiProviciePerRegione(cboRegioni.Text);
            cboProvince.Items.Clear();
            cboProvince.Items.Add(" ");
            foreach (string item in province)
            {
                cboProvince.Items.Add(item);
            }
            //si mette sul primo elemento
            cboProvince.SelectedIndex = 0;

            int anno;
            for (int i = 0; i <= 50; i++)
            {
                anno = i + 2000;
                cboAnno.Items.Add(new ListItem(anno.ToString(), anno.ToString()));
            }
            cboAnno.SelectedValue = DateTime.Now.Year.ToString();
        }

        protected void cboRegioni_SelectedIndexChanged(object sender, EventArgs e)
        {

            GeoLocationFacade f = SessionServiceManager.GeoHandlerService;
            
            IList province = f.GetGeoHandler().GetNomiProviciePerRegione(cboRegioni.Text);
            cboProvince.Items.Clear();
            cboProvince.Items.Add(" ");
            foreach (string item in province)
            {
                cboProvince.Items.Add(item);
            }
            //si mette sul primo elemento
            cboProvince.SelectedIndex = 0;

            setDatiBilancio();
            setVisibility();
        }

        protected void buttonSaveAs_Click(object sender, EventArgs e)
        {
            treeListExporter1.Settings.AutoWidth = true;
            treeListExporter1.Settings.ExpandAllNodes = true;

            switch (DropDownList4.SelectedIndex)
            {
                case 0:
                    treeListExporter1.WritePdfToResponse();
                    break;
                case 1:
                    treeListExporter1.WriteXlsToResponse();
                    break;
                case 2:
                    treeListExporter1.WriteXlsxToResponse();
                    break;
                case 3:
                    treeListExporter1.WriteRtfToResponse(); 
                    break;
                    
            }
        }

        protected void ASPxTreeList1_HtmlRowPrepared(object sender, DevExpress.Web.ASPxTreeList.TreeListHtmlRowEventArgs e)
        {
            if (Object.Equals(e.GetValue("IsLeaf"), "False"))
                e.Row.Font.Bold = true;
        }
        protected void ASPxTreeList2_HtmlRowPrepared(object sender, DevExpress.Web.ASPxTreeList.TreeListHtmlRowEventArgs e)
        {
            if (Object.Equals(e.GetValue("IsLeaf"), "False"))
                e.Row.Font.Bold = true;
        }

        protected void cboProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDatiBilancio();
            setVisibility();

        }

        protected void cboAnno_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDatiBilancio();
            setVisibility();
        }

        protected void cboTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDatiBilancio();
            setVisibility();
        }

        protected void setVisibility()
        {
            //if (ASPxTreeList2.Nodes.Count == 0)
            //{
            //    //lblNotify.Visible = true;
            //    //lblNotify.Text = "Nessun bilancio presente";
            //    //Panel1.Visible = false;
            //    //Panel2.Visible = false;
            //    //Panel3.Visible = false;
            //}
            //else
            //{
            //    //lblNotify.Visible = false;
            //    //Panel1.Visible = true;
            //    //Panel2.Visible = true;
            //    //Panel3.Visible = true;
            //    ASPxTreeList2.ExpandToLevel(2);
            //}
            if (ASPxTreeList2.Nodes.Count != 0)
                ASPxTreeList2.ExpandToLevel(2);

        }

        protected void setDatiBilancio()
        {
            Rendiconto ren ;
            IPersistenceFacade p = SessionServiceManager.PersistentService;

            BilancioHandler bilH = new BilancioHandler(p, SessionServiceManager.GeoHandlerService  );

            bool forRlst = false;
            if (cboTipo.Text == "RLST")
                forRlst = true;

            bilH.LoadRendiconto(cboRegioni.SelectedValue.Trim (), cboProvince.SelectedValue.Trim (), int.Parse(cboAnno.SelectedValue),forRlst);


            if (bilH.Found)
            {
               _found = true;
               if (!forRlst)
                   ren = bilH.Rendiconto;
               else
                   ren = bilH.RendicontoFromTemplate(Server.MapPath("rendiconto.xml"));


               _quadratura =  CreateQuadratura(ren);
               _conto = CreateContoEconomico(ren);

               RenderStatoPatrimoniale(ren.StatoPatrimoniale);
               string province = cboProvince.SelectedValue.Trim();
               if (!string.IsNullOrEmpty(province))
                    _intestazioneBilancio = string.Format("Rendiconto per la provincia di <strong>{0}</strong> (<strong>{1}</strong>) nell'anno <strong>{2}</strong>", cboProvince.SelectedValue, cboRegioni.SelectedValue, cboAnno.SelectedValue);
               else
                   _intestazioneBilancio = string.Format("Rendiconto per la regione <strong>{0}</strong> nell'anno <strong>{1}</strong>", cboRegioni.SelectedValue, cboAnno.SelectedValue);
            }
            else
            {
                _found = false;
               // lblNotify.Text = "Non è stato trovato nessun bilancio!";
                string province = cboProvince.SelectedValue.Trim();
                if (!string.IsNullOrEmpty(province))
                    _intestazioneBilancio = string.Format("Non è stato trovato alcun rendiconto per la provincia di <strong>{0}</strong> nell'anno <strong>{1}</strong>", cboProvince.SelectedValue, cboAnno.SelectedValue);
                else
                    _intestazioneBilancio = string.Format("Non è stato trovato alcun rendiconto per la regione <strong>{0}</strong> nell'anno <strong>{1}</strong>", cboRegioni.SelectedValue, cboAnno.SelectedValue);
                //litAuto.Text = "";
                //litImmobili.Text = "";
                //litDepositi.Text = "";
                //litPolizze.Text = "";
            }

            //lblProvincia.Text = cboProvince.SelectedValue;
            //lblRegione.Text = cboRegioni.SelectedValue;
            //lblAnno.Text = cboAnno.SelectedValue;
            
            
        }

        private string CreateQuadratura(Rendiconto ren)
        {
  

            double tot = ren.Bilancio.GetTotal;
            string fonti = ren.Bilancio.Attivita.GetTotal.ToString("c");
            string impieghi = ren.Bilancio.Passivita.GetTotal.ToString("c");
            //inserisco queste tre righe di codice per correggere la ricostruzione del bilancio
            //dato che la i conti finanziari non sono gestiti a saldo (si riporta la voce iniziale come un valore nel
            //conto anzichè come un semplice valore iniziale
            //double diffCassa = 0;// ren.Bilancio.CassaIniziale.GetTotal;
            //double diffAccantonamento = 0;//ren.Bilancio.AccantonamentoIniziale.GetTotal;
            //double diffB1 = 0;//ren.Bilancio.Banca1Iniziale.GetTotal;
            //double diffB2 = 0;//ren.Bilancio.Banca2Iniziale.GetTotal;
            //double diffB3 = 0;//ren.Bilancio.Banca3Iniziale.GetTotal;

            double imp = ren.Bilancio.Attivita.GetTotal - ren.Bilancio.Passivita.GetTotal;//ren.Bilancio.Impieghi1 - diffCassa - diffAccantonamento - diffB1 - diffB2 - diffB3;
         
            //non prendo il valore restituito perchè la ricostruzione 
            //non tiene conto che la finanza finale non è 
            //gestita a saldo
            //string impieghi = imp.ToString("c");
          

            
            StringBuilder b = new StringBuilder();

            b.Append("<ul style=\"list-style:none;\">");

            b.Append(string.Format("<li><img src=\"../../images/info.png\" /> <strong>Attivita':      </strong> {0}</li>", fonti));
            b.Append(string.Format("<li><img src=\"../../images/info.png\" /> <strong>Passivita':   </strong> {0}</li>", impieghi));
            b.Append(string.Format("<li><img src=\"../../images/info.png\" /> <strong >Avanzo: </strong> {0}</li>", imp.ToString("c")));


            b.Append("</ul>");

            return  b.ToString();

        }

        private string CreateContoEconomico(Rendiconto ren)
        {
            string quadratura = ren.Bilancio.GetTotal.ToString("c");
            string entrate = ren.Bilancio.Entrate.Importo.ToString("c");
            string spese = ren.Bilancio.Spese.Importo.ToString("c");

            double av = ren.Bilancio.Entrate.GetTotal - ren.Bilancio.Spese.GetTotal;
            string avanzo = av.ToString("c");



            StringBuilder b = new StringBuilder();

            b.Append("<ul style=\"list-style:none;\">");

            b.Append(string.Format("<li><img src=\"../../images/info.png\" /> <strong>Entrate:      </strong> {0}</li>", entrate));
            b.Append(string.Format("<li><img src=\"../../images/info.png\" /> <strong>Uscite:       </strong> {0}</li>", spese));
            b.Append(string.Format("<li><img src=\"../../images/info.png\" /> <strong>Avanzo: </strong> {0}</li>", avanzo));


            b.Append("</ul>");

            return b.ToString();


        }

        private void RenderStatoPatrimoniale(StatoPatrimoniale statoPatrimoniale)
        {
            StatoPatrimonialeHtmlRenderer r = new StatoPatrimonialeHtmlRenderer(statoPatrimoniale);

            r.Render();

            if (string.IsNullOrEmpty(r.Polizze.Trim()))
                litPolizze.Text = "Nessuna polizza trovata!";
            else
                litPolizze.Text = r.Polizze;

            if (string.IsNullOrEmpty(r.Auto.Trim()))
                litAuto.Text = "Nessuna auto trovata!";
            else
                litAuto.Text = r.Auto;

            if (string.IsNullOrEmpty(r.Immobili.Trim()))
                litImmobili.Text = "Nessun immobile trovato!";
            else
                litImmobili.Text = r.Immobili;


            if (string.IsNullOrEmpty(r.Depositi.Trim()))
                litDepositi.Text = "Nessun deposito trovato!";
            else
                litDepositi.Text = r.Depositi;

            if (string.IsNullOrEmpty(r.Mobili.Trim()))
                litMobili.Text = "Nessun bene mobile trovato!";
            else
                litMobili.Text = r.Mobili;

            if (string.IsNullOrEmpty(r.Accantonamenti.Trim()))
                litAccantonamenti.Text = "Nessun accantonamento TFR trovato!";
            else
                litAccantonamenti.Text = r.Accantonamenti;

            if (string.IsNullOrEmpty(r.Chiusure.Trim()))
                litChiusure.Text = "Nessun Avanzo/disavanzo trovato!";
            else
                litChiusure.Text = r.Chiusure;

            //litImmobili.Text = r.Immobili;
            //litDepositi.Text = r.Depositi;


        }

        protected void btnExpand_Click(object sender, EventArgs e)
        {
            setDatiBilancio();
            setVisibility();
            ASPxTreeList2.ExpandAll();
        }

        protected void btnCollapse_Click(object sender, EventArgs e)
        {
            setDatiBilancio();
            setVisibility();
            ASPxTreeList2.CollapseAll();
        }
 
    }
}

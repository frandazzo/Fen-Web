using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FenealgestWEB.Utility;
using System.Collections;
using WIN.BASEREUSE;
using System.Text;

namespace FenealgestWEB.Reserved.AreaPubblica
{
    public partial class CodiceFiscale : Utility.BaseReservedArea 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //esegue la verifica che un utetne è loggato;
            //inizializza le info base della pagina insieme con 
            //l'inizializzaizone del menu per l'utente
            InitializeBasePage();

          
            //inserire qui altro codice di inizializzaizone della pagina
            InitializePage();
        }



        protected override string AuthorizedFunction
        {
            get { return ""; }
        }

        protected override string SitePagePath
        {
            get { return "Home -> Area riservata -> Area pubblica -> Calcola codice fiscale"; }
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
            get { return "Calcola codice fiscale"; }
        }

        protected override void InitializePage()
        {
            //Page.Title = FunctionHeaderTitle;
            if (!this.IsPostBack)
            {
                bdpData1.Date = DateTime.Now.Date;
                //lblTitolo.Text = FunctionHeaderTitle;

                //carico le combo
                LoadComboNazioni();

                //Posizioniamoci sulla naziona italia

                cboNazione.Text = "ITALIA";

                //Carico le provincie
                LoadComboProvince();

                //carico il comune
                LoadComboComuni(cboProvincia.Text);


            
            }
        }

     

        private void LoadComboNazioni()
        {
            cboNazione.Items.Clear();

            foreach (string item in SessionServiceManager.GeoHandlerService.GetListaNazioni())
            {
                cboNazione.Items.Add(new ListItem(item));
            }


            cboNazione.SelectedIndex = 0;

        }



        private void LoadComboProvince()
        {
            IList provincie = SessionServiceManager.GeoHandlerService.GetListaProvincie();

            cboProvincia.Items.Clear();
            foreach (string item in provincie)
            {
                cboProvincia.Items.Add(item);
            }
            //si mette sul primo elemento
            cboProvincia.SelectedIndex = 0;
        }



        private void LoadComboComuni(string nomeProvincia)
        {
            IList comuni = SessionServiceManager.GeoHandlerService.GetListaComuniPerProvincia(nomeProvincia);

            cboComune.Items.Clear();
            foreach (string item in comuni)
            {
                cboComune.Items.Add(item);
            }
            //si mette sul primo elemento
            cboComune.SelectedIndex = 0;
        }


        protected override Image ImageHeader
        {
            get { return null; }
        }

        //protected override Image BkgrTable
        //{
        //    get { return ((ReservedArea)this.Master).BgkImage; }
        //}

        protected void cmdCalcolaCodiceFiscale_Click(object sender, EventArgs e)
        {
            lblDatiFiscali1.Text = "-";
            txtCodiceFiscale.Text = SessionServiceManager.GeoHandlerService.CalcolaCodiceFiscale(txtNome.Text, txtCognome.Text, cboSesso.Text, bdpData1.Date, cboComune.Text, cboNazione.Text);
        }

        protected void cmdDatiFisc_Click(object sender, EventArgs e)
        {
            try
            {
                DatiFiscali f = SessionServiceManager.GeoHandlerService.CalcolaDatiFiscali(txtCodiceFiscale.Text);

                StringBuilder b = new StringBuilder();

                b.AppendLine("Data nascita: " + f.DataNascita.ToShortDateString());

                string sesso = "";

                if (f.SessoPersona == "0")
                    sesso = "Maschio";
                else
                    sesso = "Femmina";

                b.AppendLine("Sesso: " + sesso);
                b.AppendLine("Nazione: " + f.Nazione.Descrizione);
                b.AppendLine("Provincia: " + f.Provincia.Descrizione);
                b.AppendLine("Comune: " + f.Comune.Descrizione);

                lblDatiFiscali1.Text = b.ToString();

            }
            catch (Exception ex)
            {
                lblDatiFiscali1.Text = ex.Message;
            }
        }

        protected void cboProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadComboComuni(cboProvincia.Text);
        }

        protected void cboNazione_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNazione.Text == "ITALIA")
            {
                cboProvincia.Enabled = true;
                cboComune.Enabled = true;
                LoadComboProvince();
                LoadComboComuni(cboProvincia.Text);
            }
            else
            {
                cboProvincia.Items.Clear();
                cboComune.Items.Clear();
                cboProvincia.Enabled = false;
                cboComune.Enabled = false;
            }
        }

       





    }
}

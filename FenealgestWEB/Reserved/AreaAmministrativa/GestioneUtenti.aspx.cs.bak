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
using WIN.FENGEST_NAZIONALE.DOMAIN;
using WIN.TECHNICAL.PERSISTENCE;
using WIN.FENGEST_NAZIONALE.DOMAIN.Security;
using System.Web.Configuration;
using WIN.BASEREUSE;
using WIN.TECHNICAL.SECURITY_NEW.RoleManagement;
using System.Collections.Generic;

namespace FenealgestWEB.Reserved.AreaAmministrativa
{
    public partial class GestioneUtenti : BaseReservedArea
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
            get { return "Ricerca utenti"; }
        }

        protected override string SitePagePath
        {
            get { return "Home -> Area riservata -> Amministrazione -> Ricerca utenti"; }
        }

        //protected override Menu PageMenu
        //{
        //    get { return ((ReservedArea)this.Master).MainMenu; }
        //}

        protected override DevExpress.Web.ASPxTreeView.ASPxTreeView DevPageTree
        {
            get { return null; }
        }
        protected override DevExpress.Web.ASPxMenu.ASPxMenu DevPageMenu
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
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
                { return "Aggiorna utenti"; }
                else
                { return "Crea utenti"; }
            }
        }

        protected override void InitializePage()
        {
            //Page.Title = FunctionHeaderTitle;
            if (!this.IsPostBack)
            {
                //lblTitolo.Text = FunctionHeaderTitle;

                LoadListBox();

                //Verifico se sono in modalità di update
                //e nel caso carico i dati dell'utente da modificare
                if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
                {
                    LoadUtente(Request.QueryString["Id"]);
                    rfvListBox1.Enabled = false;
                    rfvPass.Enabled = false;
                    cmdSave1.Text = "Aggiorna utente";
                }
                //altrimenti inizializzo i valori
                else
                {
                    LoadStrutture();
                    cboRegione.Items.Clear();
                    cboProvincia.Items.Clear();
                    ListBox1.Items.Clear();
                    cboRegione.Enabled = false;
                    cboProvincia.Enabled = false;
                    bdpPass1.Date = DateTime.Now;
                    cmdSave1.Text = "Inserisci nuovo utente";
                }
            }
        }

        private void LoadListBox()
        {

            IRoleProvider p = SessionServiceManager.SecurityService.RoleProvider;


            IList<WIN.TECHNICAL.SECURITY_NEW.RoleManagement.Role> lista = p.GetRoles();

            ListBox2.Items.Clear();


            foreach (WIN.TECHNICAL.SECURITY_NEW.RoleManagement.Role item in lista)
            {

                ListBox2.Items.Add(item.Name);
            }



        }

        private void LoadUtente(string id)
        {
            IPersistenceFacade p = SessionServiceManager.PersistentService;

            Utente u = p.GetObject("Utente", System.Convert.ToInt32(id)) as Utente;

            //imposto l'interfaccia utente
            if (u != null)
            {
                LoadObjectProperties(u);
                return;
            }
            throw new Exception("Id dell'utente non valido!");

            
        }

        private void LoadObjectProperties(Utente p)
        {
            //Valorizzo i campi del form legato all'utente

            txtCognome.Text = p.Cognome;
            txtNome.Text = p.Nome;
            txtMail.Text = p.Mail;

            string[] roles = p.ToRoleDescriptor().Split(';');
            foreach(string role in roles)
                if(!string.IsNullOrEmpty(role))
                     ListBox1.Items.Add(role); 

            chkLocked.Checked = p.Locked;

            LoadStrutture();
            cboTipo.SelectedValue = p.Appartenenza.Struttura.ToString();

            if (p.Appartenenza.Struttura.ToString().Equals("Feneal_Nazionale"))
            {
                //se la struttura è nazionale svuoto e disabilito  i combo per la regione e la provincia
                cboRegione.Items.Clear();
                cboProvincia.Items.Clear();
                cboProvincia.Enabled = false;
                cboRegione.Enabled = false;
            }

            else
            {
                LoadComboRegioni();
                cboRegione.Enabled = true;
                if ((p.Appartenenza.Regione != null))
                    cboRegione.SelectedValue = p.Appartenenza.Regione.Descrizione;
                cboProvincia.Items.Clear();
                cboProvincia.Enabled = false;

                if (p.Appartenenza.Struttura.ToString().Equals("Feneal_Provinciale"))
                {
                    LoadComboProvince(p.Appartenenza.Regione.Descrizione);
                    cboProvincia.Enabled = true;
                    if ((p.Appartenenza.Provicnia != null))
                        cboProvincia.SelectedValue = p.Appartenenza.Provicnia.Descrizione;
                }
            }
            txtUser.Text = p.UserName;

            //L'utente non è mai mostrato
            //txtPass.Text = p.Password;
            bdpPass1.Date = p.PasswordData;
            lblDataDeclay.Text = p.PasswordDecay.ToString("dd-MMM-yyyy");
        }

        private void LoadStrutture()
        {
            //recuoero le stringhe
            string[] arr = Enum.GetNames(typeof(StrutturaApparteneza));

            cboTipo.Items.Clear();

            foreach (string item in arr)
            {
                cboTipo.Items.Add(item);
            }
            //si mette sul primo elemento
            cboTipo.SelectedValue = "Feneal_Nazionale";
        }


        private void LoadComboRegioni()
        {
            //richiediamo il seervizio
            GeoLocationFacade f = SessionServiceManager.GeoHandlerService;

            //pulisco e aggingo tutte le voci compresa una prima voce vuota
            cboRegione.Items.Clear();
            foreach (string item in f.GetListaRegioni())
            {
                cboRegione.Items.Add(item);
            }
            //si mette sul primo elemento
            cboRegione.SelectedIndex = 0;
        }


        private void LoadComboProvince(string regione)
        {
            //richiediamo il seervizio
            GeoLocationFacade f = SessionServiceManager.GeoHandlerService;

            //pulisco e aggingo tutte le voci compresa una prima voce vuota
            cboProvincia.Items.Clear();
            foreach (string item in f.GetGeoHandler().GetNomiProviciePerRegione (regione))
            {
                cboProvincia.Items.Add(item);
            }
            //si mette sul primo elemento
            cboProvincia.SelectedIndex = 0;
        }

        protected override Image ImageHeader
        {
            get { return null; }
        }

        //protected override Image BkgrTable
        //{
        //    get { return ((ReservedArea)this.Master).BgkImage; }
        //}

        protected void cmdCreaUtente_Click(object sender, EventArgs e)
        {
            ////azzero i campi del form
            //txtCognome.Text = "";
            //txtNome.Text = "";
            //txtMail.Text = "";
            //ListBox1.Items.Clear();

            //LoadStrutture();
            //cboRegione.Items.Clear();
            //cboProvincia.Items.Clear();
            //cboRegione.Enabled = false;
            //cboProvincia.Enabled = false;
            
            //txtUser.Text = "";
            //txtPass.Text = "";
            //chkLocked.Checked = false;

            //bdpPass.Clear();
            //lblDataDeclay.Text = "";

            Response.Redirect(LinkHandler.GestioneUtenti);

        }

        protected void cboRegione_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipo.SelectedValue=="Feneal_Provinciale")
            {

                //L'utnete non è nazionale ed è di tipo provinciale.
                //Pertanto devo abilitare e settare le province della regione scelta
                LoadComboProvince(cboRegione.SelectedValue);
                cboProvincia.Enabled = true;

            }
        }

        protected void cboTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipo.SelectedValue == "Feneal_Nazionale")
            {
                cboRegione.Items.Clear();
                cboProvincia.Items.Clear();
                cboRegione.Enabled = false;
                cboProvincia.Enabled = false;
            }
            else if (cboTipo.SelectedValue == "Feneal_Regionale")
            {
                LoadComboRegioni();
                cboRegione.Enabled = true;
                cboProvincia.Items.Clear();
                cboProvincia.Enabled = false;
            }
            else 
            {
                LoadComboRegioni();
                cboRegione.Enabled = true;
                LoadComboProvince(cboRegione.SelectedValue);
                cboProvincia.Enabled = true;
            }
        }

        protected void btnDeleteRole_Click(object sender, EventArgs e)
        {
            //controllo di aver selezionato almeno un ruolo e in caso positivo avviene la cancellazione
            if (ListBox1.GetSelectedIndices().Length > 0)
                    ListBox1.Items.Remove(ListBox1.SelectedItem);
        }

        protected void btnAddRole_Click(object sender, EventArgs e)
        {
            bool trovato;
            //controllo di aver selezionato almeno un ruolo dalla lista di tutti i ruoli
            if (ListBox2.GetSelectedIndices().Length > 0)

                foreach (int i in ListBox2.GetSelectedIndices())
                {
                    //controllo che il ruolo non sia già presente
                    trovato = false;
                    for (int t = 0; t < ListBox1.Items.Count; t++)
                        if (ListBox1.Items[t].Text.Equals(ListBox2.Items[i].Text))
                            trovato = true;
                    //se il ruolo non è già presente lo inserisco
                    if (!trovato)
                        ListBox1.Items.Add(ListBox2.Items[i].Text);
                    ListBox1.SelectedValue = ListBox2.Items[i].Text;
                }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

            //codice per aggiornare
            IPersistenceFacade p = SessionServiceManager.PersistentService;
            //prendo il servizio geografica
            GeoLocationFacade f = SessionServiceManager.GeoHandlerService;

            if (cmdSave1.Text == "Aggiorna utente")
            {
                Utente u = p.GetObject("Utente", System.Convert.ToInt32(Request.QueryString["Id"])) as Utente;

                if (u != null)
                {
                    SetBaseData(f, u);

                    if (!string.IsNullOrEmpty(txtPass.Text))
                        u.Password = txtPass.Text;

                    p.UpdateObject("Utente", u);


                }
            }
            else
            {
                Utente u = new Utente();
                SetBaseData(f, u);
                u.Password = txtPass.Text;
                p.InsertObject("Utente", u);
                Response.Redirect(LinkHandler.ConstructSimpleQueryString (LinkHandler.GestioneUtenti, "ID", u.Id.ToString()));
            }


        }

        private void SetBaseData(GeoLocationFacade f, Utente u)
        {
            u.Cognome = txtCognome.Text;
            u.Nome = txtNome.Text;
            u.Mail = txtMail.Text;
            u.Appartenenza.Struttura = (WIN.FENGEST_NAZIONALE.DOMAIN.StrutturaApparteneza)Enum.Parse(typeof(WIN.FENGEST_NAZIONALE.DOMAIN.StrutturaApparteneza), cboTipo.Text);

            Provincia prov = f.GetGeoHandler().GetProvinciaByName(cboProvincia.Text);
            if (prov.Id != -1)
                u.Appartenenza.Provicnia = prov;



            Regione reg = f.GetGeoHandler().GetRegioneByName(cboRegione.Text);
            if (reg.Id != -1)
                u.Appartenenza.Regione = reg;


            u.PasswordData = bdpPass1.Date;
            u.Locked = chkLocked.Checked;




            u.UserName = txtUser.Text;

            u.Roles.Clear();

            foreach (ListItem item in ListBox1.Items)
            {
                u.AddRole(SessionServiceManager.SecurityService.GetRoleByName(item.Text));
            }
        }

        protected void cmdAnnulla_Click(object sender, EventArgs e)
        {
            Response.Redirect(LinkHandler.LinkRicercaUtenti);
        }

        protected void cmdDeleteUtente_Click(object sender, EventArgs e)
        {
            try
            {
                //codice per cancellare un utente
                IPersistenceFacade p = SessionServiceManager.PersistentService;

                Utente u = p.GetObject("Utente", System.Convert.ToInt32(Request.QueryString["Id"])) as Utente;

                if (u != null)
                {
                    p.DeleteObject("Utente", u);
                    Response.Redirect(LinkHandler.LinkRicercaUtenti);
                }
            }
            catch (Exception)
            {
                //non fa nulla
            }
            
        }
    }
}

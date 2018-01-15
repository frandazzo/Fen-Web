using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using WIN.BASEREUSE;
using WIN.WEBCONNECTOR.controls;
using WIN.WEBCONNECTOR.GeoElements;
using WIN.WEBCONNECTOR.ServiceAgents;
using WIN.WEBCONNECTOR.SharetopServices;

namespace WIN.WEBCONNECTOR
{
    public partial class TestSharetoIntegration : XtraForm
    {

        private OrganizzativeData _current;


        public TestSharetoIntegration()
        {
            InitializeComponent();
            LoadComboRegioni();
            SelectProvince(cboRegione.SelectedItem.ToString ());
            LoadComboAnni();

            XtraUserControl c = new DatiStrutturaControl(this);
            RenderSubControl(c);


            RicercaDati();
        }

        private void LoadComboAnni()
        {
            cboAnno.Properties.Items.Add("2010");
            cboAnno.Properties.Items.Add("2011");
            cboAnno.Properties.Items.Add("2012");
            cboAnno.Properties.Items.Add("2013");
            cboAnno.Properties.Items.Add("2014");
            cboAnno.Properties.Items.Add("2015");
            cboAnno.Properties.Items.Add("2016");
            cboAnno.Properties.Items.Add("2017");
            cboAnno.Properties.Items.Add("2018");
            cboAnno.Properties.Items.Add("2019");
            cboAnno.Properties.Items.Add("2020");
            cboAnno.Properties.Items.Add("2021");
            cboAnno.Properties.Items.Add("2022");
            cboAnno.Properties.Items.Add("2023");
            cboAnno.Properties.Items.Add("2024");
            cboAnno.Properties.Items.Add("2025");
            cboAnno.Properties.Items.Add("2026");
            cboAnno.SelectedIndex = 0;

            cboAnno.SelectedItem = DateTime.Now.Year.ToString(); 
        }


        private void LoadComboRegioni()
        {
            IList regioni = GeoHandlerProvider.Instance.Geo.GetListaRegioni();
            foreach (string item in regioni)
            {
                cboRegione.Properties.Items.Add(item);
            }

            cboRegione.Properties .Sorted = true;


            cboRegione.SelectedIndex = 0;

        }


        private void RenderSubControl(XtraUserControl control)
        {
            if (panelContainer.Controls.Count > 0)
            {
                panelContainer.Controls[0].Dispose();
                panelContainer.Controls.Clear();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            panelContainer.Controls.Add(control);
            control.Dock = DockStyle.Fill;
        }

        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            XtraUserControl c = new DatiStrutturaControl(this);
            RenderSubControl(c);

            //faccio partire la ricerca
            IPrintableAndSearchable s = c as IPrintableAndSearchable;
           

            s.LoadData(_current);
        }

        private void navBarItem2_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            XtraUserControl c = new OrganismiRegionaliControl(this);
            RenderSubControl(c);

            //faccio partire la ricerca
            IPrintableAndSearchable s = c as IPrintableAndSearchable;
          

            s.LoadData(_current);
        }

        private void navBarItem3_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            XtraUserControl c = new OrganismiTerritorialiControl (this);
            RenderSubControl(c);

            //faccio partire la ricerca
            IPrintableAndSearchable s = c as IPrintableAndSearchable;
           
            s.LoadData(_current);
        }

        private void navBarItem4_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            XtraUserControl c = new RapprersentanzeControl(this);
            RenderSubControl(c);

            //faccio partire la ricerca
            IPrintableAndSearchable s = c as IPrintableAndSearchable;
           

            s.LoadData(_current);
        }

        private void navBarItem5_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            XtraUserControl c = new RappresentantiControl (this);
            RenderSubControl(c);

            //faccio partire la ricerca
            IPrintableAndSearchable s = c as IPrintableAndSearchable;
           

            s.LoadData(_current);


        }

        private int RetrieveRegione()
        {
            string selecte = cboRegione.EditValue.ToString();
            if (String.IsNullOrEmpty(selecte))
                return -1;

            WIN.BASEREUSE.Regione r = GeoHandlerProvider.Instance.Geo.GetGeoHandler().GetRegioneByName(selecte);

            return r.Id;
        }

        private int RetrieveProvincia()
        {
            string selecte = cboTerritorio.EditValue.ToString(); 
            if (String.IsNullOrEmpty(selecte))
                return -1;

            WIN.BASEREUSE.Provincia r = GeoHandlerProvider.Instance.Geo.GetGeoHandler().GetProvinciaByName(selecte);

            return r.Id;
        }

        private void cboRegione_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectProvince(cboRegione.EditValue.ToString());
        }

        private void SelectProvince(string p)
        {
            cboTerritorio.Properties.Items.Clear();
            if (String.IsNullOrEmpty(p))
            {
                return;
            }

            //altrimenti recupero l'id della regione e ne richiedo le province

            IList province = GeoHandlerProvider.Instance.Geo.GetGeoHandler().GetNomiProviciePerRegione(p);
            cboTerritorio.Properties.Items.Add("");
            foreach (string item in province)
            {
                cboTerritorio.Properties.Items.Add(item);
            }

            cboTerritorio.Properties.Sorted = true;


            cboTerritorio.SelectedIndex = 0;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            RicercaDati();
        }

        private void RicercaDati()
        {
            //recupero il controllo dal panel container
            IPrintableAndSearchable s = panelContainer.Controls[0] as IPrintableAndSearchable;

            //prima di caricare i dati imposto la label che mi indica il tipo di filtro che
            //viene fatto
            string anno = cboAnno.EditValue.ToString();
            string regione = cboRegione.EditValue.ToString();
            string territorio = cboTerritorio.EditValue.ToString();

            if (String.IsNullOrEmpty(territorio))
                lblFilter.Text = String.Format("Dati struttura regionale {0} all'anno {1}", regione, anno);
            else
                lblFilter.Text = String.Format("Dati struttura territoriale {0} all'anno {1}", territorio, anno);

            LoadData();


            if (s != null)
                s.LoadData(_current);
        }

        private void LoadData()
        {
            try
            {

                WIN.GUI.UTILITY.Helper.ShowWaitBox("Richiesta dati in corso...", Properties.Resources.Waiting3);

                _current = new SharetopIntegrationServiceAgent().FindOrganizzativeData(RetrieveRegione(), RetrieveProvincia(), RetrieveAnno());

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                WIN.GUI.UTILITY.Helper.HideWaitBox();
            }



        }

        private int RetrieveAnno()
        {
            string anno = cboAnno.EditValue.ToString();
            if (String.IsNullOrEmpty(anno))
                return -1;

            try
            {
                return Int32.Parse(anno);
            }
            catch (Exception)
            {

                return -1;
            }
            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
             //recupero il controllo dal panel container
            IPrintableAndSearchable s = panelContainer.Controls[0] as IPrintableAndSearchable;

            if (s != null)
                s.PrintReport(_current);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            TestServiceAgent s = new TestServiceAgent();
            s.SaveRappresentanze();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TestServiceAgent f = new TestServiceAgent();
            f.SaveRappresentanze();
            f.SaveCongressoRegionale();
            f.SaveCongressoTerritoriale();
            f.SaveStruttura();
            f.SaveRappresentanti();
        }

        private void navBarItem7_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            FrmCongressPresence pres = new FrmCongressPresence();
            pres.ShowDialog();

            pres.Dispose();
        }

        private void reportcasseedili_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            XtraUserControl c = new ReportDatiCasseEdiuli(this);
            RenderSubControl(c);

            //faccio partire la ricerca
            IPrintableAndSearchable s = c as IPrintableAndSearchable;


            s.LoadData(null);
        }

        private void risorseumane_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            XtraUserControl c = new RisorseUmaneControl(this);
            RenderSubControl(c);

            //faccio partire la ricerca
            IPrintableAndSearchable s = c as IPrintableAndSearchable;


            s.LoadData(_current);
        }

        private void reportRisUmane_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            XtraUserControl c = new ReportRisorseUmane(this);
            RenderSubControl(c);

            //faccio partire la ricerca
            IPrintableAndSearchable s = c as IPrintableAndSearchable;


            s.LoadData(null);
        }

        private void navBarItem8_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            XtraUserControl c = new ReportDatiEconomiciControl(this);
            RenderSubControl(c);

            //faccio partire la ricerca
            IPrintableAndSearchable s = c as IPrintableAndSearchable;


            s.LoadData(null);
        }

        private void navBarItem9_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            XtraUserControl c = new ReportDatiOrganizzativiControl(this);
            RenderSubControl(c);

            //faccio partire la ricerca
            IPrintableAndSearchable s = c as IPrintableAndSearchable;


            s.LoadData(null);
        }
      

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;

namespace WIN.WEBCONNECTOR
{
    public partial class FrmViewDataForAzienda : XtraForm
    {
        IBindingList _workers1;
        IBindingList _workers2;
        IBindingList _workers3;
        public FrmViewDataForAzienda(IBindingList workers)
        {
            InitializeComponent();
            IList<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO> l = new List<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO>();
            foreach (WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO item in workers)
            {
                    l.Add(item);
            }

            _workers1 = new BindingList<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO>(Retrieve(l, "GENERALE"));
            _workers2 = new BindingList<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO>(Retrieve(l, "FENEAL"));
            _workers3 = new BindingList<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO>(Retrieve(l, ""));
            LoadWorkers();
        }

        private IList<FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO> Retrieve(IList<FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO> l, string p)
        {
            IList<FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO> result = new List<FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO>();


            foreach (var item in l)
            {
                if (string.IsNullOrEmpty(p))
                {
                    //devo verificare se iscritto è è uguale a "" o "Filca" o "fillea"
                    if (item.IscrittoA.Equals("FILCA") || item.IscrittoA.Equals("FILLEA") || item.IscrittoA.Equals(""))
                        result.Add(item);
                }
                else
                {
                    if (item.IscrittoA.Equals(p))
                        result.Add(item);
                }
                

            }


            return result;
        }

        private void LoadWorkers()
        {
           

            //serve per la gestione del master detail della griglia devexpress
            foreach (WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO item in _workers1)
            {
                item.AllineaListe();
            }

            gridControlLavoratori1.DataSource = _workers1;


            foreach (WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO item in _workers2)
            {
                item.AllineaListe();
            }

            gridControl1.DataSource = _workers2;


            foreach (WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO item in _workers3)
            {
                item.AllineaListe();
            }

            gridControl2.DataSource = _workers3;
          
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
            link.Component = gridControlLavoratori1;
            link.PaperKind = System.Drawing.Printing.PaperKind.A4;
            link.ShowPreview();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmAnnoIscrizione frmAnno = new FrmAnnoIscrizione();
            if (frmAnno.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string anni = frmAnno.AnnoIscrizione;

                if (string.IsNullOrEmpty(anni))
                    return;

                string[] anniSplitted = anni.Split(';');

                IBindingList resultToShow = new BindingList<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO>();

                foreach (WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO item in _workers1)
                {
                    if (HasSubscriptionsInYear(item, anniSplitted))
                    {
                        resultToShow.Add(item);
                    }
                }

              
                gridControlLavoratori1.DataSource = null;
              

             
                gridControlLavoratori1.DataSource = resultToShow;
            }
            frmAnno.Dispose();

        }

        private bool HasSubscriptionsInYear(FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO worker, string[] anniSplitted)
        {
            bool result = false;

            if (worker.Subscriptions == null)
                return result;

            foreach (WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.SubscriptionDTO item in worker.Subscriptions)
            {
                foreach (string filter in anniSplitted)
                {
                    if (item.Year.ToString().Equals(filter))
                        return true;
                }


            }

            return result;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            gridControlLavoratori1.DataSource = null;
            gridControlLavoratori1.DataSource = _workers1;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
            link.Component = gridControl1;
            link.PaperKind = System.Drawing.Printing.PaperKind.A4;
            link.ShowPreview();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
            link.Component = gridControl2;
            link.PaperKind = System.Drawing.Printing.PaperKind.A4;
            link.ShowPreview();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            gridControl2.DataSource = null;
            gridControl2.DataSource = _workers3;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            gridControl1.DataSource = null;
            gridControl1.DataSource = _workers2;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FrmAnnoIscrizione frmAnno = new FrmAnnoIscrizione();
            if (frmAnno.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string anni = frmAnno.AnnoIscrizione;

                if (string.IsNullOrEmpty(anni))
                    return;

                string[] anniSplitted = anni.Split(';');

                IBindingList resultToShow = new BindingList<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO>();

                foreach (WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO item in _workers2)
                {
                    if (HasSubscriptionsInYear(item, anniSplitted))
                    {
                        resultToShow.Add(item);
                    }
                }


                gridControl1.DataSource = null;



                gridControl1.DataSource = resultToShow;
            }
            frmAnno.Dispose();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            FrmAnnoIscrizione frmAnno = new FrmAnnoIscrizione();
            if (frmAnno.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string anni = frmAnno.AnnoIscrizione;

                if (string.IsNullOrEmpty(anni))
                    return;

                string[] anniSplitted = anni.Split(';');

                IBindingList resultToShow = new BindingList<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO>();

                foreach (WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO item in _workers3)
                {
                    if (HasSubscriptionsInYear(item, anniSplitted))
                    {
                        resultToShow.Add(item);
                    }
                }


                gridControl2.DataSource = null;



                gridControl2.DataSource = resultToShow;
            }
            frmAnno.Dispose();
        }

      
    }
}

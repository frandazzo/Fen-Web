using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WIN.BASEREUSE;
using WIN.WEBCONNECTOR.GeoElements;
using WIN.WEBCONNECTOR.Credential;
using WIN.TECHNICAL.MIDDLEWARE.Internet;
using System.Collections;

namespace WIN.WEBCONNECTOR
{
    public partial class FrmQueryMultipleWorkers : Form
    {

     //   private IList<string> _codes = new List<string>();

        public FrmQueryMultipleWorkers()
        {
            InitializeComponent();
            listBox1.Sorted = true;

        }




        public FrmQueryMultipleWorkers(IList<string> codes)
        {
            InitializeComponent();
       //     _codes = codes;

            listBox1.Items.Clear();


            int i = 0;
            foreach (string item in codes)
            {
                //if (i > Properties.Settings.Default.MaxFiscalCodeRequests)
                //{
                //    MessageBox.Show("Saranno presi in considerazione solo i primi " + Properties.Settings.Default.MaxFiscalCodeRequests.ToString()+ " codici fiscali inseriti!", "Messaggio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    break;
                //}
                //else
                //{
                    listBox1.Items.Add(item);
                    i++;
                //}
            }
            listBox1.Sorted = true;
            label5.Text = "Codici da ricercare: " + listBox1.Items.Count.ToString();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            FrmCalcolaCodiceFisclae frm = new FrmCalcolaCodiceFisclae();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = frm.CodiceCalcolato;
            }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            //se il codice fiscale è corretto lo inserisco nella listbox

            try
            {

                if (textBox1.Text.Trim() == "")
                    return;

                if (listBox1.Items.Count > Properties.Settings.Default.MaxFiscalCodeRequests)
                {
                    MessageBox.Show("Limite di " + Properties.Settings.Default.MaxFiscalCodeRequests.ToString() + "codici fiscali raggiunto!", "Messaggio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                if (listBox1.Items.Contains(textBox1.Text.ToUpper()))
                {
                    MessageBox.Show("Elemento già inserito", "Messaggio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                WIN.BASEREUSE.DatiFiscali f = GeoElements.GeoHandlerProvider.Instance.Geo.CalcolaDatiFiscali(textBox1.Text);

                listBox1.Items.Add(textBox1.Text.ToUpper());


                label5.Text = "Codici da ricercare: " + listBox1.Items.Count.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0)
            {
                ArrayList items = new ArrayList();
                foreach (string item in listBox1.SelectedItems)
                {
                    items.Add(item);
                }
                foreach (string item in items)
                {
                    listBox1.Items.Remove(item);
                }

                label5.Text = "Codici da ricercare: " + listBox1.Items.Count.ToString();
            }
            else
            {
                MessageBox.Show("Selezionare almeno un elemento!", "Messaggio", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            label5.Text = "Codici da ricercare: " + listBox1.Items.Count.ToString();
        }

        private void cmdImportList_Click(object sender, EventArgs e)
        {

            if (listBox1.Items.Count > Properties.Settings.Default.MaxFiscalCodeRequests)
            {
                MessageBox.Show("Limite di " + Properties.Settings.Default.MaxFiscalCodeRequests.ToString() + " codici fiscali raggiunto", "Messaggio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string file = "";
            openFileDialog1.Filter = "file excel 2000-2003 (*.xls)|*.xls|file excel 2007 (*.xlsx)|*.xlsx";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file = openFileDialog1.FileName;
            }


            if (File.Exists(file))
            {
                
                ////leggo il file
                //System.IO.StreamReader  stream   = new System.IO.StreamReader(file);
                //while (!stream.EndOfStream)
                //{
                //    string s = stream.ReadLine();
                //    if (!string.IsNullOrEmpty(s))
                //    {
                //        if (listBox1.Items.Count > Properties.Settings.Default.MaxFiscalCodeRequests)
                //            break;
                //        if(IsCodeValid(s))
                //            if(!ListConainsCode(s.ToUpper()))
                //                listBox1.Items.Add(s.ToUpper());
                //    }
                //}
                //stream.Close();
                try
                {
                    ReadAndFillList(file);
                    label5.Text = "Codici da ricercare: " + listBox1.Items.Count.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               



            }
        }

        private void ReadAndFillList(string file)
        {
            FileReaders.ExcelFiscalCodeReader r = new WIN.WEBCONNECTOR.FileReaders.ExcelFiscalCodeReader(file);

            IList l = r.ReadCodici();

            foreach (string item in l)
            {
                //if (listBox1.Items.Count > Properties.Settings.Default.MaxFiscalCodeRequests)
                //    break;
                if (IsCodeValid(item))
                    if (!ListConainsCode(item.ToUpper()))
                        listBox1.Items.Add(item.ToUpper());
            }
        }


        private bool ListConainsCode(string code)
        {
            bool found = false;
            foreach (string item in listBox1.Items)
            {
                

                if (item.Equals(code))
                    found = true;
                
            }

            return found;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (listBox1.Items.Count == 0)
                {
                    MessageBox.Show("Nessun codice inserito", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //verifico per prima cosa la connessione a internet
                if (Properties.Settings.Default.CheckInternetConnection)
                {
                    if (!InternetConnectionChecker.IsConnectedToInternet())
                    {
                        throw new Exception("Connessione ad internet non disponibile!");
                    }
                }



                Regione reg = new RegioneNulla();
                //prendo il nome della regione 
                if (!checkBox1.Checked)
                {
                    Provincia r = GeoHandlerProvider.Instance.Geo.GetGeoHandler().GetProvinciaByName(CredentialDB.Instance.Province);
                    reg = GeoHandlerProvider.Instance.Geo.GetGeoHandler().GetRegioneById(r.IdRegione.ToString());
                }


                //Recupero la lista dei codici fiscali
                string[] workers = new string[] { };

                //aggiungo i codici
                foreach (string item in listBox1.Items)
                {
                    Array.Resize<string>(ref workers, workers.Length + 1);
                    workers[workers.Length - 1] = item;
                }

                //se arrivo qui vuol dire che il codice è scritto correttamente
                //creo l'intestazione del webService
                FenealgestServices.Credenziali c = new WIN.WEBCONNECTOR.FenealgestServices.Credenziali();
                c.UserName = CredentialDB.Instance.UserName;
                c.Password = CredentialDB.Instance.Password;
                c.Province = CredentialDB.Instance.Province;

                //creo il servizio
                FenealgestServices.FenealgestwebServices service = new WIN.WEBCONNECTOR.FenealgestServices.FenealgestwebServices();


                //gli imposto l'intestazione
                service.CredenzialiValue = c;


                //metto in attesa
                WIN.GUI.UTILITY.Helper.ShowWaitBox ("Attendere richiesta codici fiscali...", Properties.Resources.Waiting3);


                //richiedo il dato
                FenealgestServices.QueryResultDTO dto = service.ExportWorkers(workers, reg.Descrizione);

                WIN.GUI.UTILITY.Helper.HideWaitBox();

                // a questo punto posso verificare la presenza di un errore di elaborazione
                if (!dto.IsResultValid)
                {
                    MessageBox.Show(dto.Error, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                // a questo punto posso inviare il dato in visualizzazione...

                //prima lo trasformo
                IList<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO> worker = DTOTranslator.ToCustomWorkerDTOList(dto.Workers);
                //poi lo invio al form per la visualizzazione

                FrmViewData frm = new FrmViewData(new BindingList<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.WorkerDTO>( worker));
                frm.Show();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                WIN.GUI.UTILITY.Helper.HideWaitBox();
            }



        }
    
            public bool IsCodeValid(string code)
            {
                try 
	            {
                    WIN.BASEREUSE.DatiFiscali f = GeoElements.GeoHandlerProvider.Instance.Geo.CalcolaDatiFiscali(code);
                    return true;
	            }
	            catch (Exception)
	            {
                    return false;
	            }   
            }
}
}

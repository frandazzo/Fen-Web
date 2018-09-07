using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WIN.BASEREUSE;
using WIN.WEBCONNECTOR.GeoElements;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using WIN.WEBCONNECTOR.Commands;
using WIN.TECHNICAL.MIDDLEWARE.XmlSerializzation;

namespace WIN.WEBCONNECTOR
{
    static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main(string[] cmdArgs)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Inizializzazione indipendente dal tipo di comando



            //try
            //{
            //    Initialize(cmdArgs);


            //    Application.Run(new TestSharetoIntegration());

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Impossibile inizializzare i servizi per la localizzazione geografica e validazione certificati!" + Environment.NewLine + "Eccezione di base: " + ex.Message, "Errore irreversibile", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            Initialize(cmdArgs);
            bool regional = Properties.Settings.Default.RegionalDeploy;


            if (regional)
            {
                //instanzio il form per la verifica delle credenziali
                FrmRegionCredential frm = new FrmRegionCredential();

                //finchè le credenziali non sono corrette non libero l'applicazione
                //a meno che non venga espressamente chiusa dall'utente
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    //parte in modalità regionale
                    Application.Run(new RegionalForm1());
                }
            }
            else
            {
                //parte in modalità provinciale

                //Aperture del programma dal desktop tramite doppio click(senza parametri)
                if (cmdArgs.Length == 0)
                {
                    ProgramParameters.Instance.OpenedByHumanActor = true;
                    OpenWithoutParameters();
                }
                else
                {
                    //Verifico che il comando sia codificato correttemante
                    if (!IsCommandValid())
                    {
                        MessageBox.Show("Comando sconosciuto!", "Errore irreversibile", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    //creo il comando che sarà eseguito
                    ICommand c = CommandFactory.CreateCommand();


                    //verifico se validare perventivamente le credenziali
                    if (ProgramParameters.Instance.CheckCredential == "")
                    {
                        //eseguo la validazione delle credenziali
                        try
                        {
                            //metodo che apre per l'esportazione o il programma semplice con vlaidazione credenziali
                            //l'apertura dell'esportazione è lunica chedeve avvenire previa validazione delle credenziali
                            OpenWithoutParameters();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Accesso non riuscito" + Environment.NewLine + ex.Message, "Errore irreversibile", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        //passo direttamente all'esecuzione del comando


                        //visualizzo in un file la ista dei paramteri




                        //string fname  = "WIN_WEBCONNECTOR.txt";
                        //fname = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), fname);

                        //string p =CreateParamsList(cmdArgs);

                        //System.IO.File.AppendAllText(fname, String.Format("Avvio programma WIN.WEBCONNECTOR alle {0}: parametri {1} {2}", DateTime.Now.ToString(), p, Environment.NewLine));



                        c.Execute();
                    }

                }
            }

        }

        private static string CreateParamsList(string[] cmdArgs)
        {
            string res = "";
            foreach (string item in cmdArgs)
            {
                res = res + "##" + item;
            }
            return res;
        }

        private static bool  IsCommandValid()
        {

            if ((ProgramParameters.Instance.Command != "1") && (ProgramParameters.Instance.Command != "2") && (ProgramParameters.Instance.Command != "3") && (ProgramParameters.Instance.Command != "4") && (ProgramParameters.Instance.Command != "5") && (ProgramParameters.Instance.Command != "6") && (ProgramParameters.Instance.Command != "7"))
            {
                return false;
            }
            return true;
        }

        private static void Initialize(string[] cmdArgs)
        {
            //inizializzo il servizio geografico
            GeoLocationFacade.InitializeInstance(new GeoHandlerClass());
            GeoHandlerProvider.Instance.Geo = GeoLocationFacade.Instance();
            //inizializzo la callback per i certificati lato server
            InitializeX509CertificateValidation();

            //Creo la lista dei parametri
            ParamCreator c = new ParamCreator(cmdArgs);
            c.Create();

        }

        private static void OpenWithoutParameters()
        {
            //instanzio il form per la verifica delle credenziali
            FrmCredential frm = new FrmCredential();

            //finchè le credenziali non sono corrette non libero l'applicazione
            //a meno che non venga espressamente chiusa dall'utente
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(ProgramParameters.Instance.FileToExport))
                {
                    WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.ExportTrace t = ObjectXMLSerializer<WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.ExportTrace>.Load(ProgramParameters.Instance.FileToExport);
                    FrmViewData frm1 = new FrmViewData(t);
                    frm1.ShowDialog();
                }
                else
                {
                    Application.Run(new Form1());
                }
            }
            
        }

        private static void InitializeX509CertificateValidation()
        {
            ServicePointManager.ServerCertificateValidationCallback = CertificateValidationCallBack;
        }


        private static  bool CertificateValidationCallBack(object sender,X509Certificate certificate,X509Chain chain,SslPolicyErrors sslPolicyErrors)
        {
            //// If the certificate is a valid, signed certificate, return true.
            //if (sslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
            //{
            //    return true;
            //}

            //// If thre are errors in the certificate chain, look at each error to determine the cause.
            //if ((sslPolicyErrors & System.Net.Security.SslPolicyErrors.RemoteCertificateChainErrors) != 0)
            //{
            //    if (chain != null && chain.ChainStatus != null)
            //    {
            //        foreach (System.Security.Cryptography.X509Certificates.X509ChainStatus status in chain.ChainStatus)
            //        {
            //            if ((certificate.Subject == certificate.Issuer) &&
            //               (status.Status == System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.UntrustedRoot))
            //            {
            //                // Self-signed certificates with an untrusted root are valid. 
            //                continue;
            //            }
            //            else
            //            {
            //                if (status.Status != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
            //                {
            //                    // If there are any other errors in the certificate chain, the certificate is invalid,
            //                    // so the method returns false.
            //                    return false;
            //                }
            //            }
            //        }
            //    }

            //    // When processing reaches this line, the only errors in the certificate chain are 
            //    // untrusted root errors for self-signed certifcates. These certificates are valid
            //    // for default Exchange server installations, so return true.
            //    return true;
            //}
            //else
            //{
            //    // In all other cases, return false.
            //    return false;
            //}
            return true;
        }
    }
}

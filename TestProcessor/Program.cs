using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
//using WIN.FENGEST_LOCALE.MAIL_IMPORT;
//using WIN.FENGEST_LOCALE.MAIL_IMPORT.Configuration;
//using WIN.FENGEST_LOCALE.MAIL_IMPORT.MailProcessing.GeoProcessing.data;
using WIN.FENGEST_NAZIONALE.HANDLERS.FenealWebImport;
using WIN.FENGEST_NAZIONALE.HANDLERS.ImportHandler;
using WIN.FENGEST_NAZIONALE.IMPORT_EXPORT;
using WIN.TECHNICAL.MIDDLEWARE.Files;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //InitializeX509CertificateValidation();
            //ApplicationServices.Instance.InitializeLocalLog();
            //Processor p = new Processor();
            //p.Process();

            //localhost.Credenziali c = new localhost.Credenziali();
            //c.UserName = "randazzo";
            //c.Password = "francesco1";

            //localhost.SharetopIntegration serv = new localhost.SharetopIntegration();
            //serv.CredenzialiValue = c;
          

            
            //serv.FindOrganizzativeData(1, 1, 1);

            QueueRetriever ret = new QueueRetriever();
            ret.Process();

            //ServerSideSettings settings = new ServerSideSettings();
            //settings.ConnectionString = "connection string";
            //settings.ProvinceSettings = new ServerSideProvinceSettings[2];

            //ServerSideProvinceSettings s1 = new ServerSideProvinceSettings();
            //s1.Credentials = new Credentials
            //{
            //    Username = "Ciccio",
            //    Password = "Ciccio",
            //    Province = "Matera"
            //};
            //s1.Province = "Matera";
            //s1.MailSettings = new ServerSideMailSettings
            //{
            //    ImapPort = 3333,
            //    ImapServer = "smtp.fenealuil.it",
            //    ImapSTARTTLS = true,
            //    MailAccount = "Ciccillo.feneauil.it",
            //    MailPassword= "ciccillo",
            //    MailReaderComponentKey = "xxxxxxx",
            //    RecipientsToNotify = new string[]{"ciccio@gmail.it, ff@libero.it"},
            //    SendersToMonitor = new string[] { "pp.yahoo.com", "hello@world.com" }

            //};



            //ServerSideProvinceSettings s2 = new ServerSideProvinceSettings();
            //s2.Credentials = new Credentials
            //{
            //    Username = "Ciccio1",
            //    Password = "Ciccio1",
            //    Province = "Matera1"
            //};
            //s2.Province = "Matera1";
            //s2.MailSettings = new ServerSideMailSettings
            //{
            //    ImapPort = 33331,
            //    ImapServer = "smtp.fenealuil.it1",
            //    ImapSTARTTLS = false,
            //    MailAccount = "Ciccillo.feneauil.it1",
            //    MailPassword = "ciccillo1",
            //    MailReaderComponentKey = "xxxxxxx1",
            //    RecipientsToNotify = new string[] { "ciccio@gmail.it1, ff@libero.it1" },
            //    SendersToMonitor = new string[] { "pp.yahoo.com1", "hello@world.com1" }

            //};
            //settings.ProvinceSettings[0] = s1;
            //settings.ProvinceSettings[1] = s2;

            //Serializer<ServerSideSettings>.Save(settings, @"C:\serverConfig.xml");



           // ApplicationServices.Instance.InitializeServerSideSettings();


            //ServerSideProcessor p = new ServerSideProcessor();
            //p.Process();

            //GeoDB.Instance.ExistComune("matera");
            //GeoDB.Instance.ExistProvince("matera");
            //GeoDB.Instance.GetComuneByFiscalCode("f052");
            //GeoDB.Instance.CheckFiscalCode("rndfnc77l14f052f");

            //ImportExportService svc = new ImportExportService();
            //svc.Process();

          
        }

        private static void InitializeX509CertificateValidation()
        {
            ServicePointManager.ServerCertificateValidationCallback = CertificateValidationCallBack;
        }

        private static bool CertificateValidationCallBack(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using FenealgestWEB.WebServices.DTOWrappers.Utils;
using PRINT_CARD_CORE_VB;
using WIN.BASEREUSE;
using WIN.FENGEST_NAZIONALE.DOMAIN.Compression;
using WIN.FENGEST_NAZIONALE.DOMAIN.ExcelExport;
using WIN.FENGEST_NAZIONALE.HANDLERS.GeoHandler;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;
using WIN.TECHNICAL.PERSISTENCE;

namespace FenealgestWEB.WebServices
{
    /// <summary>
    /// Descrizione di riepilogo per FenealgestUtils
    /// </summary>
    [WebService(Namespace = "http://www.fenealgestweb.it/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Per consentire la chiamata di questo servizio Web dallo script utilizzando ASP.NET AJAX, rimuovere il commento dalla riga seguente. 
    // [System.Web.Script.Services.ScriptService]
    public class FenealgestUtils : System.Web.Services.WebService
    {
        IPersistenceFacade f;
        WIN.BASEREUSE.GeoLocationFacade g;

        [WebMethod]
        public string CalcolaCodiceFiscale(string nome, string cognome, DateTime dataNascita, string sesso, string nomeComuneNascita, string nomeNazione)
        {
            f = DataAccessServices.SimplePersistenceFacadeInstance();
            GeoLocationFacade.InitializeInstance(new GeoHandler(f));
            g = GeoLocationFacade.Instance();

            int sex = 1;

            if (sesso.Equals("MASCHIO") ){
                          
                sex = 1;
            }
            else if (sesso.Equals("FEMMINA")){
                sex = 2;
            }
            else
            {
                sex = 1;
            }

            if (!nomeNazione.ToUpper().Equals("ITALIA") && !String.IsNullOrEmpty(nomeNazione))
            {
                Nazione nazione = g.GetGeoHandler().GetNazioneByName(nomeNazione);
                return CodiceFiscaleCalculator.CalcolaCodiceFiscale(nome, cognome, dataNascita, sex, nazione.CodiceFiscale, "");
            }
            else{

                Comune comune = g.GetGeoHandler().GetComuneByName(nomeComuneNascita);
                return CodiceFiscaleCalculator.CalcolaCodiceFiscale(nome, cognome, dataNascita, sex, "", comune.CodiceFiscale);
            }

        }

        [WebMethod]
        public Byte[] ExportDocumentToExcel(ExcelDocument document)
        {
            ////qui creo la fonte dati
            //ExcelDocument doc = new ExcelDocument();
            //doc.Rows = new List<ExcelRow>();

            //ExcelRow row1 = new ExcelRow();
            //row1.Properties = new List<ExcelProperty>();
            //row1.Properties.Add(new ExcelProperty("prop1", "ciao prop1", 3));
            //row1.Properties.Add(new ExcelProperty("prop0", "ciao prop0", 2));
            //doc.Rows.Add(row1);

            //row1.Document = new ExcelDocument();
            //row1.Document.Rows = new List<ExcelRow>();
            //ExcelRow row11 = new ExcelRow();
            //row11.Properties = new List<ExcelProperty>();
            //row11.Properties.Add(new ExcelProperty("prop11", "ciao prop11", 3));
            //row11.Properties.Add(new ExcelProperty("prop00", "ciao prop01", 2));

            //row1.Document.Rows.Add(row11);



            //ExcelRow row2 = new ExcelRow();
            //row2.Properties = new List<ExcelProperty>();
            //row2.Properties.Add(new ExcelProperty("prop1", "xxxxxx", 3));
            //row2.Properties.Add(new ExcelProperty("prop0", "yyyyyy", 2));
            //doc.Rows.Add(row2);


            string file = ExcelDocumentExporter.CreateExcelFile("test.xlsx",document);

            //verifico se esiste il path se non esiste  ritorno
            if (!File.Exists(file))
                return new Byte[] { };



            FileStream objfilestream = new FileStream(file, FileMode.Open, FileAccess.Read);
            int len = (int)objfilestream.Length;
            Byte[] documentcontents = new Byte[len];
            objfilestream.Read(documentcontents, 0, len);
            objfilestream.Close();

            return documentcontents;

        }



        [WebMethod]
        public FiscalData CalcolaDatiFiscali(string codiceFiscale)
        {
            f = DataAccessServices.SimplePersistenceFacadeInstance();
            GeoLocationFacade.InitializeInstance(new GeoHandler(f));
            g = GeoLocationFacade.Instance();

            DatiFiscali d = CodiceFiscaleCalculator.GetDatiFiscali(ref codiceFiscale, new GeoHandler(f)) as DatiFiscali;

            if (d == null)
                return new FiscalData();


            FiscalData fis = new FiscalData();
            fis.Comune = d.Comune.Descrizione;
            fis.DataNascita = d.DataNascita;
            fis.Nazione = d.Nazione.Descrizione;
            fis.Provincia = d.Provincia.Descrizione;
            if (d.SessoPersona.Equals("0"))
                fis.Sesso = "MASCHIO";
            else
                fis.Sesso = "FEMMINA";

            return fis;
        }





        [WebMethod]
        public Byte[] ExportTessere(String settore, String provincia, Tesserato[] tesserati)
        {
            
            string result = Path.GetTempPath();
            string filename = result + "StampaTessere_" + provincia.Replace(" ", "").Replace(".","") + ".pdf";

            //Tesserato t = new Tesserato();
            //t.Nome = "Francesco";
            //t.Cognome = "Randazzo";
            //t.Azienda = "Noesis";
            //t.Provincia = "Roma";
            //t.ProvinciaSindacale = "MATERA";
            //t.Comune = "Ostia lido";
            //t.Cap = "10110";
            //t.SettoreTessera = "EDILE";

            //Tesserato t1 = new Tesserato();
            //t1.Nome = "Francesco1";
            //t1.Cognome = "Randazzo1";
            //t1.Azienda = "Noesis1";
            //t1.Provincia = "Roma";
            //t1.ProvinciaSindacale = "MATERA";
            //t1.Comune = "Ostia lido";
            //t1.Cap = "10110";
            //t1.SettoreTessera = "EDILE";

            //IList a = new ArrayList();
            //a.Add(t);
            //a.Add(t1);
            IList a = new ArrayList();
            foreach (Tesserato item in tesserati)
            {
                a.Add(item);
            }

            PRINT_CARD_CORE_VB.PrintCardFacade.StampaTessere(filename, provincia, settore,a );
           
            //recuperato il file pdf posso creare il file excel per le etichette
            string etichietteFilename = "EtichetteTessere" + provincia.Replace(" ", "").Replace(".", "") + ".xlsx";
            string zipFilename = "ZipTessere_" + provincia.Replace(" ", "").Replace(".", "") + ".zip";
            string etichette = CreateFileForEtichette(a, etichietteFilename);


            //adesso posso zippare i file etichette e filename
            string[] data = new string[]{filename, etichette};
            string pathTofile = Archiver.ArchiveFiles(data, zipFilename);



            FileStream objfilestream = new FileStream(pathTofile, FileMode.Open, FileAccess.Read);
            int len = (int)objfilestream.Length;
            Byte[] documentcontents = new Byte[len];
            objfilestream.Read(documentcontents, 0, len);
            objfilestream.Close();

            return documentcontents;

        }

        

        private string CreateFileForEtichette(IList a, string etichietteFilename)
        {
            //per prima cosa creo un documento excel da stampare
            ExcelDocument doc = new ExcelDocument();
            doc.Rows = new List<ExcelRow>();



            foreach (Tesserato item in a)
            {
                //creo una riga da inserire nel doc
                ExcelRow row = new ExcelRow();
                row.Properties = new List<ExcelProperty>();

                //aggiungo le proprietà che mi necessitano
                row.Properties.Add(new ExcelProperty("Cognome", item.Cognome, 1));
                row.Properties.Add(new ExcelProperty("Nome", item.Nome, 2));
                row.Properties.Add(new ExcelProperty("Provincia", item.Provincia, 3));
                row.Properties.Add(new ExcelProperty("Comune", item.Comune, 4));
                row.Properties.Add(new ExcelProperty("Indirizzo", item.Via, 5));
                row.Properties.Add(new ExcelProperty("Cap", item.Cap, 6));
                doc.Rows.Add(row);
            }

            //creato il documento posso stamparlo su excel
            return ExcelDocumentExporter.CreateExcelFile(etichietteFilename, doc);

        }

        [WebMethod]
        public string ImportData(ExportTrace trace)
        {
            return "ok";

        }

       
    }
}

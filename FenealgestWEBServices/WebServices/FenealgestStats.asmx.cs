using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Services;
using FenealgestWEB.WebServices.DTOWrappers;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport;
using WIN.TECHNICAL.MIDDLEWARE.XmlSerializzation;

namespace FenealgestWEB.WebServices
{
    /// <summary>
    /// Descrizione di riepilogo per FenealgestStats
    /// </summary>
    [WebService(Namespace = "http://www.fenealgestweb.it/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Per consentire la chiamata di questo servizio Web dallo script utilizzando ASP.NET AJAX, rimuovere il commento dalla riga seguente. 
    // [System.Web.Script.Services.ScriptService]
    public class FenealgestStats : System.Web.Services.WebService
    {

        [WebMethod]
        public DataExportResult CalculateStatistics(string province, string filenames)
        {

            string baseFolder = WebConfigurationManager.AppSettings["ImportExportDir"];
            string baseFolderAnalisysData = "";
            //verifico se esiste il path
            if (!Directory.Exists(baseFolder))
                return new DataExportResult();

            baseFolder = baseFolder + "//" + province;
            if (!Directory.Exists(baseFolder))
                return new DataExportResult();

            baseFolderAnalisysData = baseFolder + "//ServerSideAnalysisData";
            if (!Directory.Exists(baseFolderAnalisysData))
                return new DataExportResult();

            List<DataExport> filesToElaborate = CreateFilesList(baseFolder, baseFolderAnalisysData, filenames);

            //adesso elaboro i files ottenuti e creo il risultato
            if (filesToElaborate.Count ==0)
                return new DataExportResult();

            return CreateResult(filesToElaborate);
        }

        private DataExportResult CreateResult(List<DataExport> filesToElaborate)
        {
            DataExportResult result = new DataExportResult();
             IList<IQATrace> iqaTraces= new List<IQATrace>();
            IList<LiberiTrace> liberiTraces= new List<LiberiTrace>();
       
            Hashtable feneal  = new Hashtable();
            Hashtable filca  = new Hashtable();
            Hashtable fillea = new Hashtable();
            Hashtable codici0  = new Hashtable();

            PopulateListOfTraces(filesToElaborate, iqaTraces, liberiTraces);

            PopulateFenealData(iqaTraces, feneal);

            PopulateOthersData(liberiTraces, filca, fillea, codici0);

            result.NumFeneal= feneal.Keys.Count;
            result.NumFilca = filca.Keys.Count;
            result.NumFillea = fillea.Keys.Count;
            result.NumLiberi= codici0.Keys.Count;

            result.TotSindacalizzati = result.NumFeneal + result.NumFilca + result.NumFillea;
            result.TotLavoratori = result.NumFeneal + result.NumFilca + result.NumFillea + result.NumLiberi;

            if (result.NumFeneal > 0 && (result.NumFilca > 0 || result.NumFillea > 0))
            {
                result.RappresentativitaFeneal = Convert.ToDecimal(result.NumFeneal) / Convert.ToDecimal(result.TotSindacalizzati);
                result.RappresentativitaFeneal = Math.Round(result.RappresentativitaFeneal * 100, 2);

                if (result.NumFilca > 0)
                {
                    result.RappresentativitaFilca = Convert.ToDecimal(result.NumFilca) / Convert.ToDecimal(result.TotSindacalizzati);
                    result.RappresentativitaFilca = Math.Round(result.RappresentativitaFilca * 100, 2);
                }

                if (result.NumFillea > 0)
                {
                    result.RappresentativitaFillea = Convert.ToDecimal(result.NumFillea) / Convert.ToDecimal(result.TotSindacalizzati);
                    result.RappresentativitaFillea = Math.Round(result.RappresentativitaFillea * 100, 2);
                }
            }

            result.Sindacalizzazione = Convert.ToDecimal(result.TotSindacalizzati) / Convert.ToDecimal(result.TotLavoratori);
            result.Sindacalizzazione = Math.Round(result.Sindacalizzazione * 100, 2);

            return result;
        }

        private static void PopulateOthersData(IList<LiberiTrace> liberiTraces, Hashtable filca, Hashtable fillea, Hashtable codici0)
        {
            foreach (LiberiTrace item in liberiTraces)
            {
                foreach (LiberiTraceDetail detail in item.LiberiTraceDetails)
                {
                    if (!String.IsNullOrEmpty(detail.FISCALE))
                    {

                        if (detail.ISCRITTO_A.Equals(""))
                        {
                            if (!codici0.ContainsKey(detail.FISCALE))
                            {
                                codici0.Add(detail.FISCALE, 1);
                            }

                        }
                        else if (detail.ISCRITTO_A.Equals("FILCA"))
                        {

                            if (!filca.ContainsKey(detail.FISCALE))
                            {
                                filca.Add(detail.FISCALE, 1);
                            }
                        }
                        else
                        {

                            if (!fillea.ContainsKey(detail.FISCALE))
                            {
                                fillea.Add(detail.FISCALE, 1);
                            }
                        }

                    }
                }
            }
        }

        private static void PopulateFenealData(IList<IQATrace> iqaTraces, Hashtable feneal)
        {
            foreach (IQATrace item in iqaTraces)
            {
                foreach (IqaTraceDetail detail in item.IqaTraceDetails)
                {
                    if (!String.IsNullOrEmpty(detail.FISCALE))
                    {
                        if (!feneal.Contains(detail.FISCALE))
                        {
                            feneal.Add(detail.FISCALE, 1);
                        }
                    }
                }
            }
        }

        private void PopulateListOfTraces(List<DataExport> filesToElaborate, IList<IQATrace> iqaTraces, IList<LiberiTrace> liberiTraces)
        {
            foreach (DataExport item in filesToElaborate)
            {
                if (item.IsIQA)
                {
                      IQATrace m_iqaTrace  = ObjectXMLSerializer<IQATrace>.Load(item.CompleteFileName);
                      iqaTraces.Add(m_iqaTrace);
                }
                else
                {
                      LiberiTrace m_liberiTrace = ObjectXMLSerializer<LiberiTrace>.Load(item.CompleteFileName);
                      liberiTraces.Add(m_liberiTrace);
                }
            }
        }

        private List<DataExport> CreateFilesList(string baseFolder, string baseFolderAnalisysData, string filenames)
        {
            List<DataExport> d = new List<DataExport>();
            string[] files = filenames.Split(new char[] { ';' });
            foreach (string item in files)
            {
                DataExport c = FindFile(item, baseFolder, baseFolderAnalisysData);
                if (c != null)
                    d.Add(c);
            }

          
            return d;
        }

        private DataExport FindFile(string item, string baseFolder, string baseFolderAnalisysData)
        {
            string[] files1 = Directory.GetFiles(baseFolder,item);
            if (files1.Length == 1)
                return CreateExportData(files1[0]);

            string[] files2 = Directory.GetFiles(baseFolderAnalisysData, item);
            if (files2.Length == 1)
                return CreateExportData(files2[0]);
            return null;
        }

        [WebMethod]
        public DataExport[] FindDataExports(string province)
        {
            List<DataExport> data = new List<DataExport>();

            string baseFolder = WebConfigurationManager.AppSettings["ImportExportDir"];
            string baseFolderAnalisysData = "";
            //verifico se esiste il path
            if (!Directory.Exists(baseFolder))
                return data.ToArray();

            baseFolder = baseFolder + "//" + province;
            if (!Directory.Exists(baseFolder))
                return data.ToArray();

            baseFolderAnalisysData = baseFolder + "//ServerSideAnalysisData";
            if (!Directory.Exists(baseFolderAnalisysData))
                return data.ToArray();
            //adesso posso cercare nella cartella di base o in quella dell'analisi
            //tutti ifiles xml che iniziano con "Managed" o con "Liberi" o con "IQA"

            //prendo tutti i file managed
            AddExportData(baseFolder, baseFolderAnalisysData, data, "Managed");
            AddExportData(baseFolder, baseFolderAnalisysData, data, "IQA");
            AddExportData(baseFolder, baseFolderAnalisysData, data, "Liberi");

            return data.ToArray();
        }

        private void AddExportData(string baseFolder, string baseFolderAnalisysData, List<DataExport> data, string pattern)
        {
            string[] files1 = Directory.GetFiles(baseFolder, pattern + "*.xml");
            string[] files2 = Directory.GetFiles(baseFolderAnalisysData, pattern + "*.xml");


            foreach (string item in files1)
            {
                DataExport d = CreateExportData(item);


                data.Add(d);


            }

            foreach (string item in files2)
            {
                DataExport d = CreateExportData(item);


                data.Add(d);


            }

        }

        private DataExport CreateExportData(string item)
        {
            FileInfo file = new FileInfo(item);
            string filename = file.Name;


            DataExport exp = new DataExport();
            exp.CompleteFileName = item;
            exp.FileName = filename;
            if (filename.Equals("Managed"))
            {
                exp.Managed = true;

            }
            string cleanItem = filename.Replace("Managed_", "");

            if (cleanItem.StartsWith("IQA"))
            {
                exp.IsIQA = true;
                cleanItem = cleanItem.Replace("IQA_", "");
            }
            else
            {
                exp.IsIQA = false;
                cleanItem = cleanItem.Replace("Liberi_", "");
            }
            cleanItem = cleanItem.Replace(".xml", "");



            //si parte da un formato di nome file fatto cosi: IQA_ // 2016_11_28_17_49_50 // .xml
            //mi è rimasto il nome del file nel formato:  2016_11_28_17_49_50 

            //estraggo la data dal nome del file
            string[] splitted = cleanItem.Split(new char[] { '_' }).ToArray();
            DateTime t = new DateTime(Convert.ToInt32(splitted[0]), Convert.ToInt32(splitted[1]), 
                Convert.ToInt32(splitted[2]), Convert.ToInt32(splitted[3]), 
                Convert.ToInt32(splitted[4]), Convert.ToInt32(splitted[5]));
            exp.Date = t;

            return exp;
        }
    }
}

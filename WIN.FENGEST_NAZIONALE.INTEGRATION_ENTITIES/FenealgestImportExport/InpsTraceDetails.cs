using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Collections;

namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport
{
     [Serializable]
    public class InpsTraceDetails
    {
         public InpsTraceDetails() { }

        

        [XmlAttribute("COGNOME_UTENTE")]
        public string COGNOME_UTENTE { get; set; }
         [XmlAttribute("NOME_UTENTE")]
        public string NOME_UTENTE { get; set; }
         [XmlAttribute("DATA_NASCITA_UTENTE")]
        public string DATA_NASCITA_UTENTE { get; set; }
         [XmlAttribute("FISCALE")]
        public string FISCALE { get; set; }
         [XmlAttribute("PROVINCIA")]
        public string PROVINCIA { get; set; }
         [XmlAttribute("COMUNE")]
        public string COMUNE { get; set; }
         [XmlAttribute("INDIRIZZO")]
        public string INDIRIZZO { get; set; }
         [XmlAttribute("CAP")]
        public string CAP { get; set; }
         


         [XmlAttribute("SETTORE")]
        public string SETTORE { get; set; }



         [XmlAttribute("FILE_NAME")]
         public string FILE_NAME { get; set; }


         [XmlAttribute("DATA_INIZIO")]
        public string DATA_INIZIO { get; set; }



         [XmlAttribute("DATA_FINE")]
         public string DATA_FINE { get; set; }
         [XmlAttribute("NOTE")]
         public string NOTE { get; set; }





         [XmlAttribute("QUOTA")]
        public string QUOTA { get; set; }
         [XmlAttribute("TIPO_PRESTAZIONE")]
         public string TIPO_PRESTAZIONE { get; set; }
         [XmlAttribute("NOME_REFERENTE")]
        public string NOME_REFERENTE { get; set; }
         [XmlAttribute("COGNOME_REFERENTE")]
        public string COGNOME_REFERENTE { get; set; }


         public void FromHash(Hashtable t)
         {
             COGNOME_UTENTE = (t["COGNOME_UTENTE"] == null) ? "": t["COGNOME_UTENTE"].ToString().Trim();
             NOME_UTENTE = (t["NOME_UTENTE"] == null) ? "" : t["NOME_UTENTE"].ToString().Trim();
             DATA_NASCITA_UTENTE = (t["DATA_NASCITA_UTENTE"] == null) ? "" : t["DATA_NASCITA_UTENTE"].ToString().Replace("0.00.00", "").Replace("00:00:00", "").Trim();
             FISCALE = (t["FISCALE"] == null) ? "" : t["FISCALE"].ToString().Trim();
           
             PROVINCIA = (t["PROVINCIA"] == null) ? "" : t["PROVINCIA"].ToString().Trim();
             COMUNE = (t["COMUNE"] == null) ? "" : t["COMUNE"].ToString().Trim();
             INDIRIZZO = (t["INDIRIZZO"] == null) ? "" : t["INDIRIZZO"].ToString().Trim();
             CAP = (t["CAP"] == null) ? "" : t["CAP"].ToString().Trim();
            

             SETTORE = (t["SETTORE"] == null) ? "" : t["SETTORE"].ToString().Trim();


             DATA_INIZIO = (t["DATA_INIZIO"] == null) ? "" : t["DATA_INIZIO"].ToString().Replace("0.00.00", "").Replace("00:00:00", "").Trim();
             DATA_FINE = (t["DATA_FINE"] == null) ? "" : t["DATA_FINE"].ToString().Replace("0.00.00", "").Replace("00:00:00", "").Trim();
             NOTE = (t["NOTE"] == null) ? "" : t["NOTE"].ToString();
            
             QUOTA = (t["QUOTA"] == null) ? "" : t["QUOTA"].ToString().Trim();
             TIPO_PRESTAZIONE = (t["TIPO_PRESTAZIONE"] == null) ? "" : t["TIPO_PRESTAZIONE"].ToString().Trim();
            
             NOME_REFERENTE = (t["NOME_REFERENTE"] == null) ? "" : t["NOME_REFERENTE"].ToString().Trim();
             COGNOME_REFERENTE = (t["COGNOME_REFERENTE"] == null) ? "" : t["COGNOME_REFERENTE"].ToString().Trim();
             FILE_NAME = (t["FILE_NAME"] == null) ? "" : t["FILE_NAME"].ToString().Trim();

         }


         public  Hashtable ToHash()
         {
             if (DATA_NASCITA_UTENTE == null)
                 DATA_NASCITA_UTENTE = "";
             else
                 DATA_NASCITA_UTENTE = DATA_NASCITA_UTENTE.Replace(" 0.00.00", "");

             if (DATA_INIZIO == null)
                 DATA_INIZIO = "";
             else
                 DATA_INIZIO = DATA_INIZIO.Replace(" 0.00.00", "");

             Hashtable t = new Hashtable();

             t.Add("COGNOME_UTENTE", COGNOME_UTENTE);
             t.Add("NOME_UTENTE", NOME_UTENTE);
             t.Add("DATA_NASCITA_UTENTE", DATA_NASCITA_UTENTE);
             t.Add("FISCALE", FISCALE);

             t.Add("PROVINCIA", PROVINCIA);
             t.Add("COMUNE", COMUNE);
             t.Add("INDIRIZZO", INDIRIZZO);
             t.Add("CAP", CAP);



             t.Add("SETTORE", SETTORE);



             t.Add("DATA_INIZIO", DATA_INIZIO);
             t.Add("DATA_FINE", DATA_FINE);
             t.Add("NOTE", NOTE);
             t.Add("QUOTA", QUOTA);
             t.Add("TIPO_PRESTAZIONE", TIPO_PRESTAZIONE);
           
             t.Add("NOME_REFERENTE", NOME_REFERENTE);
             t.Add("COGNOME_REFERENTE", COGNOME_REFERENTE);
             t.Add("FILE_NAME", FILE_NAME);



             return t;
         }


         private IList<string> _errors;

         public IList<string> Errors
         {
             get
             {
                 return _errors;
             }
         }

         public string ErrorsToString()
         {
             string result = "";
             if (_errors.Count > 0)
             {
                 foreach (string err in _errors)
                 {
                     result += err;
                 }
             }

             return result;
         }

         internal void Validate()
         {
             _errors = new List<string>();



             //tolgo eventualispazi di inizio e fine
             COGNOME_UTENTE = COGNOME_UTENTE == null ? "" : COGNOME_UTENTE.Trim();
             NOME_UTENTE = NOME_UTENTE == null ? "" : NOME_UTENTE.Trim();
             DATA_NASCITA_UTENTE = DATA_NASCITA_UTENTE == null ? "" : DATA_NASCITA_UTENTE.Trim();
             FISCALE = FISCALE == null ? "" : FISCALE.Trim();
            
             PROVINCIA = PROVINCIA == null ? "" : PROVINCIA.Trim();
             COMUNE = COMUNE == null ? "" : COMUNE.Trim();
             INDIRIZZO = INDIRIZZO == null ? "" : INDIRIZZO.Trim();
             CAP = CAP == null ? "" : CAP.Trim();
            


             SETTORE = SETTORE == null ? "" : SETTORE.Trim();


             FILE_NAME = FILE_NAME == null ? "" : FILE_NAME.Trim ();
           



             DATA_INIZIO = DATA_INIZIO == null ? "" : DATA_INIZIO.Trim();
           
             QUOTA = QUOTA == null ? "" : QUOTA.Trim();
             TIPO_PRESTAZIONE = TIPO_PRESTAZIONE == null ? "" : TIPO_PRESTAZIONE.Trim();
            
             NOME_REFERENTE = NOME_REFERENTE == null ? "" : NOME_REFERENTE.Trim();
             COGNOME_REFERENTE = COGNOME_REFERENTE == null ? "" : COGNOME_REFERENTE.Trim();






             

             //qui valido tutti i campi dei record da importare
             if (string.IsNullOrEmpty(COGNOME_UTENTE))
                 _errors.Add("Cognome nullo; ");
             if (string.IsNullOrEmpty(NOME_UTENTE))
                 _errors.Add("Nome nullo; ");
             if (string.IsNullOrEmpty(FISCALE))
                 _errors.Add("Codice fiscale nullo;");


             //la data di nascita se presente deve essere una DATA
             if (!string.IsNullOrEmpty(DATA_NASCITA_UTENTE))
             {
                 try 
	            {
                    DateTime d = DateTime.Parse(DATA_NASCITA_UTENTE);
	            }
	            catch (Exception)
	            {
                    _errors.Add("Formato data non corretto; ");
	            }    
             }



          

             //la validazione dei campi relativi all'azienda non è necessaria

             //valido i campi della quota associativa: datainizio, datafine, quota
             //la data di nascita se presente deve essere una DATA
             if (!string.IsNullOrEmpty(DATA_INIZIO))
             {
                 try
                 {
                     DateTime d = DateTime.Parse(DATA_INIZIO);
                 }
                 catch (Exception)
                 {
                     _errors.Add("Formato data inizio non corretto; ");
                 }
             }
             else
                 _errors.Add("Data inizio compentenza nono impostata; ");


            

             //valido la quota associativa
             try
             {
                 double q = double.Parse(QUOTA);
             }
             catch (Exception)
             {

                 QUOTA = "0.01";
             }




         }


         public bool HasNomeCognomeNotNullOrEmpty()
         {
             if (string.IsNullOrEmpty(NOME_UTENTE) || string.IsNullOrEmpty(COGNOME_UTENTE))
                 return false;

             return true;
         }

         public bool IsDataNascitaFormatCorrect()
         {
             //la data di nascita se presente deve essere una DATA
             if (!string.IsNullOrEmpty(DATA_NASCITA_UTENTE))
             {
                 try
                 {
                     DateTime d = DateTime.Parse(DATA_NASCITA_UTENTE);
                     return true;
                 }
                 catch (Exception)
                 {
                     return false;
                 }
             }

             return false;
         }

    }
}

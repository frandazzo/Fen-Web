using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Collections;

namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport
{
    [Serializable]
    public class IqaTraceDetail
    {

        public IqaTraceDetail() { }

        [XmlAttribute("COGNOME_UTENTE")]
        public string COGNOME_UTENTE { get; set; }
         [XmlAttribute("NOME_UTENTE")]
        public string NOME_UTENTE { get; set; }
         [XmlAttribute("DATA_NASCITA_UTENTE")]
        public string DATA_NASCITA_UTENTE { get; set; }
         [XmlAttribute("FISCALE")]
        public string FISCALE { get; set; }
         [XmlAttribute("PROVINCIA_NASCITA")]
        public string PROVINCIA_NASCITA { get; set; }
         [XmlAttribute("COMUNE_NASCITA")]
        public string COMUNE_NASCITA { get; set; }
         [XmlAttribute("PROVINCIA")]
        public string PROVINCIA { get; set; }
         [XmlAttribute("COMUNE")]
        public string COMUNE { get; set; }
         [XmlAttribute("INDIRIZZO")]
        public string INDIRIZZO { get; set; }
         [XmlAttribute("CAP")]
        public string CAP { get; set; }
         [XmlAttribute("TELEFONO1")]
        public string TELEFONO1 { get; set; }
         [XmlAttribute("TELEFONO2")]
        public string TELEFONO2 { get; set; }
         [XmlAttribute("CODICE_CE_UTENTE")]
        public string CODICE_CE_UTENTE { get; set; }
         [XmlAttribute("CODICE_EC_UTENTE")]
        public string CODICE_EC_UTENTE { get; set; }
         [XmlAttribute("NOTE_UTENTE")]
        public string NOTE_UTENTE { get; set; }


         [XmlAttribute("SETTORE")]
        public string SETTORE { get; set; }
         [XmlAttribute("ENTE")]
        public string ENTE { get; set; }


         [XmlAttribute("AZIENDA_IMPIEGO")]
        public string AZIENDA_IMPIEGO { get; set; }
         [XmlAttribute("PARTITA_IVA")]
        public string PARTITA_IVA { get; set; }
         [XmlAttribute("PROVINCIA_AZIENDA")]
        public string PROVINCIA_AZIENDA { get; set; }
         [XmlAttribute("COMUNE_AZIENDA")]
        public string COMUNE_AZIENDA { get; set; }
         [XmlAttribute("INDIRIZZO_AZIENDA")]
        public string INDIRIZZO_AZIENDA { get; set; }
         [XmlAttribute("CAP_AZIENDA")]
        public string CAP_AZIENDA { get; set; }
         [XmlAttribute("TELEFONO_AZIENDA")]
        public string TELEFONO_AZIENDA { get; set; }
         [XmlAttribute("NOTE_AZIENDA")]
        public string NOTE_AZIENDA { get; set; }
         [XmlAttribute("CODICE_CE_AZIENDA")]
        public string CODICE_CE_AZIENDA { get; set; }
         [XmlAttribute("CODICE_EC_AZIENDA")]
        public string CODICE_EC_AZIENDA { get; set; }
         [XmlAttribute("CONTRATTO")]
        public string CONTRATTO { get; set; }




         [XmlAttribute("DATA_INIZIO")]
        public string DATA_INIZIO { get; set; }
         [XmlAttribute("DATA_FINE")]
        public string DATA_FINE { get; set; }
         [XmlAttribute("QUOTA")]
        public string QUOTA { get; set; }
         [XmlAttribute("LIVELLO")]
        public string LIVELLO { get; set; }
         [XmlAttribute("NOTE")]
        public string NOTE { get; set; }
         [XmlAttribute("ORE_LAVORATE")]
        public string ORE_LAVORATE { get; set; }
         [XmlAttribute("ORE_MALATTIA_INFORTUNIO")]
        public string ORE_MALATTIA_INFORTUNIO { get; set; }
         [XmlAttribute("NOME_REFERENTE")]
        public string NOME_REFERENTE { get; set; }
         [XmlAttribute("COGNOME_REFERENTE")]
        public string COGNOME_REFERENTE { get; set; }


         public void FromHash(Hashtable t)
         {
             COGNOME_UTENTE = (t["COGNOME_UTENTE"] == null) ? "": t["COGNOME_UTENTE"].ToString().Trim();
             NOME_UTENTE = (t["NOME_UTENTE"] == null) ? "" : t["NOME_UTENTE"].ToString().Trim();
             DATA_NASCITA_UTENTE = (t["DATA_NASCITA_UTENTE"] == null) ? "" : t["DATA_NASCITA_UTENTE"].ToString().Trim();
             FISCALE = (t["FISCALE"] == null) ? "" : t["FISCALE"].ToString().Trim();
             PROVINCIA_NASCITA = (t["PROVINCIA_NASCITA"] == null) ? "" : t["PROVINCIA_NASCITA"].ToString().Trim();
             COMUNE_NASCITA = (t["COMUNE_NASCITA"] == null) ? "" : t["COMUNE_NASCITA"].ToString().Trim();
             PROVINCIA = (t["PROVINCIA"] == null) ? "" : t["PROVINCIA"].ToString().Trim();
             COMUNE = (t["COMUNE"] == null) ? "" : t["COMUNE"].ToString().Trim();
             INDIRIZZO = (t["INDIRIZZO"] == null) ? "" : t["INDIRIZZO"].ToString().Trim();
             CAP = (t["CAP"] == null) ? "" : t["CAP"].ToString().Trim();
             TELEFONO1 = (t["TELEFONO1"] == null) ? "" : t["TELEFONO1"].ToString().Trim();
             TELEFONO2 = (t["TELEFONO2"] == null) ? "" : t["TELEFONO2"].ToString().Trim();
             CODICE_CE_UTENTE = (t["CODICE_CE_UTENTE"] == null) ? "" : t["CODICE_CE_UTENTE"].ToString().Trim();
             CODICE_EC_UTENTE = (t["CODICE_EC_UTENTE"] == null) ? "" : t["CODICE_EC_UTENTE"].ToString().Trim();
             NOTE_UTENTE = (t["NOTE_UTENTE"] == null) ? "" : t["NOTE_UTENTE"].ToString().Trim();


             SETTORE = (t["SETTORE"] == null) ? "" : t["SETTORE"].ToString().Trim();
             ENTE = (t["ENTE"] == null) ? "" : t["ENTE"].ToString().Trim();


             AZIENDA_IMPIEGO = (t["AZIENDA_IMPIEGO"] == null) ? "" : t["AZIENDA_IMPIEGO"].ToString().Trim();
             PARTITA_IVA = (t["PARTITA_IVA"] == null) ? "" : t["PARTITA_IVA"].ToString().Trim();
             PROVINCIA_AZIENDA = (t["PROVINCIA_AZIENDA"] == null) ? "" : t["PROVINCIA_AZIENDA"].ToString().Trim();
             COMUNE_AZIENDA = (t["COMUNE_AZIENDA"] == null) ? "" : t["COMUNE_AZIENDA"].ToString().Trim();
             INDIRIZZO_AZIENDA = (t["INDIRIZZO_AZIENDA"] == null) ? "" : t["INDIRIZZO_AZIENDA"].ToString().Trim();
             CAP_AZIENDA = (t["CAP_AZIENDA"] == null) ? "" : t["CAP_AZIENDA"].ToString().Trim();
             TELEFONO_AZIENDA = (t["TELEFONO_AZIENDA"] == null) ? "" : t["TELEFONO_AZIENDA"].ToString().Trim();
             NOTE_AZIENDA = (t["NOTE_AZIENDA"] == null) ? "" : t["NOTE_AZIENDA"].ToString().Trim();
             CODICE_CE_AZIENDA = (t["CODICE_CE_AZIENDA"] == null) ? "" : t["CODICE_CE_AZIENDA"].ToString().Trim();
             CODICE_EC_AZIENDA = (t["CODICE_EC_AZIENDA"] == null) ? "" : t["CODICE_EC_AZIENDA"].ToString().Trim();
             CONTRATTO = (t["CONTRATTO"] == null) ? "" : t["CONTRATTO"].ToString().Trim();


             DATA_INIZIO = (t["DATA_INIZIO"] == null) ? "" : t["DATA_INIZIO"].ToString().Trim();
             DATA_FINE = (t["DATA_FINE"] == null) ? "" : t["DATA_FINE"].ToString().Trim();
             QUOTA = (t["QUOTA"] == null) ? "" : t["QUOTA"].ToString().Trim();
             LIVELLO = (t["LIVELLO"] == null) ? "" : t["LIVELLO"].ToString().Trim();
             NOTE = (t["NOTE"] == null) ? "" : t["NOTE"].ToString().Trim();
             ORE_LAVORATE = (t["ORE_LAVORATE"] == null) ? "" : t["ORE_LAVORATE"].ToString().Trim();
             ORE_MALATTIA_INFORTUNIO = (t["ORE_MALATTIA_INFORTUNIO"] == null) ? "" : t["ORE_MALATTIA_INFORTUNIO"].ToString().Trim();
             NOME_REFERENTE = (t["NOME_REFERENTE"] == null) ? "" : t["NOME_REFERENTE"].ToString().Trim();
             COGNOME_REFERENTE = (t["COGNOME_REFERENTE"] == null) ? "" : t["COGNOME_REFERENTE"].ToString().Trim();
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

             if (DATA_FINE == null)
                 DATA_FINE = "";
             else
                 DATA_FINE = DATA_FINE.Replace(" 0.00.00", "");

             Hashtable t = new Hashtable();

             t.Add("COGNOME_UTENTE", COGNOME_UTENTE);
             t.Add("NOME_UTENTE", NOME_UTENTE);
             t.Add("DATA_NASCITA_UTENTE", DATA_NASCITA_UTENTE);
             t.Add("FISCALE", FISCALE);
             t.Add("PROVINCIA_NASCITA", PROVINCIA_NASCITA);
             t.Add("COMUNE_NASCITA", COMUNE_NASCITA);
             t.Add("PROVINCIA", PROVINCIA);
             t.Add("COMUNE", COMUNE);
             t.Add("INDIRIZZO", INDIRIZZO);
             t.Add("CAP", CAP);
             t.Add("TELEFONO1", TELEFONO1);
             t.Add("TELEFONO2", TELEFONO2);
             t.Add("CODICE_CE_UTENTE", CODICE_CE_UTENTE);
             t.Add("CODICE_EC_UTENTE", CODICE_EC_UTENTE);
             t.Add("NOTE_UTENTE", NOTE_UTENTE);


             t.Add("SETTORE", SETTORE);
             t.Add("ENTE", ENTE);


             t.Add("AZIENDA_IMPIEGO", AZIENDA_IMPIEGO);
             t.Add("PARTITA_IVA", PARTITA_IVA);
             t.Add("PROVINCIA_AZIENDA", PROVINCIA_AZIENDA);
             t.Add("COMUNE_AZIENDA", COMUNE_AZIENDA);
             t.Add("INDIRIZZO_AZIENDA", INDIRIZZO_AZIENDA);
             t.Add("CAP_AZIENDA", CAP_AZIENDA);
             t.Add("TELEFONO_AZIENDA", TELEFONO_AZIENDA);
             t.Add("NOTE_AZIENDA", NOTE_AZIENDA);
             t.Add("CODICE_CE_AZIENDA", CODICE_CE_AZIENDA);
             t.Add("CODICE_EC_AZIENDA", CODICE_EC_AZIENDA);
             t.Add("CONTRATTO", CONTRATTO);


             t.Add("DATA_INIZIO", DATA_INIZIO);
             t.Add("DATA_FINE", DATA_FINE);
             t.Add("QUOTA", QUOTA);
             t.Add("LIVELLO", LIVELLO);
             t.Add("NOTE", NOTE);
             t.Add("ORE_LAVORATE", ORE_LAVORATE);
             t.Add("ORE_MALATTIA_INFORTUNIO", ORE_MALATTIA_INFORTUNIO);
             t.Add("NOME_REFERENTE", NOME_REFERENTE);
             t.Add("COGNOME_REFERENTE", COGNOME_REFERENTE);




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
             PROVINCIA_NASCITA = PROVINCIA_NASCITA == null ? "" : PROVINCIA_NASCITA.Trim();
             COMUNE_NASCITA = COMUNE_NASCITA == null ? "" : COMUNE_NASCITA.Trim();
             PROVINCIA = PROVINCIA == null ? "" : PROVINCIA.Trim();
             COMUNE = COMUNE == null ? "" : COMUNE.Trim();
             INDIRIZZO = INDIRIZZO == null ? "" : INDIRIZZO.Trim();
             CAP = CAP == null ? "" : CAP.Trim();
             TELEFONO1 = TELEFONO1 == null ? "" : TELEFONO1.Trim();
             TELEFONO2 = TELEFONO2 == null ? "" : TELEFONO2.Trim();
             CODICE_CE_UTENTE = CODICE_CE_UTENTE == null ? "" : CODICE_CE_UTENTE.Trim();
             CODICE_EC_UTENTE = CODICE_EC_UTENTE == null ? "" : CODICE_EC_UTENTE.Trim();
             NOTE_UTENTE = NOTE_UTENTE == null ? "" : NOTE_UTENTE.Trim();


             SETTORE = SETTORE == null ? "" : SETTORE.Trim();
             ENTE = ENTE == null ? "" : ENTE.Trim();


             AZIENDA_IMPIEGO = AZIENDA_IMPIEGO == null ? "" : AZIENDA_IMPIEGO.Trim();
             PARTITA_IVA = PARTITA_IVA == null ? "" : PARTITA_IVA.Trim();
             PROVINCIA_AZIENDA = PROVINCIA_AZIENDA == null ? "" : PROVINCIA_AZIENDA.Trim();
             COMUNE_AZIENDA = COMUNE_AZIENDA == null ? "" : COMUNE_AZIENDA.Trim();
             INDIRIZZO_AZIENDA = INDIRIZZO_AZIENDA == null ? "" : INDIRIZZO_AZIENDA.Trim();
             CAP_AZIENDA = CAP_AZIENDA == null ? "" : CAP_AZIENDA.Trim();
             TELEFONO_AZIENDA = TELEFONO_AZIENDA == null ? "" : TELEFONO_AZIENDA.Trim();
             NOTE_AZIENDA = NOTE_AZIENDA == null ? "" : NOTE_AZIENDA.Trim();
             CODICE_CE_AZIENDA = CODICE_CE_AZIENDA == null ? "" : CODICE_CE_AZIENDA.Trim();
             CODICE_EC_AZIENDA = CODICE_EC_AZIENDA == null ? "" : CODICE_EC_AZIENDA.Trim();
             CONTRATTO = CONTRATTO == null ? "" : CONTRATTO.Trim();



             DATA_INIZIO = DATA_INIZIO == null ? "" : DATA_INIZIO.Trim();
             DATA_FINE = DATA_FINE == null ? "" : DATA_FINE.Trim();
             QUOTA = QUOTA == null ? "" : QUOTA.Trim();
             LIVELLO = LIVELLO == null ? "" : LIVELLO.Trim();
             NOTE = NOTE == null ? "" : NOTE.Trim();
             ORE_LAVORATE = ORE_LAVORATE == null ? "" : ORE_LAVORATE.Trim();
             ORE_MALATTIA_INFORTUNIO = ORE_MALATTIA_INFORTUNIO == null ? "" : ORE_MALATTIA_INFORTUNIO.Trim();
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



             // i campi settore ed ente sono obbligatori

             if (SETTORE != "EDILE")
                 SETTORE = "EDILE"; //aggiusto direttamente io


             if (ENTE != "CASSA EDILE" && ENTE != "EDILCASSA")
                 _errors.Add("Ente diverso da cassa edile o edilcassa; ");


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
                     _errors.Add("Formato data inizio competenza non corretto; ");
                 }
             }
             else
                 _errors.Add("Data inizio compentenza nono impostata; ");


             if (!string.IsNullOrEmpty(DATA_FINE))
             {
                 try
                 {
                     DateTime d = DateTime.Parse(DATA_FINE);
                 }
                 catch (Exception)
                 {
                     _errors.Add("Formato data fine competenza non corretto; ");
                 }
             }
             else
                 _errors.Add("Data fine compentenza nono impostata; ");



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
    }
}

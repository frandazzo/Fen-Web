using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml.Serialization;

namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport
{
    [Serializable]
    public class LiberiTraceDetail
    {

        public LiberiTraceDetail() { }

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




        [XmlAttribute("DATA")]
        public string DATA { get; set; }
        [XmlAttribute("LIVELLO")]
        public string LIVELLO { get; set; }
        [XmlAttribute("ISCRITTO_A")]
        public string ISCRITTO_A { get; set; }
      


        public void FromHash(Hashtable t)
        {

            COGNOME_UTENTE = (t["COGNOME_UTENTE"] == null) ? "" : t["COGNOME_UTENTE"].ToString().Trim();
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

            DATA = (t["DATA"] == null) ? "" : t["DATA"].ToString().Trim();
            LIVELLO = (t["LIVELLO"] == null) ? "" : t["LIVELLO"].ToString().Trim();
            ISCRITTO_A = (t["ISCRITTO_A"] == null) ? "" : t["ISCRITTO_A"].ToString().Trim();

        }


        public Hashtable ToHash()
        {
            if (DATA_NASCITA_UTENTE == null)
                DATA_NASCITA_UTENTE = "";
            else
                DATA_NASCITA_UTENTE = DATA_NASCITA_UTENTE.Replace(" 0.00.00", "");

            if (DATA == null)
                DATA = "";
            else
                DATA = DATA.Replace(" 0.00.00", "");

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


            t.Add("DATA", DATA);
            t.Add("LIVELLO", LIVELLO);
            t.Add("ISCRITTO_A", ISCRITTO_A);
           




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


            DATA = DATA == null ? "" : DATA.Trim();
            LIVELLO = LIVELLO == null ? "" : LIVELLO.Trim();
            ISCRITTO_A = ISCRITTO_A == null ? "" : ISCRITTO_A.Trim();









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



            // il campo ente è obbligatorio

            if (ENTE != "CASSA EDILE" && ENTE != "EDILCASSA")
                _errors.Add("Ente diverso da cassa edile o edilcassa; ");


            //valido l'azienda di impiego
            if (string.IsNullOrEmpty(AZIENDA_IMPIEGO))
                _errors.Add("Azienda di impiego non specificata; ");


           

            //valido i campi della del record libero : data e iscritto_a
            if (!string.IsNullOrEmpty(DATA))
            {
                try
                {
                    DateTime d = DateTime.Parse(DATA);
                }
                catch (Exception)
                {
                    _errors.Add("Formato data non corretto; ");
                }
            }
            else
                _errors.Add("Data nono impostata; ");


            if (!string.IsNullOrEmpty(ISCRITTO_A))
                if ((ISCRITTO_A != "FILCA" && ISCRITTO_A != "FILLEA"))
                    _errors.Add("il campo iscritto_a può essere: FILCA o FILLEA");



        }




        public LiberoDTO ToLiberoDTO()
        {
            LiberoDTO result = new LiberoDTO();
            result.Address = this.INDIRIZZO;
            try
            {
                result.BirthDate = DateTime.Parse(this.DATA_NASCITA_UTENTE);
            }
            catch (Exception)
            {
                result.BirthDate = new DateTime(1900, 1, 1);
            }
           
            result.BirthPlace = this.COMUNE_NASCITA;
            result.BirthProvince = this.PROVINCIA_NASCITA;
            result.Cap = this.CAP;
            result.CurrentAzienda = this.AZIENDA_IMPIEGO;
            result.Ente = this.ENTE;
            result.Fiscalcode = this.FISCALE;
            result.IscrittoA = this.ISCRITTO_A;
             try
            {
             
                 result.LiberoAl =  DateTime.Parse(this.DATA);
            }
            catch (Exception)
            {
                result.BirthDate = DateTime.Now.Date;
            }
            
            result.LivingPlace = this.COMUNE;
            result.LivingProvince = this.PROVINCIA;
            result.Name = this.NOME_UTENTE;
            result.Nationality = "";
            result.Phone = this.TELEFONO1 + " - " + this.TELEFONO2;
            result.ProvinceName = "";
            result.Surname = this.COGNOME_UTENTE;
            


            return result;

        }
    }
}

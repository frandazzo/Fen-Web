using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.WEBCONNECTOR.SharetopServices;

namespace WIN.WEBCONNECTOR.ServiceAgents
{
    public class TestServiceAgent
    {
        private List<Struttura> CreateStruttureRegionaliEProvinciali()
        {
            List<Struttura> result = new List<Struttura>();
            //creo il congresso regione piemionte id regione 10
            Struttura piemonte = new Struttura();
            piemonte.BaseData = new SharetopBaseData();

            piemonte.BaseData.Regione = new Regione();
            piemonte.BaseData.Regione.Key = new Key();
            piemonte.BaseData.Regione.Key.Id = 10;

            piemonte.BaseData.Anno = 2014;
            piemonte.BaseData.SurveyId = 100;

            piemonte.Indirizzo = "via piemonte 14 torino";
            piemonte.RecapitiTelefonici = "010 556479";
            piemonte.Mail = "piemonte@fenealuil.it";
            piemonte.SitoInternet = "www.fenealpiemonte.it";


            CoordinataBancaria[] banche = new CoordinataBancaria[2];

            CoordinataBancaria b1 = new CoordinataBancaria();
            b1.Banca = "Mediobanca";
            b1.Iban = "00000ITPIE356989548645";
            b1.Indirizzo = "via banca 1";
            b1.NumAgenzia = "001";

            CoordinataBancaria b2 = new CoordinataBancaria();
            b2.Banca = "Popolare";
            b2.Iban = "00000ITPIExxxxxxxx5";
            b2.Indirizzo = "via bed 234";
            b2.NumAgenzia = "007";
            banche[0] = b1;
            banche[1] = b2;
            piemonte.CoordinateBancarie = banche;

            result.Add(piemonte);


            Struttura torino = new Struttura();
            torino.BaseData = new SharetopBaseData();

            torino.BaseData.Regione = new Regione();
            torino.BaseData.Regione.Key = new Key();
            torino.BaseData.Regione.Key.Id = 10;

            torino.BaseData.Provincia = new Provincia();
            torino.BaseData.Provincia.Key = new Key();
            torino.BaseData.Provincia.Key.Id = 1;

            torino.BaseData.Anno = 2012;
            torino.BaseData.SurveyId = 101;


            torino.Indirizzo = "via torino ";
            torino.RecapitiTelefonici = "010 xxxxxx";
            torino.Mail = "torino@fenealuil.it";
            torino.SitoInternet = "www.fenealto.it";

            CoordinataBancaria[] banche1 = new CoordinataBancaria[1];

            CoordinataBancaria b3 = new CoordinataBancaria();
            b3.Banca = "Mediobanca";
            b3.Iban = "00000ITPIE356989548645";
            b3.Indirizzo = "via banca 1";
            b3.NumAgenzia = "001";
            banche1[0] = b3;
            torino.CoordinateBancarie = banche1;


            result.Add(torino);




            return result;

        }





        private List<CongressoRegionale> CreateCongressoRegionali()
        {
            List<CongressoRegionale> result = new List<CongressoRegionale>();
            //creo il congresso regione piemionte id regione 10
            CongressoRegionale piemonte = new CongressoRegionale();
            piemonte.BaseData = new SharetopBaseData();

            piemonte.BaseData.Regione = new Regione();
            piemonte.BaseData.Regione.Key = new Key();
            piemonte.BaseData.Regione.Key.Id = 10;

            piemonte.BaseData.Anno = 2014;
            piemonte.BaseData.SurveyId = 100;

            piemonte.SegretarioGenerale = "Beppe Manta";
            piemonte.SegretarioOrganizzativo = "Beppe Manta";
            piemonte.Tesoriere = "Beppe Manta";
            piemonte.MembriSegretaria = "Beppe Manta;Chiara Maffè; Teresa marcon";
            piemonte.ConsiglioTerritoriale = "Beppe Manta;Chiara Maffè; Teresa marcon;Valeriano Delicio";
            piemonte.ConsiglioTerritorialeSupplente = "Gianfranco De Palo; Mino Paolicelli";
            piemonte.AssembleaTerritoriale = "Teresa marcon;Valeriano Delicio";
            piemonte.AssembleaTerritorialeSupplente = "Chiara Maffè; Teresa marcon";

            piemonte.RevisoriDeiConti = "Rocco Randazzo";
            piemonte.Probiviri = "Nessuno";

            result.Add(piemonte);


            CongressoRegionale piemonteold = new CongressoRegionale();
            piemonteold.BaseData = new SharetopBaseData();

            piemonteold.BaseData.Regione = new Regione();
            piemonteold.BaseData.Regione.Key = new Key();
            piemonteold.BaseData.Regione.Key.Id = 10;

            piemonteold.BaseData.Anno = 2012;
            piemonteold.BaseData.SurveyId = 101;

            piemonteold.SegretarioGenerale = "Beppecccccccc Manta";
            piemonteold.SegretarioOrganizzativo = "Beppeccccccc Manta";
            piemonteold.Tesoriere = "Beppeccccccc Manta";
            piemonteold.MembriSegretaria = "Beppeccccccc Manta;Chiara Maffè; Teresa marcon";
            piemonteold.ConsiglioTerritoriale = "Beppeccccccc Manta;Chiara Maffè; Teresa marcon;Valeriano Delicio";
            piemonteold.ConsiglioTerritorialeSupplente = "Gianfrancoccccccccc De Palo; Mino Paolicelli";
            piemonteold.AssembleaTerritoriale = "Teresaccccccc marcon;Valeriano Delicio";
            piemonteold.AssembleaTerritorialeSupplente = "Chiaraccccc Maffè; Teresa marcon";

            piemonteold.RevisoriDeiConti = "Rocco Randazzocccccc";
            piemonteold.Probiviri = "Nessunocccccc";

            result.Add(piemonteold);


            CongressoRegionale toscana = new CongressoRegionale();
            toscana.BaseData = new SharetopBaseData();

            toscana.BaseData.Regione = new Regione();
            toscana.BaseData.Regione.Key = new Key();
            toscana.BaseData.Regione.Key.Id = 90;

            toscana.BaseData.Anno = 2014;
            toscana.BaseData.SurveyId = 102;

            toscana.SegretarioGenerale = "Beppe Manta11";
            toscana.SegretarioOrganizzativo = "Beppe Manta1111";
            toscana.Tesoriere = "Beppe Manta11111";
            toscana.MembriSegretaria = "Beppe11111 Manta;Chiara Maffè; Teresa marcon";
            toscana.ConsiglioTerritoriale = "Beppe111111 Manta;Chiara Maffè; Teresa marcon;Valeriano Delicio";
            toscana.ConsiglioTerritorialeSupplente = "Gianfranco11111 De Palo; Mino Paolicelli";
            toscana.AssembleaTerritoriale = "Teresa11111 marcon;Valeriano Delicio";
            toscana.AssembleaTerritorialeSupplente = "Chiar11111a Maffè; Teresa marcon";

            toscana.RevisoriDeiConti = "Rocco Randazzo11111";
            toscana.Probiviri = "Nessuno1111111";


            result.Add(toscana);

            return result;

        }


        private List<CongressoTerritoriale> CreateCongressoTerritoriale()
        {
            List<CongressoTerritoriale> result = new List<CongressoTerritoriale>();
            //creo il congresso regione piemionte id regione 10
            CongressoTerritoriale torino = new CongressoTerritoriale();
            torino.BaseData = new SharetopBaseData();

            torino.BaseData.Regione = new Regione();
            torino.BaseData.Regione.Key = new Key();
            torino.BaseData.Regione.Key.Id = 10;


            torino.BaseData.Provincia = new Provincia();
            torino.BaseData.Provincia.Key = new Key();
            torino.BaseData.Provincia.Key.Id = 1;

            torino.BaseData.Anno = 2014;
            torino.BaseData.SurveyId = 100;

            torino.SegretarioGenerale = "Beppe Manta";
            torino.SegretarioOrganizzativo = "Beppe Manta";
            torino.Tesoriere = "Beppe Manta";
            torino.MembriSegretaria = "Beppe Manta;Chiara Maffè; Teresa marcon";
            torino.ConsiglioTerritoriale = "Beppe Manta;Chiara Maffè; Teresa marcon;Valeriano Delicio";
            torino.ConsiglioTerritorialeSupplente = "Gianfranco De Palo; Mino Paolicelli";
            torino.AssembleaTerritoriale = "Teresa marcon;Valeriano Delicio";
            torino.AssembleaTerritorialeSupplente = "Chiara Maffè; Teresa marcon";

            torino.RevisoriDeiConti = "Rocco Randazzo";


            result.Add(torino);


            CongressoTerritoriale torinoold = new CongressoTerritoriale();
            torinoold.BaseData = new SharetopBaseData();

            torinoold.BaseData.Regione = new Regione();
            torinoold.BaseData.Regione.Key = new Key();
            torinoold.BaseData.Regione.Key.Id = 10;

            torinoold.BaseData.Provincia = new Provincia();
            torinoold.BaseData.Provincia.Key = new Key();
            torinoold.BaseData.Provincia.Key.Id = 1;

            torinoold.BaseData.Anno = 2012;
            torinoold.BaseData.SurveyId = 101;

            torinoold.SegretarioGenerale = "Beppecccccccc Manta";
            torinoold.SegretarioOrganizzativo = "Beppeccccccc Manta";
            torinoold.Tesoriere = "Beppeccccccc Manta";
            torinoold.MembriSegretaria = "Beppeccccccc Manta;Chiara Maffè; Teresa marcon";
            torinoold.ConsiglioTerritoriale = "Beppeccccccc Manta;Chiara Maffè; Teresa marcon;Valeriano Delicio";
            torinoold.ConsiglioTerritorialeSupplente = "Gianfrancoccccccccc De Palo; Mino Paolicelli";
            torinoold.AssembleaTerritoriale = "Teresaccccccc marcon;Valeriano Delicio";
            torinoold.AssembleaTerritorialeSupplente = "Chiaraccccc Maffè; Teresa marcon";

            torinoold.RevisoriDeiConti = "Rocco Randazzocccccc";


            result.Add(torinoold);


            CongressoTerritoriale livorno = new CongressoTerritoriale();
            livorno.BaseData = new SharetopBaseData();

            livorno.BaseData.Regione = new Regione();
            livorno.BaseData.Regione.Key = new Key();
            livorno.BaseData.Regione.Key.Id = 90;

            livorno.BaseData.Provincia = new Provincia();
            livorno.BaseData.Provincia.Key = new Key();
            livorno.BaseData.Provincia.Key.Id = 49;

            livorno.BaseData.Anno = 2014;
            livorno.BaseData.SurveyId = 102;

            livorno.SegretarioGenerale = "Beppe Manta11";
            livorno.SegretarioOrganizzativo = "Beppe Manta1111";
            livorno.Tesoriere = "Beppe Manta11111";
            livorno.MembriSegretaria = "Beppe11111 Manta;Chiara Maffè; Teresa marcon";
            livorno.ConsiglioTerritoriale = "Beppe111111 Manta;Chiara Maffè; Teresa marcon;Valeriano Delicio";
            livorno.ConsiglioTerritorialeSupplente = "Gianfranco11111 De Palo; Mino Paolicelli";
            livorno.AssembleaTerritoriale = "Teresa11111 marcon;Valeriano Delicio";
            livorno.AssembleaTerritorialeSupplente = "Chiar11111a Maffè; Teresa marcon";

            livorno.RevisoriDeiConti = "Rocco Randazzo11111";



            result.Add(livorno);

            return result;

        }


        public void SaveCongressoRegionale()
        {

            SharetopServices.Credenziali c = new SharetopServices.Credenziali();
            c.UserName = "randazzo";
            c.Password = "francesco1";

            SharetopServices.SharetopIntegration serv = new SharetopIntegration();
            serv.CredenzialiValue = c;
            CongressoRegionale l = CreateCongressoRegionali()[0];
            CongressoRegionale l1 = CreateCongressoRegionali()[1];
            CongressoRegionale l2 = CreateCongressoRegionali()[2];
            String output = serv.SaveCongressoRegionale(l);
            serv.SaveCongressoRegionale(l1);
            serv.SaveCongressoRegionale(l2);
        }

        public void SaveCongressoTerritoriale()
        {

            SharetopServices.Credenziali c = new SharetopServices.Credenziali();
            c.UserName = "randazzo";
            c.Password = "francesco1";

            SharetopServices.SharetopIntegration serv = new SharetopIntegration();
            serv.CredenzialiValue = c;

            CongressoTerritoriale l = CreateCongressoTerritoriale()[0];
            CongressoTerritoriale l1 = CreateCongressoTerritoriale()[1];
            CongressoTerritoriale l2 = CreateCongressoTerritoriale()[2];
            String output = serv.SaveCongressoTerritoriale(l);
            serv.SaveCongressoTerritoriale(l1);
            serv.SaveCongressoTerritoriale(l2);
        }


        public void SaveStruttura()
        {

            SharetopServices.Credenziali c = new SharetopServices.Credenziali();
            c.UserName = "randazzo";
            c.Password = "francesco1";

            SharetopServices.SharetopIntegration serv = new SharetopIntegration();
            serv.CredenzialiValue = c;
            List<Struttura> h = CreateStruttureRegionaliEProvinciali();
            Struttura l = h[0];
            Struttura l1 = h[1];

            String output = serv.SaveStruttura(l);
            serv.SaveStruttura(l1);

        }


        public void SaveRappresentanti()
        {

            SharetopServices.Credenziali c = new SharetopServices.Credenziali();
            c.UserName = "randazzo";
            c.Password = "francesco1";

            SharetopServices.SharetopIntegration serv = new SharetopIntegration();
            serv.CredenzialiValue = c;
            List<Rappresentante> h = CreateRappresentanti();


            String output = serv.SaveRappresentanti(h.ToArray());


        }

        public void SaveRappresentanze()
        {

            SharetopServices.Credenziali c = new SharetopServices.Credenziali();
            c.UserName = "randazzo";
            c.Password = "francesco1";

            SharetopServices.SharetopIntegration serv = new SharetopIntegration();
            serv.CredenzialiValue = c;
            List<Rappresentanza> h = CreateRappresentanze();


            String output = serv.SaveRappresentanze(h.ToArray());


        }


        private List<Rappresentante> CreateRappresentanti()
        {
            List<Rappresentante> r = new List<Rappresentante>();

            Rappresentante r1 = new Rappresentante();


            r1.BaseData = new SharetopBaseData();
            r1.BaseData.Regione = new Regione();
            r1.BaseData.Regione.Key = new Key();
            r1.BaseData.Regione.Key.Id = 10;


            r1.BaseData.Provincia = new Provincia();
            r1.BaseData.Provincia.Key = new Key();
            r1.BaseData.Provincia.Key.Id = 1;

            r1.BaseData.Anno = 2014;
            r1.BaseData.SurveyId = 100;


            r1.Azienda = "Natuzzi";
            r1.CodiceFiscale = "RNDFNC77L14F052F";
            r1.CodInpsAzienda = "000067";
            r1.Cognome = "Randazzo";
            r1.ComuneAzienda = "Matera";
            r1.Contratto = "CCNL";
            r1.Mail = "fg.randazzo@hotmail.it";
            r1.Nome = "Francesco";
            r1.ProvinciaAzienda = "MT";
            r1.Telefono = "0835381028";
            r1.Tipo = "RSA";

            r.Add(r1);



            Rappresentante r2 = new Rappresentante();
            r2.BaseData = new SharetopBaseData();
            r2.BaseData.Regione = new Regione();
            r2.BaseData.Regione.Key = new Key();
            r2.BaseData.Regione.Key.Id = 10;


            r2.BaseData.Provincia = new Provincia();
            r2.BaseData.Provincia.Key = new Key();
            r2.BaseData.Provincia.Key.Id = 1;

            r2.BaseData.Anno = 2014;
            r2.BaseData.SurveyId = 100;

            r1.BaseData.Anno = 2014;
            r1.BaseData.SurveyId = 100;
            r2.Azienda = "Natuzzixxxxx";
            r2.CodiceFiscale = "RNDFNC77L14F052Fxxxxx";
            r2.CodInpsAzienda = "000067xxxx";
            r2.Cognome = "Randazzoxxx";
            r2.ComuneAzienda = "Materaxxxx";
            r2.Contratto = "CCNLxxxxx";
            r2.Mail = "fg.randazzo@hotmail.itxxxxxx";
            r2.Nome = "Francescoxxxxx";
            r2.ProvinciaAzienda = "MTxxxxx";
            r2.Telefono = "0835381028xxxxx";
            r2.Tipo = "RSUxxxxx";

            r.Add(r2);

            return r;
        }


        private List<Rappresentanza> CreateRappresentanze()
        {
            List<Rappresentanza> r = new List<Rappresentanza>();

            Rappresentanza r1 = new Rappresentanza();


            r1.BaseData = new SharetopBaseData();
            r1.BaseData.Regione = new Regione();
            r1.BaseData.Regione.Key = new Key();
            r1.BaseData.Regione.Key.Id = 10;


            r1.BaseData.Provincia = new Provincia();
            r1.BaseData.Provincia.Key = new Key();
            r1.BaseData.Provincia.Key.Id = 1;

            r1.BaseData.Anno = 2014;
            r1.BaseData.SurveyId = 100;


            r1.Azienda = "Natuzzi";
            r1.CodInpsAzienda = "000067";
            r1.ProvinciaAzienda = "MT";
            r1.ComuneAzienda = "Matera";
            r1.Contratto = "CCNL";
            r1.DataElezione = new DateTime(2015, 7, 14);
            r1.NumDipendenti = 15;
            r1.NumIscrittiFeneal = 7;
            r1.NumRappresentantiFeneal = 2;
            r1.TipoNomina = "RSU";
            r1.UrlVerbale = "http://www.cvsa.eu/utile/LA%20FeLV.pdf";
            r1.VotiListaFeneal = 6;

            r.Add(r1);



            Rappresentanza r2 = new Rappresentanza();
            r2.BaseData = new SharetopBaseData();
            r2.BaseData.Regione = new Regione();
            r2.BaseData.Regione.Key = new Key();
            r2.BaseData.Regione.Key.Id = 10;


            r2.BaseData.Provincia = new Provincia();
            r2.BaseData.Provincia.Key = new Key();
            r2.BaseData.Provincia.Key.Id = 1;

            r2.BaseData.Anno = 2014;
            r2.BaseData.SurveyId = 100;

            r1.BaseData.Anno = 2014;
            r1.BaseData.SurveyId = 100;
            r2.Azienda = "Natuzzixxxxx";
            r2.CodInpsAzienda = "000067xxxx";
            r2.ProvinciaAzienda = "MTxxxxx";
            r2.ComuneAzienda = "Materaxxxx";
            r2.Contratto = "CCNLxxxxx";

            r2.DataElezione = DateTime.MinValue;// new DateTime(1900, 1, 1);
            r2.NumDipendenti = 115;
            r2.NumIscrittiFeneal = 17;
            r2.NumRappresentantiFeneal = 12;
            r2.TipoNomina = "RSA";
            r2.UrlVerbale = "www.cvsa.eu/utile/LA%20FeLV.pdf";
            r2.VotiListaFeneal = 16;


            r.Add(r2);

            return r;
        }

    }





}

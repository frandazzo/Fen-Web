using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIN.WEBCONNECTOR.SharetopServices;

namespace WIN.WEBCONNECTOR.ServiceAgents
{
    public class SharetopIntegrationServiceAgent
    {

        public AdministrativeRecord[] FindAdministrativeData(int year)
        {
            SharetopServices.Credenziali c = new SharetopServices.Credenziali();
            c.UserName = "randazzo";
            c.Password = "francesco1";

            SharetopServices.SharetopIntegration serv = new SharetopIntegration();
            serv.CredenzialiValue = c;
            AdministrativeRecord[] o = serv.FindAdministrativeData(year);

            return o;
        }

        public OrganizativeRecord[] FindOrganizzativeData(int year)
        {
            SharetopServices.Credenziali c = new SharetopServices.Credenziali();
            c.UserName = "randazzo";
            c.Password = "francesco1";

            SharetopServices.SharetopIntegration serv = new SharetopIntegration();
            serv.CredenzialiValue = c;
            OrganizativeRecord[] o = serv.FindOrganizativeData(year);

            return o;
        }

        public OrganizzativeData FindOrganizzativeData(int regionId, int provinceId, int year)
        {
                SharetopServices.Credenziali c = new SharetopServices.Credenziali();
                c.UserName = "randazzo";
                c.Password = "francesco1";

                SharetopServices.SharetopIntegration serv = new SharetopIntegration();
                serv.CredenzialiValue = c;
                OrganizzativeData o = serv.FindOrganizzativeData(regionId, provinceId, year);

                return o;
        }

        public CongressPresence[] FindCongressPresence(string name, string surname)
        {
            SharetopServices.Credenziali c = new SharetopServices.Credenziali();
            c.UserName = "randazzo";
            c.Password = "francesco1";
            SharetopServices.SharetopIntegration serv = new SharetopIntegration();
            serv.CredenzialiValue = c;
            return serv.findDuplicateRoles(name, surname);

        }


        public RisorsaUmana[] FindRisorseUmane(int year)
        {
            SharetopServices.Credenziali c = new SharetopServices.Credenziali();
            c.UserName = "randazzo";
            c.Password = "francesco1";
            SharetopServices.SharetopIntegration serv = new SharetopIntegration();
            serv.CredenzialiValue = c;
            return serv.FindRisorseUmaneByYear(year);

        }

        public DatiCassaEdile[] FindDatiCasseEdili(int year)
        {
            SharetopServices.Credenziali c = new SharetopServices.Credenziali();
            c.UserName = "randazzo";
            c.Password = "francesco1";
            SharetopServices.SharetopIntegration serv = new SharetopIntegration();
            serv.CredenzialiValue = c;
            return serv.FindDatiCasseEdiliByYear(year);

        }

        public void TestSaveDatiCasseEdili()
        {
            SharetopServices.Credenziali c = new SharetopServices.Credenziali();
            c.UserName = "randazzo";
            c.Password = "francesco1";
            SharetopServices.SharetopIntegration serv = new SharetopIntegration();
            serv.CredenzialiValue = c;
            serv.SaveDatiCassaEdile(constructDataDatiCassa());
        }

        public void TestSaveRisorseUmane()
        {
            SharetopServices.Credenziali c = new SharetopServices.Credenziali();
            c.UserName = "randazzo";
            c.Password = "francesco1";
            SharetopServices.SharetopIntegration serv = new SharetopIntegration();
            serv.CredenzialiValue = c;


            serv.SaveRisorseUmane(constructDataRisorse());
        }


        private RisorsaUmana[] constructDataRisorse()
        {
            RisorsaUmana[] r = new RisorsaUmana[2];
            RisorsaUmana s = new RisorsaUmana();
            s.BaseData = new SharetopBaseData();
            s.BaseData.SurveyId = 1;
            s.BaseData.Regione = new Regione();
            s.BaseData.Regione.Key = new SharetopServices.Key();
            s.BaseData.Regione.Key.Id = 10;

            s.BaseData.Provincia = new Provincia();
            s.BaseData.Provincia.Key = new SharetopServices.Key();
            s.BaseData.Provincia.Key.Id = 1;
            s.BaseData.Anno = 2015;

            s.CodiceFiscale = "rndfnc77l14f052f";
            s.Cognome = "randazzo";
            s.Nome = "francesco";
            s.Mail = "fg.randazzo@hotmail.it";
            s.Indirizzo = "via gravina 39 matera";
            s.DataNascita = "14-07-1977";
            s.Note = "nessuna nota";
            s.Ruolo = "segretario generale";
            s.Telefono = "3385269726";
            s.Tipo = "regionale";
            s.TipoRapporto = "parttime";

            r[0] = s;

            RisorsaUmana s1 = new RisorsaUmana();
            s1.BaseData = new SharetopBaseData();
            s1.BaseData.SurveyId = 1;
            s1.BaseData.Regione = new Regione();
            s1.BaseData.Regione.Key = new SharetopServices.Key();
            s1.BaseData.Regione.Key.Id = 10;

            s1.BaseData.Provincia = new Provincia();
            s1.BaseData.Provincia.Key = new SharetopServices.Key();
            s1.BaseData.Provincia.Key.Id = 1;
            s1.BaseData.Anno = 2015;

            s1.CodiceFiscale = "rndfnc77l14f052fddd";
            s1.Cognome = "randazzoddd";
            s1.Nome = "francescoddd";
            s1.Mail = "fg.randazzo@hotmail.itddd";
            s1.Indirizzo = "via gravina 39 materaddd";
            s1.DataNascita = "14-07-1977ddd";
            s1.Note = "nessuna notaddd";
            s1.Ruolo = "segretario generaleddd";
            s1.Telefono = "3385269726ddd";
            s1.Tipo = "regionaleddd";
            s1.TipoRapporto = "parttimeddd";
            r[1] = s1;

            return r;
        }
        private DatiCassaEdile[] constructDataDatiCassa()
        {
            DatiCassaEdile[] r = new DatiCassaEdile[2];
            DatiCassaEdile s = new DatiCassaEdile();
            s.BaseData = new SharetopBaseData();
            s.BaseData.SurveyId = 1;
            s.BaseData.Regione = new Regione();
            s.BaseData.Regione.Key = new SharetopServices.Key();
            s.BaseData.Regione.Key.Id = 10;
            
            s.BaseData.Provincia = new Provincia();
            s.BaseData.Provincia.Key = new SharetopServices.Key();
            s.BaseData.Provincia.Key.Id = 1;
           
            s.BaseData.Anno = 2015;

            s.Addetti = 15;
            s.DelegheFeneal = 110;
            s.DelegheFilca = 100;
            s.DelegheFillea = 1234;
            s.ImportoDelegheFeneal = 11.65;
            s.ImportoQACN = 87.78;
            s.ImportoQACP = 77.00;
            s.ImportoQACR = 22.12;
            s.Imprese = 1234;
            s.IscrittiOOSS = 100;
            s.MSDenunciato = 1111112344.67;
            s.MSVersato = 33567754.890;
            s.PercFeneal = 12.678;
            s.PercFilca = 69.8;
            s.PercFillea = 0.87;
            s.PercSindacalizzati = 122.33;
            



            r[0] = s;

            DatiCassaEdile s1 = new DatiCassaEdile();
            s1.BaseData = new SharetopBaseData();
            s1.BaseData.SurveyId = 1;
            s1.BaseData.Regione = new Regione();
            s1.BaseData.Regione.Key = new SharetopServices.Key();
            s1.BaseData.Regione.Key.Id = 10;

            s1.BaseData.Provincia = new Provincia();
            s1.BaseData.Provincia.Key = new SharetopServices.Key();
            s1.BaseData.Provincia.Key.Id = 1;
            s1.BaseData.Anno = 2015;

            s1.Addetti = 15;
            s1.DelegheFeneal = 110;
            s1.DelegheFilca = 100;
            s1.DelegheFillea = 1234;
            s1.ImportoDelegheFeneal = 11.65;
            s1.ImportoQACN = 87.78;
            s1.ImportoQACP = 77.00;
            s1.ImportoQACR = 22.12;
            s1.Imprese = 1234;
            s1.IscrittiOOSS = 100;
            s1.MSDenunciato = 1111112344.67;
            s1.MSVersato = 33567754.890;
            s1.PercFeneal = 12.678;
            s1.PercFilca = 69.8;
            s1.PercFillea = 0.87;
            s1.PercSindacalizzati = 122.33;


            r[1] = s1;
            return r;
        }
  

    }
}

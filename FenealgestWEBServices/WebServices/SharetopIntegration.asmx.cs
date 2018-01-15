using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using WIN.FENGEST_NAZIONALE.DOMAIN.GestioneOrganizzativa;
using WIN.FENGEST_NAZIONALE.DOMAIN.Security;
using WIN.FENGEST_NAZIONALE.DOMAIN.SharetopIntegration;
using WIN.FENGEST_NAZIONALE.HANDLERS.GestioneOrganizzativa;
using WIN.FENGEST_NAZIONALE.HANDLERS.SecurityProviders;
using WIN.FENGEST_NAZIONALE.HANDLERS.SharetopIntegration;
using WIN.TECHNICAL.PERSISTENCE;
using WIN.TECHNICAL.SECURITY_NEW;
using WIN.TECHNICAL.SECURITY_NEW.Login;

namespace FenealgestWEB.WebServices
{
    /// <summary>
    /// Descrizione di riepilogo per SharetopIntegration
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Per consentire la chiamata di questo servizio Web dallo script utilizzando ASP.NET AJAX, rimuovere il commento dalla riga seguente. 
    // [System.Web.Script.Services.ScriptService]
    public class SharetopIntegration : System.Web.Services.WebService
    {
        IPersistenceFacade f;
        SecurityController c;
        

        public Credenziali credential;


        private string CheckIdentificationAndAuthorizzation(LoginResult r)
        {
            //verifico l'accesso
            if (!r.CanAccess)
                return r.LoginMessage;



            return "";
        }

        private void Initialize()
        {
            //inizializzo i servizi per la persistenza
            f = DataAccessServices.SimplePersistenceFacadeInstance();


            //inizializzo i servizi per la sicurezza
            c = SecurityController.NewInstance;
            c.InializeSecurityController(new UserProvider(f), new WIN.FENGEST_NAZIONALE.DOMAIN.Security.RoleProvider(), new AccessChecker(new UserLocker(f)));
            c.ResetLogin();
        }



        [WebMethod]
        [SoapHeader("credential")]
        public CongressPresence[] findDuplicateRoles(string name, string surname)
        {

            
                string userName = credential.UserName;
                string password = credential.Password;


                Initialize();



                //eseguo il login
                LoginResult r = c.Login(userName, password);

                string error = CheckIdentificationAndAuthorizzation(r);

                if (!string.IsNullOrEmpty(error))
                {
                    List<CongressPresence> l = new List<CongressPresence>();
                    CongressPresence s1 = new CongressPresence();
                  
                    l.Add(s1);
                    return l.ToArray();
                }
                   


                try
                {
                    CongressDataRetriever ret = new CongressDataRetriever(f);
                    IList<CongressPresence> l = ret.FindCongressData(name, surname);
                    return l.ToArray();
                    
                }
                catch (Exception ex)
                {
                    List<CongressPresence> l = new List<CongressPresence>();
                    CongressPresence s1 = new CongressPresence();
                   
                    l.Add(s1);
                    return l.ToArray();
                }


                
        }






        [WebMethod]
        [SoapHeader("credential")]
        public string SaveDatiCassaEdile(DatiCassaEdile[] datiCassaEdile)
        {

            try
            {
                string userName = credential.UserName;
                string password = credential.Password;


                Initialize();



                //eseguo il login
                LoginResult r = c.Login(userName, password);

                string error = CheckIdentificationAndAuthorizzation(r);

                if (!string.IsNullOrEmpty(error))
                {
                    return error;
                }

                DatiCassaEdileDataHandler h = new DatiCassaEdileDataHandler(f);
               
                h.Save(datiCassaEdile);
                

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [WebMethod]
        [SoapHeader("credential")]
        public string SaveRisorseUmane(RisorsaUmana[] risorseUmane)
        {

            try
            {
                string userName = credential.UserName;
                string password = credential.Password;


                Initialize();



                //eseguo il login
                LoginResult r = c.Login(userName, password);

                string error = CheckIdentificationAndAuthorizzation(r);

                if (!string.IsNullOrEmpty(error))
                {
                    return error;
                }

                RisorsaUmanaDatahandler h = new RisorsaUmanaDatahandler(f);
                
                h.Save(risorseUmane);
               

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        [WebMethod]
        [SoapHeader("credential")]
        public string SaveCongressoRegionale(CongressoRegionale congresso)
        {
            
            try
            {
                string userName = credential.UserName;
                string password = credential.Password;


                Initialize();



                //eseguo il login
                LoginResult r = c.Login(userName, password);

                string error = CheckIdentificationAndAuthorizzation(r);

                if (!string.IsNullOrEmpty(error))
                {
                    return error;
                }


                CongressoRegionaleDataHandler h = new CongressoRegionaleDataHandler(f);
                h.Save(congresso);

                
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [WebMethod]
        [SoapHeader("credential")]
        public string SaveCongressoTerritoriale(CongressoTerritoriale congresso)
        {
            try
            {
                string userName = credential.UserName;
                string password = credential.Password;


                Initialize();



                //eseguo il login
                LoginResult r = c.Login(userName, password);

                string error = CheckIdentificationAndAuthorizzation(r);

                if (!string.IsNullOrEmpty(error))
                {
                    return error;
                }


                CongressoTerritorialeDataHandler h = new CongressoTerritorialeDataHandler(f);
                h.Save(congresso);


                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [WebMethod]
        [SoapHeader("credential")]
        public string SaveStruttura(Struttura struttura)
        {
            try
            {
                string userName = credential.UserName;
                string password = credential.Password;


                Initialize();



                //eseguo il login
                LoginResult r = c.Login(userName, password);

                string error = CheckIdentificationAndAuthorizzation(r);

                if (!string.IsNullOrEmpty(error))
                {
                    return error;
                }


                StrutturaDataHandler h = new StrutturaDataHandler(f);
                h.Save(struttura);


                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [WebMethod]
        [SoapHeader("credential")]
        public string SaveRappresentanti(Rappresentante[] rappresentanti)
        {
            try
            {
                string userName = credential.UserName;
                string password = credential.Password;


                Initialize();



                //eseguo il login
                LoginResult r = c.Login(userName, password);

                string error = CheckIdentificationAndAuthorizzation(r);

                if (!string.IsNullOrEmpty(error))
                {
                    return error;
                }


                RappresentanteDataHandler h = new RappresentanteDataHandler(f);

                h.Save(rappresentanti);
               


                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [WebMethod]
        [SoapHeader("credential")]
        public string SaveRappresentanze(Rappresentanza[] rappresentanze)
        {
            try
            {
                string userName = credential.UserName;
                string password = credential.Password;


                Initialize();



                //eseguo il login
                LoginResult r = c.Login(userName, password);

                string error = CheckIdentificationAndAuthorizzation(r);

                if (!string.IsNullOrEmpty(error))
                {
                    return error;
                }


                RappresentanzaDataHandler h = new RappresentanzaDataHandler(f);

                h.Save(rappresentanze);



                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [WebMethod]
        [SoapHeader("credential")]
        public List<OrganizativeRecord> FindOrganizativeData(int year)
        {
           
                string userName = credential.UserName;
                string password = credential.Password;

                Initialize();

                //eseguo il login
                LoginResult r = c.Login(userName, password);

                string error = CheckIdentificationAndAuthorizzation(r);

               if (!string.IsNullOrEmpty(error))
                    throw new Exception( error);
               

                OrganizzativeDataHandler h = new OrganizzativeDataHandler(f);

                return h.GetDataByYear(year);

        }

        [WebMethod]
        [SoapHeader("credential")]
        public List<AdministrativeRecord> FindAdministrativeData(int year)
        {

            string userName = credential.UserName;
            string password = credential.Password;

            Initialize();

            //eseguo il login
            LoginResult r = c.Login(userName, password);

            string error = CheckIdentificationAndAuthorizzation(r);

            if (!string.IsNullOrEmpty(error))
                throw new Exception(error);


            AdministrativeDataHandler h = new AdministrativeDataHandler(f);

            return h.GetDataByYear(year);

        }




        [WebMethod]
        [SoapHeader("credential")]
        public string SaveAdministrativeData(int year, AdministrativeRecord[] data)
        {
            try
            {
                string userName = credential.UserName;
                string password = credential.Password;

                Initialize();

                //eseguo il login
                LoginResult r = c.Login(userName, password);

                string error = CheckIdentificationAndAuthorizzation(r);

                if (!string.IsNullOrEmpty(error))
                {
                    return error;
                }

                AdministrativeDataHandler h = new AdministrativeDataHandler(f);

                h.InsertData(year,new List<AdministrativeRecord>( data));

                return "";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [WebMethod]
        [SoapHeader("credential")]
        public string SaveOrganizativeData(int year, OrganizativeRecord[] data)
        {
            try
            {
                string userName = credential.UserName;
                string password = credential.Password;

                Initialize();

                //eseguo il login
                LoginResult r = c.Login(userName, password);

                string error = CheckIdentificationAndAuthorizzation(r);

                if (!string.IsNullOrEmpty(error))
                {
                    return error;
                }

                OrganizzativeDataHandler h = new OrganizzativeDataHandler(f);

                h.InsertData(year, new List<OrganizativeRecord>(data));

                return "";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }





       
        private Struttura FindStruttura(int regionId, int provinceId, int year)
        {
            try
            {
                StrutturaDataRetriever ret = new StrutturaDataRetriever(f);
                return ret.FindStruttura(regionId, provinceId, year);
            }
            catch (Exception ex)
            {
                Struttura s = new Struttura();
                s.BaseData = new SharetopBaseData();
                s.BaseData.OperationResult.Error = ex.Message;
                return s;
            }
            
        }

      
        private CongressoRegionale FindCongressoRegionale(int regionId, int year)
        {
            try
            {
               
                CongressoRegionaleDataRetriever ret = new CongressoRegionaleDataRetriever(f);
                return ret.FindCongressoRegionale(regionId, year);
            }
            catch (Exception ex)
            {
                CongressoRegionale s = new CongressoRegionale();
                s.BaseData = new SharetopBaseData();
                s.BaseData.OperationResult.Error = ex.Message;
                return s;
            }

        }

        
        private CongressoTerritoriale FindCongressoTerritoriale(int regionId, int provinceId, int year)
        {
            try
            {
                

                CongressoTerritorialeDataRetriever ret = new CongressoTerritorialeDataRetriever(f);
                return ret.FindCongressoTerritoriale(regionId, provinceId, year);
            }
            catch (Exception ex)
            {
                CongressoTerritoriale s = new CongressoTerritoriale();
                s.BaseData = new SharetopBaseData();
                s.BaseData.OperationResult.Error = ex.Message;
                return s;
            }

        }


        
        private List<Rappresentante> FindRappresentanti(int regionId, int provinceId, int year)
        {
            try
            {
                
                RappresentanteDataRetriever ret = new RappresentanteDataRetriever(f);
                return ret.FindRappresentante (regionId, provinceId, year);
            }
            catch (Exception ex)
            {
                List<Rappresentante> l = new List<Rappresentante>();
                Rappresentante s1 = new Rappresentante();
                s1.BaseData = new SharetopBaseData();
                s1.BaseData.OperationResult.Error = ex.Message ;
                l.Add(s1);
                return l;
            }

        }


        private List<RisorsaUmana> FindRisorsaUmana(int regionId, int provinceId, int year)
        {
            try
            {

                RisorsaUmanaDataRetriever ret = new RisorsaUmanaDataRetriever(f);
                return ret.FindRisorseUmane(regionId, provinceId, year);
            }
            catch (Exception ex)
            {
                List<RisorsaUmana> l = new List<RisorsaUmana>();
                RisorsaUmana s1 = new RisorsaUmana();
                s1.BaseData = new SharetopBaseData();
                s1.BaseData.OperationResult.Error = ex.Message;
                l.Add(s1);
                return l;
            }

        }




       
        private List<Rappresentanza> FindRappresentanze(int regionId, int provinceId, int year)
        {
            try
            {
                

                RappresentanzaDataRetriever ret = new RappresentanzaDataRetriever(f);
                return ret.FindRappresentanza(regionId, provinceId, year);
            }
            catch (Exception ex)
            {
                List<Rappresentanza> l = new List<Rappresentanza>();
                Rappresentanza s1 = new Rappresentanza();
                s1.BaseData = new SharetopBaseData();
                s1.BaseData.OperationResult.Error = ex.Message ;
                l.Add(s1);
                return l;
            }

        }


        [WebMethod]
        [SoapHeader("credential")]
        public DatiCassaEdile[] FindDatiCasseEdiliByYear(int year)
        {
            try
            {
                string userName = credential.UserName;
                string password = credential.Password;


                Initialize();



                //eseguo il login
                LoginResult r = c.Login(userName, password);

                string error = CheckIdentificationAndAuthorizzation(r);

                if (!string.IsNullOrEmpty(error))
                {
                    throw new Exception(error);
                }

                DatiCassaEdileDataRetriever ret = new DatiCassaEdileDataRetriever(f);
                IList result = ret.FindDatiCassaEdileByAnno(year);
                if (result.Count == 0)
                    return new DatiCassaEdile[] { };

                DatiCassaEdile[] l = new DatiCassaEdile[result.Count];
                int counter = 0;
                foreach (DatiCassaEdile item in result)
                {
                    l[counter] = item;
                    counter++;
                }
                return l;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        [WebMethod]
        [SoapHeader("credential")]
        public RisorsaUmana[] FindRisorseUmaneByYear(int year)
        {
            try
            {
                string userName = credential.UserName;
                string password = credential.Password;


                Initialize();



                //eseguo il login
                LoginResult r = c.Login(userName, password);

                string error = CheckIdentificationAndAuthorizzation(r);

                if (!string.IsNullOrEmpty(error))
                {
                    throw new Exception(error);
                }

                RisorsaUmanaDataRetriever ret = new RisorsaUmanaDataRetriever(f);
                IList result = ret.FindRisorseUmaneByAnno(year);
                if(result.Count == 0)
                    return new RisorsaUmana[]{};

                RisorsaUmana[] l = new RisorsaUmana[result.Count];
                int counter = 0;
                foreach (RisorsaUmana item in result)
                {
                    l[counter] = item;
                    counter++;
                }
                return l;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }





        [WebMethod]
        [SoapHeader("credential")]
        public OrganizzativeData  FindOrganizzativeData(int regionId, int provinceId, int year)
        {
            try
            {
                string userName = credential.UserName;
                string password = credential.Password;


                Initialize();



                //eseguo il login
                LoginResult r = c.Login(userName, password);

                string error = CheckIdentificationAndAuthorizzation(r);

                if (!string.IsNullOrEmpty(error))
                {
                    OrganizzativeData d = new OrganizzativeData();
                    d.FirstLevelError = error;
                    
                    return d;
                }

                OrganizzativeData d1 = new OrganizzativeData();

                d1.Struttura = FindStruttura(regionId, provinceId, year);
                if (provinceId <= 0)
                    d1.CongressoRegionale = FindCongressoRegionale(regionId, year);
                else
                    d1.CongressoTerritoriale = FindCongressoTerritoriale(regionId, provinceId, year);
                if (provinceId   > 0)
                {
                    d1.Rappresentanti = FindRappresentanti(regionId, provinceId, year);
                    d1.Rappresentanze = FindRappresentanze(regionId, provinceId, year);
                }
                d1.RisorseUmane = FindRisorsaUmana(regionId, provinceId, year);

                return d1;
               
            }
            catch (Exception ex)
            {
                OrganizzativeData d = new OrganizzativeData();
                d.FirstLevelError = ex.Message ;

                return d;
            }

        }



    }
}

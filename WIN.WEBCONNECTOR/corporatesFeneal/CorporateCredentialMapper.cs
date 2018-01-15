using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIN.WEBCONNECTOR.corporatesFeneal
{
    public class CorporateCredentialMapper
    {

        private List<CorporateCredential> _corporateCedentials = new List<CorporateCredential>();

        public CorporateCredentialMapper()
        {
            //qui popolo tutte le credenziali mappate

            //alta lombardia 
            CorporateCredential altalomb = new CorporateCredential();
            altalomb.Password = "altafenlomb1";
            altalomb.Username = "fenealaltalombardia";

            //como lecco varese sondrio
            Credential como = new Credential();
            como.RealUsername = "fenealcomo";
            como.RealPassword = "comofeneal";
            como.Province = "COMO";

            Credential lecco = new Credential();
            lecco.RealUsername = "feneallecco";
            lecco.RealPassword = "lecco12fen";
            lecco.Province = "LECCO";

            Credential varese = new Credential();
            varese.RealUsername = "fenealvarese";
            varese.RealPassword = "vare5fen";
            varese.Province = "VARESE";

            Credential sondrio = new Credential();
            sondrio.RealUsername = "fenealsondrio";
            sondrio.RealPassword = "MARIA1988";
            sondrio.Province = "SONDRIO";

            altalomb.Credentials.Add(como);
            altalomb.Credentials.Add(varese);
            altalomb.Credentials.Add(lecco);
            altalomb.Credentials.Add(sondrio);

            _corporateCedentials.Add(altalomb);







            CorporateCredential mila = new CorporateCredential();
            mila.Password = "millapavilo";
            mila.Username = "fenealmilanolodipavia";

            //milano lodi pavia
            Credential milano = new Credential();
            milano.RealUsername = "fenealmilano";
            milano.RealPassword = "p_milano";
            milano.Province = "MILANO";

            Credential lodi = new Credential();
            lodi.RealUsername = "feneallodi";
            lodi.RealPassword = "lodifeneal";
            lodi.Province = "LODI";

            Credential pavia = new Credential();
            pavia.RealUsername = "fenealpavia";
            pavia.RealPassword = "feneal1a2";
            pavia.Province = "PAVIA";


            mila.Credentials.Add(lodi);
            mila.Credentials.Add(milano);
            mila.Credentials.Add(pavia);


            _corporateCedentials.Add(mila);













            //biella vercelli
            CorporateCredential bieverc = new CorporateCredential();
            bieverc.Password = "biellavercfen";
            bieverc.Username = "fenealbiellavercelli";

            //biella vercelli
            Credential biella = new Credential();
            biella.RealUsername = "fenealbiella";
            biella.RealPassword = "fenbinet46";
            biella.Province = "BIELLA";

            Credential vercelli = new Credential();
            vercelli.RealUsername = "fenealvercelli";
            vercelli.RealPassword = "teresa";
            vercelli.Province = "VERCELLI";

            bieverc.Credentials.Add(biella);
            bieverc.Credentials.Add(vercelli);

            _corporateCedentials.Add(bieverc);



            //verbania cuso ossola
            CorporateCredential verb = new CorporateCredential();
            verb.Password = "vercusooosl";
            verb.Username = "fenealverbanocusoossola";

            //biella vercelli
            Credential aa = new Credential();
            aa.RealUsername = "fenealverbania";
            aa.RealPassword = "verba23fe/";
            aa.Province = "VERBANIA";

            verb.Credentials.Add(aa);
            _corporateCedentials.Add(verb);




            //treviso belluno
            CorporateCredential trebell = new CorporateCredential();
            trebell.Password = "teebelfen";
            trebell.Username = "fenealtrevisobelluno";

        
            Credential treviso = new Credential();
            treviso.RealUsername = "fenealtreviso";
            treviso.RealPassword = "fenealt-reviso";
            treviso.Province = "TREVISO";

            Credential belluno = new Credential();
            belluno.RealUsername = "fenealbelluno";
            belluno.RealPassword = "tiziania";
            belluno.Province = "BELLUNO";

            trebell.Credentials.Add(treviso);
            trebell.Credentials.Add(belluno);

            _corporateCedentials.Add(trebell);




            //firenze prato
            CorporateCredential fireprat = new CorporateCredential();
            fireprat.Password = "firprafene";
            fireprat.Username = "fenealfirenzeprato";

        
            Credential firenze = new Credential();
            firenze.RealUsername = "fenealfirenze";
            firenze.RealPassword = "firenzefeneal";
            firenze.Province = "FIRENZE";

            Credential prato = new Credential();
            prato.RealUsername = "fenealprato";
            prato.RealPassword = "pratofeneal";
            prato.Province = "PRATO";

            fireprat.Credentials.Add(firenze);
            fireprat.Credentials.Add(prato);

            _corporateCedentials.Add(fireprat);







            //siena arezzo
            CorporateCredential sieare = new CorporateCredential();
            sieare.Password = "aresien1";
            sieare.Username = "fenealsienaarezzo";

         
            Credential arezzo = new Credential();
            arezzo.RealUsername = "fenealarezzo";
            arezzo.RealPassword = "arezzofeneal";
            arezzo.Province = "AREZZO";

            Credential siena = new Credential();
            siena.RealUsername = "fenealsiena";
            siena.RealPassword = "sienafeneal";
            siena.Province = "SIENA";

            sieare.Credentials.Add(siena);
            sieare.Credentials.Add(arezzo);

            _corporateCedentials.Add(sieare);





            //ancona macerata
            CorporateCredential ancomac = new CorporateCredential();
            ancomac.Password = "ancmer11";
            ancomac.Username = "fenealanconamacerata";

           
            Credential ancona = new Credential();
            ancona.RealUsername = "fenealancona";
            ancona.RealPassword = "anconafeneal";
            ancona.Province = "ANCONA";

            Credential macerata = new Credential();
            macerata.RealUsername = "fenealmacerata";
            macerata.RealPassword = "maceratafeneal";
            macerata.Province = "MACERATA";

            ancomac.Credentials.Add(ancona);
            ancomac.Credentials.Add(macerata);

            _corporateCedentials.Add(ancomac);







            //aquila teramo
            CorporateCredential aqter = new CorporateCredential();
            aqter.Password = "aqrter23";
            aqter.Username = "fenealaquilateramo";


            Credential aquila = new Credential();
            aquila.RealUsername = "feneallaquila";
            aquila.RealPassword = "fen3laqui.";
            aquila.Province = "L'AQUILA";

            Credential teramo = new Credential();
            teramo.RealUsername = "fenealteramo";
            teramo.RealPassword = "rlstritorno13";
            teramo.Province = "TERAMO";

            aqter.Credentials.Add(aquila);
            aqter.Credentials.Add(teramo);

            _corporateCedentials.Add(aqter);





            //pescara chieti
            CorporateCredential pescchi = new CorporateCredential();
            pescchi.Password = "pescheti23";
            pescchi.Username = "fenealpescarachieti";


            Credential pescara = new Credential();
            pescara.RealUsername = "fenealabruzzo";
            pescara.RealPassword = "pescara";
            pescara.Province = "PESCARA";

            Credential chieti = new Credential();
            chieti.RealUsername = "fenealchieti";
            chieti.RealPassword = "PacWal49";
            chieti.Province = "CHIETI";

            pescchi.Credentials.Add(pescara);
            pescchi.Credentials.Add(chieti);

            _corporateCedentials.Add(pescchi);






            //enna caltanissetta
            CorporateCredential ennacalt = new CorporateCredential();
            ennacalt.Password = "ennacalt43";
            ennacalt.Username = "fenealennacaltanissetta";


            Credential enna = new Credential();
            enna.RealUsername = "fenealenna";
            enna.RealPassword = "ennafeneal";
            enna.Province = "ENNA";

            Credential caltanissetta = new Credential();
            caltanissetta.RealUsername = "fenealcaltanissetta";
            caltanissetta.RealPassword = "caltanissettafeneal";
            caltanissetta.Province = "CALTANISSETTA";

            ennacalt.Credentials.Add(enna);
            ennacalt.Credentials.Add(caltanissetta);

            _corporateCedentials.Add(ennacalt);






            //caserta benevento avellino
            CorporateCredential casben = new CorporateCredential();
            casben.Password = "casbenav";
            casben.Username = "fenealcasertaavellinobenevento";


            Credential caserta = new Credential();
            caserta.RealUsername = "fenealcaserta";
            caserta.RealPassword = "casertafeneal";
            caserta.Province = "CASERTA";

            Credential benevento = new Credential();
            benevento.RealUsername = "fenealbenevento";
            benevento.RealPassword = "beneventofeneal";
            benevento.Province = "BENEVENTO";

            Credential avellino = new Credential();
            avellino.RealUsername = "fenealavellino";
            avellino.RealPassword = "fenealavellino";
            avellino.Province = "AVELLINO";

            casben.Credentials.Add(caserta);
            casben.Credentials.Add(benevento);
            casben.Credentials.Add(avellino);

            _corporateCedentials.Add(casben);





            //catanzaro vibo
            CorporateCredential catvib = new CorporateCredential();
            catvib.Password = "catviboval1";
            catvib.Username = "fenealcatanzarovibo";


            Credential catanzaro = new Credential();
            catanzaro.RealUsername = "fenealcatanzaro";
            catanzaro.RealPassword = "catanzarofeneal";
            catanzaro.Province = "CATANZARO";

            Credential vibo = new Credential();
            vibo.RealUsername = "fenealvibo";
            vibo.RealPassword = "vibofeneal";
            vibo.Province = "VIBO VALENTIA";

            catvib.Credentials.Add(catanzaro);
            catvib.Credentials.Add(vibo);
            _corporateCedentials.Add(catvib);





            //matera potenza
            CorporateCredential pertern = new CorporateCredential();
            pertern.Password = "basfenmt";
            pertern.Username = "fenealbasilicata";

            //biella vercelli
            Credential matera = new Credential();
            matera.RealUsername = "fenealmatera";
            matera.RealPassword = "serenamente";
            matera.Province = "MATERA";

            Credential potenza = new Credential();
            potenza.RealUsername = "fenealbasilicata";
            potenza.RealPassword = "basilicatafeneal";
            potenza.Province = "POTENZA";

            pertern.Credentials.Add(matera);
            pertern.Credentials.Add(potenza);

            _corporateCedentials.Add(pertern);




            //matera potenza
            CorporateCredential perugiter = new CorporateCredential();
            perugiter.Password = "fen11umbr";
            perugiter.Username = "fenealumbria";

            //biella vercelli
            Credential terni = new Credential();
            terni.RealUsername = "fenealterni";
            terni.RealPassword = "ternifeneal";
            terni.Province = "TERNI";

            Credential perugia = new Credential();
            perugia.RealUsername = "fenealperugia";
            perugia.RealPassword = "perugina";
            perugia.Province = "PERUGIA";

            perugiter.Credentials.Add(terni);
            perugiter.Credentials.Add(perugia);

            _corporateCedentials.Add(perugiter);





        }

        public bool isCorporateAccount(string username)
        {
            foreach (CorporateCredential item in _corporateCedentials)
            {
                if (username.ToLower().Equals(item.Username.ToLower()))
                {
                    return true;
                }
            }

            return false;
        }

        public IList<string> RetrieveCorporateAcconutTeritories(string username)
        {
            IList<string> result = new List<string>();
            CorporateCredential c = RetrieveCorporateCredential(username);

            if (c == null)
                return result;

            return c.Credentials.Select(x => x.Province).ToList();
        }

        public Credential RetrieveRealCredentialsForCorporateAccount(string username, string password, string province)
        {
            CorporateCredential c = RetrieveCorporateCredential(username);
            if (c == null)
                return null;

            //adesso che ho trovato il mappaggio con una credenziale corporate posso recuperare le credenziali reali in base
            //alla provincia e alla password
            if (c.Password.Equals(password) )
            {
                Credential cc = c.Credentials.Find(x => x.Province.Equals(province));
                if (cc != null)
                    return cc;
            }

            return null;
        }

        private CorporateCredential RetrieveCorporateCredential(string username)
        {
            foreach (CorporateCredential item in _corporateCedentials)
            {
                if (username.ToLower().Equals(item.Username.ToLower()))
                {
                    return item;
                }
            }
            return null;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES
{
    public class FenealValidator : IWorkerValidator 
    {

        #region IWorkerValidator Membri di

        public ValidationResult Validate(WorkerDTO worker, IGeoElementChecker checker, ExportTrace trace)
        {


            StringBuilder b = new StringBuilder();


            ValidationResult result = new ValidationResult();


            //Convalida della struttura
            if (!string.IsNullOrEmpty(trace.Struttura))
                if (trace.Struttura.ToUpper() != UILDescriber.Instance.Feneal)
                    b.AppendLine("Struttura diversa dalla Feneal per il lavoratore");

         

         

            //Convalida nome
            if (string.IsNullOrEmpty(worker.Name))
            {
                worker.Name = "";
            }
             

            if (!string.IsNullOrEmpty(worker.Name))
                if (worker.Name.Length > 40)
                    b.AppendLine("Il nome non può superare i 40 caratteri");



            //Convalida cognome
            if (string.IsNullOrEmpty(worker.Surname))
                b.AppendLine("Cognome nullo");

            if (!string.IsNullOrEmpty(worker.Surname ))
                if (worker.Surname.Length > 80)
                    b.AppendLine("Il cognome non può superare i 80 caratteri");


            //Convalida codice fiscale
            if (string.IsNullOrEmpty(worker.Fiscalcode ))
                b.AppendLine("Codice fiscale nullo");

            if (!string.IsNullOrEmpty(worker.Fiscalcode))
                if (worker.Fiscalcode.Length != 16)
                    b.AppendLine("Il codice fiscale deve essere di 16 caratteri");

            //try
            //{
            //    if (!string.IsNullOrEmpty(worker.Fiscalcode))
            //    {
            //        string cfError = checker.CheckFiscalCode (worker.Fiscalcode);
            //        if (!string.IsNullOrEmpty (cfError))
            //            b.AppendLine("Il codice fiscale è errato. " + cfError);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    b.AppendLine("Il codice fiscale è errato. " + ex.Message);
            //}

           

            //Convalida data di nascita
            if (worker.BirthDate.Equals (DateTime .MaxValue ) ||worker.BirthDate.Equals (DateTime .MinValue ))
                b.AppendLine("Data di nascita nulla");


     
            //Convalida comune di nascita
            if (!string.IsNullOrEmpty (worker.BirthPlace ))
                if (worker.BirthPlace.Length > 70)
                    b.AppendLine("Il comune di nascita può essere di massimo 70 caratteri");

            //verifico se esiste la transcodifica del comune
            try
            {
                CheckComune(worker, checker);
            }
            catch (Exception)
            {
                worker.BirthPlace = "";
            }
           


            //Convalida comune di residenza
            if (!string.IsNullOrEmpty(worker.LivingPlace))
                if (worker.LivingPlace.Length > 70)
                    b.AppendLine("Il comune di residenza può essere di massimo 70 caratteri");


            //verifico se esiste la transcodifica del comune
            try
            {
                CheckComuneResidenza(worker, checker);
            }
            catch (Exception)
            {
                worker.LivingPlace = "";
            }
           




            //Convalida indirizzo
            if (!string.IsNullOrEmpty(worker.Address))
                if (worker.Address.Length > 200)
                    b.AppendLine("L'indirizzo può essere di massimo 200 caratteri");


            //Convalida cap
            if (!string.IsNullOrEmpty(worker.Cap))
                if (worker.Cap.Length > 10)
                    b.AppendLine("Il cap può essere di massimo 10 caratteri");


            //Convalida telefono
            if (!string.IsNullOrEmpty(worker.Phone))
                if (worker.Phone.Length > 50)
                    b.AppendLine("Il telefono può essere di massimo 50 caratteri");

            




            //Convalida iscrizione
            SubscriptionDTO subscription = worker.Subscription;

            

            //verifica che l'iscrizione non sia nulla
            if (subscription == null)
                b.AppendLine("Nessun dato di iscrizione per il lavoratore");
            //per prima cosa convalido la provincia
            if (subscription != null)
            {
                if (string.IsNullOrEmpty(subscription.Province))
                    b.AppendLine("La provincia di iscrizione non è stata impostata");
                if (!subscription.Province.ToLower().Equals(trace.Province.ToLower()))
                    b.AppendLine("La provincia di iscrizione dei lavoratori è diversa dalla provincia di importazione.");
            }



            //verifica presenza settore
            if (subscription != null)
                if (string.IsNullOrEmpty (subscription.Sector ))
                    b.AppendLine("Settore mancante");



            //verifica presenza settore corretto
            if (subscription != null)
                if (!string.IsNullOrEmpty(subscription.Sector))
                    if ((subscription.Sector == "EDILE") || (subscription.Sector == "IMPIANTI FISSI") || (subscription.Sector == "INPS"))
                    {
                        if (subscription.Sector == "EDILE")
                            if (subscription.PeriodType != PeriodType.Semester)
                                b.AppendLine("Tipo periodo per il settore edile non corretto");
                        //ok
                    }
                    else
                    {
                        b.AppendLine("Settore non corretto");
                    }



            if (subscription != null)
            {
                if (subscription.PeriodType == PeriodType.Semester)
                {

                    //verifica anno
                    if (subscription != null)
                        if ((subscription.Year < 1980) || (subscription.Year > 2040))
                            b.AppendLine("Anno non corretto. Immettere dati dal 1980 fino al massimo 2040");


                    //verifica semestre
                    if (subscription != null)
                        if ((subscription.Semester != 1) && (subscription.Semester != 2))
                            b.AppendLine("Semestre non corretto. Immettere un semestre valido");

                }
                else if (subscription.PeriodType == PeriodType.Interval)
                {
                    //verifica date
                    if (subscription != null)
                        if ((subscription.InitialDate == DateTime.MinValue) || (subscription.InitialDate == DateTime.MaxValue))
                            b.AppendLine("Data inizio non valida. ");

                    //verifica data fine
                    if (subscription != null)
                        if ((subscription.EndDate == DateTime.MinValue) || (subscription.EndDate == DateTime.MaxValue))
                            b.AppendLine("Data fine non valida. ");

                    //verifica date
                    if (subscription != null)
                        if ((subscription.EndDate < subscription.InitialDate))
                            b.AppendLine("Data fine precedente data inizio.");

                }
                else
                {
                    //verifica anno
                    if (subscription != null)
                        if ((subscription.Year < 1980) || (subscription.Year > 2040))
                            b.AppendLine("Anno non corretto. Immettere dati dal 1980 fino al massimo 2040");


                    //verifica semestre
                    if (subscription != null)
                        if ((subscription.Semester < 1) || (subscription.Semester > 12))
                            b.AppendLine("Mese non corretto. Immettere un semestre valido");
                }
            }
           

            //Convalida livello
            if (subscription != null)
                if (!string.IsNullOrEmpty(subscription.Level))
                    if (subscription.Level.Length  > 50)
                        b.AppendLine("Il livello non può superare i 50 caratteri");

            //Convalida contratto
            if (subscription != null)
                if (!string.IsNullOrEmpty(subscription.Contract))
                    if (subscription.Contract.Length > 100)
                        b.AppendLine("Il contratto non può superare i 100 caratteri");


            //Convalida partita iva
            if (subscription != null)
                if (!string.IsNullOrEmpty(subscription.FiscalCode))
                    if (subscription.FiscalCode.Length > 30)
                        b.AppendLine("La partita iva non può superare i 30 caratteri");

            if (!EntityValidator.ExistEntity (subscription))
                b.AppendLine("Il campo ENTE deve essere compilato con settore EDILE impostato; L'ente deve essere uno dei seguenti: CASSA EDILE, EDILCASSA," +
                                    " CALEC, CEA, CEAV, CEC, CEDA, CEDAF, CEDAM, CELCOF, CEMA, CERT, CEVA, CEDAIIER, FALEA");




            //Convalida azienda
            if (subscription != null)
                if (!string.IsNullOrEmpty(subscription.EmployCompany))
                    if (subscription.EmployCompany.Length > 100)
                        b.AppendLine("L'azienda può essere di massimo 100 caratteri");



            //imposto il risultato dell'elaborazione sul worker
            if (string.IsNullOrEmpty (b.ToString ()))
            {
                result.IsValidated = true;
                result .Errors = "";
            }
            else
            {
                result.IsValidated = false;
                result .Errors = b.ToString ();
            }

            //imposto i dati del worker
            worker.IsValid = result.IsValidated;
            worker.Errors = result.Errors;

            return result;
        }

        public ValidationResult Validate(LiberoDTO worker, IGeoElementChecker checker, int rowNumber)
        {
            string line = "Alla linea " + rowNumber.ToString() + ": ";

            StringBuilder b = new StringBuilder();


            ValidationResult result = new ValidationResult();



            //Convalida nome
            if (string.IsNullOrEmpty(worker.Name))
            {
                worker.Name = "";
            }

            if (!string.IsNullOrEmpty(worker.Name))
                if (worker.Name.Length > 40)
                    b.AppendLine(line + "Il nome non può superare i 40 caratteri");



            //Convalida cognome
            if (string.IsNullOrEmpty(worker.Surname))
                b.AppendLine(line + "Cognome nullo");

            if (!string.IsNullOrEmpty(worker.Surname))
                if (worker.Surname.Length > 80)
                    b.AppendLine(line + "Il cognome non può superare i 80 caratteri");


            //Convalida codice fiscale
            if (string.IsNullOrEmpty(worker.Fiscalcode))
                b.AppendLine(line + "Codice fiscale nullo");

            if (!string.IsNullOrEmpty(worker.Fiscalcode))
                if (worker.Fiscalcode.Length != 16)
                    b.AppendLine(line + "Il codice fiscale deve essere di 16 caratteri");

            try
            {
                if (!string.IsNullOrEmpty(worker.Fiscalcode))
                {
                    string cfError = checker.CheckFiscalCode(worker.Fiscalcode);
                    if (!string.IsNullOrEmpty(cfError))
                        b.AppendLine(line + "Il codice fiscale è errato. " + cfError);
                }
            }
            catch (Exception ex)
            {
                b.AppendLine(line + "Il codice fiscale è errato. " + ex.Message);
            }



            //Convalida data di nascita
            if (worker.BirthDate.Equals(DateTime.MaxValue) || worker.BirthDate.Equals(DateTime.MinValue))
                b.AppendLine(line + "Data di nascita nulla");



            //Convalida comune di nascita
            if (!string.IsNullOrEmpty(worker.BirthPlace))
                if (worker.BirthPlace.Length > 70)
                    b.AppendLine(line + "Il comune di nascita può essere di massimo 70 caratteri");

            //verifico se esiste la transcodifica del comune
            try
            {
                CheckComune(worker, checker);
            }
            catch (Exception)
            {
                worker.BirthPlace = "";
            }



            //Convalida comune di residenza
            if (!string.IsNullOrEmpty(worker.LivingPlace))
                if (worker.LivingPlace.Length > 70)
                    b.AppendLine(line + "Il comune di residenza può essere di massimo 70 caratteri");


            //verifico se esiste la transcodifica del comune
            try
            {
                CheckComuneResidenza(worker, checker);
            }
            catch (Exception)
            {
                worker.LivingPlace = "";
            }





            //Convalida indirizzo
            if (!string.IsNullOrEmpty(worker.Address))
                if (worker.Address.Length > 200)
                    b.AppendLine(line + "L'indirizzo può essere di massimo 200 caratteri");


            //Convalida cap
            if (!string.IsNullOrEmpty(worker.Cap))
                if (worker.Cap.Length > 10)
                    b.AppendLine(line + "Il cap può essere di massimo 10 caratteri");


            //Convalida telefono
            if (!string.IsNullOrEmpty(worker.Phone))
                if (worker.Phone.Length > 50)
                    b.AppendLine(line + "Il telefono può essere di massimo 50 caratteri");




            //valido la data di libero al  e campo iscritti
            if (worker.LiberoAl == null || worker.LiberoAl == DateTime.MinValue || worker.LiberoAl == DateTime.MaxValue)
                worker.LiberoAl = DateTime.Now;

            if (worker.IscrittoA == null)
                worker.IscrittoA = "";

            if (!worker.IscrittoA.ToLower().Equals("filca") && !worker.IscrittoA.ToLower().Equals("fillea"))
            {
                worker.IscrittoA = "";
            }

            //valido l'ente e l'azienda
            if (string.IsNullOrEmpty(worker.CurrentAzienda))
                b.AppendLine(line + "L'azienda è nulla");

            if (!string.IsNullOrEmpty(worker.CurrentAzienda))
                    if (worker.CurrentAzienda.Length > 150)
                        b.AppendLine(line + "L'azienda può essere di massimo 150 caratteri");

            if (!EntityValidator.ExistEntity(worker.Ente))
                b.AppendLine(line + "Il campo ENTE deve essere compilato con settore EDILE impostato; L'ente deve essere uno dei seguenti: CASSA EDILE, EDILCASSA," +
                                    " CALEC, CEA, CEAV, CEC, CEDA, CEDAF, CEDAM, CELCOF, CEMA, CERT, CEVA, CEDAIIER, FALEA");





          

            //imposto il risultato dell'elaborazione sul worker
            if (string.IsNullOrEmpty(b.ToString()))
            {
                result.IsValidated = true;
                result.Errors = "";
            }
            else
            {
                result.IsValidated = false;
                result.Errors = b.ToString();
            }

            //imposto i dati del worker
            worker.IsValid = result.IsValidated;
            worker.Errors = result.Errors;

            return result;
        }

        private void CheckComuneResidenza(LiberoDTO worker, IGeoElementChecker checker)
        {
            if (worker.LivingPlace == null)
                worker.LivingPlace = "";

           
        }

        private void CheckComune(LiberoDTO worker, IGeoElementChecker checker)
        {
            if (worker.BirthPlace == null)
                worker.BirthPlace = "";

          
            //tento di trovarlo manualmente
            if ((checker.ExistComune(worker.BirthPlace) == false))
            {
                string c = "";
                if (!string.IsNullOrEmpty(worker.Fiscalcode))
                    c = checker.GetComuneByFiscalCode(worker.Fiscalcode);
                if (!string.IsNullOrEmpty(c))
                {
                   
                    worker.BirthPlace = c;
                }
            }
        }



        public ValidationResult Validate(WorkerDTO worker, IGeoElementChecker checker)
        {


            StringBuilder b = new StringBuilder();


            ValidationResult result = new ValidationResult();



            //Convalida nome
            if (string.IsNullOrEmpty(worker.Name))
            {
                worker.Name = "";
            }

            if (!string.IsNullOrEmpty(worker.Name))
                if (worker.Name.Length > 40)
                    b.AppendLine("Il nome non può superare i 40 caratteri");



            //Convalida cognome
            if (string.IsNullOrEmpty(worker.Surname))
                b.AppendLine("Cognome nullo");

            if (!string.IsNullOrEmpty(worker.Surname))
                if (worker.Surname.Length > 80)
                    b.AppendLine("Il cognome non può superare i 80 caratteri");


            //Convalida codice fiscale
            if (string.IsNullOrEmpty(worker.Fiscalcode))
                b.AppendLine("Codice fiscale nullo");

            if (!string.IsNullOrEmpty(worker.Fiscalcode))
                if (worker.Fiscalcode.Length != 16)
                    b.AppendLine("Il codice fiscale deve essere di 16 caratteri");

            try
            {
                if (!string.IsNullOrEmpty(worker.Fiscalcode))
                {
                    string cfError = checker.CheckFiscalCode(worker.Fiscalcode);
                    if (!string.IsNullOrEmpty(cfError))
                        b.AppendLine("Il codice fiscale è errato. " + cfError);
                }
            }
            catch (Exception ex)
            {
                b.AppendLine("Il codice fiscale è errato. " + ex.Message);
            }



            //Convalida data di nascita
            if (worker.BirthDate.Equals(DateTime.MaxValue) || worker.BirthDate.Equals(DateTime.MinValue))
                b.AppendLine("Data di nascita nulla");



            //Convalida comune di nascita
            if (!string.IsNullOrEmpty(worker.BirthPlace))
                if (worker.BirthPlace.Length > 70)
                    b.AppendLine("Il comune di nascita può essere di massimo 70 caratteri");

            //verifico se esiste la transcodifica del comune
            try
            {
                CheckComune(worker, checker);
            }
            catch (Exception)
            {
                worker.BirthPlace = "";
            }



            //Convalida comune di residenza
            if (!string.IsNullOrEmpty(worker.LivingPlace))
                if (worker.LivingPlace.Length > 70)
                    b.AppendLine("Il comune di residenza può essere di massimo 70 caratteri");


            //verifico se esiste la transcodifica del comune
            try
            {
                CheckComuneResidenza(worker, checker);
            }
            catch (Exception)
            {
                worker.LivingPlace = "";
            }





            //Convalida indirizzo
            if (!string.IsNullOrEmpty(worker.Address))
                if (worker.Address.Length > 200)
                    b.AppendLine("L'indirizzo può essere di massimo 200 caratteri");


            //Convalida cap
            if (!string.IsNullOrEmpty(worker.Cap))
                if (worker.Cap.Length > 10)
                    b.AppendLine("Il cap può essere di massimo 10 caratteri");


            //Convalida telefono
            if (!string.IsNullOrEmpty(worker.Phone))
                if (worker.Phone.Length > 50)
                    b.AppendLine("Il telefono può essere di massimo 50 caratteri");






            ////Convalida iscrizione
            //SubscriptionDTO subscription = worker.Subscription;



            ////verifica che l'iscrizione non sia nulla
            //if (subscription == null)
            //    b.AppendLine("Nessun dato di iscrizione per il lavoratore");
            ////per prima cosa convalido la provincia
            //if (subscription != null)
            //{
            //    if (string.IsNullOrEmpty(subscription.Province))
            //        b.AppendLine("La provincia di iscrizione non è stata impostata");
            //    //if (!subscription.Province.ToLower().Equals(trace.Province.ToLower()))
            //    //    b.AppendLine("La provincia di iscrizione dei lavoratori è diversa dalla provincia di importazione.");
            //}



            ////verifica presenza settore
            //if (subscription != null)
            //    if (string.IsNullOrEmpty(subscription.Sector))
            //        b.AppendLine("Settore mancante");



            ////verifica presenza settore corretto
            //if (subscription != null)
            //    if (!string.IsNullOrEmpty(subscription.Sector))
            //        if ((subscription.Sector == "EDILE") || (subscription.Sector == "IMPIANTI FISSI") || (subscription.Sector == "INPS"))
            //        {
            //            if (subscription.Sector == "EDILE")
            //                if (subscription.PeriodType != PeriodType.Semester)
            //                    b.AppendLine("Tipo periodo per il settore edile non corretto");
            //            //ok
            //        }
            //        else
            //        {
            //            b.AppendLine("Settore non corretto");
            //        }



            //if (subscription != null)
            //{
            //    if (subscription.PeriodType == PeriodType.Semester)
            //    {

            //        //verifica anno
            //        if (subscription != null)
            //            if ((subscription.Year < 1980) || (subscription.Year > 2040))
            //                b.AppendLine("Anno non corretto. Immettere dati dal 1980 fino al massimo 2040");


            //        //verifica semestre
            //        if (subscription != null)
            //            if ((subscription.Semester != 1) && (subscription.Semester != 2))
            //                b.AppendLine("Semestre non corretto. Immettere un semestre valido");

            //    }
            //    else if (subscription.PeriodType == PeriodType.Interval)
            //    {
            //        //verifica date
            //        if (subscription != null)
            //            if ((subscription.InitialDate == DateTime.MinValue) || (subscription.InitialDate == DateTime.MaxValue))
            //                b.AppendLine("Data inizio non valida. ");

            //        //verifica data fine
            //        if (subscription != null)
            //            if ((subscription.EndDate == DateTime.MinValue) || (subscription.EndDate == DateTime.MaxValue))
            //                b.AppendLine("Data fine non valida. ");

            //        //verifica date
            //        if (subscription != null)
            //            if ((subscription.EndDate < subscription.InitialDate))
            //                b.AppendLine("Data fine precedente data inizio.");

            //    }
            //    else
            //    {
            //        //verifica anno
            //        if (subscription != null)
            //            if ((subscription.Year < 1980) || (subscription.Year > 2040))
            //                b.AppendLine("Anno non corretto. Immettere dati dal 1980 fino al massimo 2040");


            //        //verifica semestre
            //        if (subscription != null)
            //            if ((subscription.Semester < 1) || (subscription.Semester > 12))
            //                b.AppendLine("Mese non corretto. Immettere un semestre valido");
            //    }
            //}


            ////Convalida livello
            //if (subscription != null)
            //    if (!string.IsNullOrEmpty(subscription.Level))
            //        if (subscription.Level.Length > 50)
            //            b.AppendLine("Il livello non può superare i 50 caratteri");

            ////Convalida contratto
            //if (subscription != null)
            //    if (!string.IsNullOrEmpty(subscription.Contract))
            //        if (subscription.Contract.Length > 100)
            //            b.AppendLine("Il contratto non può superare i 100 caratteri");



            //if (!EntityValidator.ExistEntity(subscription))
            //    b.AppendLine("Il campo ENTE deve essere compilato con settore EDILE impostato; L'ente deve essere uno dei seguenti: CASSA EDILE, EDILCASSA," +
            //                        " CALEC, CEA, CEAV, CEC, CEDA, CEDAF, CEDAM, CELCOF, CEMA, CERT, CEVA, CEDAIIER, FALEA");




            ////Convalida azienda
            //if (subscription != null)
            //    if (!string.IsNullOrEmpty(subscription.EmployCompany))
            //        if (subscription.EmployCompany.Length > 60)
            //            b.AppendLine("L'azienda può essere di massimo 60 caratteri");



            //imposto il risultato dell'elaborazione sul worker
            if (string.IsNullOrEmpty(b.ToString()))
            {
                result.IsValidated = true;
                result.Errors = "";
            }
            else
            {
                result.IsValidated = false;
                result.Errors = b.ToString();
            }

            //imposto i dati del worker
            worker.IsValid = result.IsValidated;
            worker.Errors = result.Errors;

            return result;
        }

        private void CheckComuneResidenza(WorkerDTO worker, IGeoElementChecker checker)
        {
            if (worker.LivingPlace == null)
                worker.LivingPlace= "";

            if (checker.ExistComune(worker.LivingPlace))
                worker.ExistLivingPlace = true;
            else
                worker.ExistLivingPlace = false;

        }

        private  void CheckComune(WorkerDTO worker, IGeoElementChecker checker)
        {
            if (worker.BirthPlace == null)
                worker.BirthPlace = "";

            if (checker.ExistComune(worker.BirthPlace))
                worker.ExistBirthPlace = true;
            else
                worker.ExistBirthPlace = false;


            //tento di trovarlo manualmente
            if (worker.ExistBirthPlace == false)
            {
                string c = "";
                if (!string.IsNullOrEmpty(worker.Fiscalcode))
                    c = checker.GetComuneByFiscalCode(worker.Fiscalcode);
                if (!string.IsNullOrEmpty(c))
                {
                    worker.ExistBirthPlace = true;
                    worker.BirthPlace = c;
                }
            }
             


            

        }

        #endregion
    }





    public class EntityValidator
    {
        public static bool ExistEntity(string entity)
        {

            if (entity != null)
                
                            //verifico la presenza dell'ente
                            switch (entity)
                            {

                                case "CASSA EDILE":
                                    return true;
                                case "EDILCASSA":
                                    return true;
                                case "CALEC":
                                    return true;
                                case "CEA":
                                    return true;
                                case "CEAV":
                                    return true;
                                case "CEC":
                                    return true;
                                case "CEDA":
                                    return true;
                                case "CEDAF":
                                    return true;
                                case "CEDAM":
                                    return true;
                                case "CELCOF":
                                    return true;
                                case "CEMA":
                                    return true;
                                case "CERT":
                                    return true;
                                case "CEVA":
                                    return true;
                                case "CEDAIIER":
                                    return true;
                                case "FALEA":
                                    return true;
                                case "COOP":
                                    return true;
                                default:
                                    return false;
                            }
                     


            return false;

        }
        public static bool ExistEntity(SubscriptionDTO subscription)
        {

            if (subscription != null)
                if (!string.IsNullOrEmpty(subscription.Sector))
                    if (subscription.Sector == "EDILE") 
                    {
                        if (!string.IsNullOrEmpty(subscription.Entity))
                        {
                            //verifico la presenza dell'ente
                            switch (subscription.Entity)
                            {

                                case "CASSA EDILE":
                                    return true;
                                case "EDILCASSA":
                                    return true;
                                case "CALEC":
                                    return true;
                                case "CEA":
                                    return true;
                                case "CEAV":
                                    return true;
                                case "CEC":
                                    return true;
                                case "CEDA":
                                    return true;
                                case "CEDAF":
                                    return true;
                                case "CEDAM":
                                    return true;
                                case "CELCOF":
                                    return true;
                                case "CEMA":
                                    return true;
                                case "CERT":
                                    return true;
                                case "CEVA":
                                    return true;
                                case "CEDAIIER":
                                    return true;
                                case "FALEA":
                                    return true;
                                default:
                                    return false;
                            }
                        }
                        else
                            return false;
                    }


            return true;
            
        }
    }


}

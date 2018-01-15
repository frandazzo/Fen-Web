using System;
using System.Collections.Generic;
using System.Text;
using WIN.WEBCONNECTOR.Credential;
using System.Text.RegularExpressions;
using WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES;

namespace WIN.WEBCONNECTOR.FileReaders
{
    public class InputHeader
    {

        private string _respoonsible = "";
        private string _sector = "";
        private string _entity = "";
        private string _mail = "";
        private string _struttura = "";
       
        private int _year = 2010;
        private int _period = 1;


        public SubscriptionPeriod CalculatePeriod()
        {
            if (string.IsNullOrEmpty(_sector))
                throw new Exception("Impossibile calcolare il periodo di iscrizione. Settore nullo");


            SubscriptionPeriod p;


            if (_sector == "EDILE")
                p = new SubscriptionPeriod(_period, _year, PeriodType.Semester);
            else
                p = new SubscriptionPeriod(_year);

            return p;
        }


        public void CheckValidity()
        {
            StringBuilder s = new StringBuilder();

            if (string.IsNullOrEmpty(_respoonsible))
                s.AppendLine("Responsabile esportazione non specificato");

            if (string.IsNullOrEmpty (CredentialDB.Instance.Province))
                s.AppendLine("Provincia non specificata");



            if (string.IsNullOrEmpty(_sector))
                s.AppendLine("Settore non specificato");


            if (!string.IsNullOrEmpty(_sector))
                if (_sector == "EDILE")
                {
                    if (string.IsNullOrEmpty(_entity))
                        s.AppendLine("Ente bilaterale per il settore edile non specificato");

                    if (_period != 1 && _period != 2)
                        s.AppendLine("Semestre non corretto per il settore edile");
                }

            if ((_year < 1980) && (_year > 2050))
                s.AppendLine("Anno specificato non corretto");



            if (_mail == null)
                _mail = "";

            if (!string.IsNullOrEmpty(_mail ))
            {
                //Mail regular exp pattern:\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*
                string MatchEmailPattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                if (!Regex.IsMatch(_mail, MatchEmailPattern))
                    s.AppendLine("Mail non valida");
            }


            string ex = s.ToString();

            if (!string.IsNullOrEmpty(ex))
                throw new Exception(ex);

        }


        public int Period
        {
            get { return _period ; }
            set { _period = value; }
        }


        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }


        public string Mail
        {
            get { return _mail; }
            set { _mail = value; }
        }





        public string Entity
        {
            get { return _entity; }
            set { _entity = value; }
        }



        public string Sector
        {
            get { return _sector; }
            set { _sector = value; }
        }



        public string Responsible
        {
            get { return _respoonsible; }
            set { _respoonsible = value; }
        }


        public string Struttura
        {
            get { return _struttura; }
            set { _struttura = value; }
        }

    }
}

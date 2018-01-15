using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Collections;

namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport
{
    [XmlRootAttribute("IQATrace", Namespace = "www.fenealgestweb.it", IsNullable = false)]
    public class IQATrace
    {
        public IQATrace() { }


        private string _subject;
          [XmlAttribute("Subject")]
        public string Subject
        {
            get
            {
                return _subject;
            }
            set
            {
                _subject = value;
            }
        }



        private IList<string> _validationErrors;

        public IList<string> ValidationErrors
        {
            get { return _validationErrors; }
        }


        public void Validate()
        {
            _validationErrors = new List<string>();


            if (string.IsNullOrEmpty(_subject))
                _validationErrors.Add("Oggetto non specificato");

            if (Period != 1 && Period != 2)
                _validationErrors.Add(string.Format ("Periodo non corretto. ({0})", Period));

            if (Anno < 1990 || Anno > 2050)
                _validationErrors.Add(string.Format("Anno non corretto. ({0})", Anno));


            if (string.IsNullOrEmpty(_mailto))
                _validationErrors.Add("Mail non specificata");

            if (string.IsNullOrEmpty(Provincia ))
                _validationErrors.Add("Provincia non impostata");


            if (!CheckEntity (Entity))
                _validationErrors.Add(string.Format("Ente non corretto. ({0})", Entity ));


            //adesso valido i dettagli
            if (_iqaTraceDetails.Length == 0)
                _validationErrors.Add("Nessuna quota inserita");


            int row = 0;
            foreach (IqaTraceDetail  item in _iqaTraceDetails)
            {
                row++;
                item.Validate();

                if (!string.IsNullOrEmpty(item.ErrorsToString()))
                    _validationErrors.Add(string.Format("Errore alla riga {0}: {1}", row, item.ErrorsToString()));

            }


        }

        private bool CheckEntity(string entity)
        {
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
                        default:
                            return false;
                    }
               
                
 
        }



        public IList ToDetailsHashArrayList()
        {
            ArrayList l = new ArrayList();


            foreach (IqaTraceDetail item in _iqaTraceDetails)
            {
                l.Add(item.ToHash());
            }


            return l;
        }


        public void FromDetailsHashList(IList data)
        {
            _iqaTraceDetails = new IqaTraceDetail[] { };


            foreach (Hashtable  item in data)
            {
                IqaTraceDetail d = new IqaTraceDetail();
                d.FromHash(item);

                //aggiungo all'array
                Array.Resize<IqaTraceDetail>(ref _iqaTraceDetails, _iqaTraceDetails.Length + 1);
                _iqaTraceDetails[_iqaTraceDetails.Length - 1] = d;
            }

        }



        private IqaTraceDetail[] _iqaTraceDetails;

        [XmlArray("IqaTraceDetails"), XmlArrayItem("IqaTraceDetail", typeof(IqaTraceDetail))]
        public IqaTraceDetail[] IqaTraceDetails
        {
            get { return _iqaTraceDetails; }
            set { _iqaTraceDetails = value; }
        }

        private string _mailto;
          [XmlAttribute("Mailto")]
        public string Mailto
        {
            get
            {
                return _mailto;
            }
            set
            {
                _mailto = value;
            }
        }

          private bool _isIQP;
        //flag che indica se si tratta di quota previsionale
        [XmlAttribute("IsIQP")]
          public bool IsIQP
          {
              get
              {
                  return _isIQP;
              }
              set
              {
                  _isIQP = value;
              }
          }


        private string _provincia;
         [XmlAttribute("Provincia")]
        public string Provincia
        {
            get
            {
                return _provincia;
            }
            set
            {
                _provincia = value;
            }
        }


        private int _anno;
        [XmlAttribute("Anno")]
        public int Anno
        {
            get
            {
                return _anno;
            }
            set
            {
                _anno = value;
            }
        }


        private string _entity;
          [XmlAttribute("Entity")]
        public string Entity
        {
            get
            {
                return _entity;
            }
            set
            {
                _entity = value;
            }
        }


        private int _period;
         [XmlAttribute("Period")]
        public int Period
        {
            get
            {
                return _period;
            }
            set
            {
                _period = value;
            }
        }




    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Collections;

namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport
{
     [XmlRootAttribute("InpsTrace", Namespace = "www.fenealgestweb.it", IsNullable = false)]
    public class InpsTrace
    {
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



        public InpsTrace() { }


        private InpsTraceDetails[] _inpsTraceDetails;

           [XmlArray("InpsTraceDetails"), XmlArrayItem("InpsTraceDetail", typeof(InpsTraceDetails))]
           public InpsTraceDetails[] InpsTraceDetails
           {
               get { return _inpsTraceDetails; }
               set { _inpsTraceDetails = value; }
           }



           public IList ToDetailsHashArrayList()
           {
               ArrayList l = new ArrayList();

               foreach (InpsTraceDetails item in _inpsTraceDetails)
               {
                   l.Add(item.ToHash());
               }


               return l;
           }


           public void FromDetailsHashList(IList data)
           {
               _inpsTraceDetails = new InpsTraceDetails[] { };


               foreach (Hashtable item in data)
               {
                   InpsTraceDetails d = new InpsTraceDetails();
                   d.FromHash(item);

                   //aggiungo all'array
                   Array.Resize<InpsTraceDetails>(ref _inpsTraceDetails, _inpsTraceDetails.Length + 1);
                   _inpsTraceDetails[_inpsTraceDetails.Length - 1] = d;
               }

           }


         [XmlAttribute("Provincia")]
           private string _provincia;

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


           private IList<string> _validationErrors;

           public IList<string> ValidationErrors
           {
               get { return _validationErrors; }
           }


           public void Validate()
           {
               _validationErrors = new List<string>();


               //if (string.IsNullOrEmpty(_provincia ))
               //    _validationErrors.Add("Provincia non specificata");

               if (string.IsNullOrEmpty(_mailto))
                   _validationErrors.Add("Mail non specificata");


               if (string.IsNullOrEmpty(_subject))
                   _validationErrors.Add("Oggetto non specificato");

               //adesso valido i dettagli
               if (_inpsTraceDetails.Length == 0)
                   _validationErrors.Add("Nessun record quote inps inserito");


               int row = 0;
               foreach (InpsTraceDetails item in _inpsTraceDetails)
               {
                   row++;
                   item.Validate();

                   if (!string.IsNullOrEmpty(item.ErrorsToString()))
                        _validationErrors.Add(string.Format("Errore alla riga {0}: {1}", row, item.ErrorsToString()));

               }


           }
    }
}

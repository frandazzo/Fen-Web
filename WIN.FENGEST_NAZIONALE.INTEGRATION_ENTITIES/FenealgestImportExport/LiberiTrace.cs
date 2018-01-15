using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Collections;

namespace WIN.FENGEST_NAZIONALE.INTEGRATION_ENTITIES.FenealgestImportExport
{
     [XmlRootAttribute("LiberiTrace", Namespace = "www.fenealgestweb.it", IsNullable = false)]
    public class LiberiTrace
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



           public LiberiTrace() { }


           private LiberiTraceDetail[] _liberiTraceDetails;

           [XmlArray("LiberiTraceDetails"), XmlArrayItem("LiberiTraceDetail", typeof(LiberiTraceDetail))]
           public LiberiTraceDetail[] LiberiTraceDetails
           {
               get { return _liberiTraceDetails; }
               set { _liberiTraceDetails = value; }
           }


           private LiberoDTO[] _notSubscribers;

           [XmlIgnore]
           public LiberoDTO[] NotSubscribers
           {
               get { return _notSubscribers; }
               set { _notSubscribers = value; }
           }



           public IList ToDetailsHashArrayList()
           {
               ArrayList l = new ArrayList();

               foreach (LiberiTraceDetail item in _liberiTraceDetails)
               {
                   l.Add(item.ToHash());
               }


               return l;
           }


           public void FromDetailsHashList(IList data)
           {
               _liberiTraceDetails = new LiberiTraceDetail[] { };


               foreach (Hashtable item in data)
               {
                   LiberiTraceDetail d = new LiberiTraceDetail();
                   d.FromHash(item);

                   //aggiungo all'array
                   Array.Resize<LiberiTraceDetail>(ref _liberiTraceDetails, _liberiTraceDetails.Length + 1);
                   _liberiTraceDetails[_liberiTraceDetails.Length - 1] = d;
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

              


               if (string.IsNullOrEmpty(_mailto))
                   _validationErrors.Add("Mail non specificata");


               if (string.IsNullOrEmpty(_subject))
                   _validationErrors.Add("Oggetto non specificato");




               //adesso valido i dettagli
               if (_liberiTraceDetails.Length == 0)
                   _validationErrors.Add("Nessun record libero inserito");


               int row = 0;
               foreach (LiberiTraceDetail item in _liberiTraceDetails)
               {
                   row++;
                   item.Validate();

                   if (!string.IsNullOrEmpty(item.ErrorsToString()))
                       _validationErrors.Add(string.Format("Errore alla riga {0}: {1} - {2}", row, item.ErrorsToString(), string.IsNullOrEmpty (item.FISCALE )?"No FiscalCode": item.FISCALE ));

               }


           }

           public string Errore { get; set; }

           public int ExportNumber = 0;

           public LiberiTrace Clone()
           {
               LiberiTrace t = new LiberiTrace();
               t.Mailto = this.Mailto;
               t.Provincia = this.Provincia;
               t.Subject = this.Subject;

               return t;
           }
    }
}

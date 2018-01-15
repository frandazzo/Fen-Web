using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WIN.BASEREUSE;
using WIN.WEBCONNECTOR.Credential;

namespace WIN.WEBCONNECTOR.Commands
{
    public class SendFileForIqaOrLiberi : ICommand
    {
        string _filename;
        bool _isIqa;


        public SendFileForIqaOrLiberi(string filename, bool isIqa)
        {
            _filename = filename;
            _isIqa = isIqa;
        }

        public void Execute()
        {
            FileStream objfilestream = new FileStream(_filename, FileMode.Open, FileAccess.Read);
            int len = (int)objfilestream.Length;
            Byte[] mybytearray = new Byte[len];
            objfilestream.Read(mybytearray, 0, len);

            FenealgestServices.FenealgestwebServices w = new FenealgestServices.FenealgestwebServices();
            FenealgestServices.Credenziali c = new WIN.WEBCONNECTOR.FenealgestServices.Credenziali();
            c.UserName = CredentialDB.Instance.UserName;
            c.Password = CredentialDB.Instance.Password;
            c.Province = CredentialDB.Instance.Province;

            //gli imposto l'intestazione
            w.CredenzialiValue = c;


            string error = w.SaveImportExportFile(mybytearray, _isIqa);
            objfilestream.Close();


           if (!string.IsNullOrEmpty(error ))
               throw new Exception(string.Format("si sono verificati errori nell'invio del file: {0}",error));
        }
    }
}

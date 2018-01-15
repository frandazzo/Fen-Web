using System;
using System.Collections.Generic;
using System.Text;
using WIN.WEBCONNECTOR.Credential;
using System.IO;

namespace WIN.WEBCONNECTOR
{
    public class ParamCreator
    {
        string[] _args;

        public ParamCreator(string[] args)
        {
            _args = args;
        }


        public void Create()
        {
            if (_args == null)
                return;

            foreach (string item in _args)
            {
                if (item.StartsWith("usn"))
                {
                    CreateUserName(item);
                }
		        else if (item.StartsWith("log"))
                {
                    CreateLog(item);
                }
                else if (item.StartsWith("ent"))
                {
                    CreateEntity(item);
                }
                else if (item.StartsWith("mai"))
                {
                    CreateMail(item);
                }
                else if (item.StartsWith("mas"))
                {
                    CreateMailSubject(item);
                }
                else if (item.StartsWith("per"))
                {
                    CreatePeriod(item);
                }
                else if (item.StartsWith("yea"))
                {
                    CreateYear(item);
                }
                else if (item.StartsWith("usp"))
                {
                    CreatePassword(item);
                }
                else if (item.StartsWith("pro"))
                {
                    CreateProvince(item);
                }
                else if (item.StartsWith("chk"))
                {
                    CreateCheck(item);
                }
                else if (item.StartsWith("com"))
                {
                    CreateCommand(item);
                }
                else if (item.StartsWith("exp"))
                {
                    CreateExport(item);
                }
                else if (item.StartsWith("fil"))
                {
                    CreateFilename(item);
                }
                else if (item.StartsWith("fis"))
                {
                    CreateFiscalCode(item);
                }
                else if (item.StartsWith("flc"))
                {
                    CreateFiscalCodeList(item);
                }
                else if (item.StartsWith("azi"))
                {
                    CreateAzienda(item);
                }

            }
        }

        private void CreateAzienda(string item)
        {
            ProgramParameters.Instance.Azienda = ClearPrefix(item);
        }

        private void CreateMailSubject(string item)
        {
            ProgramParameters.Instance.MailSubject = ClearPrefix(item);
        }

        private void CreateMail(string item)
        {
            ProgramParameters.Instance.Mailto = ClearPrefix(item);
        }

        private void CreateYear(string item)
        {
            try 
	        {
                ProgramParameters.Instance.Year = Convert.ToInt32(ClearPrefix(item)).ToString();
	        }
	        catch (Exception)
	        {

                ProgramParameters.Instance.Year = DateTime.Now.Year.ToString();
	        }
            
        }

        private void CreatePeriod(string item)
        {
            try
            {
                ProgramParameters.Instance.Period = Convert.ToInt32(ClearPrefix(item)).ToString();
            }
            catch (Exception)
            {

                ProgramParameters.Instance.Period = "1";
            }
            
        }

        private void CreateEntity(string item)
        {
            ProgramParameters.Instance.Entity = ClearPrefix(item);
        }

        private void CreateFiscalCodeList(string item)
        {
            try
            {
                string tempFile = ClearPrefix(item);
                 string s = "";
                //una volta ottenuto il file 
                //verifico che esista
                if(File.Exists (tempFile))
                {
                    //leggo il file
                    using (System.IO.StreamReader stream = new System.IO.StreamReader(tempFile))
                    {
                        while (!stream.EndOfStream)
                        {
                            //leggo l'unica linea
                            s= stream.ReadToEnd();
                        }
                    }
                    

                    //a questo punto splitto la stringa di codici fiscali in un array di stringhe

                    string[] param = new string[] { " " };

                    string[] arr = s.Split(param, StringSplitOptions.RemoveEmptyEntries );

                    foreach (string st in arr)
                    {

                        ProgramParameters.Instance.FiscalCodeList.Add(st);
                    }

                }


            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message ,"Errore", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
               
            }
            
        }

        private void CreateFiscalCode(string item)
        {
            ProgramParameters.Instance.FiscalCode  = ClearPrefix(item);
        }

        private void CreateFilename(string item)
        {
            ProgramParameters.Instance.FileToExport = ClearPrefix(item);
        }

	private void CreateLog(string item)
        {
            ProgramParameters.Instance.LogFile = ClearPrefix(item);
        }

        private void CreateExport(string item)
        {
            ProgramParameters.Instance.ExportType = ClearPrefix(item);
        }

        private string ClearPrefix(string item)
        {
            return item.Substring(4);
        }

        private void CreateCommand(string item)
        {
            ProgramParameters.Instance.Command = ClearPrefix(item);
        }

        private void CreateCheck(string item)
        {
            ProgramParameters.Instance.CheckCredential  = ClearPrefix(item);
        }

        private void CreateProvince(string item)
        {
            ProgramParameters.Instance.Province  = ClearPrefix(item);
            //CredentialDB.Instance.Province = ClearPrefix(item);
        }

        private void CreatePassword(string item)
        {
            ProgramParameters.Instance.Password  = ClearPrefix(item);
           // CredentialDB.Instance.Password = ClearPrefix(item);
        }

        private void CreateUserName(string item)
        {
            ProgramParameters.Instance.UserName = ClearPrefix(item);
            //CredentialDB.Instance.UserName = ClearPrefix(item);
        }



    }
}

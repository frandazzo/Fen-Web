using System;
using System.Collections.Generic;
using System.Text;
using WIN.TECHNICAL.MENU_CUSTOMIZER;
using System.IO;
using System.Reflection;
using WIN.FENGEST_NAZIONALE.DOMAIN.Serializzation;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.Security
{
    public class MenuProvider : ITemplateMenuProvider
    {
        private string _fileName = "Menu.xml";
        private MenuRepresentation result;
        

        public MenuRepresentation CreateApplicationMenuRepresentation()
        {
            //Recupero il path del file xml dei ruoli
            string file = Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", "");
            FileInfo f = new FileInfo(file);
            string path = Path.Combine(f.DirectoryName, _fileName);

            if (!File.Exists(path))
                throw new FileNotFoundException("File dei menu non trovato");

            MenuDescriptor r = ObjectXMLSerializer<MenuDescriptor>.Load(path);

            if (r == null)
                throw new Exception("File dei menu non compilato correttamente!");

            if (r.MenuRepresentations.Length == 0)
                throw new Exception("File dei menu non compilato correttamente! Nessuna rappresentazione trovata");

            result = r.MenuRepresentations[0];


            return result;
        }


     

    }
}

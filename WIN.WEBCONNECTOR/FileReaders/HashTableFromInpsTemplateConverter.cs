using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace WIN.WEBCONNECTOR.FileReaders
{
    public class HashTableFromInpsTemplateConverter
    {
        public static IList ConvertToIQITemplateHashList(IList dataToConvert)
        {

        //    'in questa funzione converto una lista di hashtables dove ogni hashtable è un record
        //'proveninete da un file excel sul modello dell'inps
        //'in una lista di hashtable sul temoplate iqi fenealgest


            ArrayList list = new ArrayList();
            foreach (Hashtable  item in dataToConvert )
            {
                list.Add(ConvertHashRecord(item));
            }

            return list;

     
        }


        private static Hashtable ConvertHashRecord(Hashtable inpsTemplate)
        {
        


            Hashtable result = new Hashtable();



        //'adesso valorizzo tutte le proprietà della hash table risultato con 
        //'le proprietà della hashtable di input

        //'recupero il nome e il cognome
            string[] nome  = inpsTemplate["Cognome/Nome"].ToString().Split(new string[]{"/"},   StringSplitOptions.RemoveEmptyEntries );
            result.Add ("COGNOME_UTENTE",nome[0]);
            result.Add ("NOME_UTENTE",nome[1]);

            result.Add ("DATA_NASCITA_UTENTE",inpsTemplate["Data di Nascita"]);
            result.Add ("FISCALE",inpsTemplate["Codice Fiscale"]);
            result.Add ("COMUNE",inpsTemplate["Comune Res."]);
            result.Add ("QUOTA",inpsTemplate["Importo Pagamento"]);
            result.Add ("DATA_INIZIO",inpsTemplate["Data Valuta"]);
            result.Add ("DOMANDA",inpsTemplate["N. Domanda"]);
            result.Add("FILE_NAME", inpsTemplate["FILE_NAME"]);
            result.Add("TIPO_PRESTAZIONE", inpsTemplate["TIPO_PRESTAZIONE"]);

            result.Add("PROVINCIA", "");
            result.Add("INDIRIZZO", "");
            result.Add("CAP", "");


            result.Add("SETTORE", "");
    
       

            return result;
        }
    }
}

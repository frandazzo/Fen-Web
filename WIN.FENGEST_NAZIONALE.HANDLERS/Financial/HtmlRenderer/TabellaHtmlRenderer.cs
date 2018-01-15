using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.FENGEST_NAZIONALE.HANDLERS.Financial.HtmlRenderer
{
    public class TabellaHtmlRenderer
    {

        public static string CreaTabella(string htmlContent)
        {
            StringBuilder b = new StringBuilder();
            //costruisco la tabella
            b.Append("<table class=\"generic_table\">");

            b.Append(htmlContent);


            //chiudo la tabella
            b.Append("</table>");

            return b.ToString();
        }


        public static string  CreaColonna( string valore)
        {
            StringBuilder b = new StringBuilder();
            //apro la prima colonna
            b.Append("<td>");
            //inserisco il contenuto
            b.Append(valore);
            //chiudo la prima colonna
            b.Append("</td>");

            return b.ToString();

        }

        public static  string CreaRigaConListaColonne( string[] arr)
        {
            if (arr == null)
                return "";
            if (arr.Length == 0)
                return "";

            StringBuilder b = new StringBuilder();
            //apro la riga
            b.Append("<tr>");


            foreach (string item in arr)
            {
                string val = item;
                if (val == null)
                    val = "";

                b.Append(CreaColonna(val));
            }

            //chiudo la riga
            b.Append("</tr>");

            return b.ToString();

        }

    }
}

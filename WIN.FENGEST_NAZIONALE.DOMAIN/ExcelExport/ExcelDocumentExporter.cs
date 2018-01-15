using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using DevExpress.Spreadsheet;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.ExcelExport
{
    public class ExcelDocumentExporter
    {
        public static string CreateExcelFile(string filename, ExcelDocument document)
        {
            Workbook workbook = new DevExpress.Spreadsheet.Workbook();

            // Access the first worksheet in the workbook.
            Worksheet worksheet = workbook.Worksheets[0];

            int lastEditedRow = 0;
                
            foreach (ExcelRow row in document.Rows)
            {
                //se sono alla riga 0 devo scrivere l'intestazione
                if (lastEditedRow == 0)
                {
                    WriteHeader(row, worksheet);
                    lastEditedRow++;
                }

                WriteRow(worksheet, row, ref lastEditedRow, 0);
               // lastEditedRow++;

                
            }

            worksheet.GetUsedRange().AutoFitColumns();

            //creo un file temporaneo in una cartella temporanea
            string result = Path.GetTempPath();
            filename = result  + filename;

            workbook.SaveDocument(filename);

            return filename;

        }

        private static void WriteRow(Worksheet worksheet, ExcelRow row, ref int lastEditedRow, int column)
        {
            List<ExcelProperty> properties = row.Properties;
            properties.Sort(delegate(ExcelProperty x, ExcelProperty y)
            {

                return x.Priority.CompareTo(y.Priority);

            });

            foreach (ExcelProperty item in properties)
            {
                worksheet.Cells[lastEditedRow, column].Value = item.Value;
                column++;

            }
            lastEditedRow++;

            // a questo punto punto posso verificare se cè un documento da stampare
            if (row.Document == null)
                return;

            if (row.Document != null)
                if (row.Document.Rows == null)
                    return;

            if (row.Document != null)
                if (row.Document.Rows != null)
                    if (row.Document.Rows.Count == 0)
                        return;

            //qui devo stampare il documento figlio
            WriteChildDocument(worksheet, row.Document, ref lastEditedRow);
        }


     
        private static void WriteChildDocument(Worksheet worksheet, ExcelDocument excelDocument, ref int lastEditedRow)
        {
            //creo la riga diintestazione
            WriteChildHeader(excelDocument.Rows[0], worksheet, ref lastEditedRow);
            lastEditedRow++;

            foreach (ExcelRow item in excelDocument.Rows)
            {
                WriteRow(worksheet, item, ref lastEditedRow, 1);
            }

        }



        private static void WriteChildHeader(ExcelRow row, Worksheet worksheet, ref int lastEditedRow)
        {
            //ordino 
            List<ExcelProperty> properties = row.Properties;
            properties.Sort(delegate(ExcelProperty x, ExcelProperty y)
            {

                return x.Priority.CompareTo(y.Priority);

            });

            //adesso posso stampare la riga iniziale
            int column = 1;
            foreach (ExcelProperty item in properties)
            {

                worksheet.Cells[lastEditedRow, column].Value = item.Name;
                column++;
            }



            Range range = worksheet.Range.FromLTRB(1, lastEditedRow, column, lastEditedRow);
            Formatting rangeFormatting = range.BeginUpdateFormatting();
            rangeFormatting.Fill.BackgroundColor = Color.LightGray;
            rangeFormatting.Font.Size = 16;
            rangeFormatting.Font.Bold = true;
            range.EndUpdateFormatting(rangeFormatting);
        }

        private static void WriteHeader(ExcelRow row, Worksheet worksheet)
        {
            //ordino 
            List<ExcelProperty> properties = row.Properties;
            properties.Sort(delegate(ExcelProperty x, ExcelProperty y)
                {
            
                        return x.Priority.CompareTo(y.Priority);
           
                });

            //adesso posso stampare la riga iniziale
            int column = 0;
            foreach (ExcelProperty item in properties)
            {

                worksheet.Cells[0, column].Value = item.Name;
                column++;
            }



            Range range = worksheet.Range.FromLTRB(0, 0, column, 0);
            Formatting rangeFormatting = range.BeginUpdateFormatting();
            rangeFormatting.Fill.BackgroundColor = Color.LightGray;
            rangeFormatting.Font.Size = 16;
            rangeFormatting.Font.Bold = true;
            range.EndUpdateFormatting(rangeFormatting);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DevExpress.Compression;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.Compression
{
    public class Archiver
    {
        public static string ArchiveFiles(string[] sourceFiles, string filename)
        {
            using (ZipArchive archive = new ZipArchive())
            {
                foreach (string file in sourceFiles)
                {
                    try
                    {
                        archive.AddFile(file);
                    }
                    catch (Exception)
                    {
                        
                        throw;
                    }
                    
                }
                string result = Path.GetTempPath();
                filename = result + filename;
                archive.Save(filename);

                return filename;
            }
        }
    }
}

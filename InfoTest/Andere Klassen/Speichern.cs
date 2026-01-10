using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoTest
{
    public static class Speichern 
    {
        private static string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SpielStaende"); //Recherchiert in StackOverflow 
        public static void SpeichereLevel(string spielerName, int levelNummer)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                //C:\Users\Michi\Documents\GitHub\Informatik-Projekt-Test\InfoTest\InfoTest\bin\Debug\net9.0-windows\SpielStaende
            }

            string pfad = Path.Combine(folderPath, spielerName + ".txt");

            File.WriteAllText(pfad, levelNummer.ToString());
        }

        public static int LadeLevel(string spielerName)
        {
            string pfad = Path.Combine(folderPath, spielerName + ".txt");

            if (File.Exists(pfad))
            {
                string inhalt = File.ReadAllText(pfad);
                if (int.TryParse(inhalt, out int level))
                {
                    return level;
                }
            }
            return 0; 
        }
    }
}

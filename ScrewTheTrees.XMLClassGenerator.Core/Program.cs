using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace ScrewTheTrees.XmlClassGenerator.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            string output = @"C:\Classes";
            if (!Directory.Exists(output))
                Directory.CreateDirectory(output);

            output += @"\output";
            if (!Directory.Exists(output))
                Directory.CreateDirectory(output);

            XmlEntityImporter handle = new XmlEntityImporter("Classes.xml", output);

            handle.Load();
            List<XmlClassEntity> entitites = handle.CreateEntities();

            //IF we sort them according to string length, it will always generate the essential folders before their subfolders!
            entitites.Sort(XmlClassEntity.CompareByDirectoryLength);


            foreach (XmlClassEntity e in entitites)
            {
                ClassGeneratorClassFile cgcf = new ClassGeneratorClassFile(e, output);
                Thread workerThread = new Thread(cgcf.Execute);
                workerThread.Start();
                //cgcf.Execute();
            }

            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;

namespace ScrewTheTrees.XmlClassGenerator.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            string output = "C:\\Classes";
            if (!Directory.Exists(output))
                Directory.CreateDirectory(output);

            output += "\\output\\";
            if (!Directory.Exists(output))
                Directory.CreateDirectory(output);

            XmlEntityImporter handle = new XmlEntityImporter("Classes.xml", output);

            handle.Load();
            List<XmlClassEntity> entitites = handle.CreateEntities();

            foreach (XmlClassEntity e in entitites)
            {
                ClassGeneratorClassFile cgcf = new ClassGeneratorClassFile(e, output);
                cgcf.Execute();
            }

            Console.ReadLine();
        }
    }
}

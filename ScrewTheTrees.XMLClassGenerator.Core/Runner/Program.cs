﻿using ScrewTheTrees.XmlClassGenerator.Core.Injector;
using System;
using System.Collections.Generic;
using System.IO;


namespace ScrewTheTrees.XmlClassGenerator.Core.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            //I had to hardcode it to go into either C: or D: , otherwise the directory length gets too long
            string output = @"C:\Classes";
            if (!Directory.Exists(output))
                Directory.CreateDirectory(output);
            
            //Hardcoded... until a proper console app is built
            Console.WriteLine("Importing XML classes");
            XmlEntityImporter handle = new XmlEntityImporter("Classes.xml", output);
            handle.Load();
            List<XmlClassEntity> entitites = handle.CreateEntities();


            Console.WriteLine("Generating Entity Data and injecting data.");
            //Generate all their values
            foreach (XmlClassEntity e in entitites)
            {
                e.GenerateFields();
                e.GenerateIncludes();
                e.GenerateHeader();
                e.GenerateFinalize();

                ListInjector inject = new ListInjector(e);
                inject.Load();
                inject.Inject();
            }
            Console.WriteLine("Generating \"Agents.h\" file");
            //Generate the core file before we sort it
            ClassGeneratorMainFile agents = new ClassGeneratorMainFile(entitites, output);
            agents.Execute();

            Console.WriteLine("Sorting entities");
            //IF we sort them according to string length to the folder path, it will always generate the essential folders before their subfolders!
            entitites.Sort(XmlClassEntity.CompareByDirectoryLength);

            Console.WriteLine("Generating ClassFiles into the filesystem");
            foreach (XmlClassEntity e in entitites)
            {
                ClassGeneratorClassFile cgcf = new ClassGeneratorClassFile(e, output);
                cgcf.Execute();
            }

            Console.WriteLine("Processed "+entitites.Count+" Entities");
            Console.WriteLine("All done!");

            Console.ReadLine();
        }
    }
}

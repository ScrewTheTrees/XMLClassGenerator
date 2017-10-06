

using System;
using System.IO;

namespace ScrewTheTrees.XmlClassGenerator.Core
{
    class ClassGeneratorClassFile : ITask
    {
        private XmlClassEntity XmlClass;
        private string OutputDirectory;

        public ClassGeneratorClassFile(XmlClassEntity xmlClass, string outputDirectory)
        {
            XmlClass = xmlClass;
            OutputDirectory = outputDirectory;
        }

        public void Execute()
        {
            GenerateFoldersToFile();
            GenerateFile();
        }

        public void GenerateFile()
        {
            StreamWriter write = new StreamWriter(OutputDirectory + @"\" + XmlClass.Directory + @"\" + XmlClass.Name + ".h");

            write.WriteLine(XmlClass);


        }

        private void GenerateFoldersToFile()
        {
            XmlClassEntity parent = XmlClass.ParentClass;

            string extraDir = XmlClass.Directory;

            if (!Directory.Exists(OutputDirectory + @"\" + extraDir))
                Directory.CreateDirectory(OutputDirectory + @"\" + extraDir);
        }
    }
}

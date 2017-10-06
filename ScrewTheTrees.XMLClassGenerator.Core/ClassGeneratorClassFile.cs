

using System;
using System.IO;

namespace ScrewTheTrees.XmlClassGenerator.Core
{
    public class ClassGeneratorClassFile : ITask
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
            GenerateFolder();
            GenerateFile();
        }

        public void GenerateFile()
        {
            StreamWriter write = new StreamWriter(OutputDirectory + @"\" + XmlClass.Directory + @"\" + XmlClass.Name + ".h");

            //Generate Includes
            foreach (string inc in XmlClass.Includes)
                write.WriteLine(inc);

            write.WriteLine();

            //Header
            foreach (string h in XmlClass.Header)
                write.WriteLine(h);
            //My body is ready
            foreach (string b in XmlClass.Body)
                write.WriteLine(b);


            write.Close();
        }

        /// <summary>
        /// This function does not generate the folders up to this one, please sort before you generate the folder using:
        /// XmlClassEntity.CompareByDirectoryLength
        /// </summary>
        private void GenerateFolder()
        {
            string extraDir = XmlClass.Directory;

            if (!Directory.Exists(OutputDirectory + @"\" + extraDir))
                Directory.CreateDirectory(OutputDirectory + @"\" + extraDir);
        }
    }
}

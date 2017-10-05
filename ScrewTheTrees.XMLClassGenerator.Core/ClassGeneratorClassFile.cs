

using System.IO;

namespace ScrewTheTrees.XmlClassGenerator.Core
{
    class ClassGeneratorClassFile : ITask
    {
        private XmlClassEntity xmlClass;
        private string outputDirectory;

        public ClassGeneratorClassFile(XmlClassEntity xmlClass, string outputDirectory)
        {
            this.xmlClass = xmlClass;
            this.outputDirectory = outputDirectory;
        }

        public bool Execute()
        {
            GenerateFoldersToFile();
            GenerateFile();

            return true;
        }

        public void GenerateFile()
        {
            //TODO: Generate File
        }

        private void GenerateFoldersToFile()
        {
            XmlClassEntity parent = xmlClass.parentClass;
            string extraDir = "";

            while (parent != null)
            {
                extraDir = (parent.Name.Substring(1, parent.Name.Length - 1)) + "\\" + extraDir;

                if (!Directory.Exists(outputDirectory + extraDir))
                    Directory.CreateDirectory(outputDirectory + extraDir);

                parent = parent.parentClass;
            }
        }
    }
}



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
            //TODO: Make Folder Generator
        }
    }
}

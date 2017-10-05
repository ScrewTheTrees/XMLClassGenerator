

namespace ScrewTheTrees.XmlClassGenerator.Core
{
    class ClassGeneratorClassFile
    {
        private XmlClassEntity xmlClass;

        public ClassGeneratorClassFile(XmlClassEntity classes)
        {
            this.xmlClass = classes;
        }

        public void GenerateFile(string outputDir)
        {
            //TODO: Generate File
        }

        private void GenerateFoldersToFile(string outputDir, string baseDir)
        {
            //TODO: Make Folder Generator
        }
    }
}

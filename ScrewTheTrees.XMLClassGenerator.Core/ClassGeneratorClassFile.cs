

namespace ScrewTheTrees.XMLClassGenerator.Core
{
    class ClassGeneratorClassFile
    {
        private XMLClassEntity xmlClass;

        public ClassGeneratorClassFile(XMLClassEntity classes)
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

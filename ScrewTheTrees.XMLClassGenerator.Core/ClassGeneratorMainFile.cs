

namespace ScrewTheTrees.XmlClassGenerator.Core
{
    class ClassGeneratorMainFile : ITask
    {
        private XmlClassEntity[] xmlClasses;
        private string outputDirectory;

        public ClassGeneratorMainFile(XmlClassEntity[] xmlClasses, string outputDirectory)
        {
            this.xmlClasses = xmlClasses;
            this.outputDirectory = outputDirectory;
        }

        public void Execute()
        {
            GenerateFile();
        }

        public void GenerateFile()
        {
            //TODO: Generate File
        }
    }
}

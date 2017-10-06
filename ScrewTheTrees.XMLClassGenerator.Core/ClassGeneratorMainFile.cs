

namespace ScrewTheTrees.XmlClassGenerator.Core
{
    class ClassGeneratorMainFile : ITask
    {
        private XmlClassEntity[] XmlClasses;
        private string OutputDirectory;

        public ClassGeneratorMainFile(XmlClassEntity[] xmlClasses, string outputDirectory)
        {
            XmlClasses = xmlClasses;
            OutputDirectory = outputDirectory;
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

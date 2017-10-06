using System.Collections.Generic;
using System.IO;

namespace ScrewTheTrees.XmlClassGenerator.Core
{
    public class ClassGeneratorMainFile : ITask
    {
        private List<XmlClassEntity> XmlClasses;
        private string OutputDirectory;
        private string Name = "agents";

        public ClassGeneratorMainFile(List<XmlClassEntity> xmlClasses, string outputDirectory)
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
            StreamWriter write = new StreamWriter(OutputDirectory + @"\" + Name + ".h");

            foreach (XmlClassEntity e in XmlClasses)
            {
                write.Write(string.Format("#include \"{0}{1}.h\"", e.Directory.Replace('\\', '/'), e.Name));
                if (e.ParentClass != null)
                    write.Write(string.Format("\\\\ : {0}", e.ParentClass.Name));
                write.WriteLine();
            }

            write.Close();
        }
    }
}

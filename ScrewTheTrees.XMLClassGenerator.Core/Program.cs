using System;



namespace ScrewTheTrees.XmlClassGenerator.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlEntityImporter handle = new XmlEntityImporter("Classes.xml", "\\output");

            handle.Load();
            handle.CreateEntities();

            Console.ReadLine();
        }
    }
}

using System;



namespace ScrewTheTrees.XMLClassGenerator.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            XMLEntityImporter handle = new XMLEntityImporter("Classes.xml", "\\output");

            handle.Load();
            handle.createEntities();

            Console.ReadLine();
        }
    }
}

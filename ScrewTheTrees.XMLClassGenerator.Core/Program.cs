using System;



namespace ScrewTheTrees.XMLClassGenerator.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            XMLHandler handle = new XMLHandler("Classes.xml", "\\output");

            handle.Load();
            handle.createEntities();

            Console.ReadLine();
        }
    }
}

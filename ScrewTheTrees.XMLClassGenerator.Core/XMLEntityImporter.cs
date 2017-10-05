using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Xml.Linq;

namespace ScrewTheTrees.XmlClassGenerator.Core
{
    class XmlEntityImporter
    {
        public XDocument doc= new XDocument();
        public List<XmlClassEntity> eList = new List<XmlClassEntity>();
        private string outputDirectory;
        private string loadDocument;

        public XmlEntityImporter(string loadDocument, string outputDirectory)
        {
            this.loadDocument = loadDocument;
            this.outputDirectory = outputDirectory;
        }

        public XDocument Load()
        {
            doc = XDocument.Load(loadDocument);

            return doc;
        }
        public XDocument Load(string document)
        {
            doc = XDocument.Load(document);

            return doc;
        }


        public List<XmlClassEntity> CreateEntities()
        {
            List<XmlClassEntity> entities = new List<XmlClassEntity>();

            List<XElement> root = new List<XElement>();
            root.AddRange(doc.Elements().Where(x => x.Name == "class"));

            Console.WriteLine(root.Attributes().FirstOrDefault() + "  - Children:  " + root.Elements().Count().ToString());

            //Loop through all the elements to create their ClassEntities
            foreach (XElement node in root)
                entities = ParseNode(node, entities, null);

            //Calculate directories (and print debug i guess)
            foreach (XmlClassEntity e in entities)
            {
                e.CalculateDirectory();

                Console.WriteLine("Name: " + e.Name + "      Size:" + e.Size + "   ID:" + e.ID + "   Handler:" + e.Sandler);
            }

            return entities;
        }


        private List<XmlClassEntity> ParseNode(XElement element, List<XmlClassEntity> entities, XmlClassEntity parent)
        {
            Console.WriteLine(element.Attributes().FirstOrDefault() + " - Children:  " + element.Elements().Count().ToString());

            XmlClassEntity currentEntity = MakeEntityFromElement(element);
            currentEntity.parentClass = parent;

            entities.Add(currentEntity);

            if (element.Elements().Count() > 0 && element != null)
            {
                //Only loop classes here.
                foreach (XElement node in element.Elements().Where(x => x.Name == "class"))
                    entities = ParseNode(node, entities, currentEntity);
            }
            
            return entities;
        }


        private XmlClassEntity MakeEntityFromElement(XElement element)
        {
            return new XmlClassEntity(
                id: element.Attribute("id").Value,
                name: element.Attribute("name").Value,
                handler: element.Attribute("handler").Value,
                size: int.Parse(element.Attribute("size").Value
                ));

        }


    }
}

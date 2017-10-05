using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Xml.Linq;

namespace ScrewTheTrees.XMLClassGenerator.Core
{
    class XMLEntityImporter
    {
        public XDocument doc= new XDocument();
        public List<XMLClassEntity> eList = new List<XMLClassEntity>();
        private string outputDirectory;
        private string loadDocument;

        public XMLEntityImporter(string loadDocument, string outputDirectory)
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


        public List<XMLClassEntity> createEntities()
        {
            List<XMLClassEntity> entities = new List<XMLClassEntity>();

            List<XElement> root = new List<XElement>();
            root.AddRange(doc.Elements().Where(x => x.Name == "class"));

            Console.WriteLine(root.Attributes().FirstOrDefault() + "  - Children:  " + root.Elements().Count().ToString());

            //Loop through all the elements to create their ClassEntities
            foreach (XElement node in root)
                entities = parseNode(node, entities, null);

            //Calculate directories (and print debug i guess)
            foreach (XMLClassEntity e in entities)
            {
                e.calculateDirectory();

                Console.WriteLine("name: " + e.name + " - Size:" + e.size + " - ID:" + e.id + " - Handler:" + e.handler);
            }

            return entities;
        }


        private List<XMLClassEntity> parseNode(XElement element, List<XMLClassEntity> entities, XMLClassEntity parent)
        {
            Console.WriteLine(element.Attributes().FirstOrDefault() + " - Children:  " + element.Elements().Count().ToString());

            XMLClassEntity currentEntity = makeEntityFromElement(element);
            currentEntity.parentClass = parent;

            entities.Add(currentEntity);

            if (element.Elements().Count() > 0 && element != null)
            {
                foreach (XElement node in element.Elements())
                    entities = parseNode(node, entities, currentEntity);
            }
            
            return entities;
        }


        private XMLClassEntity makeEntityFromElement(XElement element)
        {
            return new XMLClassEntity(
                id: element.Attribute("id").Value,
                name: element.Attribute("name").Value,
                handler: element.Attribute("handler").Value,
                size: int.Parse(element.Attribute("size").Value
                ));

        }


    }
}

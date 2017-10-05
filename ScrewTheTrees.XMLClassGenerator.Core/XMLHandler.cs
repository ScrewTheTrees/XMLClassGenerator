using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Xml.Linq;

namespace ScrewTheTrees.XMLClassGenerator.Core
{
    class XMLHandler
    {
        public XDocument doc= new XDocument();
        public List<XMLClassEntity> eList = new List<XMLClassEntity>();
        private string outputDirectory;
        private string loadDocument;

        public XMLHandler(string loadDocument, string outputDirectory)
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
            List<XmlNode> allNodes = new List<XmlNode>();

            List<XElement> root = new List<XElement>();
            root.AddRange(doc.Elements().Where(x => x.Name == "class"));

            Console.WriteLine(root.Attributes().FirstOrDefault() + "  - Children:  " + root.Elements().Count().ToString());

            //Loop parent
            foreach (XElement node in root)
                entities = parseNode(node, entities, null);



            foreach (XMLClassEntity e in entities)
            {
                e.calculateDirectory();

                Console.WriteLine(e.name + " : " + e.directory);
            }


            Console.WriteLine("");
            Console.WriteLine("done!");

            return entities;
        }


        private List<XMLClassEntity> parseNode(XElement root, List<XMLClassEntity> entities, XMLClassEntity parent)
        {
            //TODO: Loop logic
            Console.WriteLine(root.Attributes().FirstOrDefault() + "  - Children:  " + root.Elements().Count().ToString());

            XMLClassEntity currentEntity = makeEntityFromElement(root);
            currentEntity.parentClass = parent;

            entities.Add(currentEntity);

            if (root.Elements().Count() > 0 && root != null)
            {
                foreach (XElement node in root.Elements())
                entities = parseNode(node, entities, currentEntity);
            }
            
            return entities;
        }


        private XMLClassEntity makeEntityFromElement(XElement root)
        {
            return new XMLClassEntity(
                id: root.Attribute("id").Value,
                name: root.Attribute("name").Value,
                handler: root.Attribute("handler").Value,
                size: int.Parse(root.Attribute("size").Value
                ));

        }


    }
}

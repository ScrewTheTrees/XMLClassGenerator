using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Xml.Linq;

namespace ScrewTheTrees.XmlClassGenerator.Core
{
    public class XmlEntityImporter
    {
        public XDocument Doc= new XDocument();
        public List<XmlClassEntity> EList = new List<XmlClassEntity>();
        private string OutputDirectory;
        private string LoadDocument;

        public XmlEntityImporter(string loadDocument, string outputDirectory)
        {
            LoadDocument = loadDocument;
            OutputDirectory = outputDirectory;
        }

        public XDocument Load()
        {
            Doc = XDocument.Load(LoadDocument);

            return Doc;
        }
        public XDocument Load(string document)
        {
            Doc = XDocument.Load(document);

            return Doc;
        }

        public List<XmlClassEntity> CreateEntities()
        {
            List<XmlClassEntity> entities = new List<XmlClassEntity>();
            List<XElement> rootClass = new List<XElement>();
            rootClass.AddRange(Doc.Elements().Where(x => x.Name == "class"));

            Console.WriteLine(rootClass.Attributes().FirstOrDefault() + "  - Children:  " + rootClass.Elements().Count().ToString());

            //Loop through all the elements to create their ClassEntities
            foreach (XElement node in rootClass)
                entities = ParseNode(node, entities, null);

            //Calculate directories (and print debug i guess)
            foreach (XmlClassEntity e in entities)
            {
                e.CalculateDirectory();
            }

            return entities;
        }

        private List<XmlClassEntity> ParseNode(XElement element, List<XmlClassEntity> entities, XmlClassEntity parent)
        {
            XmlClassEntity currentEntity = MakeEntityFromElement(element);
            currentEntity.ParentClass = parent;

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
            return (new XmlClassEntity() {
                ID = element.Attribute("id").Value,
                Name = element.Attribute("name").Value,
                Handler = element.Attribute("handler").Value,
                Size = int.Parse(element.Attribute("size").Value)
                });
        }
    }
}

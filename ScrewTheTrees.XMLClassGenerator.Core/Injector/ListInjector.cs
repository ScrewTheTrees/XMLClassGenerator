using ScrewTheTrees.XMLClassGenerator.Core.Injector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ScrewTheTrees.XmlClassGenerator.Core.Injector
{
    public class ListInjector
    {
        private XmlClassEntity Entity;
        private XDocument Doc = new XDocument();

        public ListInjector(XmlClassEntity entity)
        {
            Entity = entity;
        }

        public XDocument Load()
        {
            Doc = XDocument.Load(@"Injects\" + Entity.Name);

            return Doc;
        }
        public XDocument Load(string document)
        {
            Doc = XDocument.Load(document);

            return Doc;
        }

        public void Inject()
        {
            List<XElement> rootClass = new List<XElement>();
            rootClass.AddRange(Doc.Elements().Where(x => x.Name == "class"));

            Console.WriteLine(rootClass.Attributes("ID") + "  - Lists:  " + rootClass.Elements().Count().ToString());

            if (rootClass[0].HasElements)
            {
                if (rootClass[0].Elements().Any(x => x.Name == "Includes"))
                {
                    XElement includes = rootClass[0].Elements().Single(x => x.Name == "Includes");
                    List<string> addem = Entity.Includes;

                    ParseElements(includes, addem);
                }
            }
        }


        public void ParseElements(XElement elements, List<string> list)
        {
            List<IClassElement> injectorElements = new List<IClassElement>();

            foreach(XElement x in elements.Elements())
            {
                if (x.Name == "FieldReplace")
                    injectorElements.Add(new ClassFieldReplace(x.Attribute("FieldName").Value, x.Attribute("FieldType").Value, x.Attribute("NewName").Value, x.Attribute("NewType").Value, x.Attribute("Comment").Value));
            }
        }
    }
}

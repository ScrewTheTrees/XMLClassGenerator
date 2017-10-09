using ScrewTheTrees.XMLClassGenerator.Core.Injector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ScrewTheTrees.XmlClassGenerator.Core.Injector
{
    public class ListInjector
    {
        private XmlClassEntity Entity;
        private XDocument XmlDoc = new XDocument();

        public ListInjector(XmlClassEntity entity)
        {
            Entity = entity;
        }

        public XDocument Load()
        {
            try {
            if (File.Exists(Directory.GetCurrentDirectory() + @"\Injects\" + Entity.Name + ".xml"))
                XmlDoc = XDocument.Load(Directory.GetCurrentDirectory() + @"\Injects\" + Entity.Name + ".xml");
            else XmlDoc = null;
            }
            catch (Exception e)
            {
                XmlDoc = null;
                Console.WriteLine("WARNING! Could not Load injection XML");
                Console.WriteLine(e);
            }


            return XmlDoc;
        }
        public XDocument Load(string document)
        {
            if (File.Exists(document))
                XmlDoc = XDocument.Load(document);
            else XmlDoc = null;

            return XmlDoc;
        }

        public void Inject()
        {
            if (XmlDoc != null)
            {
                XElement Doc = XmlDoc.Descendants().FirstOrDefault();
                Console.WriteLine("Injecting into: " + Entity.Name);

                if (Doc.Descendants().Any(x => x.Name == "Includes"))
                {
                    XElement includes = Doc.Descendants().Single(x => x.Name == "Includes");
                    List<string> addem = Entity.InjectIncludes;
                    ParseElements(includes, addem);
                    Entity.GenerateIncludes();
                }
                if (Doc.Descendants().Any(x => x.Name == "Header"))
                {
                    XElement includes = Doc.Descendants().Single(x => x.Name == "Header");
                    List<string> addem = Entity.InjectHeader;
                    ParseElements(includes, addem);
                    Entity.GenerateHeader();
                }
                if (Doc.Descendants().Any(x => x.Name == "Fields"))
                {
                    XElement includes = Doc.Descendants().Single(x => x.Name == "Fields");
                    List<string> addem = Entity.Fields;
                    ParseElements(includes, addem);
                }
                if (Doc.Descendants().Any(x => x.Name == "BeforeFields"))
                {
                    XElement includes = Doc.Descendants().Single(x => x.Name == "BeforeFields");
                    List<string> addem = Entity.InjectBeforeFields;
                    ParseElements(includes, addem);
                    Entity.GenerateFinalize();
                }
                if (Doc.Descendants().Any(x => x.Name == "AfterFields"))
                {
                    XElement includes = Doc.Descendants().Single(x => x.Name == "AfterFields");
                    List<string> addem = Entity.InjectAfterFields;
                    ParseElements(includes, addem);
                    Entity.GenerateFinalize();
                }
            }
        }


        public void ParseElements(XElement elements, List<string> list)
        {
            List<IClassElement> injectorElements = new List<IClassElement>();

            foreach(XElement x in elements.Elements())
            {
                if (x.Name == "FieldReplace")
                {
                    ClassFieldReplace field = new ClassFieldReplace(x.Attribute("Name")?.Value ?? "", x.Attribute("NewName")?.Value ?? "", x.Attribute("Type")?.Value ?? "");
                    field.Comment = x.Value;
                    injectorElements.Add(field);
                }

                else if (x.Name == "FieldRemove")
                    injectorElements.Add(new ClassFieldRemove(x.Attribute("Name")?.Value ?? ""));

                else if (x.Name == "Include")
                    injectorElements.Add(new ClassInclude(x.Attribute("IncludeString")?.Value ?? ""));

                else if (x.Name == "String")
                    if (x.Attributes().Any(a => a.Name == "Text"))
                        injectorElements.Add(new ClassString(x.Attribute("Text")?.Value ?? ""));
                    else injectorElements.Add(new ClassString(x.Value));

                else Console.WriteLine("Ignoring unknown XML tag: " + x.Name);
            }

            foreach (IClassElement e in injectorElements)
            {
                e.InjectIntoList(list);
            }
        }
    }
}

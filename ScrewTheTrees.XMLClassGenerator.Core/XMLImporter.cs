using System.Xml;

namespace ScrewTheTrees.XMLClassGenerator.Core
{
    class XMLHandler
    {
        public XmlDocument docu = new XmlDocument();
        
        public XMLHandler(string document)
        {
            docu.Load(document);
        }
    }
}

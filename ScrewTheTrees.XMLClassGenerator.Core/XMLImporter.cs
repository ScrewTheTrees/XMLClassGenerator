using System.Collections.Generic;
using System.Xml;

namespace ScrewTheTrees.XMLClassGenerator.Core
{
    class XMLHandler
    {
        public XmlDocument docu = new XmlDocument();
        public List<XMLClassEntity> eList = new List<XMLClassEntity>();
        private string outputDirectory;
        private string loadDocument;


        public XMLHandler(string loadDocument, string outputDirectory)
        {
            this.loadDocument = loadDocument;
            this.outputDirectory = outputDirectory;
        }

        public XmlDocument Load()
        {
            docu.Load(loadDocument);

            return docu;
        }

    }
}

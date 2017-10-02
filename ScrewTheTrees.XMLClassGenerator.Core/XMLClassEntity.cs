

namespace ScrewTheTrees.XMLClassGenerator.Core
{
    class XMLClassEntity
    {
        public string id { get; }
        public string name { get; }
        public string handler { get; }
        public int size { get; }

        public XMLClassEntity parentClass = null;
        public XMLClassEntity[] childrenClasses = null;


        public XMLClassEntity(string id, string name, string handler, int size)
        {
            this.id = id;
            this.name = name;
            this.handler = handler;
            this.size = size;
        }

    }
}

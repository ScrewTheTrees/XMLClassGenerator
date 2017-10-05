

namespace ScrewTheTrees.XMLClassGenerator.Core
{
    class XMLClassEntity
    {
        public string id { get; }
        public string name { get; }
        public string handler { get; }
        public int size { get; }
        public string directory = "\\";

        public XMLClassEntity parentClass = null;
        public XMLClassEntity[] childrenClasses = null;


        public XMLClassEntity(string id, string name, string handler, int size)
        {
            this.id = id;
            this.name = name;
            this.handler = handler;
            this.size = size;
        }

        /**
         * Calculates the relative directory this instance is gonna need...
         * 
         * */
        public void calculateDirectory()
        {
            XMLClassEntity parent = parentClass;

            while (parent != null)
            {
                directory = ("\\" + parent.name.Substring(1, parent.name.Length - 1)) + directory;

                parent = parent.parentClass;
            }

        }

    }
}

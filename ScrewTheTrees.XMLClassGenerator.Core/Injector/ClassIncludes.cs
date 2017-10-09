
using System.Collections.Generic;

namespace ScrewTheTrees.XmlClassGenerator.Core
{
    public class ClassIncludes
    {
        public string IncludeString;
        public string Comment;

        public ClassIncludes(string includeString)
        {
            IncludeString = includeString;
        }

        /// <summary>
        /// Injects this Include into a string List
        /// </summary>
        /// <param name="list">List to Inject into.</param>
        /// <returns>Returns whenever it was successfully injected or not.</returns>
        public bool InjectIntoList(List<string> list)
        {
            string comment;
            if (Comment != null)
                comment = "//" + Comment;
            else comment = "";

            list.Add( string.Format("{0} {1}", IncludeString, comment));

            return true;
        }
    }
}

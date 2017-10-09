

using ScrewTheTrees.XMLClassGenerator.Core.Injector;
using System.Collections.Generic;

namespace ScrewTheTrees.XmlClassGenerator.Core.Injector
{
    public class ClassFieldReplace : IClassElement
    {
        public string FieldName;
        public string FieldType;

        public string NewName;
        public string NewType;

        public string Comment;

        public ClassFieldReplace(string fieldName, string fieldType, string newName, string newType, string comment)
        {
            FieldName = fieldName;
            FieldType = fieldType;
            NewName = newName;
            NewType = newType;
            Comment = comment;
        }

        /// <summary>
        /// Injects this ClassField into a string List
        /// </summary>
        /// <param name="list">List to Inject into.</param>
        /// <returns>Returns whenever it was successfully injected or not.</returns>
        public bool InjectIntoList(List<string> list)
        {
            string comment;
            if (Comment != null)
                comment = "//" + Comment;
            else comment = "";

            for (int i = 0; i < list.Count; i++)
            {
                string tempString = list[i];

                tempString = tempString.Trim(' ');

                if (tempString.StartsWith(string.Format("{0} {1};" , FieldType, FieldName)))
                {
                    list[i] = string.Format("{0} {1}; {2}", FieldType, FieldName, comment);

                    return true;
                }
            }

            return false;
        }
    }
}

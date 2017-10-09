using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrewTheTrees.XMLClassGenerator.Core.Injector
{
    public class ClassFieldRemove : IClassElement
    {

        public string FieldName;
        public string FieldType;

        public ClassFieldRemove(string fieldName)
        {
            FieldName = fieldName;
            FieldType = "int";
        }

        /// <summary>
        /// Injects this Include into a string List
        /// </summary>
        /// <param name="list">List to Inject into.</param>
        /// <returns>Returns true.</returns>
        public bool InjectIntoList(List<string> list)
        {
            list.Remove(string.Format("  {0} {1};", FieldType, FieldName));

            return true;
        }
    }
}

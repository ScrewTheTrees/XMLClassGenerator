
using System.Collections.Generic;

namespace ScrewTheTrees.XMLClassGenerator.Core.Injector
{
    //Woho!
    public interface IClassElement
    {
        bool InjectIntoList(List<string> list);
    }
}

using IsracardTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsracardTest.Utility
{
    public interface IDataProvider
    {
        IEnumerable<CustomCard> GetBySearchString(string searchString);
    }
}

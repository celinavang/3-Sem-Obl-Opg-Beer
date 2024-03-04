using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarLib
{
    public interface IBeerRepository
    {
        // Retrieves all objects if no parameters are provided.
        // Otherwise, retrieves objects based on the specified parameters.
        IEnumerable<Beer> Get(Double? abvOver = null, Double? abvUnder = null, string? orderBy = null);

        // Retrieves object from list based on Id
        Beer? GetById(int id);

        // Adds new object to list
        Beer Add(Beer beerToAdd);

        // Removes object from list based on Id
        Beer? Remove(int id);

        // Updates existing object on list
        Beer? Update(int id, Beer data);

    }
}

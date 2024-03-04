using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarLib
{
    public class BeerRepository : IBeerRepository
    {
        private int _nextId = 1;
        private readonly List<Beer> _beers = new();

        public BeerRepository() 
        {
            _beers.Add(new() { Id = _nextId++, Name = "Beer1", Abv = 1});
            _beers.Add(new() { Id = _nextId++, Name = "Beer2", Abv = 5.5 });
            _beers.Add(new() { Id = _nextId++, Name = "Beer3", Abv = 10 });
            _beers.Add(new() { Id = _nextId++, Name = "Beer4", Abv = 7 });
            _beers.Add(new() { Id = _nextId++, Name = "Beer5", Abv = 2.5 });
        }


        // Retrieves all objects if no parameters are provided.
        // Otherwise, retrieves objects based on the specified parameters.
        public IEnumerable<Beer> Get(Double? abvOver = null, Double? abvUnder = null, string? orderBy = null)
        {
            // Filters by Abv equal to or larger than parameter
            IEnumerable<Beer> result = new List<Beer>(_beers);
            if (abvOver != null)
            {
                result = result.Where(b => b.Abv >= abvOver);
            }
            // Filters by Abv equal to or lower than parameter
            if (abvUnder != null)
            {
                result = result.Where(b => b.Abv <= abvUnder);
            }
            // Sorting 
            if (orderBy != null)
            {
                orderBy = orderBy.ToLower();
                switch (orderBy)
                {
                    case "name":
                        result = result.OrderBy(b => b.Name);
                        break;
                    case "name_desc":
                        result = result.OrderByDescending(b => b.Name);
                        break;
                    case "abv":
                        result = result.OrderBy(b => b.Abv);
                        break;
                    case "abs_desc":
                        result = result.OrderByDescending(b => b.Abv);
                        break;
                    default:
                        break;
                }
            }
            return result;
        }

        // Retrieves object from list based on Id
        public Beer? GetById(int id)
        {
            return _beers.Find(b => b.Id == id);
        }

        // Adds new object to list
        public Beer Add(Beer beerToAdd)
        {
            // Validating Name and Abv before 
            beerToAdd.Validate();

            // Sets Id to ensure unique Id
            beerToAdd.Id = _nextId++;

            // Adds object to list
            _beers.Add(beerToAdd);
            return beerToAdd;
        }

        // Removes object from list based on Id
        public Beer? Remove(int id)
        {
            // Retrieves by Id

            Beer? beerToRemove = GetById(id);

            // If object exists on list ->
            if (beerToRemove != null)
            {
                _beers.Remove(beerToRemove);
                return beerToRemove;
            }
            // If object does not exists on list ->
            return null;
        }

        // Updates existing object on list
        public Beer? Update(int id, Beer data) 
        {
            // Validates new Abv and Name
            data.Validate();

            // Retrieves by Id
            Beer? beerToUpdate = GetById(id);

            // If object exists on list ->
            if (beerToUpdate != null)
            {
                beerToUpdate.Name = data.Name;
                beerToUpdate.Abv = data.Abv;
                return beerToUpdate;
            }
            // If object does not exists on list ->
            return null;
        }
    }
}

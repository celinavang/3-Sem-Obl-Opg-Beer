using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarLib.Tests
{
    
    [TestClass()]
    public class BeerRepositoryTests
    {
        private IBeerRepository _repo;

        [TestInitialize]
        public void Init()
        {
            _repo = new BeerRepository();
        }


        // Tests the Get method.
        // Method retrieves all objects from list, if no parameters has been set
        [TestMethod()]
        public void GetTest()
        {
            // Tests the methods ability to retrieve all objects 
            // There should be 5 objects on the list, as set in our repository
            IEnumerable<Beer> beers = _repo.Get();
            Assert.AreEqual(5, beers.Count());

            // Tests descending Name sorting
            // The fist object has a name equal to "Beer1", as set in our repository
            IEnumerable<Beer> nameAsc = _repo.Get(orderBy: "name");
            Assert.AreEqual(nameAsc.First().Name, "Beer1");

            // Tests ascending Abv sorting
            // The object with the lowest abv has an abv of 1, as set in our repository
            IEnumerable<Beer> abvAsc = _repo.Get(orderBy: "abv");
            Assert.AreEqual(abvAsc.First().Abv, 1);
        }

        // Tests the GetById method
        // Method retrieves a single object with the provided Id
        [TestMethod()]
        public void GetByIdTest()
        {
            // Tests if there is an object with the Id of 1
            // Should be true as that is our starting Id, as set in our repository
            Assert.IsNotNull(_repo.GetById(1));

            // Tests if there is an object with a value of one more that the count of our list (Here: 6)
            // As we have not removed any objects from the list, such an object should not exist at current - this could change 
            Assert.IsNull(_repo.GetById(_repo.Get().Count()+1));
        }

        // Tests the Add method
        // Method adds new object to list
        [TestMethod()]
        public void AddTest()
        {
            // Counts the current length of our list
            int currentCount = _repo.Get().Count();

            // Valid beer
            Beer _data = new() { Name = "Beer6", Abv = 40 };
            // Not valid beer
            Beer _badData = new() { Abv = 40 };

            // Adds valid beer
            Beer addedBeer = _repo.Add(_data);

            // Checks if an object has been added to list 
            // The count should have changed by 1
            Assert.AreEqual(currentCount+1, _repo.Get().Count());

            // Checks if our new objects can be retrieved from the list
            Assert.IsNotNull(_repo.Get(addedBeer.Id));

            // Counts new current count 
            currentCount = _repo.Get().Count();

            // Checks if our validation on the Add method inhibits us from adding the invalid beer
            Assert.ThrowsException<ArgumentNullException>(() => _repo.Add(_badData));

            // Checks that nothing has been added to list. 
            // Count should be the same as previously
            Assert.AreEqual(currentCount, _repo.Get().Count());
            
        }

        // Tests the Remove method
        // Method should be able to remove an object from list
        [TestMethod()]
        public void RemoveTest()
        {
            // Counts the current length of our list
            int currentCount = _repo.Get().Count();

            // Tries to remove non existing id 
            _repo.Remove(currentCount+1);

            // Count should be same as previously
            Assert.AreEqual(currentCount, _repo.Get().Count());

            // Tries to remove valid id
            _repo.Remove(1);

            // Count should be 1 less than previously
            Assert.AreEqual(_repo.Get().Count(), currentCount-1);
        }

        // Tests the update method 
        // Method should be able to update an object
        [TestMethod()]
        public void UpdateTest()
        {
            // Counts the current length of our list
            int currentCount = _repo.Get().Count();

            // Valid beer
            Beer _data = new() { Name = "updatedName", Abv = 12 };

            // Invalid beer
            Beer _badData = new() { Name = "b1", Abv = 100 };


            // Tries to update data of invalid Id
            Assert.IsNull(_repo.Update(currentCount+1, _data));

            // Updates valid id, and checks if Id is the same
            Assert.AreEqual(2, _repo.Update(2, _data)?.Id);

            // Checks that the name has been updated
            Assert.AreEqual(_data.Name, _repo.GetById(2)?.Name);

            // Checks that no new objects has been added to the list 
            Assert.AreEqual(currentCount, _repo.Get().Count());

            // Checks that exception is thrown if beer data is not valid
            Assert.ThrowsException<ArgumentException>(() => _repo.Update(2, _badData));

            // Checks that the name has not been updated
            Assert.AreEqual(_data.Name, _repo.GetById(2)?.Name);
        }
    }
}
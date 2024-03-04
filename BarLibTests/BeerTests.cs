using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BarLib.Tests
{
    [TestClass()]
    public class BeerTests
    {
        private readonly Beer _beer = new() {Id = 1,Name = "Beer1", Abv = 45};
        private readonly Beer _nameNull = new() { Id = 1, Abv = 45 };
        private readonly Beer _nameShort = new() { Id = 1, Name = "B1", Abv = 45 };
        private readonly Beer _abvNull = new() { Id = 1, Name = "Beer1"};
        private readonly Beer _abvLow = new() { Id = 1, Name = "Beer1", Abv = -45 };
        private readonly Beer _abvHigh = new() { Id = 1, Name = "Beer1", Abv = 100 };


        // Tests that the to string method is correct
        [TestMethod()]
        public void ToStringTest()
        {
            Assert.AreEqual($"{_beer.Id} | Name: {_beer.Name} - {_beer.Abv}%", _beer.ToString());
        }

        // Tests the validate name method
        [TestMethod()]
        public void ValidateNameTest()
        {
            // Valid name
            _beer.ValidateName();

            // Name is null
            Assert.ThrowsException<ArgumentNullException>(() => _nameNull.ValidateName());

            // Name is 2 characters
            Assert.ThrowsException<ArgumentException>(() => _nameShort.ValidateName());
        }

        // Tests the validate abv method
        [TestMethod()]
        public void ValidateAbvTest()
        {
            // valid abv
            _beer.ValidateAbv();

            // Null abv
            Assert.ThrowsException<ArgumentNullException>(() => _abvNull.ValidateAbv());

            // Negative abv | -45
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _abvLow.ValidateAbv());

            // High abv | 100
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _abvHigh.ValidateAbv());
        }

        // Tests the validate method 
        [TestMethod()]
        public void ValidateTest()
        {
            // Valid beer
            _beer.Validate();
        }
    }
}
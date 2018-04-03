using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ConsoleApp1
{
    [TestFixture]
    public class Tests
    {
        /* STATION WAGON */
        [Test]
        public void DoorsArmedInitialy()
        {
            Assert.IsTrue((new StationWagon()).AreDoorsArmed() == false);
        }

        [Test]
        public void FuelLevelInitial()
        {
            Assert.IsTrue((new StationWagon()).getFuelLevel() == 0);
        }

        [Test]
        public void UpdatingFuelLevelWrong()
        {
            var testStationWagon = new StationWagon();
            testStationWagon.UpdateFuelLevel(-10.0);
            Assert.IsTrue(testStationWagon.getFuelLevel() == 0);
        }

        [Test]
        public void UpdatingFuelLevelCorrect()
        {
            var testStationWagon = new StationWagon();
            testStationWagon.UpdateFuelLevel(50.0);
            Assert.IsTrue(testStationWagon.getFuelLevel() == 50.0);
        }

        /* SEDAN */
        [Test]
        public void GetInitRange()
        {
            Assert.IsTrue(((Sedan)new Sedan()).GetRange() == 0.0);
        }

        [Test]
        public void GetRangeAfterRefueling()
        {
            var testSedan = new Sedan();
            testSedan.UpdateFuelLevel(30);
            Assert.IsTrue(testSedan.GetRange() == 600.0);
        }

        [Test]
        public void ClimateModeInitialy()
        {
            Assert.IsTrue((new Sedan()).GetCurrentClimateMode() == CLIMATE_MODE.off);
        }
        
        [Test]
        public void CheckInitOilLevel()
        {
            Assert.IsTrue((new Sedan()).OilLevel == 6.0);
        }
    }
}

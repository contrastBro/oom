using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Sedan sedan1 = new Sedan();
            Console.WriteLine("sedan1 initial:\n" + sedan1.getShortSummary() + "\n");


            StationWagon stationWagon1 = new StationWagon();
            Console.WriteLine("stationWagon1 initial:\n" + stationWagon1.getShortSummary() + "\n");
        }
    }
    
    /* abstract base class containing only abstract methods: interface */
    interface ICar
    {
        void toggleEngineStartButton();
        void toggleDoorsLockButton();
        void toggleHazardsButton();
    }

    enum climateMode { manual, auto, off };

    class Sedan : ICar
    {
        // private field(s)
        private bool engineOn;
        private bool doorsLocked;
        private bool hazardsOn;
        private double fuelLevel;
        private climateMode climMode;

        // public properties
        public bool IsEngineRunning { get { return engineOn; } }
        public bool AreDoorsLocked { get { return doorsLocked; } }
        public bool IsHazardOn { get { return hazardsOn; } }
        public double FuelTank { get { return fuelLevel; } set { fuelLevel = value; } }

        // 1 constructor
        public Sedan()
        {
            this.engineOn = false;
            this.doorsLocked = true;
            this.hazardsOn = false;
            this.fuelLevel = 0;
            this.climMode = climateMode.off;
        }

        // interface implementation
        public void toggleDoorsLockButton()
        {
            this.doorsLocked = !this.doorsLocked;
        }

        public void toggleEngineStartButton()
        {
            this.engineOn = !this.engineOn;
        }

        public void toggleHazardsButton()
        {
            this.hazardsOn = !this.hazardsOn;
        }

        // public method(s)
        public void toggleClimateControlAutoButton()
        {
            if (this.climMode == climateMode.auto)
            {
                this.climMode = climateMode.off;
            }
            else
            {
                this.climMode = climateMode.auto;
            }
        }

        public String getShortSummary()
        {
            return "Engine running?: " + this.IsEngineRunning + "\nDoors locked?: " + this.AreDoorsLocked + "\nHazard on?: " + this.IsHazardOn + "\nFuel level: " + this.FuelTank + "\nClimate mode: " + this.climMode;
        }

    }

    class StationWagon : ICar
    {
        // private field(s)
        private bool engineOn;
        private bool doorsLocked;
        private bool hazardsOn;
        private double fuelLevel;
        private bool rearHatchOpen;

        // public properties
        public bool IsEngineRunning { get { return engineOn; } }
        public bool AreDoorsLocked { get { return doorsLocked; } }
        public bool IsHazardOn { get { return hazardsOn; } }
        public double FuelTank { get { return fuelLevel; } set { fuelLevel = value; } }

        // 1 constructor
        public StationWagon()
        {
            this.doorsLocked = true;
            this.engineOn = false;
            this.hazardsOn = false;
            this.fuelLevel = 0;
        }

        // interface implementation
        public void toggleDoorsLockButton()
        {
            this.doorsLocked = !this.doorsLocked;
        }

        public void toggleEngineStartButton()
        {
            this.engineOn = !this.engineOn;
        }

        public void toggleHazardsButton()
        {
            this.hazardsOn = !this.hazardsOn;
        }

        // public method(s)
        public void toggleRearHatchOpenButton()
        {
            this.rearHatchOpen = true;
        }

        public String getShortSummary()
        {
            return "Engine running?: " + this.IsEngineRunning + "\nDoors locked?: " + this.AreDoorsLocked + "\nHazard on?: " + this.IsHazardOn + "\nFuel level: " + this.FuelTank + "\nRear Hatch open?: " + this.rearHatchOpen;
        }

    }
}

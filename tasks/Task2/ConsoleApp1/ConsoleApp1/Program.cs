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
            Console.WriteLine("sedan1 initial:\n" + sedan1.GetShortSummary() + "\n");


            StationWagon stationWagon1 = new StationWagon();
            Console.WriteLine("stationWagon1 initial:\n" + stationWagon1.GetShortSummary() + "\n");
        }
    }
    
    class GeneralFeatures
    {
        private bool engineOn;
        private bool doorsLocked;
        private bool hazardsOn;
        private double fuelLevel;

        public bool EngineOn { get; set; }
        public bool DoorsLocked { get; set; }
        public bool HazardsOn { get; set; }
        public double FuelLevel {
            get
            {
                return fuelLevel;
            }
            set
            {
                if(value < 0)
                {
                    fuelLevel = 0;
                }
                fuelLevel = value;
            }
        }

        public GeneralFeatures()
        {
            this.EngineOn = false;
            this.DoorsLocked = true;
            this.HazardsOn = false;
            this.FuelLevel = 0;
        }
    }

    /* abstract base class containing only abstract methods: interface */
    interface ICar
    {
        void ToggleEngineStartButton();
        void ToggleDoorsLockButton();
        void ToggleHazardsButton();
        void UpdateFuelLevel(double newFuelLevel);
    }

    enum climateMode { manual, auto, off };

    class Sedan : ICar
    {
        // private field(s)
        private GeneralFeatures generalFeatures;
        private climateMode climMode;

        // public properties

        // 1 constructor
        public Sedan()
        {
            this.generalFeatures = new GeneralFeatures();
            this.climMode = climateMode.off;
        }

        // interface implementation
        public void ToggleDoorsLockButton()
        {
            this.generalFeatures.DoorsLocked = !this.generalFeatures.DoorsLocked;
        }

        public void ToggleEngineStartButton()
        {
            this.generalFeatures.EngineOn = !this.generalFeatures.EngineOn;
        }

        public void ToggleHazardsButton()
        {
            this.generalFeatures.HazardsOn = !this.generalFeatures.HazardsOn;
        }

        public void UpdateFuelLevel(double newFuelLevel)
        {
            this.generalFeatures.FuelLevel = newFuelLevel;
        }

        // public method(s)
        public void ToggleClimateControlAutoButton()
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

        public String GetShortSummary()
        {
            return "Engine running?: " + this.generalFeatures.EngineOn + "\nDoors locked?: " + this.generalFeatures.DoorsLocked + "\nHazard on?: " + this.generalFeatures.HazardsOn + "\nFuel level: " + this.generalFeatures.FuelLevel + "\nClimate mode: " + this.climMode;
        }

    }

    class StationWagon : ICar
    {
        // private field(s)
        private GeneralFeatures generalFeatures;
        private bool rearHatchOpen;

        // public properties

        // 1 constructor
        public StationWagon()
        {
            this.generalFeatures = new GeneralFeatures();
        }

        // interface implementation
        public void ToggleDoorsLockButton()
        {
            this.generalFeatures.DoorsLocked = !this.generalFeatures.DoorsLocked;
        }

        public void ToggleEngineStartButton()
        {
            this.generalFeatures.EngineOn = !this.generalFeatures.EngineOn;
        }

        public void ToggleHazardsButton()
        {
            this.generalFeatures.HazardsOn = !this.generalFeatures.HazardsOn;
        }

        public void UpdateFuelLevel(double newFuelLevel)
        {
            this.generalFeatures.FuelLevel = newFuelLevel;
        }

        // public method(s)
        public void ToggleRearHatchOpenButton()
        {
            this.rearHatchOpen = true;
        }

        public String GetShortSummary()
        {
            return "Engine running?: " + this.generalFeatures.EngineOn + "\nDoors locked?: " + this.generalFeatures.DoorsLocked + "\nHazard on?: " + this.generalFeatures.HazardsOn + "\nFuel level: " + this.generalFeatures.FuelLevel + "\nRear Hatch open?: " + this.rearHatchOpen;
        }

    }
}

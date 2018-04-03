using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             *
                T3.4
                In the main function, create an array (of your interface type) containing a mix of instances of all your classes which implement this interface.
                Add some test code similar to the lesson3 example (e.g. a loop over the array which prints properties or returns values of method calls).
             */

            var bunchOfCars = new List<ICar> { new Sedan(), new Sedan(), new StationWagon() };
            bunchOfCars[0].ToggleDoorsLockUnlockButton();
            bunchOfCars[1].ToggleHazardsOnOffButton();
            ((StationWagon)bunchOfCars[2]).ToggleRearHatchOpenButton();

            bunchOfCars[1].UpdateFuelLevel(10);

            for (int i = 0, i_limit = bunchOfCars.Count; i < i_limit; i++)
            {
                Console.WriteLine("Status of Car-Type: " + bunchOfCars[i].GetType() + "\n" + bunchOfCars[i].GetShortSummary() + "\n\n");
            }

            /*
            bunchOfCars[1].UpdateFuelLevel(30.0);
            Console.WriteLine("GetRangeAfterRefueling: " + ((Sedan)bunchOfCars[1]).GetRange());
            Console.WriteLine("CurrentClimateMode: " + ((Sedan)bunchOfCars[0]).GetCurrentClimateMode());
            */

            Console.WriteLine();

            /*
             *
                T4.3
                Create an array of several objects of your project's classes.
                Serialize (write) and deserialize (read) these objects to/from a .json file.
                Print the contents of the file to the console.
                Use the examples at http://www.newtonsoft.com/json/help/html/Samples.htm
                If you do not succeed in serializing your objects, create an issue at https://github.com/bicoom/oom/issues, including a detailed problem description.
             */

            /* no exception handling, information is not serialized, as the fields are private, and there is no explicit properties supporting the complicated structure */
            var config = new JsonSerializerSettings() { Formatting = Formatting.Indented, TypeNameHandling = TypeNameHandling.Auto };
            File.WriteAllText("cars.json", JsonConvert.SerializeObject(bunchOfCars, config));
            Console.WriteLine("content from deserialized json:");
            var bunchOfCarsFromFile = JsonConvert.DeserializeObject<List<ICar>>(File.ReadAllText("cars.json"), config);
            for (int i = 0, i_limit = bunchOfCarsFromFile.Count; i < i_limit; i++)
            {
                Console.WriteLine("Status of Car-Type: " + bunchOfCarsFromFile[i].GetType() + "\n" + bunchOfCarsFromFile[i].GetShortSummary() + "\n\n");
            }
        }
    }

    enum TOGGLE_STATE { off, on }
    enum ENGINE_STATE { off, error, preheat, emergency_on, on }
    enum DOORS_STATE { off, error, off_unarmed, on, on_armed }
    enum CLIMATE_MODE { manual, auto, off };
    enum REAR_HATCH_MODE { closed, error, opened }

    class EngineManagement
    {
        private ENGINE_STATE engineState;
        private double fuelLevel;

        public double FuelLevel { get { return fuelLevel; } set { fuelLevel = (value < 0) ? 0 : value; } }
        public ENGINE_STATE EngineState { get; set; }

        public EngineManagement()
        {
            this.FuelLevel = 0.0;
            this.EngineState = ENGINE_STATE.off;
        }
    }

    class ComfortManagement
    {
        private DOORS_STATE doors;
        private TOGGLE_STATE hazards;

        public DOORS_STATE DoorLocking { get { return doors; } set { doors = value; } }
        public TOGGLE_STATE HazardToggle { get { return hazards; } set { hazards = value; } }

        public ComfortManagement()
        {
            this.DoorLocking = DOORS_STATE.off;
            this.HazardToggle = TOGGLE_STATE.off;
        }
    }

    /* abstract base class containing only abstract methods: interface */
    interface ICar
    {
        void ToggleEngineStartStopButton();
        void ToggleDoorsLockUnlockButton();
        void ToggleHazardsOnOffButton();
        void UpdateFuelLevel(double newFuelLevel);
        String GetShortSummary();
    }

    class Sedan : ICar
    {
        private EngineManagement engineMgmnt;
        private ComfortManagement comfortMgmnt;
        private CLIMATE_MODE climMode;
        private double oilLevel;

        public double OilLevel { get { return oilLevel; } set { oilLevel = (value < 0) ? 0 : value; } }

        public Sedan()
        {
            this.engineMgmnt = new EngineManagement();
            this.comfortMgmnt = new ComfortManagement();
            this.climMode = CLIMATE_MODE.off;
            this.OilLevel = 6.0;
        }

        // interface implementation
        public void ToggleDoorsLockUnlockButton() { this.comfortMgmnt.DoorLocking = this.comfortMgmnt.DoorLocking == DOORS_STATE.off ? DOORS_STATE.on : DOORS_STATE.off; }

        public void ToggleEngineStartStopButton() { this.engineMgmnt.EngineState = this.engineMgmnt.EngineState == ENGINE_STATE.off ? ENGINE_STATE.on : ENGINE_STATE.off; }

        public void ToggleHazardsOnOffButton() { this.comfortMgmnt.HazardToggle = this.comfortMgmnt.HazardToggle == TOGGLE_STATE.off ? TOGGLE_STATE.on : TOGGLE_STATE.off; }
        
        public void UpdateFuelLevel(double newFuelLevel) { this.engineMgmnt.FuelLevel = newFuelLevel; }

        // other methods
        public void ToggleClimateControlAutoButton() { this.climMode = (this.climMode != CLIMATE_MODE.auto) ? CLIMATE_MODE.auto : CLIMATE_MODE.manual; }
        
        public double GetRange() { return this.engineMgmnt.FuelLevel > 0.0 ? (this.engineMgmnt.FuelLevel / 5.0) * 100.0 : 0.0; }

        public CLIMATE_MODE GetCurrentClimateMode() { return this.climMode; }
        
        public String GetShortSummary()
        {
            return "Engine running?: " + this.engineMgmnt.EngineState 
                + "\nDoors locked?: " + this.comfortMgmnt.DoorLocking 
                + "\nHazard on?: " + this.comfortMgmnt.HazardToggle 
                + "\nFuel level: " + this.engineMgmnt.FuelLevel 
                + "\nClimate mode: " + this.climMode;
        }
    }

    class StationWagon : ICar
    {
        private EngineManagement engineMgmnt;
        private ComfortManagement comfortMgmnt;
        private REAR_HATCH_MODE rearHatch;

        public StationWagon()
        {
            this.engineMgmnt = new EngineManagement();
            this.comfortMgmnt = new ComfortManagement();
            this.rearHatch = REAR_HATCH_MODE.closed;
        }

        // interface implementation
        public void ToggleDoorsLockUnlockButton() { this.comfortMgmnt.DoorLocking = this.comfortMgmnt.DoorLocking == DOORS_STATE.off ? DOORS_STATE.on : DOORS_STATE.off; }

        public void ToggleEngineStartStopButton() { this.engineMgmnt.EngineState = this.engineMgmnt.EngineState == ENGINE_STATE.off ? ENGINE_STATE.on : ENGINE_STATE.off; }

        public void ToggleHazardsOnOffButton() { this.comfortMgmnt.HazardToggle = this.comfortMgmnt.HazardToggle == TOGGLE_STATE.off ? TOGGLE_STATE.on : TOGGLE_STATE.off; }

        public void UpdateFuelLevel(double newFuelLevel) { this.engineMgmnt.FuelLevel = newFuelLevel; }

        // other methods
        public void ToggleRearHatchOpenButton() { this.rearHatch = (this.rearHatch != REAR_HATCH_MODE.closed) ? REAR_HATCH_MODE.opened : REAR_HATCH_MODE.closed; }

        public bool AreDoorsArmed() { return this.comfortMgmnt.DoorLocking == DOORS_STATE.on_armed ? true : false; }

        public double getFuelLevel() { return this.engineMgmnt.FuelLevel; }

        public String GetShortSummary()
        {
            return "Engine running?: " + this.engineMgmnt.EngineState
                + "\nDoors locked?: " + this.comfortMgmnt.DoorLocking
                + "\nHazard on?: " + this.comfortMgmnt.HazardToggle
                + "\nFuel level: " + this.engineMgmnt.FuelLevel
                + "\nRear Hatch open?: " + this.rearHatch;
        }
    }
}

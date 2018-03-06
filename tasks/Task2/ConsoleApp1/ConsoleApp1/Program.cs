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
            Car redCar = new Car("Max Mustermann");
            redCar.licensePlate = "W - 123456";
            redCar.vinNumber = "WVWZZZ6NZTY112309";
            Console.WriteLine("red-car initial:\n" + redCar.getShortSummary() + "\n");
            redCar.updateOwnerId("Hans Meier");
            Console.WriteLine("red-car altered:\n" + redCar.getShortSummary() + "\n");

            Car blueCar = new Car("Max Mustermann's Frau");
            blueCar.licensePlate = "W - 123457";
            blueCar.vinNumber = "WVWZZZ6NZTY112310";
            Console.WriteLine("blue-car initial:\n" + blueCar.getShortSummary() + "\n");
            blueCar.updateOwnerId("Hans Meier's Frau");
            Console.WriteLine("blue-car altered:\n" + blueCar.getShortSummary() + "\n");
        }
    }

    class Car
    {
        // 1 private field
        private String ownerId;

        // 2 public properties (auto-implemented property)
        public String vinNumber { get; set; }
        public String licensePlate { get; set; }

        // 1 constructor
        public Car(String owner)
        {
            this.ownerId = owner;
        }

        // 1 or more public method(s)
        public String getShortSummary()
        {
            return "Owner: " + this.ownerId + "\nVIN: " + this.vinNumber + "\nLicense plate: " + this.licensePlate;
        }

        public void updateOwnerId(String newOwner)
        {
            this.ownerId = newOwner;
        }

    }
}

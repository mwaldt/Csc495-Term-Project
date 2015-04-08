using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermProject {
    class VehicleCollection : Persistable {
        protected List<Vehicle> vehicles;

        public VehicleCollection()
            : base() // call parent default constructor
        {
            connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" +
                @"Data source= C:\Users\Lisa\Documents" +
                @"\BicycleRental.accdb";
            vehicles = new List<Vehicle>();
        }

        public void PopulateWithGoodAndAvailableBikes() {
            string queryString = "SELECT ID FROM Vehicle WHERE PhysicalCondition = 'Good' AND Status = 'Active'";
            List<Object> bikeIds = getValues(queryString);
            foreach (Object element in bikeIds) {
                Vehicle v = new Vehicle();
                v.Populate((int)((object[])element)[0]);
                vehicles.Add(v);
            }

        }

        public List<Vehicle> GetBikeList() {
            return vehicles;
        }

        public void PrintAll() {
            foreach(Vehicle vehicle in vehicles){
                vehicle.Print();
            }
        }
    }
}

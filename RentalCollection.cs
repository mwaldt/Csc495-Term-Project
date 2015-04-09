using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermProject {
    class RentalCollection : Persistable {
        protected List<Vehicle> rentals;

        public RentalCollection()
            : base() { rentals = new List<Vehicle>(); } // call parent default constructor


        public void PopulateWithGoodAndAvailableBikes() {
            string queryString = "SELECT ID FROM Vehicle WHERE PhysicalCondition = 'Good' AND Status = 'Active'";
            List<Object> bikeIds = getValues(queryString);
            foreach (Object element in bikeIds) {
                Vehicle v = new Vehicle();
                v.Populate((int)((object[])element)[0]);
                rentals.Add(v);
            }

        }

        public List<Vehicle> GetBikeList() {
            return rentals;
        }

        
        public void PrintAll() {
            
            foreach(Rental rental in rentals){
                rental.Print();
            }
            
        } 
    }
}

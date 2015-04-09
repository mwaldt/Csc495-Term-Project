using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermProject
{
    class RentalCollection : Persistable
    {
        protected List<Rental> rentalsOut;

        public RentalCollection()
            : base() { rentalsOut = new List<Rental>(); } // call parent default constructor


        public void PopulateWithRentedOutBikes()
        {
            string queryString = "SELECT ID FROM Rental WHERE (DateReturned = '' OR DateReturned IS NULL)";
            List<Object> rentalIds = getValues(queryString);
            foreach (Object element in rentalIds)
            {
                Rental r = new Rental();
                r.Populate((int)((object[])element)[0]);
                rentalsOut.Add(r);
            }

        }

        public List<Rental> GetBikeList()
        {
            return rentalsOut;
        }


        public void PrintAll()
        {
            foreach (Rental rental in rentalsOut)
            {
                rental.Print();
            }

        }
    }
}


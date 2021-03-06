using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermProject
{
    class Rental : Persistable
    {
        public int ID { get; set; }
        public string VehicleID { get; set; }
        public string RenterID { get; set; }
        public string DateRented { get; set; }
        public string TimeRented { get; set; }
        public string DateDue { get; set; }
        public string TimeDue { get; set; }
        public string DateReturned { get; private set; }
        public string TimeReturned { get; set; }
        public string CheckoutWorkerID { get; set; }
        public string CheckinWorkerID { get; set; }


        public Rental() : base() { } // call parent default constructor


        //------------------------------------------------------------------
        public void Populate(int ID)
        {
            string queryString = "SELECT * FROM Rental WHERE (ID = " + ID + ")";
            List<Object> results = getValues(queryString);
            if (results != null)
            {
                foreach (object result in results)
                {
                    IEnumerable<Object> row = result as IEnumerable<Object>;
                    int count = 0;
                    foreach (object rowValue in row)
                    {
                        // DEBUG Console.WriteLine(rowValue);
                        if (count == 0)
                            this.ID = Convert.ToInt32(rowValue);
                        else if (count == 1)
                            this.VehicleID = Convert.ToString(rowValue);
                        else if (count == 2)
                            RenterID = Convert.ToString(rowValue);
                        else if (count == 3)
                            DateRented = Convert.ToString(rowValue);
                        else if (count == 4)
                            TimeRented = Convert.ToString(rowValue);
                        else if (count == 5)
                            DateDue = Convert.ToString(rowValue);                        
                        else if (count == 6)
                            TimeDue = Convert.ToString(rowValue);                        
                        else if (count == 7)
                            DateReturned = Convert.ToString(rowValue);                            
                        else if (count == 8)
                            TimeReturned = Convert.ToString(rowValue);                      
                        else if (count == 9)
                            CheckoutWorkerID = Convert.ToString(rowValue);
                        else if (count == 10)
                            CheckinWorkerID = Convert.ToString(rowValue);
                        count = count + 1;
                    }
                }
            }
        }

        //------------------------------------------------------------------
        public void Insert()
        {
            this.DateRented = DateTime.Now.ToString("yyyy-MM-dd");
            string insertQuery =
            "INSERT INTO Rental (VehicleID, RenterID, DateRented, TimeRenter, DateDue, TimeDue, DateReturned, TimeReturned, CheckoutWorker, CheckinWorker) " +
            "VALUES (" +
            "'" + this.VehicleID + "', '" +
            this.RenterID + "', '" +
            this.DateRented + "', '" +
            this.TimeRented + "', '" +
            this.DateDue + "', '" +
            this.TimeDue + "', '" +
            this.DateReturned + "', '" +
            this.TimeReturned + "', '" +
            this.CheckoutWorkerID + "', '" +
            this.CheckinWorkerID + "')";
            int returnCode = ModifyDatabase(insertQuery);
            if (returnCode != 0)
            {
                Console.WriteLine("Error in inserting Rental object into database");
            }
            else
            {
                Console.WriteLine("Rental object successfully inserted");
                string idQueryString = "SELECT MAX(ID) FROM Rental";
                List<Object> results = getValues(idQueryString);
                if (results != null)
                {
                    // DEBUG Console.WriteLine("Got an id from id query");
                    foreach (object result in results)
                    {
                        IEnumerable<Object> row = result as IEnumerable<Object>;
                        foreach (object rowValue in row)
                        {
                            // DEBUG Console.WriteLine("Retrieved id = " + rowValue);
                            this.ID = Convert.ToInt32(rowValue);
                        }
                    }
                }
            }
        }

        //------------------------------------------------------------------
        public void Update()
        {
            string updateQuery = "UPDATE Worker SET " +
                " VehicleID  = '" + this.VehicleID + "' ," +
                " RenterID  = '" + this.RenterID + "' ," +
                " DateRented  = '" + this.DateRented + "' ," +
                " TimeRented  = '" + this.TimeRented + "' ," +
                " DateDue  = '" + this.DateDue + "' ," +
                " TimeDue  = '" + this.TimeDue + "', " +
                " DateReturned  = '" + this.DateReturned + "', " +
                " TimeReturned  = '" + this.TimeReturned + "', " +
                " CheckoutWorkerID  = '" + this.CheckoutWorkerID + "' " +
                " CheckinWorkerID   = '" + this.CheckinWorkerID + "' " +
                " WHERE " +
                " ID = " + this.ID;
            int returnCode = ModifyDatabase(updateQuery);
            if (returnCode != 0)
                Console.WriteLine("Error in updating rental object into database");
            else
                Console.WriteLine("Rental object successfully updated");
        }

        //------------------------------------------------------------------
        public void Delete()
        {
            string deleteQuery = "DELETE FROM Rental WHERE " +
                " ID = " + this.ID;
            Console.WriteLine(deleteQuery);
            int returnCode = ModifyDatabase(deleteQuery);
            if (returnCode != 0)
                Console.WriteLine("Error in deleting rental object from database");
            else
                Console.WriteLine("Rental object successfully deleted");
        }

        public override string ToString()
        {
            return "\nID:\t\t\t" + this.ID +
                "\nVehicle ID:\t\t" + this.VehicleID +
                "\nRenter ID:\t\t" + this.RenterID +
                "\nDate Rented:\t\t" + this.DateRented +
                "\nTime Rented:\t\t" + this.TimeRented +
                "\nDate Due:\t\t" + this.DateDue +
                "\nTime Due:\t\t" + this.TimeDue +
                "\nDate Returned:\t" + this.DateReturned +
                "\nTime Returned:\t" + this.TimeReturned +
                "\nCheckout Worker ID:\t" + this.CheckoutWorkerID +
                "\nCheckin Worker ID:\t" + this.CheckinWorkerID;
        }

        public void Print() { Console.WriteLine("\n********\n" + this.ToString() + "\n********\n"); }
    }
}


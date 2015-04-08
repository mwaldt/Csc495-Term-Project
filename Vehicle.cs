using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermProject {
    class Vehicle : Persistable {
        public int ID { get; set; }
        public string BikeMake { get; set; }
        public string ModelNumber { get; set; }
        public string SerialNumber { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string PhysicalCondition { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
        public string DateStatusUpdated { get; set; }


        public Vehicle() : base() { } // call parent default constructor

        //------------------------------------------------------------------
        public void Populate(int ID) {
            string queryString = "SELECT * FROM Vehicle WHERE (ID = " + ID + ")";
            List<Object> results = getValues(queryString);
            if (results != null) {
                foreach (object result in results) {
                    IEnumerable<Object> row = result as IEnumerable<Object>;
                    int count = 0;
                    foreach (object rowValue in row) {
                        // DEBUG Console.WriteLine(rowValue);
                        if (count == 0)
                            this.ID = Convert.ToInt32(rowValue);
                        else if (count == 1)
                            this.BikeMake = Convert.ToString(rowValue);
                        else if (count == 2)
                            ModelNumber = Convert.ToString(rowValue);
                        else if (count == 3)
                            SerialNumber = Convert.ToString(rowValue);
                        else if (count == 4)
                            Color = Convert.ToString(rowValue);
                        else if (count == 5)
                            Description = Convert.ToString(rowValue);
                        else if (count == 6)
                            Location = Convert.ToString(rowValue);
                        else if (count == 7)
                            PhysicalCondition = Convert.ToString(rowValue);
                        else if (count == 8)
                            Notes = Convert.ToString(rowValue);
                        else if (count == 9)
                            Status = Convert.ToString(rowValue);
                        else if (count == 10)
                            DateStatusUpdated = Convert.ToString(rowValue);
                        count = count + 1;
                    }
                }
            }
        }
        //------------------------------------------------------------------
        public void Insert() {
            string insertQuery =
            "INSERT INTO Vehicle (BikeMake, ModelNumber, SerialNumber, Color, Description, Location, PhysicalCondition, Notes, Status, DateStatusUpdated) " +
            "VALUES (" +
            "'" + this.BikeMake + "', '" +
            this.ModelNumber + "', '" +
            this.SerialNumber + "', '" +
            this.Color + "', '" +
            this.Description + "', '" +
            this.Location + "', '" +
            this.PhysicalCondition + "', '" +
            this.Notes + "', '" +
            this.Status + "', '" +
            this.DateStatusUpdated + "')";
            int returnCode = ModifyDatabase(insertQuery);
            if (returnCode != 0) {
                Console.WriteLine("Error in inserting Vehicle object into database");
            } else {
                Console.WriteLine("Vehicle object successfully inserted");
                string idQueryString = "SELECT MAX(ID) FROM Vehicle";
                List<Object> results = getValues(idQueryString);
                if (results != null) {
                    foreach (object result in results) {
                        IEnumerable<Object> row = result as IEnumerable<Object>;
                        foreach (object rowValue in row) {
                            this.ID = Convert.ToInt32(rowValue);
                        }
                    }
                }
            }
        }
        //------------------------------------------------------------------
        public void Update() {
            string updateQuery = "UPDATE Vehicle SET " +
                " BikeMake = '" + this.BikeMake + "' ," +
                " ModelNumber = '" + this.ModelNumber + "' ," +
                " SerialNumber = '" + this.SerialNumber + "' ," +
                " Color = '" + this.Color + "' ," +
                " Description = '" + this.Description + "' ," +
                " Location = '" + this.Location + "', " +
                " PhysicalCondition = '" + this.PhysicalCondition + "', " +
                " Notes = '" + this.Notes + "', " +
                " Status = '" + this.Status + "', " +
                " DateStatusUpdated = '" + this.DateStatusUpdated + "' " +
                " WHERE " +
                " ID = " + this.ID;
            int returnCode = ModifyDatabase(updateQuery);
            if (returnCode != 0)
                Console.WriteLine("Error in updating Vehicle object into database");
            else
                Console.WriteLine("Vehicle object successfully updated");
        }
        //------------------------------------------------------------------
        public void Delete() {
            string deleteQuery = "DELETE FROM Vehicle WHERE " +
                " ID = " + this.ID;
            Console.WriteLine(deleteQuery);
            int returnCode = ModifyDatabase(deleteQuery);
            if (returnCode != 0)
                Console.WriteLine("Error in deleting Vehicle object from database");
            else
                Console.WriteLine("Vehicle object successfully deleted");
        }

        public override string ToString() {
            return "ID:\t\t\t" + this.ID + "\nBike Make:\t\t" + this.BikeMake + "\nModel Number:\t\t" + this.ModelNumber + "\nSerial Number:\t\t" + this.SerialNumber + "\nColor:\t\t\t" + this.Color +
                "\nDescription:\t\t" + this.Description + "\nLocation:\t\t" + this.Location + "\nPhysical Condition:\t" + this.PhysicalCondition +
                "\nNotes:\t\t\t" + this.Notes + "\nStatus\t\t\t" + this.Status + "\nDate status updated:\t" + this.DateStatusUpdated;
        }

        public void Print() { Console.WriteLine("\n********\n" + this.ToString() + "\n********\n"); }
    }
}

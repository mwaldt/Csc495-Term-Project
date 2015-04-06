using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermProject {
    class User : Persistable{

        public string bannerID {set; get;}
        public string firstName{set; get;}
        public string lastName{set; get;}
        public string phoneNumber{set; get;}
        public string email{set; get;}
        public string userType{set; get;}
        public string dateOfRegistration{set; get;}
        public string notes{set; get;}
        public string status{set; get;}
        public string dateStatusUpdated{set; get;}
        
        public User() : base() {
            //This string will changed based on the computers file directory, 
            connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" +
                @"Data source = E:\Workspace\C#\Asgn06 Vehicle\Asgn06 Vehicle\Vehicle.accdb";   //changes based on where you are going to have your working directory
        }

        public void Populate(int id) {
            string queryString = "SELECT * FROM [User] WHERE (ID = " + id + ")";
            ArrayList results = getValues(queryString);

            if (results != null) {
                foreach (object result in results) {
                    IEnumerable row = result as IEnumerable;
                    int count = 0;
                    foreach (object rowValue in row) {
                        if (count == 0)
                            this.bannerID = Convert.ToString(rowValue);
                        else if (count == 1)
                            this.firstName = Convert.ToString(rowValue);
                        else if (count == 2)
                            this.lastName = Convert.ToString(rowValue);
                        else if (count == 3)
                            this.phoneNumber = Convert.ToString(rowValue);
                        else if (count == 4)
                            this.email = Convert.ToString(rowValue);
                        else if (count == 5)
                            this.userType = Convert.ToString(rowValue);
                        else if (count == 6)
                            this.dateOfRegistration = Convert.ToString(rowValue);
                        else if (count == 7)
                            this.notes = Convert.ToString(rowValue);
                        else if (count == 8)
                            this.status = Convert.ToString(rowValue);
                        else if (count == 9)
                            this.dateStatusUpdated = Convert.ToString(rowValue);
                        count++;
                    }
                }
            }
        }

        public void Insert() {
            string query = "INSERT INTO [User] (BannerID, FirstName, LastName, PhoneNumber, Email, UserType, DateOfRegistration, Notes, Status, DateStatusUpdated) VALUES ('" +
                this.bannerID + "', '" + this.firstName + "', '" + this.lastName + "', '" + this.phoneNumber + "', '" + this.email + "', '" + this.userType
                + "', '" + this.dateOfRegistration + "', '" + this.notes + "', '" + this.status + "', '" + this.dateStatusUpdated + "')";
            int returnCode = ModifyDatabase(query);
            if (returnCode != 0) {
                Console.WriteLine("Error in inserting User into Database");
            } else {
                Console.WriteLine("User object successfuly inserted");
                string idQueryString = "SELECT MAX(ID) FROM [User]";
                ArrayList results = getValues(idQueryString);
                if (results != null) {
                    foreach (object result in results) {
                        IEnumerable row = result as IEnumerable;
                        foreach (object rowValue in row) {
                            this.id = Convert.ToInt32(rowValue);
                            this.id++;
                        }
                    }
                }
            }
        }

        public void Update() {
            string updateQuery = "UPDATE [User] SET" +
                " FirstName " + this.firstName +
                " LastName " + this.lastName +
                " PhoneNumber " + this.phoneNumber +
                " Email " + this.email +
                " UserType " + this.userType +
                " DateOfRegistration " + this.dateOfRegistration +
                " Notes " + this.notes +
                " Status " + this.status +
                " DateStatusUpdated " + this.dateStatusUpdated +
                " WHERE ID = " + this.bannerID;
            int returnCode = ModifyDatabase(updateQuery);
            if (returnCode != 0)
                Console.WriteLine("Error in updating user object in database");
            else
                Console.WriteLine("User successfully updated");
        }

        public void Delete() {
            string updateQuery = "DELETE FROM [User] WHERE ID = " + this.id;
            int returnCode = ModifyDatabase(updateQuery);
            if (returnCode != 0)
                Console.WriteLine("Error in deleting User object in database");
            else
                Console.WriteLine("User successfully deleted");
        }

        public void PrintUser() {
            string printString = "************" +
                "\nBanner ID: " + this.bannerID +
                "\nFirst Name: " + this.firstName +
                "\nLast Name: " + this.lastName +
                "\nPhone Number: " + this.phoneNumber +
                "\nEmail Address: " + this.email +
                "\nUser Type: " + this.userType +
                "\nDate Of Registration: " + this.DateOfRegistration +
                "\nNotes: " + this.notes +
                "\nStatus: " + this.status + "\n";
            Console.WriteLine(printString);
        }
    }
}

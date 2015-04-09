using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermProject
{
    class User : Persistable
    {
        public int ID { get; set; }
        public string BannerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
        public string DateStatusUpdated { get; set; }


        public User() : base() { } // call parent default constructor

        //-----------------------------------------------------------------
        public User(string bannerId, string firstName, string lastName, string phoneNumber, string email,
            string userType, string notes)
        {
            this.BannerID = bannerId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PhoneNumber = phoneNumber;
            this.Email = email;
            this.UserType = userType;
            this.Notes = notes;
            this.Status = "Active";
            this.DateStatusUpdated = DateTime.Now.ToString("yyyy-MM-dd");
        }


        //------------------------------------------------------------------
        public void Populate(int ID)
        {
            string queryString = "SELECT * FROM [User] WHERE (ID = " + ID + ")";
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
                            this.BannerID = Convert.ToString(rowValue);
                        else if (count == 2)
                            FirstName = Convert.ToString(rowValue);
                        else if (count == 3)
                            LastName = Convert.ToString(rowValue);
                        else if (count == 4)
                            PhoneNumber = Convert.ToString(rowValue);
                        else if (count == 5)
                            Email = Convert.ToString(rowValue);
                        else if (count == 6)
                            UserType = Convert.ToString(rowValue);
                        else if (count == 7)
                            Notes = Convert.ToString(rowValue);
                        else if (count == 8)
                            Status = Convert.ToString(rowValue);
                        else if (count == 9)
                            DateStatusUpdated = Convert.ToString(rowValue);
                        count = count + 1;
                    }
                }
            }
        }

        //------------------------------------------------------------------
        public void Insert()
        {
            
            string insertQuery =
            "INSERT INTO [User] (BannerId, FirstName, LastName, PhoneNumber, EmailAddress, UserType, Notes, Status, DateStatusUpdated) " +
            "VALUES (" +
            "'" + this.BannerID + "', '" +
            this.FirstName + "', '" +
            this.LastName + "', '" +
            this.PhoneNumber + "', '" +
            this.Email + "', '" +
            this.UserType + "', '" +
            this.Notes + "', '" +
            this.Status + "', '" +
            this.DateStatusUpdated + "')";
            int returnCode = ModifyDatabase(insertQuery);
            if (returnCode != 0)
            {
                Console.WriteLine("Error in inserting User object into database");
            }
            else
            {
                Console.WriteLine("User object successfully inserted");
                string idQueryString = "SELECT MAX(ID) FROM [User]";
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
            string updateQuery = "UPDATE [User] SET " +
                " BannerId = '" + this.BannerID + "' ," +
                " FirstName = '" + this.FirstName + "' ," +
                " LastName = '" + this.LastName + "' ," +
                " PhoneNumber = '" + this.PhoneNumber + "' ," +
                " EmailAddress = '" + this.Email + "' ," +
                " UserType = '" + this.UserType + "', " +
                " Notes = '" + this.Notes + "', " +
                " Status = '" + this.Status + "', " +
                " DateStatusUpdated = '" + this.DateStatusUpdated + "' " +
                " WHERE " +
                " ID = " + this.ID;
            int returnCode = ModifyDatabase(updateQuery);
            if (returnCode != 0)
                Console.WriteLine("Error in updating User object into database");
            else
                Console.WriteLine("User object successfully updated");
        }

        //------------------------------------------------------------------
        public void Delete()
        {
            string deleteQuery = "DELETE FROM [User] WHERE " +
                " ID = " + this.ID;
            Console.WriteLine(deleteQuery);
            int returnCode = ModifyDatabase(deleteQuery);
            if (returnCode != 0)
                Console.WriteLine("Error in deleting User object from database");
            else
                Console.WriteLine("User object successfully deleted");
        }

        public override string ToString()
        {
            return "\nID:\t\t\t" + this.ID +
                "\nBanner Id:\t\t" + this.BannerID +
                "\nFirst name:\t\t" + this.FirstName +
                "\nLast Name:\t\t" + this.LastName +
                "\nPhone number:\t\t" + this.PhoneNumber +
                "\nEmail address:\t\t" + this.Email +
                "\nUser Type:\t\t" + this.UserType +
                "\nNotes:\t\t\t" + this.Notes +
                "\nStatus:\t\t\t" + this.Status +
                "\nDate status updated:\t" + this.DateStatusUpdated;
        }
    }
}


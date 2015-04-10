/* Persistable.cs
 * Max Waldt & Lisa Moore
 * Description: The super class for all persistable objects to be inserted into the database.
 * Contains all methods for interaction with the database.
*/

using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermProject {
    class Persistable {
        System.Data.OleDb.OleDbConnection conn;
        protected static string connectionString { get; set; }

        //Default Constructor
        public Persistable() {
            conn = new System.Data.OleDb.OleDbConnection();
            connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" +
                //@"Data source = C:\Users\Lisa\Documents" +                                             //Lisa laptop?
                @"Data source = E:\Workspace\C#\TermProject\TermProject" +                              //Max Desktop
                //@"Data source = C:\Users\Maximus\Documents\Visual Studio 2013\Projects\TermProject\TermProject" +            //Max laptop, will fill in later
                @"\BicycleRental.accdb";
        }

        //Sets up connection to the connection to the database
        public void configureConnection() {
            conn.ConnectionString = connectionString;
        }

        //Queries the database to retrieve results based on the parameter queryString
        //Returns the List<Object> of all results
        public List<Object> getValues(string queryString) {
            List<Object> results = new List<Object>();
            configureConnection();
            using (conn) {
                System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(queryString, conn);
                try {
                    conn.Open();
                    System.Data.OleDb.OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read()) {
                        Object[] nextRow = new Object[reader.FieldCount];
                        reader.GetValues(nextRow);
                        results.Add(nextRow);
                    }
                    return results;
                } catch (Exception e) {
                    Console.WriteLine(e.ToString());
                    return null;
                }

            }
        }

        //Method to test connection to database absed on the parameter query
        public int ModifyDatabase(string queryString) {
            configureConnection();
            using (conn) {
                System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(queryString);
                command.Connection = conn;

                try {
                    conn.Open();
                    command.ExecuteNonQuery();
                    return 0;
                } catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                    return 1;
                }
            }
        }

        //Returns the current date as a string, used when database updates.
        private string SetDateUpdated() { return DateTime.Now.ToString("yyyy-MM-dd"); }

    }


}

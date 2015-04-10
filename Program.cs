/* Program.cs
 * Max Waldt & Lisa Moore
 * Description: The main runtime and console based menu system of the program.
 * Contains methods for all the interactions with the database and use of the persistable objects.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace TermProject {
    class Program {

        //Data members
        private Worker myWorker = new Worker();
        private User myUser = new User();
        private Vehicle myVehicle = new Vehicle();
        private Rental myRental = new Rental();
        private VehicleCollection vehicles = new VehicleCollection();
        private RentalCollection rentals = new RentalCollection();
        private bool keepRunning = true;

        //Default Constructor
        public Program() {
            while (keepRunning == true) { RunProgram(); }
        }

        //Main method
        public static void Main(string[] args) {
            Program myProg = new Program();
        }

        //Controls the run of the program
        private void RunProgram() {
            ParseMainInput(MainMenuPrompt());
        }

        //Prints a break line, used to divide console output
        private void PrintBreakLine() { Console.WriteLine("--------------------"); }

        //Reinitializes all the data members for smoother runtime
        private void ReinitializeObjects() {
            myWorker = new Worker();
            myUser = new User();
            myVehicle = new Vehicle();
            myRental = new Rental();
            vehicles = new VehicleCollection();
            rentals = new RentalCollection();
        }

        //Prompts the Main menu of options, returns the users input #
        private int MainMenuPrompt() {
            ReinitializeObjects();
            PrintBreakLine();
            Console.WriteLine("To view worker information: Press 1 ");
            Console.WriteLine("To add a new worker: Press 2");
            Console.WriteLine("To update a worker: Press 3");
            Console.WriteLine("To delete a worker: Press 4");

            Console.WriteLine("\nTo view vehicle information: Press 5 ");
            Console.WriteLine("To add a new vehicle: Press 6");
            Console.WriteLine("To update a vehicle: Press 7");
            Console.WriteLine("To delete a vehicle: Press 8");

            Console.WriteLine("\nTo view user information: Press 9 ");
            Console.WriteLine("To add a new user: Press 10");
            Console.WriteLine("To update a user: Press 11");
            Console.WriteLine("To delete a user: Press 12");

            Console.WriteLine("\nTo view a rental: Press 13");
            Console.WriteLine("To create a rental: Press 14");
            Console.WriteLine("To update a rental: Press 15");
            Console.WriteLine("To delete a rental: Press 16");
            Console.WriteLine("To return a rental: Press 17");

            Console.WriteLine("\nTo view a list of good and available bikes: Press 18");
            Console.WriteLine("To view a list of all active rentals: Press 19");
            Console.WriteLine("To exit the program: Press 0");

            PrintBreakLine();
            int response = Convert.ToInt32(Console.ReadLine());
            return response;
        }

        //Reads user input and picks the correct transaction
        private void ParseMainInput(int inputInt) {
            switch (inputInt) {
                case 0:
                    ExitProgram();
                    break;
                case 1:             //Workers
                    ShowWorkerTransaction();
                    break;
                case 2:
                    CreateNewWorkerTrans();
                    break;
                case 3:
                    ModifyWorkerTrans();
                    break;
                case 4:
                    DeleteWorkerTrans();
                    break;
                case 5:             //Vehicles
                    ShowVehicleTransaction();
                    break;
                case 6:
                    CreateNewVehicleTrans();
                    break;
                case 7:
                    ModifyVehicleTrans();
                    break;
                case 8:
                    DeleteVehicleTrans();
                    break;
                case 9:             //Users
                    ShowUserTransaction();
                    break;
                case 10:
                    CreateNewUserTrans();
                    break;
                case 11:
                    ModifyUserTrans();
                    break;
                case 12:
                    DeleteUserTrans();
                    break;
                case 13:            //Rental
                    ShowRentalTransaction();
                    break;
                case 14:
                    CreateNewRentalTrans();
                    break;
                case 15:
                    ModifyRentalTrans();
                    break;
                case 16:
                    DeleteRentalTrans();
                    break;
                case 17:            //Return
                    ReturnRentalTrans();
                    break;
                case 18:            //Vehicle collection
                    vehicles.PopulateWithGoodAndAvailableBikes();
                    vehicles.PrintAll();
                    break;
                case 19:            //Rental collection
                    rentals.PopulateWithActiveRentals();
                    rentals.PrintAll();
                    break;
                    
            }
        }

        //Prompts for an id and displays the worker with that ID #
        private void ShowWorkerTransaction() {
            Console.WriteLine("\n");
            PrintBreakLine();
            Console.WriteLine("Get Worker Information");
            PrintBreakLine();
            GetAndShowWorker();
            Console.WriteLine("\n");
        }

        //Prompts for an id and displays the worker with that ID #
        private void GetAndShowWorker() {
            int workerId;
            Console.Write("Please enter the worker id: ");
            workerId = Convert.ToInt32(Console.ReadLine());
            //view specified worker from worker table
            try {
                myWorker.Populate(workerId);
                if (myWorker.ID == 0)
                    Console.WriteLine("Not a valid worker number.");
                else
                    Console.WriteLine(myWorker.ToString());
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        //Prompts for new worker's information then insersts the new user into the database
        private void CreateNewWorkerTrans() {
            Console.WriteLine("\n");
            PrintBreakLine();
            Console.WriteLine("Create New Worker");
            PrintBreakLine();
            ReadAssignWorkerInput();
            myWorker.Insert();
            PrintBreakLine();
            Console.WriteLine("\n");
        }

        //Prompts for and reads input for a worker's variables (used in creation and modification)
        //This would end up being a lot cleaner in a GUI situation
        private void ReadAssignWorkerInput() {
            Console.Write("Please enter the banner Id: ");
            myWorker.BannerID = Console.ReadLine();
            Console.Write("Please enter the first name: ");
            myWorker.FirstName = Console.ReadLine();
            Console.Write("Please enter the last name: ");
            myWorker.LastName = Console.ReadLine();
            Console.Write("Please enter the phone number: ");
            myWorker.PhoneNumber = Console.ReadLine();
            Console.Write("Please enter the email address: ");
            myWorker.Email = Console.ReadLine();
            Console.Write("Please enter the credential: ");
            myWorker.Credential = Console.ReadLine();
            Console.Write("Please enter the worker password: ");
            myWorker.WorkerPassword = Console.ReadLine();
            Console.Write("Please enter some notes: ");
            myWorker.Notes = Console.ReadLine();
            Console.Write("Please enter the status: ");
            myWorker.Status = Console.ReadLine();
            myWorker.DateStatusUpdated = (DateTime.Now.ToString("yyyy-MM-dd"));
        }

        //Modify an existing worker, Prompts for a worker's ID then provides his inpormation and a prompt to modify the worker
        private void ModifyWorkerTrans() {
            Console.WriteLine("\n");
            PrintBreakLine();
            Console.WriteLine("Modify an existing Worker Information");
            PrintBreakLine();
            GetAndShowWorker();
            Console.WriteLine("Is this the worker to modify? Enter 'y' to confirm modify, any other input will cancel modify");
            string responce = Console.ReadLine();
            if (responce == "y") { ReadAssignWorkerInput(); myWorker.Update(); }
            else { Console.WriteLine("Worker not modified"); }
            PrintBreakLine();
            Console.WriteLine("\n");
        }

        //Delete a worker, Prompts for the workers ID, displays the worker than prompts to confirm that this is the correct decision. Will delete after prompt
        private void DeleteWorkerTrans() {
            Console.WriteLine("\n");
            PrintBreakLine();
            Console.WriteLine("Delete a worker,");
            PrintBreakLine();
            GetAndShowWorker();
            Console.WriteLine("Is this the worker to delete? Enter 'y' to confirm delete, any other input will cancel deletion");
            string responce = Console.ReadLine();
            if (responce == "y") { myWorker.Delete(); }
            else { Console.WriteLine("Worker not deleted"); }
            PrintBreakLine();
            Console.WriteLine("\n");
        }


        //*****************************************************************************************

        //Prompts for an id and displays the user with that ID #
        private void ShowUserTransaction() {
            Console.WriteLine("\n");
            PrintBreakLine();
            Console.WriteLine("Get User Information");
            PrintBreakLine();
            GetAndShowUser();
            Console.WriteLine("\n");
        }

        //Prompts for an id and displays the user with that ID #
        private void GetAndShowUser() {
            int userId;
            Console.Write("Please enter the user id: ");
            userId = Convert.ToInt32(Console.ReadLine());
            //view specified user from user table
            try {
                myUser.Populate(userId);
                if (myUser.ID == 0)
                    Console.WriteLine("Not a valid user number.");
                else
                    Console.WriteLine(myUser.ToString());
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            
        }

        //Prompts for new user's information then insersts the new user into the database
        private void CreateNewUserTrans() {
            Console.WriteLine("\n");
            PrintBreakLine();
            ReadAssignUserInput();
            myUser.Insert();
            PrintBreakLine();
            Console.WriteLine("\n");
        }

        //Prompts for and reads input for a user's variables (used in creation and modification)
        //This would end up being a lot cleaner in a GUI situation
        private void ReadAssignUserInput() {
            Console.Write("Please enter the banner Id: ");
            myUser.BannerID = Console.ReadLine();
            Console.Write("Please enter the first name: ");
            myUser.FirstName = Console.ReadLine();
            Console.Write("Please enter the last name: ");
            myUser.LastName = Console.ReadLine();
            Console.Write("Please enter the phone number: ");
            myUser.PhoneNumber = Console.ReadLine();
            Console.Write("Please enter the email address: ");
            myUser.Email = Console.ReadLine();
            Console.Write("Please enter the user type: ");
            myUser.UserType = Console.ReadLine();
            Console.Write("Please enter some notes: ");
            myUser.Notes = Console.ReadLine();
            Console.Write("Please enter the status: ");
            myUser.Status = Console.ReadLine();
            myUser.DateStatusUpdated = (DateTime.Now.ToString("yyyy-MM-dd"));
        }

        //Modify an existing user, Prompts for a user's ID then provides his inpormation and a prompt to modify the user
        private void ModifyUserTrans() {
            Console.WriteLine("\n");
            PrintBreakLine();
            GetAndShowUser();
            Console.WriteLine("Is this the user to modify? Enter 'y' to confirm modify, any other input will cancel modify.");
            string responce = Console.ReadLine();
            if (responce == "y") { ReadAssignUserInput(); myUser.Update(); } else { Console.WriteLine("User not modified"); }
            PrintBreakLine();
            Console.WriteLine("\n");
        }

        //Delete a user, Prompts for the user's ID, displays the user than prompts to confirm that this is the correct decision.
        //Will delete after prompt
        private void DeleteUserTrans() {
            Console.WriteLine("\n");
            PrintBreakLine();
            Console.WriteLine("Delete a User,");
            PrintBreakLine();
            GetAndShowUser();
            Console.WriteLine("Is this the user to delete? Enter 'y' to confirm delete, any other input will cancel deletion");
            string responce = Console.ReadLine();
            if (responce == "y") { myUser.Delete(); } else { Console.WriteLine("User not deleted"); }
            PrintBreakLine();
            Console.WriteLine("\n");
        }


        //*****************************************************************************************

        //Prompts for an id and displays the vehicle with that ID #
        private void ShowVehicleTransaction() {
            Console.WriteLine("\n");
            PrintBreakLine();
            Console.WriteLine("Get Vehicle Information");
            PrintBreakLine();
            GetAndShowVehicle();
            PrintBreakLine();
            Console.WriteLine("\n");
        }

        //Prompts for an id and displays the vehicle with that ID #
        private void GetAndShowVehicle() {
            int vehicleId;
            Console.Write("Please enter the vehicle id: ");
            vehicleId = Convert.ToInt32(Console.ReadLine());
            //view specified worker from worker table
            try {
                myVehicle.Populate(vehicleId);
                if (myVehicle.ID == 0)
                    Console.WriteLine("Not a valid vehicle number.");
                else
                    Console.WriteLine(myVehicle.ToString());
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        //Prompts for new worker's information then insersts the new user into the database
        private void CreateNewVehicleTrans() {
            Console.WriteLine("\n");
            PrintBreakLine();
            Console.WriteLine("Create new Vehicle");
            PrintBreakLine();
            ReadAssignVehicleInput();
            myVehicle.Insert();
            PrintBreakLine();
            Console.WriteLine("\n");
        }

        //Prompts for and reads input for a worker's variables (used in creation and modification)
        //This would end up being a lot cleaner in a GUI situation
        private void ReadAssignVehicleInput() {
            Console.Write("Please enter the vehicle make: ");
            myVehicle.BikeMake = Console.ReadLine();
            Console.Write("Please enter the model number: ");
            myVehicle.ModelNumber = Console.ReadLine();
            Console.Write("Please enter serial number: ");
            myVehicle.SerialNumber = Console.ReadLine();
            Console.Write("Please enter the color: ");
            myVehicle.Color = Console.ReadLine();
            Console.Write("Please enter the description: ");
            myVehicle.Description = Console.ReadLine();
            Console.Write("Please enter the location: ");
            myVehicle.Location = Console.ReadLine();
            Console.Write("Please enter the physical condition: ");
            myVehicle.PhysicalCondition = Console.ReadLine();
            Console.Write("Please enter some notes: ");
            myVehicle.Notes = Console.ReadLine();
            Console.Write("Please enter the status: ");
            myVehicle.Status = Console.ReadLine();
            myVehicle.DateStatusUpdated = (DateTime.Now.ToString("yyyy-MM-dd"));
        }

        //Modify an existing worker, Prompts for a worker's ID then provides his inpormation and a prompt to modify the worker
        private void ModifyVehicleTrans() {
            Console.WriteLine("\n");
            PrintBreakLine();
            Console.WriteLine("Modify an existing Vehicle");
            PrintBreakLine();
            GetAndShowVehicle();
            Console.WriteLine("Is this the vehicle to modify? Enter 'y' to confirm delete, any other input will cancel modify");
            string responce = Console.ReadLine();
            if (responce == "y") { ReadAssignVehicleInput(); myVehicle.Update(); } else { Console.WriteLine("Vehicle not modified"); }
            PrintBreakLine();
            Console.WriteLine("\n");
        }

        //Delete a worker, Prompts for the workers ID, displays the worker than prompts to confirm that this is the correct decision. Will delete after prompt
        private void DeleteVehicleTrans() {
            Console.WriteLine("\n");
            PrintBreakLine();
            Console.WriteLine("Delete a vehicle,");
            PrintBreakLine();
            GetAndShowVehicle();
            Console.WriteLine("Is this the vehicle to delete? Enter 'y' to confirm delete, any other input will cancel deletion");
            string responce = Console.ReadLine();
            if (responce == "y") { myVehicle.Delete(); } else { Console.WriteLine("Vehicle not deleted"); }
            PrintBreakLine();
            Console.WriteLine("\n");
        }

        //*****************************************************************************************

        //Prompts for an id and displays the Rental with that ID #
        private void ShowRentalTransaction() {
            Console.WriteLine("\n");
            PrintBreakLine();
            Console.WriteLine("Get Rental Information");
            PrintBreakLine();
            GetAndShowRental();
            Console.WriteLine("\n");
        }

        //Prompts for an id and displays the Rental with that ID #
        private void GetAndShowRental() {
            int rentalId;
            Console.Write("Please enter the Rental id: ");
            rentalId = Convert.ToInt32(Console.ReadLine());
            //view specified rental from rental table
            try {
                myRental.Populate(rentalId);
                if (myRental.ID == 0)
                    Console.WriteLine("Not a valid Rental number.");
                else
                    Console.WriteLine(myRental.ToString());
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        //Prompts for new Rental's information then insersts the new user into the database
        private void CreateNewRentalTrans() {
            Console.WriteLine("\n");
            PrintBreakLine();
            Console.WriteLine("Create New Rental");
            PrintBreakLine();
            ReadAssignRentalCreateInput();
            myRental.Insert();
            PrintBreakLine();
            Console.WriteLine("\n");
        }

        //Prompts for and reads input for a Rental's variables (used in creation)
        //This would end up being a lot cleaner in a GUI situation
        private void ReadAssignRentalCreateInput() {
            Console.Write("Please enter the Vehicle ID: ");
            myRental.VehicleID = Console.ReadLine();
            Console.Write("Please enter the Renter ID: ");
            myRental.RenterID = Console.ReadLine();
            myRental.DateRented = (DateTime.Now.ToString("yyyy-MM-dd"));
            myRental.TimeRented = (DateTime.Now.ToString("HH:mm tt"));
            myRental.DateDue = (DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
            myRental.TimeDue = "12:00 AM";
            myRental.DateReturned = "";
            myRental.TimeReturned = "";
            Console.Write("Please enter the checkout worker ID: ");
            myRental.CheckoutWorkerID = Console.ReadLine();
            myRental.CheckinWorkerID = "0";
        }

        //Prompts for and reads input for a Rental's variables (used in modification)
        //This would end up being a lot cleaner in a GUI situation
        private void ReadAssignRentalModifyInput() {
            Console.Write("Please enter the Vehicle ID: ");
            myRental.VehicleID = Console.ReadLine();
            Console.Write("Please enter the Renter ID: ");
            myRental.RenterID = Console.ReadLine();
            myRental.DateReturned = "";
            myRental.TimeReturned = "";
            Console.Write("Please enter the checkout worker ID: ");
            myRental.CheckoutWorkerID = Console.ReadLine();
            myRental.CheckinWorkerID = "0";
        }

        //Modify an existing worker, Prompts for a worker's ID then provides his inpormation and a prompt to modify the worker
        private void ModifyRentalTrans() {
            Console.WriteLine("\n");
            PrintBreakLine();
            Console.WriteLine("Modify an existing Rental Information");
            PrintBreakLine();
            GetAndShowRental();
            Console.WriteLine("Is this the Rental to modify? Enter 'y' to confirm modify, any other input will cancel modify");
            string responce = Console.ReadLine();
            if (responce == "y") { ReadAssignRentalModifyInput(); myRental.Update(); } else { Console.WriteLine("Rental not modified"); }
            PrintBreakLine();
            Console.WriteLine("\n");
        }

        //Delete a worker, Prompts for the workers ID, displays the worker than prompts to confirm that this is the correct decision. Will delete after prompt
        private void DeleteRentalTrans() {
            Console.WriteLine("\n");
            PrintBreakLine();
            Console.WriteLine("Delete a Rental");
            PrintBreakLine();
            GetAndShowRental();
            Console.WriteLine("Is this the Rental to delete? Enter 'y' to confirm delete, any other input will cancel deletion");
            string responce = Console.ReadLine();
            if (responce == "y") { myRental.Delete(); } else { Console.WriteLine("Rental not deleted"); }
            PrintBreakLine();
            Console.WriteLine("\n");
        }

        //Return a rental
        private void ReturnRentalTrans() {
            Console.WriteLine("\n");
            PrintBreakLine();
            Console.WriteLine("Return a Rental");
            PrintBreakLine();
            GetAndShowRental();
            Console.WriteLine("Is this the Rental to return? Enter 'y' to confirm, any other input will cancel");
            string responce = Console.ReadLine();
            if (responce == "y") { ReturnRental(); } else { Console.WriteLine("Rental not returned"); }
            PrintBreakLine();
            Console.WriteLine("\n");
        }

        //Handles the setters for returning a rental
        private void ReturnRental() {
            Console.Write("Please enter the return Worker ID: ");
            myRental.CheckinWorkerID = Console.ReadLine();
            myRental.DateReturned = (DateTime.Now.ToString("yyyy-MM-dd"));
            myRental.TimeReturned = (DateTime.Now.ToString("HH:mm tt"));
            myRental.Update();
        }

        //Method to gracefully terminate the program
        private void ExitProgram() {
            Console.WriteLine("Program terminating, press any key to exit...");
            Console.ReadKey();
            keepRunning = false;
            
        }
    }
}






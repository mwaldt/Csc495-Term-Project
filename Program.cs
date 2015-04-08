using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace TermProject {
    class Program {

        private Worker myWorker = new Worker();
        private User myUser = new User();
        private Vehicle myVehicle = new Vehicle();

        private VehicleCollection vehicles = new VehicleCollection();
        private RentalCollection rentals = new RentalCollection();

        private bool keepRunning = true;

        public Program() {
            while (keepRunning == true) { RunProgram(); }
        }

        public static void Main(string[] args) {
            Program myProg = new Program();
        }

        private void RunProgram() {
            ParseMainInput(MainMenuPrompt());
        }

        //Prompts the Main menu of options, returns the users input #
        private int MainMenuPrompt() {
            Console.WriteLine("\nTo view worker information: Press 1 ");
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

            Console.WriteLine("To view a list of good and available bikes: Press 13");
            Console.WriteLine("To view a list of all active rentals: Press 14");
            int response = Convert.ToInt32(Console.ReadLine());
            return response;
        }

        private void ParseMainInput(int inputInt) {
            switch (inputInt) {
                case 1:
                    GetAndShowWorker();
                    break;
                case 2:
                    CreateNewWorkerTrans();
                    break;
                case 3:
                    ModifyWorkerTrans();
                    break;
                case 4:
                    ModifyWorkerTrans();
                    break;
                case 5:
                    GetAndShowUser();
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
                case 9:
                    GetAndShowVehicle();
                    break;
                case 10:
                    CreateNewUserTrans();
                    break;
                case 11:
                    ModifyUserTrans();
                    break;
                case 12:
                    ModifyUserTrans();
                    break;

                case 13:
                    vehicles.PopulateWithGoodAndAvailableBikes();
                    vehicles.PrintAll();
                    break;
                case 14:
                    rentals.PopulateWithGoodAndAvailableBikes();
                    rentals.PrintAll();
                    break;
                    
            }
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
            ReadAssignWorkerInput();
            myWorker.Insert();
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
            GetAndShowWorker();
            Console.WriteLine("Is this the worker to delete? Enter 'y' to confirm delete, any other input will cancel deletion");
            string responce = Console.ReadLine();
            if (responce == "y") { ReadAssignWorkerInput(); myWorker.Update(); }
            else { Console.WriteLine("Worker not deleted"); }
        }

        //Delete a worker, Prompts for the workers ID, displays the worker than prompts to confirm that this is the correct decision. Will delete after prompt
        private void DeleteWorkerTrans() {
            Console.WriteLine("Delete a worker,");
            GetAndShowWorker();
            Console.WriteLine("Is this the worker to delete? Enter 'y' to confirm delete, any other input will cancel deletion");
            string responce = Console.ReadLine();
            if (responce == "y") { myWorker.Delete(); }
            else { Console.WriteLine("Worker not deleted"); }
        }


        //*****************************************************************************************

        //Prompts for an id and displays the user with that ID #
        private void GetAndShowUser() {
            int userId;
            Console.Write("Please enter the user id: ");
            userId = Convert.ToInt32(Console.ReadLine());
            //view specified worker from worker table
            try {
                myWorker.Populate(userId);
                if (myWorker.ID == 0)
                    Console.WriteLine("Not a valid user number.");
                else
                    Console.WriteLine(myWorker.ToString());
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        //Prompts for new worker's information then insersts the new user into the database
        private void CreateNewUserTrans() {
            ReadAssignUserInput();
            myWorker.Insert();
        }

        //Prompts for and reads input for a worker's variables (used in creation and modification)
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
            Console.Write("Please enter the credential: ");
            myUser.UserType = Console.ReadLine();
            Console.Write("Please enter some notes: ");
            myUser.Notes = Console.ReadLine();
            Console.Write("Please enter the status: ");
            myUser.Status = Console.ReadLine();
            myUser.DateStatusUpdated = (DateTime.Now.ToString("yyyy-MM-dd"));
        }

        //Modify an existing worker, Prompts for a worker's ID then provides his inpormation and a prompt to modify the worker
        private void ModifyUserTrans() {
            GetAndShowUser();
            Console.WriteLine("Is this the worker to delete? Enter 'y' to confirm delete, any other input will cancel deletion");
            string responce = Console.ReadLine();
            if (responce == "y") { ReadAssignUserInput(); myWorker.Update(); } else { Console.WriteLine("Worker not deleted"); }
        }

        //Delete a worker, Prompts for the workers ID, displays the worker than prompts to confirm that this is the correct decision. Will delete after prompt
        private void DeleteUserTrans() {
            Console.WriteLine("Delete a worker,");
            GetAndShowUser();
            Console.WriteLine("Is this the worker to delete? Enter 'y' to confirm delete, any other input will cancel deletion");
            string responce = Console.ReadLine();
            if (responce == "y") { myWorker.Delete(); } else { Console.WriteLine("Worker not deleted"); }
        }


        //*****************************************************************************************

        //Prompts for an id and displays the user with that ID #
        private void GetAndShowVehicle() {
            int vehicleId;
            Console.Write("Please enter the vehicle id: ");
            vehicleId = Convert.ToInt32(Console.ReadLine());
            //view specified worker from worker table
            try {
                myWorker.Populate(vehicleId);
                if (myWorker.ID == 0)
                    Console.WriteLine("Not a valid vehicle number.");
                else
                    Console.WriteLine(myWorker.ToString());
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        //Prompts for new worker's information then insersts the new user into the database
        private void CreateNewVehicleTrans() {
            ReadAssignVehicleInput();
            myWorker.Insert();
        }

        //Prompts for and reads input for a worker's variables (used in creation and modification)
        //This would end up being a lot cleaner in a GUI situation
        private void ReadAssignVehicleInput() {
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
        private void ModifyVehicleTrans() {
            GetAndShowVehicle();
            Console.WriteLine("Is this the worker to delete? Enter 'y' to confirm delete, any other input will cancel deletion");
            string responce = Console.ReadLine();
            if (responce == "y") { ReadAssignUserInput(); myWorker.Update(); } else { Console.WriteLine("Worker not deleted"); }
        }

        //Delete a worker, Prompts for the workers ID, displays the worker than prompts to confirm that this is the correct decision. Will delete after prompt
        private void DeleteVehicleTrans() {
            Console.WriteLine("Delete a worker,");
            GetAndShowVehicle();
            Console.WriteLine("Is this the worker to delete? Enter 'y' to confirm delete, any other input will cancel deletion");
            string responce = Console.ReadLine();
            if (responce == "y") { myWorker.Delete(); } else { Console.WriteLine("Worker not deleted"); }
        }



    }
}






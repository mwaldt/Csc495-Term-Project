using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace TermProject
{
    class Program
    {
        // worker variables
        private static string workerBannerId;
        private static string workerFirstName;
        private static string workerLastName;
        private static string workerPhoneNumber;
        private static string workerEmail;
        private static string workerCredential;
        private static string workerPassword;
        private static string workerNotes;
        private static string workerStatus;
        private static string workerDateStatusUpdated;

        // vehicle variables
        private static string vehicleBikeMake;
        private static string vehicleModelNumber;
        private static string vehicleSerialNumber;
        private static string vehicleColor;
        private static string vehicleDescription;
        private static string vehicleLocation;
        private static string vehicleCondition;
        private static string vehicleNotes;
        private static string vehicleStatus;
        private static string vehicleDateStatusUpdated;


        static void Main(string[] args)
        {
            int vehicleId;
            int workerId;
            int response;
            String answer = "y";
            while (answer.Equals("y", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("\nTo view worker information: Press 1 ");
                Console.WriteLine("To add a new worker: Press 2");
                Console.WriteLine("To update a worker: Press 3");
                Console.WriteLine("To delete a worker: Press 4");
                Console.WriteLine("\nTo view vehicle information: Press 5 ");
                Console.WriteLine("To add a new vehicle: Press 6");
                Console.WriteLine("To update a vehicle: Press 7");
                Console.WriteLine("To delete a vehicle: Press 8");
                Console.WriteLine("To view a list of good and available bikes: Press 9");
                response = Convert.ToInt32(Console.ReadLine());
                switch (response)
                {
                    case 1:
                        workerId = GetAndShowWorker();
                        break;
                    case 2:
                        GetWorkerInput();
                        workerDateStatusUpdated = (DateTime.Now.ToString("yyyy-MM-dd"));
                        InsertWorker(workerBannerId, workerFirstName, workerLastName, workerPhoneNumber, workerEmail, workerCredential, workerPassword, workerNotes, workerStatus, workerDateStatusUpdated);
                        break;
                    case 3:
                        workerId = GetAndShowWorker();
                        Console.WriteLine("worker id ", workerId);
                        if (workerId != 0)
                        {
                            Console.Write("Is this the worker to be updated? (y/n): ");
                            answer = Console.ReadLine();
                            if (answer.Equals("y", StringComparison.OrdinalIgnoreCase))
                            {
                                GetWorkerInput();
                                UpdateWorker(workerId);
                            }
                        }
                        break;
                    case 4:
                        workerId = GetAndShowWorker();
                        if (workerId != 0)
                        {
                            Console.Write("Is this the worker to be deleted? (y/n): ");
                            answer = Console.ReadLine();
                            if (answer.Equals("y", StringComparison.OrdinalIgnoreCase))
                            {
                                DeleteWorker(workerId);
                            }
                        }
                        break;
                    case 5:
                        vehicleId = GetAndShowVehicle();
                        break;
                    case 6:
                        GetVehicleInput();
                        vehicleDateStatusUpdated = (DateTime.Now.ToString("yyyy-MM-dd"));
                        InsertVehicle(vehicleBikeMake, vehicleModelNumber, vehicleSerialNumber, vehicleColor, vehicleDescription, vehicleLocation, vehicleCondition, vehicleNotes, workerStatus, workerDateStatusUpdated);
                        break;
                    case 7:
                        vehicleId = GetAndShowVehicle();
                        Console.WriteLine("vehicle id ", vehicleId);
                        if (vehicleId != 0)
                        {
                            Console.Write("Is this the vehicle to be updated? (y/n): ");
                            answer = Console.ReadLine();
                            if (answer.Equals("y", StringComparison.OrdinalIgnoreCase))
                            {
                                GetVehicleInput();
                                UpdateVehicle(vehicleId);
                            }
                        }
                        break;
                    case 8:
                        vehicleId = GetAndShowVehicle();
                        if (vehicleId != 0)
                        {
                            Console.Write("Is this the vehicle to be deleted? (y/n): ");
                            answer = Console.ReadLine();
                            if (answer.Equals("y", StringComparison.OrdinalIgnoreCase))
                            {
                                DeleteWorker(vehicleId);
                            }
                        }
                        break;
                    case 9:
                        VehicleCollection vehicleCollection = new VehicleCollection();
                        vehicleCollection.PopulateWithGoodAndAvailableBikes();
                        PrintVehicleCollection(vehicleCollection);
                        break;

                }

                Console.Write("Do you want to continue? (y/n): ");
                answer = Console.In.ReadLine();
            }
        }// end of main

        //-----------------------------------------------------------------------------------------------------------
        public static void GetWorkerInput()
        {
            Console.Write("Please enter the banner Id: ");
            workerBannerId = Console.ReadLine();
            Console.Write("Please enter the first name: ");
            workerFirstName = Console.ReadLine();
            Console.Write("Please enter the last name: ");
            workerLastName = Console.ReadLine();
            Console.Write("Please enter the phone number: ");
            workerPhoneNumber = Console.ReadLine();
            Console.Write("Please enter the email address: ");
            workerEmail = Console.ReadLine();
            Console.Write("Please enter the credential: ");
            workerCredential = Console.ReadLine();
            Console.Write("Please enter the worker password: ");
            workerPassword = Console.ReadLine();
            Console.Write("Please enter some notes: ");
            workerNotes = Console.ReadLine();
            Console.Write("Please enter the status: ");
            workerStatus = Console.ReadLine();
        }

        //-------------------------------------------------------------------------------------------------------------
        public static void DeleteWorker(int workerId)
        {
            Worker worker = new Worker();
            try
            {
                worker.ID = workerId;
                worker.delete();
                Console.WriteLine("Delete successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------
        public static int GetAndShowWorker()
        {
            int workerId;
            Console.Write("Please enter the worker id: ");
            workerId = Convert.ToInt32(Console.ReadLine());
            //view specified worker from worker table
            Worker worker = new Worker();
            try
            {
                worker.populate(workerId);
                if (worker.ID == 0)
                    Console.WriteLine("Not a valid worker number.");
                else
                    Console.WriteLine(worker.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return worker.ID;
        }
        //----------------------------------------------------------------------------------------------------------------------------------------
        public static void UpdateWorker(int workerId)
        {
            workerDateStatusUpdated = DateTime.Now.ToString("yyyy-MM-dd");
            Worker worker = new Worker(workerBannerId, workerFirstName, workerLastName, workerPhoneNumber, workerEmail, workerCredential, workerPassword, workerNotes, workerStatus, workerDateStatusUpdated);
            try
            {
                worker.ID = workerId;
                worker.update();
                Console.WriteLine("Update successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        //----------------------------------------------------------------------------------------------------------------------------------------
        public static void InsertWorker(string bannerId, string firstName, string lastName, string phone, string email, string credential, string password, string notes, string status, string dateUpdated)
        {
            Worker worker = new Worker(bannerId, firstName, lastName, phone, email, credential, password, notes, status, dateUpdated);
            try
            {
                worker.insert();
                Console.WriteLine("Insert successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        // Start of vehicle methods
        //-----------------------------------------------------------------------------------------------------------------------------------------
        public static void GetVehicleInput()
        {
            Console.Write("Please enter the bike make: ");
            vehicleBikeMake = Console.ReadLine();
            Console.Write("Please enter the model number: ");
            vehicleModelNumber = Console.ReadLine();
            Console.Write("Please enter the serial number: ");
            vehicleSerialNumber = Console.ReadLine();
            Console.Write("Please enter the color: ");
            vehicleColor = Console.ReadLine();
            Console.Write("Please enter the description: ");
            vehicleDescription = Console.ReadLine();
            Console.Write("Please enter the location: ");
            vehicleLocation = Console.ReadLine();
            Console.Write("Please enter the condition: ");
            vehicleCondition = Console.ReadLine();
            Console.Write("Please enter some notes: ");
            vehicleNotes = Console.ReadLine();
            Console.Write("Please enter the status: ");
            vehicleStatus = Console.ReadLine();
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------
        public static int GetAndShowVehicle()
        {
            int vehicleId;
            Console.Write("Please enter the vehicle id: ");
            vehicleId = Convert.ToInt32(Console.ReadLine());
            //view specified vehicle from worker table
            Vehicle vehicle = new Vehicle();
            try
            {
                vehicle.populate(vehicleId);
                if (vehicle.ID == 0)
                    Console.WriteLine("Not a valid vehicle number.");
                else
                    Console.WriteLine(vehicle.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return vehicle.ID;
        }
        //----------------------------------------------------------------------------------------------------------------------------------------
        public static void UpdateVehicle(int vehicleId)
        {
            vehicleDateStatusUpdated = DateTime.Now.ToString("yyyy-MM-dd");
            Vehicle vehicle = new Vehicle(vehicleBikeMake, vehicleModelNumber, vehicleSerialNumber, vehicleColor, vehicleDescription, vehicleLocation, vehicleCondition, vehicleNotes, vehicleStatus, vehicleDateStatusUpdated);
            try
            {
                vehicle.ID = vehicleId;
                vehicle.update();
                Console.WriteLine("Vehicle update successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        //----------------------------------------------------------------------------------------------------------------------------------------
        public static void InsertVehicle(string bikeMake, string modelNumber, string serialNumber, string color, string description, string location, string condition, string notes, string status, string dateUpdated)
        {
            Vehicle vehicle = new Vehicle(bikeMake, modelNumber, serialNumber, color, description, location, condition, notes, status, dateUpdated);
            try
            {
                vehicle.insert();
                Console.WriteLine("Vehicle insert successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public static void PrintVehicleCollection(VehicleCollection vehicleCollection)
        {


            foreach (Vehicle v in vehicleCollection.GetBikeList())
            {
                Console.Out.WriteLine(v.ToString());
                Console.WriteLine();
            }
        }
    }
}


    



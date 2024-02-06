using Hotell567.Data;
using Hotell567.Models;
using System.Diagnostics;

namespace Hotell567.Logic
{
    public static class AppManager
    {
        
        public static bool isInitialized = false;
        public static bool isDebugging = true;

        
        public static string dbFile = "hoteldatabase.db";
        public static string connectionString;
        public static string imageFolder;

        
        public static string solutionFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\..\\..\\Resources\\"));
        public static string dbFilePath = Path.Combine(solutionFolder, dbFile);
        public static string imageFolderSolution = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\..\\..\\Resources\\Images\\Rooms\\"));


        public static string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public static string appDataFolder = Path.Combine(appDirectory, "HotelApplicationData");
        public static string appDataImageFolder = Path.Combine(appDataFolder, "Images");

        
        public static UserDatabase userDatabase;
        public static UserFactory userFactory;

        public static RoomDatabase roomDatabase;
        public static RoomFactory roomFactory;
        public static RoomFiltering roomFiltering;


        public static ReservationDatabase reservationDatabase;
        public static ReservationFactory reservationFactory;

        
        public static User currentUser;

        
        public static DateTime selectedCheckInDate;
        public static DateTime selectedCheckOutDate;

        static AppManager()
        {
            Debug.WriteLine("AppManager created");

            if (isDebugging)
            {
                if (File.Exists(dbFilePath))
                {
                    Debug.WriteLine("Found database at: " + dbFilePath);
                    connectionString = $"Data Source={dbFilePath}";
                    imageFolder = imageFolderSolution;
                }
                else
                {
                    Debug.WriteLine("Failed to find database!");
                }
            }
            else
            {
                if (!Directory.Exists(appDataFolder))
                {
                    try
                    {
                        Debug.WriteLine($"Creating a folder in {appDataFolder}");
                        Directory.CreateDirectory(appDataFolder);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine($"Couldn't create a folder in {appDataFolder}, because: {e}");
                        throw;
                    }
                }

                string filePath = Path.Combine(appDataFolder, dbFile);

                if (File.Exists(filePath))
                {
                    Debug.WriteLine("Found database at: " + filePath);
                    connectionString = $"Data Source={filePath}";
                }
                else
                {
                    Debug.WriteLine("Failed to find database!");
                }

                if (!Directory.Exists(appDataImageFolder))
                {
                    try
                    {
                        Debug.WriteLine($"Creating a folder in {appDataImageFolder}");
                        Directory.CreateDirectory(appDataImageFolder);
                        imageFolder = appDataImageFolder;
                        Debug.WriteLine("Found image folder at: " + imageFolder);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine($"Couldn't create a folder in {appDataImageFolder}, because: {e}");
                        throw;
                    }
                }
                else
                {
                    imageFolder = appDataImageFolder;
                    Debug.WriteLine("Found image folder at: " + imageFolder);
                }
            }


            
            userDatabase = new UserDatabase();
            userFactory = new UserFactory();

            
            roomDatabase = new RoomDatabase();
            roomFactory = new RoomFactory();
            roomFiltering = new RoomFiltering();

            
            reservationDatabase = new ReservationDatabase();
            reservationFactory = new ReservationFactory();
        }

        public static void InitializePermissions(int permissionLevel)
        {
            if (permissionLevel == 0)
            {
                
                Debug.WriteLine("Default Permissions!");
                AppShell.GetSingleton.ShowPagesAfterLogin(permissionLevel);
            }
            else if (permissionLevel == 1)
            {
                
                Debug.WriteLine("Admin Permissions!");
                AppShell.GetSingleton.ShowPagesAfterLogin(permissionLevel);
            }
        }

        public static void InitializeUserData(User u)
        {
            currentUser = u;
        }

        public static void LogUserOut()
        {
            currentUser = null;
            AppShell.GetSingleton.HidePagesAfterLogout();
        }
    }
}

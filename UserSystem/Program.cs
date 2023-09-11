using System;

namespace UserSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            string command = "";
            string[,] userTable = new string[4, 2];

            // main loop 
            do
            {
                Console.ResetColor();
                string[] commandArgs = Console.ReadLine().Split(" ");

                // validate arguments
                bool error = ErrorCheck(commandArgs.Length < 3, "Too few parameters.");

                if (error)
                    continue;

                string username = commandArgs[1];
                string password = commandArgs[2];

                // Validate username
                error = ErrorCheck(username.Length < 3, "Username must be at least 3 characters long");

                if (error)
                    continue;

                // Validate password
                error = ErrorCheck(password.Length < 3, "Password must be at least 3 characters long");

                if (error)
                    continue;

                switch (commandArgs[0])
                {
                    case "register":
                        Register(username, password, userTable);
                        break;
                    case "delete":
                        Delete(username, password, userTable);
                        break;
                }
            }
            while (command != "end");
        }

        static bool Register(string username, string password, string[,] userTable)
        {
            // check if username exists
            int num;
            bool usernameExists = true;

            num = Iteration(userTable, "check if exist", username, password);

            if (num == -1)
            {
                usernameExists = false;
            }

            bool error = ErrorCheck(usernameExists, "Username already exists.");

            if (error)
                return false;

            // Find free slot
            int freeSlotIndex = Iteration(userTable, "find free slot");
            error = ErrorCheck(freeSlotIndex == -1, "The system supports a maximum number of 4 users.");

            if (error)
                return false;

            // Save user
            Success(userTable, freeSlotIndex, "Registered user.", username, password);

            return true;
        }
        static bool Delete(string username, string password, string[,] userTable)
        {
            // Find account to delete
            int accountIndex = Iteration(userTable, "delete", username, password);
            bool error = ErrorCheck(accountIndex == -1, "Invalid account/password.");

            if (error)
                return false;

            Success(userTable, accountIndex, "Deleted account.", null, null);

            return true;
        }
        static bool ErrorCheck(bool condition, string message)
        {
            if (condition)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message);
                return true;
            }

            return false;
        }
        static int Iteration(string[,] userTable, string operation, string username = "1", string password = "1")
        {
            int index = -1;

            for (int i = 0; i < userTable.GetLength(0); i++)
            {
                if (userTable[i, 0] == null && operation == "find free slot")
                {
                    index = i;
                }

                if (userTable[i, 0] == username && userTable[i, 1] == password && operation == "delete")
                {
                    index = i;
                }

                if (userTable[i, 0] == username && operation == "check if exist")
                {
                    index = 1;
                }

            }

            return index;
        }
        static void Success(string[,] userTable, int index, string message, string username, string password)
        {
            userTable[index, 0] = username;
            userTable[index, 1] = password;

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(message);
            // Ready
            // New comment
        }
    }
}

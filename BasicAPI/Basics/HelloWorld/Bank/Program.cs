namespace Program
{
    public class BankApp
    {
        /// <summary>
        /// Main method that runs the banking system, allowing the user to interact with the bank functionalities.
        /// </summary>
        static void Main(string[] args)
        {
            Banking.Bank bank = new Banking.Bank();
            int choice;
            string firstName, lastName;
            long accountNumber;
            float balance, amount;

            do
            {
                #region Display Menu
                Console.WriteLine("*** Banking System ***");
                Console.WriteLine("1. Open an Account");
                Console.WriteLine("2. Balance Enquiry");
                Console.WriteLine("3. Deposit");
                Console.WriteLine("4. Withdrawal");
                Console.WriteLine("5. Close an Account");
                Console.WriteLine("6. Show All Accounts");
                Console.WriteLine("7. Show Transaction History");
                Console.WriteLine("8. Quit");
                Console.Write("Enter your choice: ");
                choice = int.Parse(Console.ReadLine());
                #endregion

                try
                {
                    #region Handle User Choices
                    switch (choice)
                    {
                        case 1:
                            Console.Write("Enter First Name: ");
                            firstName = Console.ReadLine();
                            Console.Write("Enter Last Name: ");
                            lastName = Console.ReadLine();
                            Console.Write("Enter Initial Balance: ");
                            balance = float.Parse(Console.ReadLine());

                            Banking.Account newAccount = bank.OpenAccount(firstName, lastName, balance);
                            Console.WriteLine("Account created successfully!");
                            Console.WriteLine(newAccount);
                            break;

                        case 2:
                            Console.Write("Enter Account Number: ");
                            accountNumber = long.Parse(Console.ReadLine());

                            Banking.Account accEnquiry = bank.BalanceEnquiry(accountNumber);
                            Console.WriteLine("Account Details:");
                            Console.WriteLine(accEnquiry);
                            break;

                        case 3:
                            Console.Write("Enter Account Number: ");
                            accountNumber = long.Parse(Console.ReadLine());
                            Console.Write("Enter Deposit Amount: ");
                            amount = float.Parse(Console.ReadLine());

                            Banking.Account accDeposit = bank.Deposit(accountNumber, amount);
                            Console.WriteLine("Amount Deposited!");
                            Console.WriteLine(accDeposit);
                            break;

                        case 4:
                            Console.Write("Enter Account Number: ");
                            accountNumber = long.Parse(Console.ReadLine());
                            Console.Write("Enter Withdrawal Amount: ");
                            amount = float.Parse(Console.ReadLine());

                            Banking.Account accWithdraw = bank.Withdraw(accountNumber, amount);
                            Console.WriteLine("Amount Withdrawn!");
                            Console.WriteLine(accWithdraw);
                            break;

                        case 5:
                            Console.Write("Enter Account Number: ");
                            accountNumber = long.Parse(Console.ReadLine());

                            bank.CloseAccount(accountNumber);
                            Console.WriteLine("Account Closed!");
                            break;

                        case 6:
                            bank.ShowAllAccounts();
                            break;

                        case 7:
                            Console.Write("Enter Account Number to View Transaction History: ");
                            accountNumber = long.Parse(Console.ReadLine());
                            bank.ShowTransactionHistory(accountNumber);
                            break;

                        case 8:
                            Console.WriteLine("Thank you for using the Banking System!");
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

            } while (choice != 8);
        }
    }
}

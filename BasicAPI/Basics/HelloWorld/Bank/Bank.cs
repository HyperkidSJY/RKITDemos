using System.Data;

namespace Banking
{
    #region Bank Class
    /// <summary>
    /// Provides functionality for a banking system to manage accounts.
    /// Includes operations for account creation, deposits, withdrawals, balance enquiries, and persistent storage.
    /// </summary>
    public class Bank
    {
        private Dictionary<long, Account> accounts = new Dictionary<long, Account>();
        private DataTable transactionHistory;

        #region Constructor
        // Initializes the Bank instance and loads accounts from a data file.
        public Bank()
        {
            transactionHistory = new DataTable("Transactions");
            transactionHistory.Columns.Add("AccountNumber", typeof(long));
            transactionHistory.Columns.Add("TransactionType", typeof(string));
            transactionHistory.Columns.Add("Amount", typeof(float));
            transactionHistory.Columns.Add("Timestamp", typeof(DateTime));
            LoadAccounts();
            LoadTransactions();
        }
        #endregion

        #region Account Management Methods
        // Opens a new account and saves it to the system.
        public Account OpenAccount(string firstName, string lastName, float balance)
        {
            Account account = new Account(firstName, lastName, balance);
            accounts.Add(account.AccountNumber, account);
            SaveAccounts();
            LogTransaction(account.AccountNumber, "Account Opened", balance);
            return account;
        }

        // Retrieves account details by account number.
        public Account BalanceEnquiry(long accountNumber)
        {
            if (accounts.TryGetValue(accountNumber, out Account account))
                return account;
            throw new KeyNotFoundException("Account not found.");
        }

        // Deposits an amount to the specified account.
        public Account Deposit(long accountNumber, float amount)
        {
            if (accounts.TryGetValue(accountNumber, out Account account))
            {
                account.Deposit(amount);
                SaveAccounts();
                LogTransaction(accountNumber, "Deposit", amount);
                return account;
            }
            throw new KeyNotFoundException("Account not found.");
        }

        // Withdraws an amount from the specified account.
        public Account Withdraw(long accountNumber, float amount)
        {
            if (accounts.TryGetValue(accountNumber, out Account account))
            {
                account.Withdraw(amount);
                SaveAccounts();
                LogTransaction(accountNumber, "Withdrawal", amount);
                return account;
            }
            throw new KeyNotFoundException("Account not found.");
        }

        // Closes the specified account and removes it from the system.
        public void CloseAccount(long accountNumber)
        {
            if (accounts.Remove(accountNumber))
            {
                SaveAccounts();
                LogTransaction(accountNumber, "Account Closed", 0);
            }
            else
            {
                throw new KeyNotFoundException("Account not found.");
            }
        }
        #endregion

        #region Transaction Logging
        private void LogTransaction(long accountNumber, string transactionType, float amount)
        {
            transactionHistory.Rows.Add(accountNumber, transactionType, amount, DateTime.Now);
            SaveTransactions();
        }

        public void ShowTransactionHistory(long accountNumber)
        {
            var rows = transactionHistory.Select($"AccountNumber = {accountNumber}");
            if (rows.Length == 0)
            {
                Console.WriteLine("No transactions found for this account.");
                return;
            }

            Console.WriteLine("Transaction History:");
            foreach (var row in rows)
            {
                Console.WriteLine($"Transaction: {row["TransactionType"]}, Amount: {row["Amount"]}, Timestamp: {row["Timestamp"]}");
            }
        }
        #endregion

        #region Account Display Methods
        // Displays all accounts stored in the system.
        public void ShowAllAccounts()
        {
            foreach (Account account in accounts.Values)
            {
                Console.WriteLine(account);
                Console.WriteLine();
            }
        }
        #endregion

        #region Data Persistence Methods
        // Loads accounts from the data file into the system.
        private void LoadAccounts()
        {
            if (File.Exists("Bank.data"))
            {
                var lines = File.ReadAllLines("Bank.data");
                for (int i = 0; i < lines.Length; i += 4)
                {
                    var accountNumber = long.Parse(lines[i]);
                    var firstName = lines[i + 1];
                    var lastName = lines[i + 2];
                    var balance = float.Parse(lines[i + 3]);

                    Account account = new Account(firstName, lastName, balance, accountNumber);
                    accounts.Add(accountNumber, account);
                }
                Account.SetLastAccountNumber(accounts.Keys.DefaultIfEmpty(0).Max());
            }
        }

        // Saves all accounts to the data file for persistence.
        private void SaveAccounts()
        {
            using (StreamWriter writer = new StreamWriter("Bank.data"))
            {
                foreach (Account account in accounts.Values)
                {
                    writer.WriteLine(account.AccountNumber);
                    writer.WriteLine(account.FirstName);
                    writer.WriteLine(account.LastName);
                    writer.WriteLine(account.Balance);
                }
            }
        }
        // Loads transactions from the file into the system.
        private void LoadTransactions()
        {
            if (File.Exists("Transactions.data"))
            {
                var lines = File.ReadAllLines("Transactions.data");
                foreach (var line in lines)
                {
                    var fields = line.Split(',');
                    var accountNumber = long.Parse(fields[0]);
                    var transactionType = fields[1];
                    var amount = float.Parse(fields[2]);
                    var timestamp = DateTime.Parse(fields[3]);

                    transactionHistory.Rows.Add(accountNumber, transactionType, amount, timestamp);
                }
            }
        }

        // Saves all transactions to the file for persistence.
        private void SaveTransactions()
        {
            using (StreamWriter writer = new StreamWriter("Transactions.data"))
            {
                foreach (DataRow row in transactionHistory.Rows)
                {
                    var accountNumber = row["AccountNumber"];
                    var transactionType = row["TransactionType"];
                    var amount = row["Amount"];
                    var timestamp = row["Timestamp"];

                    writer.WriteLine($"{accountNumber},{transactionType},{amount},{timestamp}");
                }
            }
        }

        #endregion
    }
    #endregion
}

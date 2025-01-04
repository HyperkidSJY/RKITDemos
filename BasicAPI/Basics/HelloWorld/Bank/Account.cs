namespace Banking
{
    #region Exception Class for Insufficient Funds
    /// <summary>
    /// Custom exception class to handle insufficient funds during withdrawal.
    /// </summary>
    public class InsufficientFunds : Exception
    {
        public InsufficientFunds(string message) : base(message) { }
    }
    #endregion

    #region Account Class
    /// <summary>
    /// Represents a bank account, providing functionality for deposits, withdrawals, 
    /// and managing account details such as the account number, name, and balance.
    /// </summary>
    public class Account
    {
        private static long NextAccountNumber = 0;

        // Auto-generated or explicitly set account number for this account.
        public long AccountNumber { get; private set; }

        // First name of the account holder.
        public string FirstName { get; private set; }

        // Last name of the account holder.
        public string LastName { get; private set; }

        // Current balance of the account.
        public float Balance { get; private set; }

        #region Constructor
        // Initializes a new account with given details. Auto-generates account number if not provided.
        public Account(string firstName, string lastName, float balance, long accountNumber = -1)
        {
            if (accountNumber == -1)
            {
                NextAccountNumber++;
                AccountNumber = NextAccountNumber;
            }
            else
            {
                AccountNumber = accountNumber;
            }
            FirstName = firstName;
            LastName = lastName;
            Balance = balance;
        }
        #endregion

        #region Deposit and Withdraw Methods
        // Deposits the specified amount into the account.
        public void Deposit(float amount)
        {
            Balance += amount;
        }

        // Withdraws the specified amount from the account, ensuring the balance doesn't go below 500.
        public void Withdraw(float amount)
        {
            if (Balance - amount < 500)
                throw new InsufficientFunds("Insufficient funds for withdrawal.");
            Balance -= amount;
        }
        #endregion

        #region Static Methods for Account Number Management
        // Sets the last used account number.
        public static void SetLastAccountNumber(long accountNumber)
        {
            NextAccountNumber = accountNumber;
        }

        // Retrieves the last used account number.
        public static long GetLastAccountNumber()
        {
            return NextAccountNumber;
        }
        #endregion

        #region ToString Method
        // Returns a string representation of the account details.
        public override string ToString()
        {
            return $"Account Number: {AccountNumber}\nFirst Name: {FirstName}\nLast Name: {LastName}\nBalance: {Balance}";
        }
        #endregion
    }
    #endregion
}

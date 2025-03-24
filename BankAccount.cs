/*
+================================================+
|                                                |
|                                                |
|                                                |
|     __           .__   __                      |
|   _/  |_ ___  ___|__|_/  |_ ___  ___  ______   |
|   \   __\\  \/  /|  |\   __\\  \/  / /  ___/   |
|    |  |   >    < |  | |  |   >    <  \___ \    |
|    |__|  /__/\_ \|__| |__|  /__/\_ \/____  >   |
|                \/                 \/     \/    |
|                                                |
|                                                |
|                                                |
+================================================+
*/

namespace Classes;

public class BankAccount
{
    private static int s_accountNumberSeed = 1234567890;
    public string Number { get; }
    public string Owner { get; set; }
    public decimal Balance 
    { 
        get
        {   // Definiert die Balance mit allen Transactionen
            decimal balance = 0;
            foreach (var item in _allTransactions)
            {
                balance += item.Amount;
            }

            return balance;
         }
    }
    
    public void MakeDeposit(decimal amount, DateTime date, string note)
    {
        //Error handling das man negative Geldbeträge nicht einzahlen kann
        if (amount <= 0)
        {
            throw new ArgumentException(nameof(amount), "Amount of deposit must be positive");
        }

        // Tut die Transacation in die Liste hinzufügen zur Dokumentation
        var deposit = new Transaction(amount, date, note);
        _allTransactions.Add(deposit);
    }

    public void MakeWithdrawal(decimal amount, DateTime date, string note)
    {   //Error handling das man einen positiven Geldbetrag abheben muss
        if (amount <= 0)
        {
            throw new ArgumentException(nameof(amount), "Amount of withdrawal must be positive");
        }
        if(Balance - amount < 0 )
        {  
             //Error handling das man nicht genug Geld auf dem Konto hat um diesen Betrag abzuheben
            throw new InvalidOperationException("Not sufficient funds for this withdrawal");
        }

        //Tut das Abheben in das Journey eintragen
        var withdrawal = new Transaction(- amount, date, note);
        _allTransactions.Add(withdrawal);
    }

    //Formatiert die Ausgabe für die Dokumentations Rückgabe
    public string GetAccountHistory()
    {
        var report = new System.Text.StringBuilder();

        decimal balance = 0;
        report.AppendLine("Date\t\tAmount\tBalance\tNote");
        foreach (var item in _allTransactions)
        {
            balance += item.Amount;
            report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Notes}");
        }

        return report.ToString();
    }

    private List<Transaction> _allTransactions = new List<Transaction>();
    
    public BankAccount(string name, decimal initialBalance)
    {   
        Number = s_accountNumberSeed.ToString();
        s_accountNumberSeed++;

        Owner = name;
        MakeDeposit(initialBalance, DateTime.Now, "Initial Balance");

        
    }

    
}



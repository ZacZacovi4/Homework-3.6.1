using System;

namespace BankAccountClassWork
{

    public enum AccountType { Current, Savings }
    public class BankAccount
    {
        private int _accountNumber;
        private int _balance;
        private AccountType _type;

        public int AccountNumber
        {
            get { return _accountNumber; }
            set { _accountNumber = value; }
        }

        public int Balance
        {
            get { return _balance; }
            //set { _Balance = value; }
        }

        public AccountType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public void Retract(int summa)
        {
            if (this._balance < summa)
            {

                throw new Exception("У вас недостаточно денег");
            }

            this._balance -= summa;
        }

        public void Put(int summa)
        {
            this._balance += summa;
        }
        public bool Equals(BankAccount that)
        {
            BankAccount a = this;
            BankAccount b = that;
            return
                a._accountNumber == b._accountNumber &&
                a._balance == b._balance &&
                a._type == b._type;
        }

        public override bool Equals(object obj)
        {
            bool result = false;
            if (obj is BankAccount)
            {
                result = this.Equals(obj as BankAccount);
            }
            return result;
        }
        public override int GetHashCode()
        {
            return this._balance * this._balance + this._accountNumber + this._accountNumber;
        }

        public static bool operator ==(BankAccount a, BankAccount b)
        {
            Object aAsObj = a as Object;
            Object bAsObj = b as Object;
            if (aAsObj == null || bAsObj == null)
            {
                return aAsObj == bAsObj;
            }
            return a.Equals(b);
        }

        public static bool operator !=(BankAccount a, BankAccount b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            string result;
            if (this._balance < 0)
            {
                result = "-";
            }
            else
            {
                result = "";
            }
            return "Your Account number is: " + this._accountNumber +
                   " Your current balance is: " + result + _balance + "$ " +
                   " Your account type is : " + _type;
        }
    }


    class Program
    {


        static void Main()
        {

            BankAccount bankAccountLcl = new BankAccount();
            BankAccount bankAccountCD = new BankAccount();

            Console.WriteLine("Введите количество денег на счету LCL: ");
            bankAccountLcl.Put(int.Parse(Console.ReadLine()));

            Console.WriteLine("Введите количество денег на счету CD: ");
            bankAccountCD.Put(int.Parse(Console.ReadLine()));

            Random rnd = new Random();
            bankAccountLcl.AccountNumber = rnd.Next(1000000000, int.MaxValue);
            bankAccountCD.AccountNumber = rnd.Next(1000000000, int.MaxValue);

            bankAccountLcl.Type = AccountType.Current;
            bankAccountCD.Type = AccountType.Savings;

            //Test
            Console.WriteLine(bankAccountLcl.ToString());
            Console.WriteLine(bankAccountCD.ToString());
            Console.WriteLine(bankAccountCD.Equals(bankAccountLcl));
            Console.WriteLine(bankAccountCD.Equals(bankAccountCD));

            //Console.WriteLine("Тип банковского аккаунта: " + bankAccountLcl.Type + ", номер счета: " + bankAccountLcl.AccountNumber + ", Баланс: " + bankAccountLcl.Balance);
            //Console.WriteLine("Тип банковского аккаунта: " + bankAccountCD.Type + ", номер счета: " + bankAccountCD.AccountNumber + ", Баланс: " + bankAccountCD.Balance);



            var conditionSucces = false;
            while (!conditionSucces)
            {
                try
                {
                    Console.WriteLine($"Введите сумму которую хотите снять c {bankAccountLcl.AccountNumber} и перевести на {bankAccountCD.AccountNumber} :");
                    int summa = int.Parse(Console.ReadLine());
                    bankAccountLcl.Retract(summa);
                    bankAccountCD.Put(summa);
                    conditionSucces = true;

                    Console.WriteLine("Перевод произведен!");
                    Console.WriteLine("на счету " + bankAccountLcl.AccountNumber + " = " + bankAccountLcl.Balance);
                    Console.WriteLine("на счету " + bankAccountCD.AccountNumber + " = " + bankAccountCD.Balance);


                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}



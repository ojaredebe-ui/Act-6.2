// napa research at nood youtube ako bigla sir
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static Stack<string> deposit = new Stack<string>();
    static Stack<string> withdraw = new Stack<string>();
    static Stack<string> inquire = new Stack<string>();
    static Stack<string> others = new Stack<string>();

    static int depServed = 2;
    static int widServed = 1;
    static int inqServed = 0;
    static int othServed = 0;

    static void Main()
    {
        deposit.Push("Coco Melyon");
        deposit.Push("Dres Bonipasio");
        deposit.Push("Potch Edrile");
        deposit.Push("Coco Melyon");
        deposit.Clear();
        deposit.Push("Editha Portel");
        deposit.Push("Dres Bonipasio");
        deposit.Push("Potch Edrile");
        deposit.Push("Coco Melyon");

        withdraw.Push("Greg Unasan");
        withdraw.Push("Moni Villar");

        inquire.Push("Francis Matalino");
        inquire.Push("Ferdie Magalang");

        Console.WriteLine("Welcome to ECO BANK, INC.");

        while (true)
        {
            Console.WriteLine("\nSelect your transaction");
            Console.WriteLine("1 - DEPOSIT");
            Console.WriteLine("2 - WITHDRAW");
            Console.WriteLine("3 - INQUIRE");
            Console.WriteLine("4 - OTHERS");
            Console.Write("Enter code: ");

            string code = Console.ReadLine();

            if (code == "10" || code == "20" ||
                code == "30" || code == "40")
            {
                FinishTransaction(code);
                DisplayQueue();
                continue;
            }

            Stack<string> selected = null;
            string transaction = "";

            switch (code)
            {
                case "1":
                    selected = deposit;
                    transaction = "DEPOSIT";
                    break;

                case "2":
                    selected = withdraw;
                    transaction = "WITHDRAW";
                    break;

                case "3":
                    selected = inquire;
                    transaction = "INQUIRE";
                    break;

                case "4":
                    selected = others;
                    transaction = "OTHERS";
                    break;

                default:
                    Console.WriteLine("Invalid code!");
                    continue;
            }

            Console.WriteLine("\nYou have chosen " + transaction);
            Console.Write("Please enter your account name: ");

            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Invalid name!");
                continue;
            }

            selected.Push(name);
            DisplayQueue();
        }
    }

    static void FinishTransaction(string code)
    {
        Stack<string> selected = null;
        string transaction = "";

        switch (code)
        {
            case "10":
                selected = deposit;
                transaction = "DEPOSIT";
                depServed++;
                break;

            case "20":
                selected = withdraw;
                transaction = "WITHDRAW";
                widServed++;
                break;

            case "30":
                selected = inquire;
                transaction = "INQUIRE";
                inqServed++;
                break;

            case "40":
                selected = others;
                transaction = "OTHERS";
                othServed++;
                break;
        }

        if (selected.Count == 0)
        {
            Console.WriteLine("\nNo clients waiting for " + transaction + "!");
            return;
        }

        Stack<string> temp = new Stack<string>();

        while (selected.Count > 0)
            temp.Push(selected.Pop());

        temp.Pop();

        while (temp.Count > 0)
            selected.Push(temp.Pop());

        Console.WriteLine("\nTransaction Completed!");
    }

    static void DisplayQueue()
    {
        Console.WriteLine("\nNOW SERVING!!! NOW SERVING!!!");
        Console.WriteLine("DEP : " + GetNext(deposit));
        Console.WriteLine("WID : " + GetNext(withdraw));
        Console.WriteLine("INQ : " + GetNext(inquire));
        Console.WriteLine("OTH : " + GetNext(others));

        Console.WriteLine("\nWAITING LIST!!! WAITING LIST!!!");
        Console.WriteLine("____________________________________________________________");

        PrintSideBySide(
            "DEPOSIT  ", depServed, deposit,
            "WITHDRAWAL", widServed, withdraw);

        Console.WriteLine();

        PrintSideBySide(
            "INQUIRE  ", inqServed, inquire,
            "OTHERS", othServed, others);

        Console.WriteLine("____________________________________________________________");
    }

    static string GetNext(Stack<string> clients)
    {
        if (clients.Count == 0)
            return "";

        return clients.Last();
    }

    static void PrintSideBySide(
        string leftTitle, int leftServed, Stack<string> left,
        string rightTitle, int rightServed, Stack<string> right)
    {
        Console.WriteLine(
            leftTitle + " (" + leftServed + " - " + left.Count + ")" +
            new string(' ', 16) + "|    " +
            rightTitle + " (" + rightServed + " - " + right.Count + ")");

        List<string> leftNames = left.Reverse().ToList();
        List<string> rightNames = right.Reverse().ToList();

        int rows = Math.Max(leftNames.Count, rightNames.Count);

        for (int i = 0; i < rows; i++)
        {
            string leftText = i < leftNames.Count
                ? (i + 1) + ". " + leftNames[i]
                : "";

            string rightText = i < rightNames.Count
                ? (i + 1) + ". " + rightNames[i]
                : "";

            Console.WriteLine(
                leftText.PadRight(33) + "|    " + rightText);
        }
    }
}
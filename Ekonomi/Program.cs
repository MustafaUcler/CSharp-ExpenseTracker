using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ExpenseTracker
{
    public class Expense
    {
        // Add variables here
        public string Name;
        public decimal Price;
        public string Category;
    }

    public class Program
    {
        public static decimal utbildningMoms = 0m;
        public static decimal böckerMoms = 0.06m;
        public static decimal livsmedelMoms = 0.12m;
        public static decimal övrigtMoms = 0.25m;

        public static List<Expense> Expenses = new List<Expense>();

        public static void Main()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine(" Hejsan!");
                int choice = ShowMenu(" Vad vill du göra? ", new[]
                {
                " Lägg till utgift",
                " Visa alla utgifter",
                " Visa summa per kategori",
                " Ändra utgift",
                " Ta bort enskild utgift",
                " Ta bort samtliga utgifter",
                " Avsluta"
                });
                Console.Clear();
                {
                    if (choice == 0)
                    {
                        AddExpense();
                    }
                    else if (choice == 1)
                    {
                        ShowExpense();
                    }
                    else if (choice == 2)
                    {
                        ShowTotalPriceForAllCategories();
                    }
                    else if (choice == 3)
                    {
                        EditExpense();
                    }
                    else if (choice == 4)
                    {
                        DelOneExpense();
                    }
                    else if (choice == 5)
                    {
                        int confirmChoice = ShowMenu(" Vill du tabort samtliga utgifter?", new List<string>
                        {
                            "Ja",
                            "Nej"
                        });
                        if (confirmChoice == 0)
                        {
                            Expenses.Clear();
                            Console.WriteLine(" Samtliga utgifter har tagits bort.");
                        }
                        Console.ReadKey();
                    }
                    else if (choice == 6)
                    {
                        Console.WriteLine(" Tryck enter för att återgå");
                        Console.WriteLine(" Tack!");
                        running = false;
                    }
                }
            }
        }

        public static void AddExpense()
        {
            Console.WriteLine(" Lägg till utgift:");
            Console.Write(" Namn: ");
            string namn = Console.ReadLine();
            Console.Write(" Pris inkl. moms: ");
            decimal pris = decimal.Parse(Console.ReadLine());

            List<string> kategorier = new List<string>
            {
                "Utbildning",
                "Böcker",
                "Livsmedel",
                "Övrigt"
            };
            int valdKategori = ShowMenu("Välj :", kategorier);
            string kategori = kategorier[valdKategori];
            Expense expense = new Expense
            {
                Name = namn,
                Price = pris,
                Category = kategori
            };
            Expenses.Add(expense);
            Console.WriteLine("Utgift tillagd!");
            Console.ReadKey();
        }

        public static void ShowExpense()
        {
            Console.WriteLine(" Alla utgifter:");

            decimal TotalPriceIncludingVat = SumExpenses(Expenses, true);
            decimal TotalPriceExcludingVat = SumExpenses(Expenses, false);

            foreach (Expense expense in Expenses)
            {
                Console.WriteLine(expense.Name + ": " + expense.Price.ToString("0.00") + " kr (" + expense.Category + ")");
            }

            Console.WriteLine();
            Console.WriteLine(
                "Totalt belopp: " + TotalPriceIncludingVat + " kr " + "( " + TotalPriceExcludingVat.ToString("0.00") + "kr exkl. moms)");
            Console.WriteLine("Antal utgifter: " + Expenses.Count);
            Console.WriteLine("Tryck enter för att återgå.");
            Console.ReadLine();
        }

        public static void ShowTotalPriceForAllCategories()
        {
            Console.WriteLine(" Summa per kategori:");
            List<Expense> books = new List<Expense>();
            foreach (Expense expense in Expenses)
            {
                if (expense.Category == "Böcker")
                {
                    books.Add(expense);
                }
            }
            decimal bookSumWithoutVat = SumExpenses(books, false);
            decimal bookSumWitVat = SumExpenses(books, true);

            List<Expense> utbildning = new List<Expense>();
            foreach (Expense expense in Expenses)
            {
                if (expense.Category == "Utbildning")
                {
                    utbildning.Add(expense);
                }
            }
            decimal utbildningSumWithoutVat = SumExpenses(utbildning, false);
            decimal utbildningSumWithVat = SumExpenses(utbildning, true);

            List<Expense> livsmedel = new List<Expense>();
            foreach (Expense expense in Expenses)
            {
                if (expense.Category == "Livsmedel")
                {
                    livsmedel.Add(expense);
                }
            }
            decimal livsmedelSumWithoutVat = SumExpenses(livsmedel, false);
            decimal livsmedmedelSumWithVat = SumExpenses(livsmedel, true);

            List<Expense> ovrigt = new List<Expense>();
            foreach (Expense expense in Expenses)
            {
                if (expense.Category == "Övrigt")
                {
                    ovrigt.Add(expense);
                }
            }
            decimal ovrigtSumWithoutVat = SumExpenses(ovrigt, false);
            decimal ovrigtSumWithVat = SumExpenses(ovrigt, true);

            Console.WriteLine($"Utbildning: {utbildningSumWithVat.ToString("0.00")} ( {utbildningSumWithoutVat.ToString("0.00")} Kr exkl. moms )");
            Console.WriteLine($"Böcker: {bookSumWitVat.ToString("0.00")} ( {bookSumWithoutVat.ToString("0.00")} Kr exkl. moms )");
            Console.WriteLine($"Livsmedel: {livsmedmedelSumWithVat.ToString("0.00")} ( {livsmedelSumWithoutVat.ToString("0.00")} Kr exkl. moms )");
            Console.WriteLine($"Övrigt: {ovrigtSumWithVat.ToString("0.00")} ( {ovrigtSumWithoutVat.ToString("0.00")} Kr exkl. moms )");

            Console.ReadLine();
        }

        public static void DelOneExpense()
        {
            List<string> options = new List<string>();
            foreach (Expense expense in Expenses)
            {
                options.Add(expense.Name + ": " + expense.Price.ToString("0.00") + " kr (" + expense.Category + ")");
            }

            // Show the menu and get the selected index.
            int expenseIndex = ShowMenu("Välj utgift att tabort:", options);

            Expense selectedExpense = Expenses[expenseIndex];
            Expenses.Remove(selectedExpense);
            Console.WriteLine("Utgiften \"" + selectedExpense.Name + "\" Har tagits bort");
            Console.ReadKey();
        }

        public static void EditExpense()
        {
            List<string> options = new List<string>();
            foreach (Expense expense in Expenses)
            {
                options.Add(expense.Name + ": " + expense.Price.ToString("0.00") + " kr (" + expense.Category + ")");
            }
            int choice = ShowMenu("Välj utgift att ändra:", options);
            Expense expenseToEdit = Expenses[choice];
            int editChoice = ShowMenu("Vad vill du ändra?", new List<string>
            {
                "Namn",
                "Pris",
                "Kategori"
            });
            switch (editChoice)
            {
                case 0:
                    Console.Write("Nytt namn: ");
                    expenseToEdit.Name = Console.ReadLine();
                    break;
                case 1:
                    Console.Write("Nytt pris inkl. moms: ");
                    expenseToEdit.Price = decimal.Parse(Console.ReadLine());
                    break;
                case 2:
                    Console.WriteLine("Ny kategori:");
                    int categoryChoice = ShowMenu("Välj kategori:", new List<string>
                    {
                        "Utbildning",
                        "Böcker",
                        "Livsmedel",
                        "Övrigt"
                    });
                    string[] kategorier = new string[] { "Utbildning", "Böcker", "Livsmedel", "Övrigt" };
                    expenseToEdit.Category = kategorier[categoryChoice];
                    break;
            }
            Console.WriteLine("Utgiften \"" + expenseToEdit.Name + "\" har ändrats");

            Console.ReadLine();
        }

        public static decimal SumExpenses(List<Expense> expenses, bool includeVAT)
        {
            decimal sum = 0;

            foreach (Expense expense in expenses)
            {
                if (includeVAT)
                {
                    sum += expense.Price;
                }
                else
                {
                    decimal moms = 0;

                    switch (expense.Category)
                    {
                        case "Utbildning":
                            moms = utbildningMoms;
                            break;

                        case "Böcker":
                            moms = böckerMoms;
                            break;

                        case "Livsmedel":
                            moms = livsmedelMoms;
                            break;

                        case "Övrigt":
                            moms = övrigtMoms;
                            break;
                    }

                    decimal expensePriceExcludingVAT = expense.Price / (1 + moms);
                    sum += expensePriceExcludingVAT;
                }
            }

            return sum;
        }

        // Do not change this method.
        // For more information about ShowMenu: https://startcoding.app/console/
        public static int ShowMenu(string prompt, IEnumerable<string> options)
        {
            if (options == null || options.Count() == 0)
            {
                throw new ArgumentException("Cannot show a menu for an empty list of options.");
            }

            Console.WriteLine(prompt);

            // Hide the cursor that will blink after calling ReadKey.
            Console.CursorVisible = false;

            // Calculate the width of the widest option so we can make them all the same width later.
            int width = options.Max(option => option.Length);

            int selected = 0;
            int top = Console.CursorTop;
            for (int i = 0; i < options.Count(); i++)
            {
                // Start by highlighting the first option.
                if (i == 0)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                var option = options.ElementAt(i);
                // Pad every option to make them the same width, so the highlight is equally wide everywhere.
                Console.WriteLine("- " + option.PadRight(width));

                Console.ResetColor();
            }
            Console.CursorLeft = 0;
            Console.CursorTop = top - 1;

            ConsoleKey? key = null;
            while (key != ConsoleKey.Enter)
            {
                key = Console.ReadKey(intercept: true).Key;

                // First restore the previously selected option so it's not highlighted anymore.
                Console.CursorTop = top + selected;
                string oldOption = options.ElementAt(selected);
                Console.Write("- " + oldOption.PadRight(width));
                Console.CursorLeft = 0;
                Console.ResetColor();

                // Then find the new selected option.
                if (key == ConsoleKey.DownArrow)
                {
                    selected = Math.Min(selected + 1, options.Count() - 1);
                }
                else if (key == ConsoleKey.UpArrow)
                {
                    selected = Math.Max(selected - 1, 0);
                }

                // Finally highlight the new selected option.
                Console.CursorTop = top + selected;
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                string newOption = options.ElementAt(selected);
                Console.Write("- " + newOption.PadRight(width));
                Console.CursorLeft = 0;
                // Place the cursor one step above the new selected option so that we can scroll and also see the option above.
                Console.CursorTop = top + selected - 1;
                Console.ResetColor();
            }

            // Afterwards, place the cursor below the menu so we can see whatever comes next.
            Console.CursorTop = top + options.Count();

            // Show the cursor again and return the selected option.
            Console.CursorVisible = true;
            return selected;
        }
    }
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void TestSumExpensesExcludingVatBooks()
        {

            List<Expense> expenses = new List<Expense>();

            expenses.Add(new Expense { Name = "Harry Potter", Price = 200, Category = "Böcker" });

            decimal bookSumWithoutVat = Program.SumExpenses(expenses, false);

            decimal roundedResult = Math.Round(bookSumWithoutVat, 2);

            Assert.AreEqual(188.68m, roundedResult);
        }
        [TestMethod]
        public void TestBookSumExcludingVat()
        {

            List<Expense> expenses = new List<Expense>();

            expenses.Add(new Expense { Name = "Cykel", Price = 1000, Category = "Övrigt" });

            decimal bookSumWithoutVat = Program.SumExpenses(expenses, true);

            decimal roundedResult = Math.Round(bookSumWithoutVat, 2);

            Assert.AreEqual(1000m, roundedResult);
        }
        [TestMethod]
        public void TestUtbildningSumExcludingVat()
        {

            List<Expense> expenses = new List<Expense>();

            expenses.Add(new Expense { Name = "Programmering", Price = 10000, Category = "Utbildning" });

            decimal bookSumWithoutVat = Program.SumExpenses(expenses, true);

            decimal roundedResult = Math.Round(bookSumWithoutVat, 2);

            Assert.AreEqual(10000m, roundedResult);
        }
    }
}
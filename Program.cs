using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;

namespace dagsoppgave_backend_uke_3_dag_1;

class Program
{
    static void Main(string[] args)
    {
        //fresh food store



        // bool mainmenu = true;
        string mainMenuInput = "";
        do
        {


            Console.Clear();
            Console.WriteLine("Welcome to Freshoods storage facility.");
            Console.WriteLine("Please enter what you'd like to do:");
            Console.WriteLine();
            Console.WriteLine("1. Check on the meat.");
            Console.WriteLine("2. Check on the fish.");
            Console.WriteLine("3. Check on the veggies.");
            Console.WriteLine("4. Sum all our current available products and their price.");
            Console.WriteLine("5. Exit program.");
            Console.WriteLine();
            Console.WriteLine("NB: Only enter one of these numbers.");

            mainMenuInput = Console.ReadLine().Replace(" ", "").Trim();

            if (mainMenuInput != null && int.TryParse(mainMenuInput, out int input))
            {
                switch (input.ToString())
                {
                    case "1":
                        MeatMenu();
                        //use class Meat to check on all meat
                        break;

                    case "2":
                        Console.WriteLine("WIP - Still under construction");
                        Console.WriteLine("Press enter to return to main menu");
                        Console.ReadLine();
                        break;

                    case "3":
                        Console.WriteLine("WIP - Still under construction");
                        Console.WriteLine("Press enter to return to main menu");
                        Console.ReadLine();
                        break;

                    case "4":
                        //sum MeatTotal, FishTotal, and VeggieTotal
                        break;
                }
            }


        } while (mainMenuInput != "5");

    }

    public static void MeatMenu()
    {
        string meatMenuInput;
        do
        {

            Console.Clear();
            Console.WriteLine("Welcome the Meat section.");
            Console.WriteLine("Please enter what you'd like to do:");
            Console.WriteLine();
            Console.WriteLine("1. Check on what meat we have.");
            Console.WriteLine("2. Check how much we have of each meat.");
            Console.WriteLine("3. Check the prices of each meat.");
            Console.WriteLine("4. Add new meat.");
            Console.WriteLine("5. Check the total price of all added meat.");
            Console.WriteLine("6. Go back.");
            Console.WriteLine();
            Console.WriteLine("NB: Only enter one of these numbers.");

            meatMenuInput = Console.ReadLine().Replace(" ", "").Trim();

            if (meatMenuInput != null && int.TryParse(meatMenuInput, out int input))
            {
                switch (input.ToString())
                {
                    case "1":

                        Console.WriteLine("This is the meat we currently have.");
                        foreach (var item in Meat.meatList)
                        {
                            Console.WriteLine(item);
                        }

                        Console.WriteLine("Press enter to return to the previous menu.");
                        Console.ReadLine();
                        break;

                    case "2":

                        Console.WriteLine("Here's how much we have of each meat.");
                        var meatListCount = Meat.meatCount.Zip(Meat.meatList, (first, second) => first + "kg of " + second + ".");
                        foreach (var item in meatListCount)
                        {
                            Console.WriteLine(item);
                        }
                        Console.WriteLine("Press enter to return to the previous menu.");
                        Console.ReadLine();

                        break;

                    case "3":

                        Console.WriteLine("Here's how much one kg of each meat costs:");
                        var meatListPrice = Meat.meatList.Zip(Meat.meatPrices, (first, second) => first + " costs " + second + "kr per kg.");
                        foreach (var meat in meatListPrice)
                        {
                            Console.WriteLine(meat);
                        }
                        Console.WriteLine("Press enter to return to the previous menu.");
                        Console.ReadLine();
                        break;
                    case "4":
                        NewMeat();
                        break;

                    case "5":
                        TotalMeat();
                        break;

                }
            }
        } while (meatMenuInput != "6");
    }

    public static void TotalMeat()
    {
        var totalMeat = Meat.meatCount.Zip(Meat.meatPrices, (first, second) => first * second);

        decimal total = totalMeat.Sum();
        Console.WriteLine($"The total amount of money our meat is currently worth is: {total}kr. ");
        Console.WriteLine("Press enter to return");
        Console.ReadLine();

    }
    public static void NewMeat()
    {
        Console.WriteLine("What kind of meat would you like to add?");
        Console.WriteLine("Be clear, no tomfoolery. The boss'll know.");

        string? addedMeatName = Console.ReadLine();

        if (addedMeatName != null)
        {


            Console.WriteLine("How many kgs of meat would you like to add?");
            Console.WriteLine("Round to the closest whole number.");

            string addedMeatCount = Console.ReadLine().ToLower().Trim().Replace(" ", "");

            if (addedMeatCount != null && int.TryParse(addedMeatCount, out int count))
            {
                Console.WriteLine("How much is each kg worth? In kr, of course.");
                string addedMeatPrice = Console.ReadLine().ToLower().Trim().Replace(" ", "");

                if (addedMeatPrice != null && decimal.TryParse(addedMeatPrice, out decimal price))
                {
                    Meat.meatList.Add(addedMeatName);
                    Meat.meatCount.Add(count);
                    Meat.meatPrices.Add(price);
                    Console.WriteLine($" {count} kg of {addedMeatName} was added to the inventory, at the price of {price} per kg.");
                    Console.WriteLine("Press enter to return to the main menu.");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("That's not a number... press enter to return to the previous menu.");
                Console.ReadLine();
            }
        }
        else
        {
            Console.WriteLine("it's got to have a name. press enter to return to the previous menu.");
            Console.ReadLine();

        }


    }
    public static void MeatList()
    {

        var allMeat = Meat.meatList.Zip(Meat.meatPrices, (first, second) => first + " for " + second + "kr  amount left:");
        var allMeat2 = allMeat.Zip(Meat.meatCount, (first, second) => first + " " + second + ".");

        foreach (var item in allMeat2)
            Console.WriteLine(item);

    }
    class Meat
    {
        #region meatfields

        //
        private string _name;

        private decimal _price;

        private int _count;

        #endregion

        public string MeatName
        {
            get
            {
                return _name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new NullReferenceException();
                }
                _name = value;
            }
        }

        public decimal MeatPrice
        {
            get
            {
                return _price;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Ey, we're not selling this for negative or nothing!");
                }
                _price = value;
            }
        }

        public int MeatAmount
        {
            get
            {
                return _count;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("We can't have less meat than no meat...");
                }
                _count = value;
            }
        }

        public decimal TotalValue
        {
            get
            {
                return _count * _price;
            }
        }

        public static List<string> meatList = new()
    {
        "Chicken",
        "Ham",
        "Beef"

    };

        public static List<decimal> meatPrices = new()
    {
        50m,
        70m,
        100m
    };

        public static List<int> meatCount = new()
    {
        20,
        30,
        15
    };

        /*    }
            class Fish
            {
                #region fishfields

                //
                private string _name;

                private decimal _price;

                private int _count;

                #endregion


                public static List<string> fishList = new()
            {
                "Trout",
                "Tuna",
                "Salmon"

            };
            }

            class Veggies
            {
                #region veggiefields

                //
                private string _name;

                private decimal _price;

                private int _count;

                #endregion

                public static Dictionary<string, decimal> veggieList = new Dictionary<string, decimal>()
                {


                };
           */
    }
}

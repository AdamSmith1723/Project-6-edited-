using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Project_6
{
    class Program
    {
       
        static List<string> fullName = new List<string>();
        static List<decimal> salesAmt = new List<decimal>();
        static List<decimal> salesSort = new List<decimal>();
        static void Main(string[] args)
        {
            Console.WriteLine("    Welcome to Sommet SalesForce   ");
            while (true)
            {
                ReadFile();
                MenuOption(); //method 
                int menuChoice = Convert.ToInt32(Console.ReadLine());
                switch (menuChoice)
                {
                   case 1: CurrentSale();
                        break;
                    
                    case 2: AdditionalSale();
                        break;

                    case 3: TopThreeSalesPerson();
                        break;
                    case 4:
                        SalesTotal();
                        break;
                }

                Console.ReadKey();
            }
            
        }
        static private void MenuOption()
        {
            Console.WriteLine("Please select the following:");
            Console.WriteLine("1.Display the information from input file: ");
            Console.WriteLine("2. Enter sales report :");
            Console.WriteLine("3.Display the  top three sales person in the order:");
            Console.WriteLine("4.Display the total sales for all saleperson:");

        }
        static private void CurrentSale()
        {
            Console.WriteLine($"The number of salesperson from the input File = {fullName.Count} people.");
            for (int i = 0; i < salesAmt.Count; i++)
            {
                Console.WriteLine($"{fullName[i]} has total sales of {salesAmt[i]:C}");
                
            }
        }
        static private void AdditionalSale ()
        {
            Console.WriteLine("Is there additional sales to report? Y/N");
            string moreSalesReport = Console.ReadLine();
            while (true)
            {
                {
                    switch (moreSalesReport)
                    {
                        case "Y":
                                Console.WriteLine("Please enter the saleperson full name");
                                string name = Console.ReadLine(); //userinput's variable is "name"
                                int salesPersonIndex = 0;
                               //declare userinput as an index. Give -1 as the index number since the zero index value in the fullName list starts at the first saleperson
                                for (int i=0; i <fullName.Count; i++) 
                                //the userinput's index will be equal to the fullname index and will loop through the List or array to find that person
                                {
                                    if (name.ToLower () ==fullName[i].ToLower()) //use if statement to trim the userinput to lower case 
                                    {
                                    salesPersonIndex = i; 
                                    break;
                                    }
                                }
                                if (salesPersonIndex != 0) //if the user input match the index in the List, add the userinput's amount into the Salesamount List 
                                {
                                    Console.WriteLine("Saleperson found! Now Please enter the sales amount to add:");
                                   decimal amount = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("The current sales amount for this person is = $" + salesAmt[salesPersonIndex]);
                                   salesAmt[salesPersonIndex] += amount;
                                    Console.WriteLine("Added the new amount to exisitng amount, the new amount is = $" + salesAmt[salesPersonIndex]);
          
                                }
                                else //if the user type a name that's not in the list, show an error message 
                                {
                                Console.WriteLine("Invaild:SalesPerson not found");
                                }
                            break;
                        case "N":
                            
                            while (true)
                            {
                                ReadFile();
                                MenuOption(); //method 
                                int menuChoice = Convert.ToInt32(Console.ReadLine());
                                switch (menuChoice)
                                {
                                    case 1:
                                        CurrentSale();
                                        break;

                                    case 2:
                                        AdditionalSale();
                                        break;

                                    case 3:
                                        TopThreeSalesPerson();
                                        break;

                                    case 4:
                                        SalesTotal();
                                        break;

                                }
                                Console.ReadKey();
                            }

                            
                            

                                          
                    }        break;
                }
            }
        }

        static private void TopThreeSalesPerson()
        {
            string highestSalePerson = FindTopSalesPerson();
            decimal highestNumber = salesAmt.Max();
            Console.WriteLine($" {highestSalePerson} with sales total of :{ highestNumber:C2}");

            string secondHighestSalePerson = FindSecondTopSalesPerson();
            decimal secondAmt = FindSecondNumber();
            Console.WriteLine($"{secondHighestSalePerson} with sales total of :{secondAmt:C2}");

            FindThridTopSalesPerson();

        }
        static private string FindTopSalesPerson ()
        {
            decimal max = salesAmt.Max();
            string outputString = "The highest ranked saleperson is ";
            for (int i=0; i<salesAmt.Count; i++)
            {
                if (salesAmt [i] == max)
                {
                    outputString += " " + fullName[i];
                }
            }
            return outputString;
        }
         static private decimal FindSecondNumber ()
         {
            decimal first = decimal.MinValue;
            decimal second = decimal.MinValue;

            for (int i = 0; i <salesAmt.Count; i++)
            {
                if (salesAmt [i]> first)
                {
                    second = first;
                    first = salesAmt[i];
                }
            }
            return second;
         }
        static private string FindSecondTopSalesPerson()
        {
            string outputString = "The second highest ranked saleperson is";
            int secondHighestSalePerson = salesAmt.IndexOf(FindSecondNumber());
            string matchedNameForSecondHighest = fullName[secondHighestSalePerson];

            return outputString += " " + matchedNameForSecondHighest;
        }
        static void  FindThridTopSalesPerson()
        {
            ReadFile();
          
            decimal firstNumber = salesSort[1];
            decimal secondNumber = salesSort[2];
            decimal thridNumber = salesSort[3]; ;
            int third = salesAmt.IndexOf(thridNumber);

            Console.WriteLine($"The third highest sales is {fullName[third]} with {salesAmt[third]:C}\n");
        }
              
        static void SalesTotal ()
        {
            decimal Totalamount;
            Totalamount = salesAmt.Sum();
            Console.WriteLine($"The total sale of all saleperson is {Totalamount:C}");
        }
        static void ReadFile ()
        {
            String[] rows = File.ReadAllLines("Proj6Input.csv");
            for (int i=0; i <rows.Length; i++)
            {
                string[] cells = rows[i].Split(','); //not string,
                string currName = cells[0] + "" + cells[1];
                fullName.Add(currName);
                salesAmt.Add(Convert.ToDecimal(cells[2]));
            }
        }
    }
}

using System;

namespace Programming
{
    internal class Program
    {
        class ProgramClass
        {
            static void Main(string[] args)
            {
                bool exitProgram = false;

                while (!exitProgram)
                {
                    ShowMenu();

                    string name = GetCustomerName();
                    double lastMonthReading = 0;
                    double thisMonthReading = 0;
                    int customerType = 0;
                    int numberOfPeople = 0;
                    double waterConsumption = 0;

                    while (true)
                    {
                        lastMonthReading = GetWaterReading("Enter last month's water index (m³): ");
                        thisMonthReading = GetWaterReading("Enter this month's water index (m³): ");

                        if (IsValidReading(thisMonthReading, lastMonthReading))
                        {
                            waterConsumption = thisMonthReading - lastMonthReading;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Error: Invalid water index values entered. Please try again.");
                        }
                    }
                    customerType = GetCustomerType();
                    numberOfPeople = GetNumberOfPeople();

                    var (waterBill, environmentFee, vat, totalBill) = CalculateWaterBill(waterConsumption, customerType, numberOfPeople);
                    DisplayBill(waterConsumption, waterBill, environmentFee, vat, totalBill);

                    Console.WriteLine();
                    Console.Write("Do you want to continue (Y/N)? ");
                    string continueInput = Console.ReadLine().Trim().ToUpper();

                    if (continueInput == "N")
                    {
                        exitProgram = true;
                        Console.WriteLine("Exiting the program...");
                    }
                    else if (continueInput == "Y")
                    {
                        ClearConsole();
                    }

                }
            }

            static void ShowMenu()
            {
                Console.Clear();
                Console.WriteLine("________Menu__________");
                Console.WriteLine("1. Add Customer");
                Console.WriteLine("2. Calculate Bills");
                Console.WriteLine("3. Print Bill");
                Console.WriteLine("4. Exit");
                Console.WriteLine();
            }

            static void ClearConsole()
            {
                Console.Clear();
                ShowMenu();
            }

            static string GetCustomerName()
            {
                Console.Write("Enter the customer's name: ");
                return Console.ReadLine();
            }

            static double GetWaterReading(string message)
            {
                double reading = 0;
                bool isValidInput = false;

                while (!isValidInput)
                {
                    Console.Write(message);
                    if (!double.TryParse(Console.ReadLine(), out reading) || reading < 0)
                    {
                        Console.WriteLine("Error: Invalid water index value. Please enter a positive number.");
                    }
                    else
                    {
                        isValidInput = true;
                    }
                }
                return reading;
            }

            static int GetCustomerType()
            {
                int customerType = 0;
                bool isValidInput = false;

                while (!isValidInput)
                {
                    Console.Write("Enter customer type (1: Household, 2: Public services, 3: Production units, 4: Business services): ");
                    if (!int.TryParse(Console.ReadLine(), out customerType) || customerType < 1 || customerType > 4)
                    {
                        Console.WriteLine("Error: Invalid customer type. Please enter a number between 1 and 4.");
                    }
                    else
                    {
                        isValidInput = true;
                    }
                }
                return customerType;
            }

            static int GetNumberOfPeople()
            {
                int numberOfPeople = 0;
                bool isValidInput = false;

                while (!isValidInput)
                {
                    Console.Write("Enter the number of people: ");
                    if (!int.TryParse(Console.ReadLine(), out numberOfPeople) || numberOfPeople < 1)
                    {
                        Console.WriteLine("Error: Invalid number of people. Please enter a positive number.");
                    }
                    else
                    {
                        isValidInput = true;
                    }
                }
                return numberOfPeople;
            }

            static bool IsValidReading(double thisMonthReading, double lastMonthReading)
            {
                return thisMonthReading >= lastMonthReading && thisMonthReading >= 0 && lastMonthReading >= 0;
            }

            static (double, double, double, double) CalculateWaterBill(double waterConsumption, int customerType, int numberOfPeople)
            {
                double waterBill = 0;
                double environmentFee = 0;
                double vat = 0;
                double totalBill = 0;
                switch (customerType)
                {
                    case 1:
                        if (waterConsumption <= 10)
                            waterBill = waterConsumption * 5.973;
                        else if (waterConsumption <= 20)
                            waterBill = 10 * 5.973 + (waterConsumption - 10) * 7.052;
                        else if (waterConsumption <= 30)
                            waterBill = 10 * 5.973 + 10 * 7.052 + (waterConsumption - 20) * 8.699;
                        else
                            waterBill = 10 * 5.973 + 10 * 7.052 + 10 * 8.699 + (waterConsumption - 30) * 15.929;
                        break;
                    case 2:
                        if (waterConsumption > 0)
                            waterBill = waterConsumption * 9.955;
                        break;
                    case 3:
                        waterBill = waterConsumption * 11.615;
                        break;
                    case 4:
                        waterBill = waterConsumption * 22.068;
                        break;
                    default:
                        Console.WriteLine("Error: Invalid customer type. Please try again.");
                        break;
                }
                waterBill *= numberOfPeople;
                environmentFee = waterBill * 0.1;
                vat = waterBill * 0.1;
                totalBill = waterBill + environmentFee + vat;

                return (waterBill, environmentFee, vat, totalBill);
            }

            static void DisplayBill(double waterConsumption, double waterBill, double environmentFee, double vat, double totalBill)
            {
                Console.WriteLine($"Water Consumption: {waterConsumption} m³.");
                Console.WriteLine($"Water Bill: {waterBill} VND.");
                Console.WriteLine($"Environment Fee: {environmentFee} VND.");
                Console.WriteLine($"VAT: {vat} VND.");
                Console.WriteLine($"Total Bill: {totalBill} VND.");
            }
        }
    }
}


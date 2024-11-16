/// <summary>
/// Assignment 3
/// 
/// Author: Frances Odunaiya
/// Date: November 1st, 2024
/// Purpose: Allows user to enter/save/load/edit/view daily sales values
///          from a file. Allows and displays simple data analysis
///          (mean/max/min/graph) of sales values for a given month.
/// </summary>

string mainMenuChoice;
string analysisMenuChoice;
bool displayMainMenu = true;
bool displayAnalysisMenu;
bool quit;
// TODO: declare a constant to represent the max size of the sales
// and dates arrays. The arrays must be large enough to store
// sales for an entire month.
const int MaxDaysInMonth = 31;


// TODO: create a double array named 'sales', use the max size constant you declared
// above to specify the physical size of the array.
double[] sales = new double[MaxDaysInMonth];

// TODO: create a string array named 'dates', use the max size constant you declared
// above to specify the physical size of the array.
string[] dates = new string[MaxDaysInMonth];

string month;
string year;
string filename;
int count = 0;
bool proceed;
double mean;
double largest;
double smallest;

DisplayProgramIntro();

// TODO: call the DisplayMainMenu method
DisplayMainMenu();

while (displayMainMenu)
{
	mainMenuChoice = Prompt("Enter MAIN MENU option ('D' to display menu): ").ToUpper();
	Console.WriteLine();

	//MAIN MENU Switch statement
	switch (mainMenuChoice)
	{
		case "N": //[N]ew Daily Sales Entry

			proceed = NewEntryDisclaimer();

			if (proceed)
			{
				// TODO: uncomment the following and call the EnterSales method below
				int currentIndex = 0;
                count = EnterSales(sales, dates, ref currentIndex);
				Console.WriteLine();
				Console.WriteLine($"Entries completed. {count} records in temporary memory.");
				Console.WriteLine();
			}
			else
			{
				Console.WriteLine("Cancelling new data entry. Returning to MAIN MENU.");
			}
			break;
		case "S": //[S]ave Entries to File
			if (count == 0)
			{
				Console.WriteLine("Sorry, LOAD data or enter NEW data before SAVING.");
			}
			else
			{
				proceed = SaveEntryDisclaimer();

				if (proceed)
				{
					filename = PromptForFilename();
                    // TODO: call the SaveSalesFile method here
                    SaveSalesFile(filename, sales, dates, count);
                    Console.WriteLine($"Data saved to {filename}");
                }
				else
				{
					Console.WriteLine("Cancelling save operation. Returning to MAIN MENU.");
				}
			}
			break;
		case "E": //[E]dit Sales Entries
			if (count == 0)
			{
				Console.WriteLine("Sorry, LOAD data or enter NEW data before EDITING.");
			}
			else
			{
				proceed = EditEntryDisclaimer();

				if (proceed)
				{
                    // TODO: call the EditEntries method here
                    EditEntries(sales, dates, count);
                }
				else
				{
					Console.WriteLine("Cancelling EDIT operation. Returning to MAIN MENU.");
				}
			}
			break;
		case "L": //[L]oad Sales File
			proceed = LoadEntryDisclaimer();
			if (proceed)
			{
				filename = Prompt("Enter name of file to load: ");
				// TODO: uncomment the following and call the LoadSalesFile method below
				count = LoadSalesFile(filename, sales, dates);
                Console.WriteLine($"{count} records were loaded.");
				Console.WriteLine();
			}
			else
			{
				Console.WriteLine("Cancelling LOAD operation. Returning to MAIN MENU.");
			}
			break;
		case "V":
			if (count == 0)
			{
				Console.WriteLine("Sorry, LOAD data or enter NEW data before VIEWING.");
			}
			else
			{
                // TODO: call the DisplayEntries method here
                DisplayEntries(sales, dates);
            }
			break;
		case "M": //[M]onthly Statistics
			if (count == 0)
			{
				Console.WriteLine("Sorry, LOAD data or enter NEW data before ANALYSIS.");
			}
			else
			{
				displayAnalysisMenu = true;
				while (displayAnalysisMenu)
				{
                    // TODO: call the DisplayAnalysisMenu here
                    DisplayAnalysisMenu();

                    analysisMenuChoice = Prompt("Enter ANALYSIS sub-menu option: ").ToUpper();
					Console.WriteLine();

					switch (analysisMenuChoice)
					{
						case "A": //[A]verage Sales
								  // TODO: uncomment the following and call the Mean method below
							mean = mean(sales);
							month = dates[0].Substring(0, 3);
							year = dates[0].Substring(7, 4);
							Console.WriteLine($"The mean sales for {month} {year} is: {mean:C}");
							Console.WriteLine();
							break;
						case "H": //[H]ighest Sales
								  // TODO: uncomment the following and call the Largest method below
							largest = largest(sales);
							month = dates[0].Substring(0, 3);
							year = dates[0].Substring(7, 4);
							Console.WriteLine($"The largest sales for {month} {year} is: {largest:C}");
							Console.WriteLine();
							break;
						case "L": //[L]owest Sales
								  // TODO: uncomment the following and call the Smallest method below
							smallest = smallest(sales);
							month = dates[0].Substring(0, 3);
							year = dates[0].Substring(7, 4);
							Console.WriteLine($"The smallest sales for {month} {year} is: {smallest:C}");
							Console.WriteLine();
							break;
						case "G": //[G]raph Sales
								  // TODO: call the DisplayChart method below
							DisplayChart(sales);
							Prompt("Press <enter> to continue...");
							break;
						case "R": //[R]eturn to MAIN MENU
							displayAnalysisMenu = false;
							break;
						default: //invalid entry. Reprompt.
							Console.WriteLine("Invalid reponse. Enter one of the letters to choose a submenu option.");
							break;
					}
				}
			}
			break;
		case "D": //[D]isplay Main Menu
				  // TODO: call the DisplayMainMenu method
			displayMainMenu();
			break;
		case "Q": //[Q]uit Program
			quit = Prompt("Are you sure you want to quit (y/N)? ").ToLower().Equals("y");
			Console.WriteLine();
			if (quit)
			{
				displayMainMenu = false;
			}
			break;
		default: //invalid entry. Reprompt.
			Console.WriteLine("Invalid reponse. Enter one of the letters to choose a menu option.");
			break;
	}
}

DisplayProgramOutro();

// ================================================================================================ //
//                                                                                                  //
//                                              METHODS                                             //
//                                                                                                  //
// ================================================================================================ //

// ++++++++++++++++++++++++++++++++++++ Difficulty 1 ++++++++++++++++++++++++++++++++++++

// TODO: create the Prompt method
static string Prompt(string message)
{
    Console.Write(message);
    return Console.ReadLine();  // Returns user input as a string
}

// TODO: create the PromptDouble method
// The method must always return a double and should not crash the program.
static double PromptDouble(string message)
{
    double result;
    while (true)
    {
        Console.Write(message);
        if (double.TryParse(Console.ReadLine(), out result)) return result;
        else Console.WriteLine("Invalid input. Please enter a valid number.");
    }
}

// TODO: create the DisplayMainMenu method
// the menu must consist of the following options:
// 
// [N]ew Daily Sales Entry
// [S]ave Entries to File
// [E]dit Sales Entries
// [L]oad Sales File
// [V]iew Entered/Loaded Sales
// [M]onthly Statistics
// [D]isplay Main Menu
// [Q]uit Program
static void DisplayMainMenu()
{
    Console.WriteLine("Main Menu:");
    Console.WriteLine("[N] New Daily Sales Entry");
    Console.WriteLine("[S] Save Entries to File");
    Console.WriteLine("[E] Edit Sales Entries");
    Console.WriteLine("[L] Load Sales File");
    Console.WriteLine("[V] View Entered/Loaded Sales");
    Console.WriteLine("[M] Monthly Statistics");
    Console.WriteLine("[D] Display Main Menu");
    Console.WriteLine("[Q] Quit Program");
}

// TODO: create the DisplayAnalysisMenu method
// the menu must consist of the following options:
//
// [A]verage Sales
// [H]ighest Sales
// [L]owest Sales
// [G]raph Sales
// [R]eturn to MAIN MENU
static void DisplayAnalysisMenu()
{
    Console.WriteLine("Analysis Menu:");
    Console.WriteLine("[A] Average Sales");
    Console.WriteLine("[H] Highest Sales");
    Console.WriteLine("[L] Lowest Sales");
    Console.WriteLine("[G] Graph Sales");
    Console.WriteLine("[R] Return to MAIN MENU");
}


// TODO: create the Largest method
static double Largest(List<double> sales)
{
    return sales.Max();
}

// TODO: create the Smallest method
static double Smallest(List<double> sales)
{
    return sales.Min();
}

// TODO: create the Mean method
static double Mean(List<double> sales)
{
    return sales.Average();
}

// ++++++++++++++++++++++++++++++++++++ Difficulty 2 ++++++++++++++++++++++++++++++++++++


// TODO: create the DisplayEntries method
static void DisplayEntries(List<double> sales)
{
    if (sales.Count == 0)
    {
        Console.WriteLine("No sales entries to display.");
    }
    else
    {
        Console.WriteLine("Sales Entries:");
        foreach (var sale in sales)
        {
            Console.WriteLine($"{sale:C}");  // Display sales in currency format
        }
    }
}

// TODO: create the EnterSales method
// Method to enter sales and dates
// Method to enter sales and dates
// Modify the EnterSales method for arrays
static int EnterSales(double[] sales, string[] dates, ref int currentIndex)
{
    bool isValid = false;
    double saleAmount;
    string dateEntry;

    // Continue prompting until a valid sale is entered
    while (!isValid && currentIndex < sales.Length)
    {
        // Prompt user for sales entry
        string input = Prompt("Enter the sales amount for the day: ");

        // Try to convert input to a double
        isValid = double.TryParse(input, out saleAmount) && saleAmount >= 0;

        if (isValid)
        {
            // Add the valid sale to the sales array
            sales[currentIndex] = saleAmount;

            // Prompt for the date entry
            dateEntry = Prompt("Enter the date for this sale (e.g., MM/DD/YYYY): ");
            dates[currentIndex] = dateEntry;

            Console.WriteLine($"Sales of {saleAmount:C} added for {dateEntry}.");

            currentIndex++; // Increment the index for the next sale entry
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a positive number for the sales amount.");
        }
    }
    // Return the number of sales entries made
    return currentIndex;
}

// TODO: create the LoadSalesFile method
static void LoadSalesFile(List<double> sales)
{
    string fileName = Prompt("Enter the file path to load sales from: ");

    try
    {
        // Read all lines from the file
        string[] lines = File.ReadAllLines(fileName);

        // Clear the existing sales list before loading new data
        sales.Clear();

        // Convert each line into a double and add to sales list
        foreach (string line in lines)
        {
            if (double.TryParse(line, out double saleAmount))
            {
                sales.Add(saleAmount);
            }
            else
            {
                Console.WriteLine($"Skipping invalid entry: {line}");
            }
        }
        Console.WriteLine("Sales data loaded successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error loading file: {ex.Message}");
    }
}


// TODO: create the SaveSalesFile method
static void SaveSalesFile(List<double> sales)
{
    string fileName = Prompt("Enter the file path to save sales to: ");

    try
    {
        // Write each sale amount to the file
        File.WriteAllLines(fileName, sales.Select(s => s.ToString()));

        Console.WriteLine("Sales data saved successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error saving file: {ex.Message}");
    }
}


// ++++++++++++++++++++++++++++++++++++ Difficulty 3 ++++++++++++++++++++++++++++++++++++

// TODO: create the EditEntries method
static void EditEntries(List<double> sales)
{
    if (sales.Count == 0)
    {
        Console.WriteLine("No sales entries to edit.");
        return;
    }

    // Display current entries
    Console.WriteLine("Current Sales Entries:");
    for (int i = 0; i < sales.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {sales[i]:C}");
    }

    int index;
    bool validIndex = false;

    // Prompt the user for a valid index
    while (!validIndex)
    {
        string input = Prompt("Enter the number of the entry you wish to edit: ");
        validIndex = int.TryParse(input, out index) && index >= 1 && index <= sales.Count;

        if (!validIndex)
        {
            Console.WriteLine("Invalid entry. Please enter a number corresponding to an existing sale.");
        }
    }

    // Prompt for the new sales value
    double newSaleAmount = PromptDouble("Enter the new sales amount: ");
    sales[index - 1] = newSaleAmount;  // Update the sale at the specified index

    Console.WriteLine($"Sales entry {index} updated to {newSaleAmount:C}");
}


// ++++++++++++++++++++++++++++++++++++ Difficulty 4 ++++++++++++++++++++++++++++++++++++

// TODO: create the DisplaySalesChart method
static void DisplaySalesChart(List<double> sales)
{
    if (sales.Count == 0)
    {
        Console.WriteLine("No sales data to display.");
        return;
    }

    Console.WriteLine("Sales Bar Chart:");
    double maxSale = sales.Max(); // Find the highest sales value to normalize the chart

    foreach (double sale in sales)
    {
        // Normalize the sale value to fit within a bar length (e.g., 50 characters max for the highest sale)
        int barLength = (int)((sale / maxSale) * 50);

        // Print a line with the sale and the corresponding bar
        Console.WriteLine($"{sale:C}: {new string('*', barLength)}");
    }

    Console.WriteLine("\nPress <Enter> to continue...");
    Console.ReadLine();
}


// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// ++++++++++++++++++++++++++++++++++++ Additional Provided Methods ++++++++++++++++++++++++++++++++++++
// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

// NOTE: Many of the following methods depend on the Prompt method and will operate correctly once
// that method has been implemented.

/// <summary>
/// Displays the Program intro.
/// </summary>
static void DisplayProgramIntro()
{
	Console.WriteLine("========================================");
	Console.WriteLine("=                                      =");
	Console.WriteLine("=            Monthly  Sales            =");
	Console.WriteLine("=                                      =");
	Console.WriteLine("========================================");
	Console.WriteLine();
}

/// <summary>
/// Displays the Program outro.
/// </summary>
static void DisplayProgramOutro()
{
	Console.Write("Program terminated. Press ENTER to exit program...");
	Console.ReadLine();
}

/// <summary>
/// Displays a disclaimer for NEW entry option.
/// </summary>
/// <returns>Boolean, if user wishes to proceed (true) or not (false).</returns>
static bool NewEntryDisclaimer()
{
	bool response;
	Console.WriteLine("Disclaimer: proceeding will overwrite all unsaved data.");
	Console.WriteLine("Hint: Select EDIT from the main menu instead, to change individual days.");
	Console.WriteLine("Hint: You'll need to enter data for the whole month.");
	Console.WriteLine();
	response = Prompt("Do you wish to proceed anyway? (y/N) ").ToLower().Equals("y");
	Console.WriteLine();
	return response;
}

/// <summary>
/// Displays a disclaimer for SAVE entry option.
/// </summary>
/// <returns>Boolean, if user wishes to proceed (true) or not (false).</returns>
static bool SaveEntryDisclaimer()
{
	bool response;
	Console.WriteLine("Disclaimer: saving to an EXISTING file will overwrite data currently on that file.");
	Console.WriteLine("Hint: Files will be saved to this program's directory by default.");
	Console.WriteLine("Hint: If the file does not yet exist, it will be created.");
	Console.WriteLine();
	response = Prompt("Do you wish to proceed anyway? (y/N) ").ToLower().Equals("y");
	Console.WriteLine();
	return response;
}

/// <summary>
/// Displays a disclaimer for EDIT entry option.
/// </summary>
/// <returns>Boolean, if user wishes to proceed (true) or not (false).</returns>
static bool EditEntryDisclaimer()
{
	bool response;
	Console.WriteLine("Disclaimer: editing will overwrite unsaved sales values.");
	Console.WriteLine("Hint: Save to a file before editing.");
	Console.WriteLine();
	response = Prompt("Do you wish to proceed anyway? (y/N) ").ToLower().Equals("y");
	Console.WriteLine();
	return response;
}

/// <summary>
/// Displays a disclaimer for LOAD entry option.
/// </summary>
/// <returns>Boolean, if user wishes to proceed (true) or not (false).</returns>
static bool LoadEntryDisclaimer()
{
	bool response;
	Console.WriteLine("Disclaimer: proceeding will overwrite all unsaved data.");
	Console.WriteLine("Hint: If you entered New Daily sales entries, save them first!");
	Console.WriteLine();
	response = Prompt("Do you wish to proceed anyway? (y/N) ").ToLower().Equals("y");
	Console.WriteLine();
	return response;
}

/// <summary>
/// Displays prompt for a filename, and returns a valid filename. 
/// Includes exception handling.
/// </summary>
/// <returns>User-entered string, representing valid filename (.txt or .csv)</returns>
static string PromptForFilename()
{
	string filename = "";
	bool isValidFilename = true;
	const string CsvFileExtension = ".csv";
	const string TxtFileExtension = ".txt";

	do
	{
		filename = Prompt("Enter name of .csv or .txt file to save to (e.g. JAN-2024-sales.csv): ");
		if (filename == "")
		{
			isValidFilename = false;
			Console.WriteLine("Please try again. The filename cannot be blank or just spaces.");
		}
		else
		{
			if (!filename.EndsWith(CsvFileExtension) && !filename.EndsWith(TxtFileExtension)) //if filename does not end with .txt or .csv.
			{
				filename = filename + CsvFileExtension; //append .csv to filename
				Console.WriteLine("It looks like your filename does not end in .csv or .txt, so it will be treated as a .csv file.");
				isValidFilename = true;
			}
			else
			{
				Console.WriteLine("It looks like your filename ends in .csv or .txt, which is good!");
				isValidFilename = true;
			}
		}
	} while (!isValidFilename);
	return filename;
}
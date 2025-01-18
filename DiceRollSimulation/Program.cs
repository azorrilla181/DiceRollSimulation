// See https://aka.ms/new-console-template for more information
using System;

internal class DiceRollSimulation
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the dice throwing simulator!");
        Console.Write("\nHow many dice rolls would you like to simulate? ");

        if (int.TryParse(Console.ReadLine(), out int numRolls) && numRolls > 0)
        {
            // Create an instance of the DiceSimulator class
            DiceSimulator simulator = new DiceSimulator();

            // Call the method to simulate dice rolls and get the results
            int[] rollResults = simulator.SimulateRolls(numRolls);

            // Display the simulation results
            PrintHistogram(rollResults, numRolls);

            Console.WriteLine("\nThank you for using the dice throwing simulator. Goodbye!");
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a positive integer.");
        }
    }

    static void PrintHistogram(int[] rollResults, int totalRolls)
    {
        const int totalAsterisks = 100; // Total asterisks to print
        int[] asteriskCounts = new int[13];
        int totalPrintedAsterisks = 0;

        // Calculate initial asterisk counts based on percentages
        for (int sum = 2; sum <= 12; sum++)
        {
            double percentage = (double)rollResults[sum] / totalRolls * totalAsterisks;
            asteriskCounts[sum] = (int)Math.Floor(percentage); // Floor to avoid partial asterisks
            totalPrintedAsterisks += asteriskCounts[sum];
        }

        // Distribute leftover asterisks
        int leftoverAsterisks = totalAsterisks - totalPrintedAsterisks;
        while (leftoverAsterisks > 0)
        {
            int maxIndex = FindMaxIndex(rollResults, asteriskCounts);
            asteriskCounts[maxIndex]++;
            leftoverAsterisks--;
        }

        // Print the formatted histogram
        Console.WriteLine("\nDICE ROLLING SIMULATION RESULTS");
        Console.WriteLine("Each \"*\" represents 1% of the total number of rolls.");
        Console.WriteLine($"Total number of rolls = {totalRolls}.\n");

        for (int sum = 2; sum <= 12; sum++)
        {
            Console.Write($"{sum,2}: ");
            Console.WriteLine(new string('*', asteriskCounts[sum]));
        }
    }

    static int FindMaxIndex(int[] rollResults, int[] asteriskCounts)
    {
        int maxIndex = 2;
        double maxFraction = 0;

        for (int sum = 2; sum <= 12; sum++)
        {
            double fraction = ((double)rollResults[sum] / rollResults.Sum()) - (double)asteriskCounts[sum] / 100;
            if (fraction > maxFraction)
            {
                maxFraction = fraction;
                maxIndex = sum;
            }
        }

        return maxIndex;
    }
}

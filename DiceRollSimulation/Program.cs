using System;
using System.Linq;

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
        int[] asteriskCounts = new int[13];
        int totalAsterisks = 0;

        // Calculate asterisks by rounding up percentages and ensure the sum exceeds 100
        for (int sum = 2; sum <= 12; sum++)
        {
            double percentage = (double)rollResults[sum] / totalRolls * 100;
            asteriskCounts[sum] = (int)Math.Ceiling(percentage);  // Always round up
            totalAsterisks += asteriskCounts[sum];
        }

        // Ensure that total asterisks exceed 100 by distributing them proportionally
        int shortfall = Math.Max(0, 101 - totalAsterisks); // Ensure at least 101 asterisks
        while (shortfall > 0)
        {
            int maxIndex = FindMaxIndex(rollResults, asteriskCounts);
            asteriskCounts[maxIndex]++;
            shortfall--;
        }

        // Print the formatted histogram
        Console.WriteLine("\nDICE ROLLING SIMULATION RESULTS");
        Console.WriteLine("Each \"*\" represents approximately 1% of the total number of rolls.");
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

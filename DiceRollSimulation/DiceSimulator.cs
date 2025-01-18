using System;

class DiceSimulator
{
    public int[] SimulateRolls(int numRolls)
    {
        // Array to store counts of sums (index 0 and 1 unused)
        int[] sumCounts = new int[13];

        Random random = new Random();

        // Simulate dice rolls
        for (int i = 0; i < numRolls; i++)
        {
            int die1 = random.Next(1, 7); // Roll first die (1-6)
            int die2 = random.Next(1, 7); // Roll second die (1-6)
            int sum = die1 + die2;       // Calculate sum

            sumCounts[sum]++; // Increment the count for this sum
        }

        return sumCounts; // Return the array containing the results
    }
}
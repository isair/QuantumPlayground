using System;

using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Simulation.Simulators;

using Quantum.Playground;

namespace Quantum.Playground
{
  class Driver
  {
    static void Main(string[] args)
    {
      using (var qsim = new QuantumSimulator())
      {
        #region Properties

        const int runCount = 1000;

        // Results table.
        const String columns = "           q0      q1";
        String[] rows = { "|0> count:", "|1> count:" };
        int[] values = { 0, 0, 0, 0 }; // Refer to columns & rows.

        int agreeCount = 0;

        long qBitCount = 0;

        #endregion

        #region Result Gathering

        for (var i = 0; i < runCount; i++)
        {
          var (resultSize, resultsRaw) = PlaygroundMain.Run(qsim).Result;

          qBitCount = resultSize;

          var qBitValues = resultsRaw.ToArray();

          if (qBitValues[0] == 0)
          {
            values[0] += 1;
          }
          else
          {
            values[2] += 1;
          }

          if (qBitValues[1] == 0)
          {
            values[1] += 1;
          }
          else
          {
            values[3] += 1;
          }

          if (qBitValues[0] == qBitValues[1])
          {
            agreeCount += 1;
          }
        }

        #endregion

        #region Console Output

        System.Console.WriteLine($"Run count: {runCount}\n");

        System.Console.WriteLine(columns);
        System.Console.WriteLine($"{rows[0]} {values[0],-4}    {values[1],-4}");
        System.Console.WriteLine($"{rows[1]} {values[2],-4}    {values[3],-4}");

        var agreeRatio = (float)agreeCount / runCount;

        System.Console.WriteLine($"\nq0-q1 agree ratio: {(int)(agreeRatio * 100),-4}%");

        #endregion
      }
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace shift_picker
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("Ok, let's start by telling me something...");
         Console.WriteLine("What about your team, who's in it? Write one name per line, end with an empty line:");
         var names = new List<string>();
         while (ReadNonEmptyLine(out var name))
         {
            names.Add(name);
         }

         Console.WriteLine();
         Console.WriteLine($"{names.Count} people, then! That's good!");
         Console.WriteLine("Now, what about the shift you people have to do? How many shifts you have to do?");

         // TODO: Add capability of managing shifts with more than one person or more shifts than persons
         int shifts = names.Count;
         //while (!Readnumber(out shifts))
         //{
         //   Console.WriteLine("Oops, that seems not a number to me!");
         //}

         Console.WriteLine($"Ok, {shifts} shifts are to go!");
         Console.WriteLine(
            $"Now give me some time to make my magic! Remember, I will randomly arrange people in shifts, the shifts combination that is selected for three times wins...");

         var result = PickShifts(names, shifts);
         Console.WriteLine("Aaand here's the results:");
         Console.WriteLine(string.Join(Environment.NewLine, result.Split(',').Select(x => x.Trim())));
         Console.WriteLine();
         Console.WriteLine("Press any key to exit!");
         Console.ReadKey();
      }

      private static string PickShifts(List<string> names, int shifts)
      {
         var shiftsPool = new List<Shift>();
            Random rnd = new Random();
         do
         {
            var shift = string.Join(", ", names.OrderBy(x => rnd.NextDouble()).ToArray());
            var inpool = shiftsPool.FirstOrDefault(x => x.Item1 == shift);
            if (inpool == null)
            {
               shiftsPool.Add(new Shift(shift, 1));
            }
            else
            {
               inpool.Item2++;
            }

         } while (shiftsPool.All(x => x.Item2 < 3));

         return shiftsPool.First(x => x.Item2 >= 3).Item1;
      }

      private static bool Readnumber(out int shifts)
      {
         return int.TryParse(Console.ReadLine(), out shifts);
      }

      private static bool ReadNonEmptyLine(out string name)
      {
         name = Console.ReadLine();
         return !string.IsNullOrWhiteSpace(name);
      }
   }

   internal class Shift
   {
      public Shift(string shift, int v)
      {
         Item1 = shift;
         Item2 = v;
      }

      public string Item1 { get; set; }
      public int Item2 { get; set; }
   }

}

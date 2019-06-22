using System;
using System.Collections.Generic;
using System.Linq;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 },
                new[] { 2, 8 },
                new[] { 5, 2 },
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" },
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 },
                new[] { 2, 8, 5, 1 },
                new[] { 5, 2, 4, 4 },
                new[] { "tFc", "tF", "Ftc" },
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 },
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 },
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 },
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" },
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            List<int> corresPondingMeals = new List<int>();
            int[] calorie = GetCalorieDetails(protein, carbs, fat);

            foreach (var dietPlan in dietPlans)
            {
                if (dietPlan.Length == 0)
                {
                    corresPondingMeals.Add(0);
                    continue;
                }

                var matchedMeals = new List<int>();

                for (int i = 0; i < protein.Length; i++)
                {
                    matchedMeals.Add(i);
                }

                foreach (var specification in dietPlan)
                {
                    matchedMeals = FindMatchingMeals(protein, carbs, fat, calorie, specification, matchedMeals);

                    if (matchedMeals.Count == 1 || specification == dietPlan.Last())
                    {
                        corresPondingMeals.Add(matchedMeals.FirstOrDefault());
                        break;
                    }
                }
            }

            return corresPondingMeals.ToArray();
        }

        public static List<int> FindMatchingMeals(int[] protein, int[] carbs, int[] fat, int[] calorie, char specification, List<int> matchedMeals)
        {
            switch (specification)
            {
                case 'C':
                    return GetMealWithMaxValue(carbs, matchedMeals);

                case 'c':
                    return GetMealWithMinValue(carbs, matchedMeals);

                case 'P':
                    return GetMealWithMaxValue(protein, matchedMeals);

                case 'p':
                    return GetMealWithMinValue(protein, matchedMeals);

                case 'F':
                    return GetMealWithMaxValue(fat, matchedMeals);

                case 'f':
                    return GetMealWithMinValue(fat, matchedMeals);

                case 'T':
                    return GetMealWithMaxValue(calorie, matchedMeals);

                case 't':
                    return GetMealWithMinValue(calorie, matchedMeals);

                default:
                    break;
            }

            return new List<int>();
        }

        public static List<int> GetMealWithMaxValue(int[] valueArray, List<int> matchedMeals)
        {
            int maxValue = int.MinValue;
            List<int> resultIndex = new List<int>();

            for (int i = 0; i < valueArray.Length; i++)
            {
                if (matchedMeals.Contains(i))
                {
                    if (valueArray[i] == maxValue)
                    {
                        resultIndex.Add(i);
                    }

                    if (valueArray[i] > maxValue)
                    {
                        maxValue = valueArray[i];
                        resultIndex.Clear();
                        resultIndex.Add(i);
                    }

                }
            }

            return resultIndex;
        }
        public static List<int> GetMealWithMinValue(int[] valueArray, List<int> matchedMeals)
        {
            int minValue = int.MaxValue;
            List<int> resultIndex = new List<int>();

            for (int i = 0; i < valueArray.Length; i++)
            {
                if (matchedMeals.Contains(i))
                {
                    if (valueArray[i] == minValue)
                    {
                        resultIndex.Add(i);
                    }

                    if (valueArray[i] < minValue)
                    {
                        minValue = valueArray[i];
                        resultIndex.Clear();
                        resultIndex.Add(i);
                    }
                }
            }

            return resultIndex;
        }

        public static int[] GetCalorieDetails(int[] protein, int[] carbs, int[] fat)
        {
            int[] calorie = new int[protein.Length];

            for (int i = 0; i < calorie.Length; i++)
            {
                calorie[i] = 9 * fat[i] + 5 * (protein[i] + carbs[i]);
            }

            return calorie;
        }
    }
}

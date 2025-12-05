using AoC2025.Day5.Properties;
using System;

namespace AoC2025.Day5;

internal static class InputParser
{
    public static IngredientDatabase ParseInput()
    {

        var inputString = System.Text.Encoding.UTF8.GetString(Resources.InputDay5a);
        //var inputString =
//@"3-5
//10-14
//16-20
//12-18

//1
//5
//8
//11
//17
//32";

        var lines = inputString.Split(Environment.NewLine);
        List<ulong> availableIngredients = [];
        List<InputRange> freshIngredients = [];

        foreach(var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            if (line.Contains('-'))
            {
                var boundary = line.Split('-');
                var range = new InputRange(ulong.Parse(boundary[0]), ulong.Parse(boundary[1]));
                freshIngredients.Add(range);
            } 
            else
            {
                availableIngredients.Add(ulong.Parse(line));
            }
        }

        return new IngredientDatabase(freshIngredients, availableIngredients);
    }
}

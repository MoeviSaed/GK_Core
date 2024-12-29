using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DG.Tweening;
using EntryPointSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public static class Constants
{
    private static StringBuilder _builder = new StringBuilder();

    public static (float cellSize, float padding) CalculateCellSize(float width, float height, int columns, int rows)
    {
        float paddingPercent = 0.08f;

        float cellWidth = width / (columns + (columns + 1) * paddingPercent);
        float cellHeight = height / (rows + (rows + 1) * paddingPercent);

        float cellSize = Mathf.Min(cellWidth, cellHeight);

        // Padding — 6% от размера ячейки
        float padding = cellSize * paddingPercent;

        return (cellSize, padding);
    }

    
    /// <summary>
    /// Получить цвет для заданной буквы из lettersMatrix.
    /// </summary>
    /// <param name="letter">Буква, для которой требуется найти цвет.</param>
    /// <returns>Цвет в формате Color.</returns>
    public static Color ConvertHexToColor(string hexColor)
    {
        if (ColorUtility.TryParseHtmlString(hexColor, out Color parsedColor))
        {
            return parsedColor;
        }
        
        return Color.white;
    }
    
    public static string[] SplitWords(string quote)
    {
        return quote.Split(new[] { ' ', '-' }, StringSplitOptions.RemoveEmptyEntries);
    }
    
    public static void ShuffleList<T>(this List<T> array)
    {
        var random = new System.Random();
        for (int i = array.Count - 1; i >= 1; i--)
        {
            int j = random.Next(i + 1);
            (array[j], array[i]) = (array[i], array[j]);
        }
    }

    public static void AnimateText(this TextMeshProUGUI textElement, 
        int startValue, int endValue, string suffix, string additionalSuffix = "")
    {
        DOTween.To(() => startValue, 
            x => textElement.text = suffix + x + additionalSuffix, endValue, 1.0f).SetEase(Ease.Linear);
    }
    
    public static void AnimateText(this TextMeshProUGUI textElement, int startValue, int endValue, string additionalSuffix = "")
    {
        DOTween.To(() => startValue, x => textElement.text = x + additionalSuffix, endValue, 1.0f).SetEase(Ease.Linear);
    }
    public static T[] ShuffleArray<T>(this T[] array)
    {
        System.Random random = new System.Random();
        for (int i = array.Length - 1; i >= 1; i--)
        {
            int j = random.Next(i + 1);
            (array[j], array[i]) = (array[i], array[j]);
        }

        return array;
    }

    public static string GenerateRandomKey(int limit = 6)
    {
        var chars = "123456789";
        var stringChars = new char[limit];
        var random = new System.Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        return new String(stringChars);
    }

    public static string GetTime(float value)
    {
        _builder.Clear();

        string firstPart = ((int)(value / 60)).ToString();
        if (firstPart.Length < 2) _builder.Append("0");
        _builder.Append(firstPart);

        _builder.Append(":");

        string secondPart = (value % 60).ToString();
        if (secondPart.Length > 1) _builder.Append(secondPart[0]);
        else _builder.Append("0");
        _builder.Append(value % 10);

        return _builder.ToString();
    }

    public static T GetRandom<T>(this List<T> array)
    {
        if (array.Count == 0) return default;
        return array[Random.Range(0, array.Count)];
    }

    public static T GetRandom<T>(this T[] array)
    {
        if (array.Length == 0) return default;
        return array[Random.Range(0, array.Length)];
    }

    public static float MakeModule(this float value)
    {
        return value < 0 ? value * -1 : value;
    }

    public static int MakeModule(this int value)
    {
        return value < 0 ? value * -1 : value;
    }

    public static float FloatParse(this string value)
    {
        if (!double.TryParse(value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var result))
            double.TryParse(value, out result);

        return (float)result;
    }

    public static int GetDifferenceBetweenDays(this DateTime largerDate, DateTime lessDate)
    {
        if (largerDate.Year == lessDate.Year)
        {
            if (largerDate.DayOfYear == lessDate.DayOfYear) return 0;
            else return largerDate.DayOfYear - lessDate.DayOfYear;
        }
        else return (largerDate.DayOfYear + 365) - lessDate.DayOfYear;
    }

    public static void SetActive(this GameObject[] gameObjects, bool state)
    {
        for (int i = 0; i < gameObjects.Length; i++)
            gameObjects[i].SetActive(state);
    }

    public static void SetActive(this MonoBehaviour[] gameObjects, bool state)
    {
        for (int i = 0; i < gameObjects.Length; i++)
            gameObjects[i].gameObject.SetActive(state);
    }
}

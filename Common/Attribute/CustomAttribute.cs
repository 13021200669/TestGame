using UnityEngine;

//×Ô¶¨ÒåPropertyAttribute
public class Label : PropertyAttribute
{
    public readonly string LabelTranslateText;
    public readonly bool UseRange = false;
    public readonly float MinValue;
    public readonly float MaxValue;

    public Label(string text)
    {
        LabelTranslateText = text;
    }

    public Label(string text, float min, float max)
    {
        LabelTranslateText = text + "[" + min + "," + max + "]";
        MinValue = min;
        MaxValue = max;
        UseRange = true;
    }
}

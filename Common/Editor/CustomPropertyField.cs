using UnityEditor;

public class CustomProperty
{
    public string label;
}

public class CustomProperty_Property : CustomProperty
{
    public SerializedProperty property;

    public float LabelWidth;
}

public class CustomProperty_Toggle : CustomProperty
{

}

public class CustomProperty_Slider : CustomProperty
{

}

public class CustomProperty_MinMaxSlider : CustomProperty
{

}
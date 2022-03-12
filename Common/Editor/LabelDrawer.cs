using UnityEngine;
using UnityEditor;

//自定义PropertyAttribute - 实现
[CustomPropertyDrawer(typeof(Label))]
public class LabelTranslateDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //获取Attribute标记的Property
        Label variable = attribute as Label;

        //修改Property的label内容
        label.text = variable.LabelTranslateText;

        //绘制EditorGUI
        if (variable.UseRange)
        {
            EditorGUI.Slider(position, property, variable.MinValue, variable.MaxValue, label);
        }
        else
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }
}

using UnityEngine;
using UnityEditor;

//自定义PropertyAttribute - 实现
[CustomPropertyDrawer(typeof(Label))]
public class LabelTranslateDrawer : PropertyDrawer
{
    public float FontHeight = 18f;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //获取Attribute标记的Property
        Label variable = attribute as Label;

        //修改Property的label内容
        label.text = variable.LabelTranslateText;

        //绘制EditorGUI
        if (variable.UseRange)
        {
            Rect rect1 = new Rect(position.x, position.y, 200, FontHeight);
            Rect rect2 = new Rect(position.x + rect1.width, position.y, 200, FontHeight);
            EditorGUI.LabelField(rect1, label);
            EditorGUI.Slider(rect2, property, variable.MinValue, variable.MaxValue, new GUIContent());
        }
        else
        {
            Rect rect1 = new Rect(position.x, position.y, 200, FontHeight);
            Rect rect2 = new Rect(position.x + rect1.width, position.y, 200, FontHeight);
            EditorGUI.LabelField(rect1, label);
            EditorGUI.PropertyField(rect2, property, new GUIContent());
        }
    }
}

using UnityEngine;
using UnityEditor;

//�Զ���PropertyAttribute - ʵ��
[CustomPropertyDrawer(typeof(Label))]
public class LabelTranslateDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //��ȡAttribute��ǵ�Property
        Label variable = attribute as Label;

        //�޸�Property��label����
        label.text = variable.LabelTranslateText;

        //����EditorGUI
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

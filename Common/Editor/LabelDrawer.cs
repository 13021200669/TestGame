using UnityEngine;
using UnityEditor;

//�Զ���PropertyAttribute - ʵ��
[CustomPropertyDrawer(typeof(Label))]
public class LabelTranslateDrawer : PropertyDrawer
{
    public float FontHeight = 18f;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //��ȡAttribute��ǵ�Property
        Label variable = attribute as Label;

        //�޸�Property��label����
        label.text = variable.LabelTranslateText;

        //����EditorGUI
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

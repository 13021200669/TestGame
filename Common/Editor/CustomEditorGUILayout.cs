using UnityEngine;
using UnityEditor;

namespace CustomEditorGUI
{
    public static class CustomEditorGUILayout
    {
        const float HorizontalHeight = 20;
        const float LabelWidth = 60;
        const float PropertyFieldMinWidth = 140;
        const float InLineSpace = 10;
        const float MinMaxSliderFloatFieldWidth = 40;

        /// <summary>
        /// ���е�Property
        /// </summary>
        /// <param name="label"></param>
        /// <param name="property"></param>
        /// <param name="GUIOptions"></param>
        public static void CustomField_Toggle(string label,Object target, ref bool isTrue)
        {
            EditorGUILayout.BeginHorizontal(GUILayout.Height(HorizontalHeight));

            EditorGUILayout.LabelField(label, GUILayout.Width(LabelWidth));

            EditorGUI.BeginChangeCheck();

            bool tempValue = isTrue;
            tempValue = EditorGUILayout.Toggle(new GUIContent(), tempValue, GUILayout.MinWidth(PropertyFieldMinWidth));

            //���property�仯
            if (EditorGUI.EndChangeCheck())
            {
                //�Գ�����Ǵ�����״̬
                Undo.RecordObject(target, "Toggle Change");

                //��ȡToggle��ֵ������property
                isTrue = tempValue;

                //public static void RecordObject (Object objectToUndo, string name)
                //���� objectToUndo ΪԤ�Ƽ�ʵ�����������ȷ����ʵ��
                //������ RecordObject ֮����� PrefabUtility.RecordPrefabInstancePropertyModifications
                PrefabUtility.RecordPrefabInstancePropertyModifications(target);
            }

            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// ���е�Property
        /// </summary>
        /// <param name="label"></param>
        /// <param name="property"></param>
        /// <param name="GUIOptions"></param>
        public static void CustomField_Property(string label, SerializedProperty property)
        {
            EditorGUILayout.BeginHorizontal(GUILayout.Height(HorizontalHeight));

            EditorGUILayout.LabelField(label, GUILayout.Width(LabelWidth));
            EditorGUILayout.PropertyField(property, new GUIContent(), GUILayout.MinWidth(PropertyFieldMinWidth));

            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// ����˫Property
        /// </summary>
        /// <param name="label1"></param>
        /// <param name="property1"></param>
        /// <param name="label2"></param>
        /// <param name="property2"></param>
        /// <param name="GUIOptions"></param>
        public static void CustomField_Property(string label1, SerializedProperty property1, string label2, SerializedProperty property2)
        {
            EditorGUILayout.BeginHorizontal(GUILayout.Height(HorizontalHeight));

            EditorGUILayout.LabelField(label1, GUILayout.Width(LabelWidth));
            EditorGUILayout.PropertyField(property1, new GUIContent(), GUILayout.MinWidth(PropertyFieldMinWidth));

            GUILayout.Space(InLineSpace);

            EditorGUILayout.LabelField(label2, GUILayout.Width(LabelWidth));
            EditorGUILayout.PropertyField(property2, new GUIContent(), GUILayout.MinWidth(PropertyFieldMinWidth));

            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// ���е�Slider
        /// </summary>
        /// <param name="label"></param>
        /// <param name="property"></param>
        /// <param name="minLimit"></param>
        /// <param name="maxLimit"></param>
        /// <param name="GUIOptions"></param>
        public static void CustomField_Slider(string label, SerializedProperty property, float minLimit, float maxLimit)
        {
            EditorGUILayout.BeginHorizontal(GUILayout.Height(HorizontalHeight));

            EditorGUILayout.LabelField(label, GUILayout.Width(LabelWidth));
            EditorGUILayout.Slider(property, minLimit, maxLimit, new GUIContent(), GUILayout.MinWidth(PropertyFieldMinWidth));

            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// ����˫Slider
        /// </summary>
        /// <param name="label1"></param>
        /// <param name="property1"></param>
        /// <param name="label2"></param>
        /// <param name="property2"></param>
        /// <param name="minLimit"></param>
        /// <param name="maxLimit"></param>
        /// <param name="GUIOptions"></param>
        public static void CustomField_Slider(string label1, SerializedProperty property1, string label2, SerializedProperty property2, params float[] para)
        {
            EditorGUILayout.BeginHorizontal(GUILayout.Height(HorizontalHeight));

            EditorGUILayout.LabelField(label1, GUILayout.Width(LabelWidth));
            EditorGUILayout.Slider(property1, para[0], para[1], new GUIContent(), GUILayout.MinWidth(PropertyFieldMinWidth));

            GUILayout.Space(InLineSpace);

            EditorGUILayout.LabelField(label2, GUILayout.Width(LabelWidth));
            EditorGUILayout.Slider(property2,
                para.Length > 2 ? para[2] : para[0],
                para.Length > 2 ? para[3] : para[1],
                new GUIContent(), GUILayout.MinWidth(PropertyFieldMinWidth));

            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// ���е�MinMaxSlider
        /// </summary>
        /// <param name="label"></param>
        /// <param name="minLimit"></param>
        /// <param name="maxLimit"></param>
        /// <param name="target"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        public static void CustomField_MinMaxSlider(string label, float minLimit, float maxLimit, Object target, ref float minValue, ref float maxValue)
        {
            //��ʼ����
            EditorGUILayout.BeginHorizontal(GUILayout.Height(HorizontalHeight));

            //property����
            EditorGUILayout.LabelField(label, GUILayout.Width(LabelWidth));
            EditorGUI.BeginChangeCheck();

            //��ȡproperty��ǰ��ֵ������MinMaxSlider
            float _LeftValue = minValue;
            float _RightValue = maxValue;

            //����MinMaxSlider�����������ı���
            _LeftValue = EditorGUILayout.DelayedFloatField(_LeftValue, GUILayout.Width(MinMaxSliderFloatFieldWidth));
            GUILayout.Space(InLineSpace);
            EditorGUILayout.MinMaxSlider(ref _LeftValue, ref _RightValue, minLimit, maxLimit, GUILayout.MinWidth(PropertyFieldMinWidth));
            GUILayout.Space(InLineSpace);
            _RightValue = EditorGUILayout.DelayedFloatField(_RightValue, GUILayout.Width(MinMaxSliderFloatFieldWidth));

            //���property�仯
            if (EditorGUI.EndChangeCheck())
            {
                //�Գ�����Ǵ�����״̬
                Undo.RecordObject(target, "MinMaxSlider Change");

                //��ȡMinMaxSlider��ֵ������property
                minValue = _LeftValue;
                maxValue = _RightValue;

                //public static void RecordObject (Object objectToUndo, string name)
                //���� objectToUndo ΪԤ�Ƽ�ʵ�����������ȷ����ʵ��
                //������ RecordObject ֮����� PrefabUtility.RecordPrefabInstancePropertyModifications
                PrefabUtility.RecordPrefabInstancePropertyModifications(target);
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}

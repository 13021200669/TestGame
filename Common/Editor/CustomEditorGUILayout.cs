using UnityEngine;
using UnityEditor;

namespace CustomEditorGUI
{
    //����ģʽ
    public enum CustomEditorGUILayoutMode
    {
        Start,  //�п�ͷ
        Insert, //�в���
        End,    //�н�β
        Whole   //����
    }

    public static class CustomEditorGUILayout
    {
        //���ֳ���
        const float Height_Horizontal = 20;
        const float Space_InLine = 10;

        const float Width_Label = 60;

        const float Width_Toggle = 20;

        const float MinWidth_TextField = 140;

        const float MinWidth_Slider = 140;

        const float MinWidth_PropertyField = 140;

        const float MinWidth_MinMaxSlider_Slider = 140;
        const float Width_MinMaxSlider_FloatField = 50;

        /// <summary>
        /// Toggle����
        /// </summary>
        /// <param name="label"></param>
        /// <param name="property"></param>
        /// <param name="GUIOptions"></param>
        public static void CustomField_Toggle(CustomEditorGUILayoutMode mode, string label, Object target, ref bool isTrue)
        {
            //�Ƿ�ʼ��һ��
            if (mode == CustomEditorGUILayoutMode.Whole || mode == CustomEditorGUILayoutMode.Start)
                EditorGUILayout.BeginHorizontal(GUILayout.Height(Height_Horizontal));

            //�����Ƿ���Ҫ���
            if (mode == CustomEditorGUILayoutMode.Insert || mode == CustomEditorGUILayoutMode.End)
                GUILayout.Space(Space_InLine);

            //property����
            EditorGUILayout.LabelField(label, GUILayout.Width(Width_Label));

            //��ʼ���property�仯
            EditorGUI.BeginChangeCheck();

            //������ʱ��������ȡ�ʹ洢propertyֵ
            bool tempValue = isTrue;

            //����property
            tempValue = EditorGUILayout.Toggle(new GUIContent(), tempValue, GUILayout.Width(Width_Toggle));

            //�������property�仯
            if (EditorGUI.EndChangeCheck())
            {
                //�Գ�����Ǵ�����״̬
                Undo.RecordObject(target, "Toggle Change");

                //��ȡpropertyֵ������
                isTrue = tempValue;

                //public static void RecordObject (Object objectToUndo, string name)
                //���� objectToUndo ΪԤ�Ƽ�ʵ�����������ȷ����ʵ��
                //������ RecordObject ֮����� PrefabUtility.RecordPrefabInstancePropertyModifications
                PrefabUtility.RecordPrefabInstancePropertyModifications(target);
            }

            //�Ƿ������ǰ��
            if (mode == CustomEditorGUILayoutMode.Whole || mode == CustomEditorGUILayoutMode.End)
                EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// ���л�Property
        /// </summary>
        /// <param name="label"></param>
        /// <param name="property"></param>
        /// <param name="GUIOptions"></param>
        public static void CustomField_Property(CustomEditorGUILayoutMode mode, string label, SerializedProperty property)
        {
            //�Ƿ�ʼ��һ��
            if (mode == CustomEditorGUILayoutMode.Whole || mode == CustomEditorGUILayoutMode.Start)
                EditorGUILayout.BeginHorizontal(GUILayout.Height(Height_Horizontal));

            //�����Ƿ���Ҫ���
            if (mode == CustomEditorGUILayoutMode.Insert || mode == CustomEditorGUILayoutMode.End)
                GUILayout.Space(Space_InLine);

            //property����
            EditorGUILayout.LabelField(label, GUILayout.Width(Width_Label));

            //����property
            EditorGUILayout.PropertyField(property, new GUIContent(), GUILayout.MinWidth(MinWidth_PropertyField));

            //�Ƿ������ǰ��
            if (mode == CustomEditorGUILayoutMode.Whole || mode == CustomEditorGUILayoutMode.End)
                EditorGUILayout.EndHorizontal();
        }


        public static void CustomField_TextField(CustomEditorGUILayoutMode mode, string label, Object target, ref string text)
        {
            //�Ƿ�ʼ��һ��
            if (mode == CustomEditorGUILayoutMode.Whole || mode == CustomEditorGUILayoutMode.Start)
                EditorGUILayout.BeginHorizontal(GUILayout.Height(Height_Horizontal));

            //�����Ƿ���Ҫ���
            if (mode == CustomEditorGUILayoutMode.Insert || mode == CustomEditorGUILayoutMode.End)
                GUILayout.Space(Space_InLine);

            //property����
            EditorGUILayout.LabelField(label, GUILayout.Width(Width_Label));

            //��ʼ���property�仯
            EditorGUI.BeginChangeCheck();

            //������ʱ��������ȡ�ʹ洢propertyֵ
            string tempValue = text;

            //����property
            tempValue = EditorGUILayout.TextField(new GUIContent(), tempValue, GUILayout.MinWidth(MinWidth_TextField));

            //�������property�仯
            if (EditorGUI.EndChangeCheck())
            {
                //�Գ�����Ǵ�����״̬
                Undo.RecordObject(target, "Text Change");

                //��ȡpropertyֵ������
                text = tempValue;

                //public static void RecordObject (Object objectToUndo, string name)
                //���� objectToUndo ΪԤ�Ƽ�ʵ�����������ȷ����ʵ��
                //������ RecordObject ֮����� PrefabUtility.RecordPrefabInstancePropertyModifications
                PrefabUtility.RecordPrefabInstancePropertyModifications(target);
            }

            //�Ƿ������ǰ��
            if (mode == CustomEditorGUILayoutMode.Whole || mode == CustomEditorGUILayoutMode.End)
                EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// Slider������
        /// </summary>
        /// <param name="label"></param>
        /// <param name="property"></param>
        /// <param name="minLimit"></param>
        /// <param name="maxLimit"></param>
        /// <param name="GUIOptions"></param>
        public static void CustomField_Slider(CustomEditorGUILayoutMode mode, string label, SerializedProperty property, float minLimit, float maxLimit)
        {
            //�Ƿ�ʼ��һ��
            if (mode == CustomEditorGUILayoutMode.Whole || mode == CustomEditorGUILayoutMode.Start)
                EditorGUILayout.BeginHorizontal(GUILayout.Height(Height_Horizontal));

            //�����Ƿ���Ҫ���
            if (mode == CustomEditorGUILayoutMode.Insert || mode == CustomEditorGUILayoutMode.End)
                GUILayout.Space(Space_InLine);

            //property����
            EditorGUILayout.LabelField(label, GUILayout.Width(Width_Label));

            //����property
            EditorGUILayout.Slider(property, minLimit, maxLimit, new GUIContent(), GUILayout.MinWidth(MinWidth_Slider));

            //�Ƿ������ǰ��
            if (mode == CustomEditorGUILayoutMode.Whole || mode == CustomEditorGUILayoutMode.End)
                EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// MinMaxSlider��Χ������
        /// </summary>
        /// <param name="label"></param>
        /// <param name="minLimit"></param>
        /// <param name="maxLimit"></param>
        /// <param name="target"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        public static void CustomField_MinMaxSlider(CustomEditorGUILayoutMode mode, string label, float minLimit, float maxLimit, Object target, ref float minValue, ref float maxValue)
        {
            //�Ƿ�ʼ��һ��
            if (mode == CustomEditorGUILayoutMode.Whole || mode == CustomEditorGUILayoutMode.Start)
                EditorGUILayout.BeginHorizontal(GUILayout.Height(Height_Horizontal));

            //�����Ƿ���Ҫ���
            if (mode == CustomEditorGUILayoutMode.Insert || mode == CustomEditorGUILayoutMode.End)
                GUILayout.Space(Space_InLine);

            //property����
            EditorGUILayout.LabelField(label, GUILayout.Width(Width_Label));

            //�������property�仯
            EditorGUI.BeginChangeCheck();

            //������ʱ��������ȡ�ʹ洢propertyֵ
            float _LeftValue = minValue;
            float _RightValue = maxValue;

            //����property
            _LeftValue = EditorGUILayout.DelayedFloatField(_LeftValue, GUILayout.Width(Width_MinMaxSlider_FloatField));
            GUILayout.Space(Space_InLine);
            EditorGUILayout.MinMaxSlider(ref _LeftValue, ref _RightValue, minLimit, maxLimit, GUILayout.MinWidth(MinWidth_MinMaxSlider_Slider));
            GUILayout.Space(Space_InLine);
            _RightValue = EditorGUILayout.DelayedFloatField(_RightValue, GUILayout.Width(Width_MinMaxSlider_FloatField));

            //�������property�仯
            if (EditorGUI.EndChangeCheck())
            {
                //�Գ�����Ǵ�����״̬
                Undo.RecordObject(target, "MinMaxSlider Change");

                //��ȡpropertyֵ������
                minValue = _LeftValue;
                maxValue = _RightValue;

                //public static void RecordObject (Object objectToUndo, string name)
                //���� objectToUndo ΪԤ�Ƽ�ʵ�����������ȷ����ʵ��
                //������ RecordObject ֮����� PrefabUtility.RecordPrefabInstancePropertyModifications
                PrefabUtility.RecordPrefabInstancePropertyModifications(target);
            }

            //�Ƿ������ǰ��
            if (mode == CustomEditorGUILayoutMode.Whole || mode == CustomEditorGUILayoutMode.End)
                EditorGUILayout.EndHorizontal();
        }
    }
}

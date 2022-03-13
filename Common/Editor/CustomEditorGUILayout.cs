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
        /// 单行单Property
        /// </summary>
        /// <param name="label"></param>
        /// <param name="property"></param>
        /// <param name="GUIOptions"></param>
        public static void CustomPropertyField(string label, SerializedProperty property)
        {
            EditorGUILayout.BeginHorizontal(GUILayout.Height(HorizontalHeight));

            EditorGUILayout.LabelField(label, GUILayout.Width(LabelWidth));
            EditorGUILayout.PropertyField(property, new GUIContent(), GUILayout.MinWidth(PropertyFieldMinWidth));

            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// 单行双Property
        /// </summary>
        /// <param name="label1"></param>
        /// <param name="property1"></param>
        /// <param name="label2"></param>
        /// <param name="property2"></param>
        /// <param name="GUIOptions"></param>
        public static void CustomPropertyField(string label1, SerializedProperty property1, string label2, SerializedProperty property2)
        {
            EditorGUILayout.BeginHorizontal(GUILayout.Height(HorizontalHeight));

            EditorGUILayout.LabelField(label1, GUILayout.Width(LabelWidth));
            EditorGUILayout.PropertyField(property1, new GUIContent(), GUILayout.MinWidth(PropertyFieldMinWidth));

            EditorGUILayout.Space(InLineSpace);

            EditorGUILayout.LabelField(label2, GUILayout.Width(LabelWidth));
            EditorGUILayout.PropertyField(property2, new GUIContent(), GUILayout.MinWidth(PropertyFieldMinWidth));

            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// 单行单Slider
        /// </summary>
        /// <param name="label"></param>
        /// <param name="property"></param>
        /// <param name="minLimit"></param>
        /// <param name="maxLimit"></param>
        /// <param name="GUIOptions"></param>
        public static void CustomSlider(string label, SerializedProperty property, float minLimit, float maxLimit)
        {
            EditorGUILayout.BeginHorizontal(GUILayout.Height(HorizontalHeight));

            EditorGUILayout.LabelField(label, GUILayout.Width(LabelWidth));
            EditorGUILayout.Slider(property, minLimit, maxLimit, new GUIContent(), GUILayout.MinWidth(PropertyFieldMinWidth));

            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// 单行双Slider
        /// </summary>
        /// <param name="label1"></param>
        /// <param name="property1"></param>
        /// <param name="label2"></param>
        /// <param name="property2"></param>
        /// <param name="minLimit"></param>
        /// <param name="maxLimit"></param>
        /// <param name="GUIOptions"></param>
        public static void CustomSlider(string label1, SerializedProperty property1, string label2, SerializedProperty property2, params float[] para)
        {
            EditorGUILayout.BeginHorizontal(GUILayout.Height(HorizontalHeight));

            EditorGUILayout.LabelField(label1, GUILayout.Width(LabelWidth));
            EditorGUILayout.Slider(property1, para[0], para[1], new GUIContent(), GUILayout.MinWidth(PropertyFieldMinWidth));

            EditorGUILayout.Space(InLineSpace);

            EditorGUILayout.LabelField(label2, GUILayout.Width(LabelWidth));
            EditorGUILayout.Slider(property2,
                para.Length > 2 ? para[2] : para[0],
                para.Length > 2 ? para[3] : para[1],
                new GUIContent(), GUILayout.MinWidth(PropertyFieldMinWidth));

            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// 单行单MinMaxSlider
        /// </summary>
        /// <param name="label"></param>
        /// <param name="minLimit"></param>
        /// <param name="maxLimit"></param>
        /// <param name="target"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        public static void CustomMinMaxSlider(string label, float minLimit, float maxLimit, Object target, ref float minValue, ref float maxValue)
        {
            //开始该行
            EditorGUILayout.BeginHorizontal(GUILayout.Height(HorizontalHeight));

            //property名称
            EditorGUILayout.LabelField(label, GUILayout.Width(LabelWidth));
            EditorGUI.BeginChangeCheck();

            //获取property当前的值，赋给MinMaxSlider
            float _LeftValue = minValue;
            float _RightValue = maxValue;

            //绘制MinMaxSlider和左右两个文本框
            _LeftValue = EditorGUILayout.DelayedFloatField(_LeftValue, GUILayout.Width(MinMaxSliderFloatFieldWidth));
            EditorGUILayout.Space(InLineSpace);
            EditorGUILayout.MinMaxSlider(ref _LeftValue, ref _RightValue, minLimit, maxLimit, GUILayout.MinWidth(PropertyFieldMinWidth));
            EditorGUILayout.Space(InLineSpace);
            _RightValue = EditorGUILayout.DelayedFloatField(_RightValue, GUILayout.Width(MinMaxSliderFloatFieldWidth));

            //检测property变化
            if (EditorGUI.EndChangeCheck())
            {
                //对场景标记待保存状态
                Undo.RecordObject(target, "MinMaxSlider Change");

                //获取MinMaxSlider的值，赋给property
                minValue = _LeftValue;
                maxValue = _RightValue;

                //public static void RecordObject (Object objectToUndo, string name)
                //对于 objectToUndo 为预制件实例的情况下正确处理实例
                //必须在 RecordObject 之后调用 PrefabUtility.RecordPrefabInstancePropertyModifications
                PrefabUtility.RecordPrefabInstancePropertyModifications(target);
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}

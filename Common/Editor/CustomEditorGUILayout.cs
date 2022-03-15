using UnityEngine;
using UnityEditor;

namespace CustomEditorGUI
{
    //布局模式
    public enum CustomEditorGUILayoutMode
    {
        Start,  //行开头
        Insert, //行插入
        End,    //行结尾
        Whole   //整行
    }

    public static class CustomEditorGUILayout
    {
        //布局常量
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
        /// Toggle开关
        /// </summary>
        /// <param name="label"></param>
        /// <param name="property"></param>
        /// <param name="GUIOptions"></param>
        public static void CustomField_Toggle(CustomEditorGUILayoutMode mode, string label, Object target, ref bool isTrue)
        {
            //是否开始新一行
            if (mode == CustomEditorGUILayoutMode.Whole || mode == CustomEditorGUILayoutMode.Start)
                EditorGUILayout.BeginHorizontal(GUILayout.Height(Height_Horizontal));

            //行内是否需要间距
            if (mode == CustomEditorGUILayoutMode.Insert || mode == CustomEditorGUILayoutMode.End)
                GUILayout.Space(Space_InLine);

            //property标题
            EditorGUILayout.LabelField(label, GUILayout.Width(Width_Label));

            //开始检测property变化
            EditorGUI.BeginChangeCheck();

            //创建临时变量，读取和存储property值
            bool tempValue = isTrue;

            //绘制property
            tempValue = EditorGUILayout.Toggle(new GUIContent(), tempValue, GUILayout.Width(Width_Toggle));

            //结束检测property变化
            if (EditorGUI.EndChangeCheck())
            {
                //对场景标记待保存状态
                Undo.RecordObject(target, "Toggle Change");

                //获取property值并导出
                isTrue = tempValue;

                //public static void RecordObject (Object objectToUndo, string name)
                //对于 objectToUndo 为预制件实例的情况下正确处理实例
                //必须在 RecordObject 之后调用 PrefabUtility.RecordPrefabInstancePropertyModifications
                PrefabUtility.RecordPrefabInstancePropertyModifications(target);
            }

            //是否结束当前行
            if (mode == CustomEditorGUILayoutMode.Whole || mode == CustomEditorGUILayoutMode.End)
                EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// 序列化Property
        /// </summary>
        /// <param name="label"></param>
        /// <param name="property"></param>
        /// <param name="GUIOptions"></param>
        public static void CustomField_Property(CustomEditorGUILayoutMode mode, string label, SerializedProperty property)
        {
            //是否开始新一行
            if (mode == CustomEditorGUILayoutMode.Whole || mode == CustomEditorGUILayoutMode.Start)
                EditorGUILayout.BeginHorizontal(GUILayout.Height(Height_Horizontal));

            //行内是否需要间距
            if (mode == CustomEditorGUILayoutMode.Insert || mode == CustomEditorGUILayoutMode.End)
                GUILayout.Space(Space_InLine);

            //property标题
            EditorGUILayout.LabelField(label, GUILayout.Width(Width_Label));

            //绘制property
            EditorGUILayout.PropertyField(property, new GUIContent(), GUILayout.MinWidth(MinWidth_PropertyField));

            //是否结束当前行
            if (mode == CustomEditorGUILayoutMode.Whole || mode == CustomEditorGUILayoutMode.End)
                EditorGUILayout.EndHorizontal();
        }


        public static void CustomField_TextField(CustomEditorGUILayoutMode mode, string label, Object target, ref string text)
        {
            //是否开始新一行
            if (mode == CustomEditorGUILayoutMode.Whole || mode == CustomEditorGUILayoutMode.Start)
                EditorGUILayout.BeginHorizontal(GUILayout.Height(Height_Horizontal));

            //行内是否需要间距
            if (mode == CustomEditorGUILayoutMode.Insert || mode == CustomEditorGUILayoutMode.End)
                GUILayout.Space(Space_InLine);

            //property标题
            EditorGUILayout.LabelField(label, GUILayout.Width(Width_Label));

            //开始检测property变化
            EditorGUI.BeginChangeCheck();

            //创建临时变量，读取和存储property值
            string tempValue = text;

            //绘制property
            tempValue = EditorGUILayout.TextField(new GUIContent(), tempValue, GUILayout.MinWidth(MinWidth_TextField));

            //结束检测property变化
            if (EditorGUI.EndChangeCheck())
            {
                //对场景标记待保存状态
                Undo.RecordObject(target, "Text Change");

                //获取property值并导出
                text = tempValue;

                //public static void RecordObject (Object objectToUndo, string name)
                //对于 objectToUndo 为预制件实例的情况下正确处理实例
                //必须在 RecordObject 之后调用 PrefabUtility.RecordPrefabInstancePropertyModifications
                PrefabUtility.RecordPrefabInstancePropertyModifications(target);
            }

            //是否结束当前行
            if (mode == CustomEditorGUILayoutMode.Whole || mode == CustomEditorGUILayoutMode.End)
                EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// Slider滑动条
        /// </summary>
        /// <param name="label"></param>
        /// <param name="property"></param>
        /// <param name="minLimit"></param>
        /// <param name="maxLimit"></param>
        /// <param name="GUIOptions"></param>
        public static void CustomField_Slider(CustomEditorGUILayoutMode mode, string label, SerializedProperty property, float minLimit, float maxLimit)
        {
            //是否开始新一行
            if (mode == CustomEditorGUILayoutMode.Whole || mode == CustomEditorGUILayoutMode.Start)
                EditorGUILayout.BeginHorizontal(GUILayout.Height(Height_Horizontal));

            //行内是否需要间距
            if (mode == CustomEditorGUILayoutMode.Insert || mode == CustomEditorGUILayoutMode.End)
                GUILayout.Space(Space_InLine);

            //property标题
            EditorGUILayout.LabelField(label, GUILayout.Width(Width_Label));

            //绘制property
            EditorGUILayout.Slider(property, minLimit, maxLimit, new GUIContent(), GUILayout.MinWidth(MinWidth_Slider));

            //是否结束当前行
            if (mode == CustomEditorGUILayoutMode.Whole || mode == CustomEditorGUILayoutMode.End)
                EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// MinMaxSlider范围滑动条
        /// </summary>
        /// <param name="label"></param>
        /// <param name="minLimit"></param>
        /// <param name="maxLimit"></param>
        /// <param name="target"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        public static void CustomField_MinMaxSlider(CustomEditorGUILayoutMode mode, string label, float minLimit, float maxLimit, Object target, ref float minValue, ref float maxValue)
        {
            //是否开始新一行
            if (mode == CustomEditorGUILayoutMode.Whole || mode == CustomEditorGUILayoutMode.Start)
                EditorGUILayout.BeginHorizontal(GUILayout.Height(Height_Horizontal));

            //行内是否需要间距
            if (mode == CustomEditorGUILayoutMode.Insert || mode == CustomEditorGUILayoutMode.End)
                GUILayout.Space(Space_InLine);

            //property名称
            EditorGUILayout.LabelField(label, GUILayout.Width(Width_Label));

            //结束检测property变化
            EditorGUI.BeginChangeCheck();

            //创建临时变量，读取和存储property值
            float _LeftValue = minValue;
            float _RightValue = maxValue;

            //绘制property
            _LeftValue = EditorGUILayout.DelayedFloatField(_LeftValue, GUILayout.Width(Width_MinMaxSlider_FloatField));
            GUILayout.Space(Space_InLine);
            EditorGUILayout.MinMaxSlider(ref _LeftValue, ref _RightValue, minLimit, maxLimit, GUILayout.MinWidth(MinWidth_MinMaxSlider_Slider));
            GUILayout.Space(Space_InLine);
            _RightValue = EditorGUILayout.DelayedFloatField(_RightValue, GUILayout.Width(Width_MinMaxSlider_FloatField));

            //结束检测property变化
            if (EditorGUI.EndChangeCheck())
            {
                //对场景标记待保存状态
                Undo.RecordObject(target, "MinMaxSlider Change");

                //获取property值并导出
                minValue = _LeftValue;
                maxValue = _RightValue;

                //public static void RecordObject (Object objectToUndo, string name)
                //对于 objectToUndo 为预制件实例的情况下正确处理实例
                //必须在 RecordObject 之后调用 PrefabUtility.RecordPrefabInstancePropertyModifications
                PrefabUtility.RecordPrefabInstancePropertyModifications(target);
            }

            //是否结束当前行
            if (mode == CustomEditorGUILayoutMode.Whole || mode == CustomEditorGUILayoutMode.End)
                EditorGUILayout.EndHorizontal();
        }
    }
}

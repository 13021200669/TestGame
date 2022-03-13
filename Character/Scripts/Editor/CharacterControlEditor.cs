using UnityEngine;
using UnityEditor;
using CustomEditorGUI;

[CustomEditor(typeof(CharacterControl))]
[CanEditMultipleObjects]
public class CharacterControlEditor : Editor
{
    //--------------------------������--------------------------
    //������
    SerializedProperty _Body;
    SerializedProperty _Camera_Player;

    //--------------------------�˶�ģ��--------------------------
    //�ƶ��ٶ�
    SerializedProperty _MoveSpeed;
    const float _MinLimit_MoveSpeed = 0f;
    const float _MaxLimit_MoveSpeed = 50f;

    //��Ծ����
    SerializedProperty _JumpForce;
    const float _MinLimit_JumpForce = 100f;
    const float _MaxLimit_JumpForce = 500f;

    //��̱���
    SerializedProperty _Accelerate_Multiple;
    const float _MinLimit_Accelerate_Multiple = 1f;
    const float _MaxLimit_Accelerate_Multiple = 5f;

    //���ʱ��
    SerializedProperty _Accelerate_Time;
    const float _MinLimit_Accelerate_Time = 0.1f;
    const float _MaxLimit_Accelerate_Time = 5f;

    //����/�����Ұ
    SerializedProperty _Normal_Field_of_View;
    SerializedProperty _Accelerate_Field_of_View;
    const float _MinLimit_Field_of_View = 40f;
    const float _MaxLimit_Field_of_View = 100f;
    
    //��Ұģ���ű�
    SerializedProperty _FocusScript;
    //��Ұģ���̶�
    SerializedProperty _Normal_FocusSize;
    SerializedProperty _Accelerate_FocusSize;
    const float _MinLimit_FocusSize = 0.15f;
    const float _MaxLimit_FocusSize = 10f;

    //--------------------------���ģ��--------------------------
    //�������ת�㼶 X/Y
    SerializedProperty _RotateX;
    SerializedProperty _RotateY;

    //������ X/Y
    SerializedProperty _Sensitive_X;
    SerializedProperty _Sensitive_Y;
    const float _MinLimit_Sensitive = 100f;
    const float _MaxLimit_Sensitive = 500f;

    //�ӽ����� Y
    const float _MinLimit_Angle_Y = -90f;
    const float _MaxLimit_Angle_Y = 90f;

    //��ʼ�Ӹ�
    SerializedProperty _CameraHeight;
    const float _MinLimit_CameraHeight = -2f;
    const float _MaxLimit_CameraHeight = 2f;

    //��ʼ�Ӿ�
    SerializedProperty _CameraDistance;
    const float _MinLimit_CameraDistance = -5f;
    const float _MaxLimit_CameraDistance = 0f;

    //�Ӿ��ٶ�
    SerializedProperty _Speed_ViewDistanceShift;
    const float _MinLimit_Speed_ViewDistanceShift = 100f;
    const float _MaxLimit_Speed_ViewDistanceShift = 500f;

    //�Ӿ�����
    const float _MinLimit_ViewDistance = -5f;
    const float _MaxLimit_ViewDistance = 0f;

    void OnEnable()
    {
        //��ȡProperty����
        _Body = serializedObject.FindProperty("Body");
        _Camera_Player = serializedObject.FindProperty("Camera_Player");

        _MoveSpeed = serializedObject.FindProperty("MoveSpeed");
        _JumpForce = serializedObject.FindProperty("JumpForce");

        _Accelerate_Multiple = serializedObject.FindProperty("Accelerate_Multiple");
        _Accelerate_Time = serializedObject.FindProperty("Accelerate_Time");

        _Normal_Field_of_View = serializedObject.FindProperty("Normal_Field_of_View");
        _Accelerate_Field_of_View = serializedObject.FindProperty("Accelerate_Field_of_View");

        _FocusScript = serializedObject.FindProperty("FocusScript");
        _Normal_FocusSize = serializedObject.FindProperty("Normal_FocusSize");
        _Accelerate_FocusSize = serializedObject.FindProperty("Accelerate_FocusSize");

        _RotateX = serializedObject.FindProperty("RotateX");
        _RotateY = serializedObject.FindProperty("RotateY");
        _Sensitive_X = serializedObject.FindProperty("Sensitive_X");
        _Sensitive_Y = serializedObject.FindProperty("Sensitive_Y");

        _CameraHeight = serializedObject.FindProperty("CameraHeight");
        _CameraDistance = serializedObject.FindProperty("CameraDistance");

        _Speed_ViewDistanceShift = serializedObject.FindProperty("Speed_ViewDistanceShift");
    }

    public override void OnInspectorGUI()
    {
        //--------------------------��ȡproperty--------------------------

        CharacterControl scriptObject = target as CharacterControl;

        //--------------------------����GUILayout--------------------------

        //--------------------------������--------------------------
        EditorGUILayout.LabelField("������");

        CustomEditorGUILayout.CustomPropertyField("������Ƥ", _Body);

        CustomEditorGUILayout.CustomPropertyField("�������", _Camera_Player);

        //--------------------------�˶�ģ��--------------------------
        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField("�˶�ģ��");

        CustomEditorGUILayout.CustomSlider("�ƶ��ٶ�", _MoveSpeed, "��Ծ����", _JumpForce,
            _MinLimit_MoveSpeed, _MaxLimit_MoveSpeed, _MinLimit_JumpForce, _MaxLimit_JumpForce);

        CustomEditorGUILayout.CustomSlider("��̱���", _Accelerate_Multiple, "���ʱ��", _Accelerate_Time,
            _MinLimit_Accelerate_Multiple, _MaxLimit_Accelerate_Multiple, _MinLimit_Accelerate_Time, _MaxLimit_Accelerate_Time);

        CustomEditorGUILayout.CustomSlider("������Ұ", _Normal_Field_of_View, "�����Ұ", _Accelerate_Field_of_View, _MinLimit_Field_of_View, _MaxLimit_Field_of_View);

        CustomEditorGUILayout.CustomPropertyField("ģ������", _FocusScript);

        CustomEditorGUILayout.CustomSlider("����ģ��", _Normal_FocusSize, "���ģ��", _Accelerate_FocusSize, _MinLimit_FocusSize, _MaxLimit_FocusSize);

        //--------------------------���ģ��--------------------------
        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField("���ģ��");

        CustomEditorGUILayout.CustomPropertyField("ˮƽ��ת", _RotateX, "��ֱ��ת", _RotateY);

        CustomEditorGUILayout.CustomSlider("ˮƽ����", _Sensitive_X, "��ֱ����", _Sensitive_Y, _MinLimit_Sensitive, _MaxLimit_Sensitive);

        CustomEditorGUILayout.CustomSlider("��ʼ�Ӹ�", _CameraHeight, "��ʼ�Ӿ�", _CameraDistance,
            _MinLimit_CameraHeight, _MaxLimit_CameraHeight, _MinLimit_CameraDistance, _MaxLimit_CameraDistance);

        CustomEditorGUILayout.CustomSlider("�Ӿ��ٶ�", _Speed_ViewDistanceShift, _MinLimit_Speed_ViewDistanceShift, _MaxLimit_Speed_ViewDistanceShift);

        CustomEditorGUILayout.CustomMinMaxSlider("�ӽǷ�Χ", _MinLimit_Angle_Y, _MaxLimit_Angle_Y, target,
            ref scriptObject.MinAngle_Y, ref scriptObject.MaxAngle_Y);

        CustomEditorGUILayout.CustomMinMaxSlider("�Ӿ෶Χ", _MinLimit_ViewDistance, _MaxLimit_ViewDistance, target,
            ref scriptObject.MinViewDistance, ref scriptObject.MaxViewDistance);

        EditorGUILayout.Space(10);
        //--------------------------�������--------------------------

        //��serializedPropertyӦ�ø���
        //ʼ����OnInspectorGUI��ĩβִ�д˲���
        serializedObject.ApplyModifiedProperties();
    }
}
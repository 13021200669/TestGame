using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CharacterController : MonoBehaviour
{
    [Header("摄像机控制")]
    [Label("水平旋转")] public Transform RotateX;
    [Label("竖直旋转")] public Transform RotateY;

    [Label("水平灵敏度")] public float Sensitive_X = 200f;
    [Label("竖直灵敏度")] public float Sensitive_Y = 200f;

    [Label("最大视角（竖直）", -90, 90)] public float MaxAngle_Y = 80f;
    [Label("最小视角（竖直）", -90, 90)] public float MinAngle_Y = -80f;

    [Label("摄像机高度", -2, 2)] public float CameraHeight = 1f;
    [Label("摄像机初始距离", -5, 0)] public float CameraDistance = -2f;

    [Label("视距调整速度")] public float Speed_ViewDistanceShift = 300f;
    [Label("视距限制", -5, 0)] public float MaxViewDistance = 0f;
    [Label("视距限制", -5, 0)] public float MinViewDistance = -5f;

    [Label("正常视野")] public float Normal_Field_of_View = 60f;
    [Label("冲刺视野")] public float Accelerate_Field_of_View = 80f;
    private float TargetFieldofView;//目标视野（用于视野渐变过渡）

    /// <summary>
    /// Start - 摄像机初始化
    /// </summary>
    void InitCameraController()
    {
        if (!Camera_Player)
            Camera_Player = GameObject.Find("Camera_Player").transform;

        Camera_Player.localPosition = new Vector3(0, CameraHeight, CameraDistance);

        TargetFieldofView = Normal_Field_of_View;
    }

    /// <summary>
    /// Update - 摄像机控制
    /// </summary>
    void UpdateCameraController()
    {
        ViewAngleControl();
        ViewDistanceControl();
    }

    /// <summary>
    /// 视角控制
    /// </summary>
    public void ViewAngleControl()
    {
        //获取鼠标输入
        float deltaMouseX = Input.GetAxis("Mouse X");
        float deltaMouseY = -Input.GetAxis("Mouse Y");

        //左右视角旋转
        float deltaX = deltaMouseX * Time.deltaTime * Sensitive_X;
        RotateX.localEulerAngles += new Vector3(0, deltaX, 0);

        //上下视角旋转
        float deltaY = deltaMouseY * Time.deltaTime * Sensitive_Y;
        float NowY = RotateY.localEulerAngles.x;
        if (NowY + deltaY > 360 + MinAngle_Y || NowY + deltaY < MaxAngle_Y)
            RotateY.localEulerAngles += new Vector3(deltaY, 0, 0);
        else
            RotateY.localEulerAngles = new Vector3(0, 0, 0);
    }

    /// <summary>
    /// 视距控制
    /// </summary>
    public void ViewDistanceControl()
    {
        //获取鼠标输入
        float deltaViewDistance = Input.GetAxis("Mouse ScrollWheel");

        //视距调整
        if (Camera_Player.localPosition.z <= MaxViewDistance && Camera_Player.localPosition.z >= MinViewDistance)
        {
            Camera_Player.localPosition += new Vector3(0, 0, deltaViewDistance * Time.deltaTime * Speed_ViewDistanceShift);

            //检查视距是否超出限制
            if (Camera_Player.localPosition.z > MaxViewDistance)
                Camera_Player.localPosition = new Vector3(0, CameraHeight, MaxViewDistance);
            else if (Camera_Player.localPosition.z < MinViewDistance)
                Camera_Player.localPosition = new Vector3(0, CameraHeight, MinViewDistance);
        }

        //视野调整
        Camera_Player.GetComponent<Camera>().fieldOfView = Mathf.Lerp(Camera_Player.GetComponent<Camera>().fieldOfView, TargetFieldofView, Time.deltaTime / 0.2f);
    }
}

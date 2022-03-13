using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CharacterControl : MonoBehaviour
{
    //子模块
    [SerializeField] public Transform RotateX;
    [SerializeField] public Transform RotateY;

    [SerializeField] public float Sensitive_X = 200f;//灵敏度 X
    [SerializeField] public float Sensitive_Y = 200f;//灵敏度 Y

    [SerializeField] public float MinAngle_Y = -80f;//最小视角 Y
    [SerializeField] public float MaxAngle_Y = 80f;//最大视角 Y

    [SerializeField] public float CameraHeight = 0.8f;//初始视高
    [SerializeField] public float CameraDistance = -2.5f;//初始视距

    [SerializeField] public float Speed_ViewDistanceShift = 300f;//视距调整速度
    [SerializeField] public float MinViewDistance = -3f;//最小视距
    [SerializeField] public float MaxViewDistance = -1.5f;//最大视距

    [SerializeField] public float Normal_Field_of_View = 60f;//正常视野
    [SerializeField] public float Accelerate_Field_of_View = 80f;//冲刺视野

    [SerializeField] public CameraFilterPack_Blur_Focus FocusScript;//视野模糊脚本
    [SerializeField] public float Normal_FocusSize = 0.15f;//正常视野模糊程度
    [SerializeField] public float Accelerate_FocusSize = 10f;//冲刺视野模糊程度

    private float TargetFieldofView;//目标视野（用于视野渐变过渡）
    private float TargetFocusSize;//目标视野模糊程度

    /// <summary>
    /// Start - 摄像机初始化
    /// </summary>
    void InitCameraController()
    {
        if (!Camera_Player)
            Camera_Player = GameObject.Find("Camera_Player").transform;

        Camera_Player.localPosition = new Vector3(0, CameraHeight, CameraDistance);

        TargetFieldofView = Normal_Field_of_View;
        TargetFocusSize = Normal_FocusSize;
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
        if (NowY + deltaY >= 360 + MinAngle_Y || NowY + deltaY <= MaxAngle_Y)
            RotateY.localEulerAngles += new Vector3(deltaY, 0, 0);
        else if (NowY + deltaY < 360 + MinAngle_Y && NowY + deltaY > 180)
            RotateY.localEulerAngles = new Vector3(360 + MinAngle_Y, 0, 0);
        else if (NowY + deltaY > MaxAngle_Y && NowY + deltaY < 180)
            RotateY.localEulerAngles = new Vector3(MaxAngle_Y, 0, 0);
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
        FocusScript._Size= Mathf.Lerp(FocusScript._Size, TargetFocusSize, Time.deltaTime / 0.2f);
    }
}
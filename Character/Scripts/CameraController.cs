using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CharacterController : MonoBehaviour
{
    [Header("鼠标灵敏度")]
    public Transform RotateX;
    public Transform RotateY;

    public float Sensitive_X = 200f;
    public float Sensitive_Y = 200f;
    public float Speed_ViewDistanceShift = 10f;

    [Header("视角限制[-90,90]")]
    [Range(-90, 90)]
    public float MaxAngle_Y = 80f;
    [Range(-90, 90)]
    public float MinAngle_Y = -80f;

    [Header("视距限制[0,20]")]
    [Range(0, 20)]
    public float MaxViewDistance = 10f;
    [Range(0, 20)]
    public float MinViewDistance = 5f;

    [Header("组件")]
    public Transform Camera_Player;

    void InitCameraController()
    {
        if (!Camera_Player)
            Camera_Player = GameObject.Find("Camera_Player").transform;
    }

    void UpdateCameraController()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            ViewControl();

            if (Input.GetKeyDown(KeyCode.Escape))
                Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void ViewControl()
    {
        //获取鼠标输入
        float deltaMouseX = Input.GetAxis("Mouse X");
        float deltaMouseY = -Input.GetAxis("Mouse Y");
        float deltaViewDistance = Input.GetAxis("Mouse ScrollWheel");

        //左右视角旋转
        float deltaX = deltaMouseX * Time.deltaTime * Sensitive_X;
        RotateX.localEulerAngles += new Vector3(0, deltaX, 0);

        //上下视角旋转
        float deltaY = deltaMouseY * Time.deltaTime * Sensitive_Y;
        float NowY = RotateY.localEulerAngles.x;
        if (NowY + deltaY > 360 + MinAngle_Y || NowY + deltaY < MaxAngle_Y)
            RotateY.localEulerAngles += new Vector3(deltaY, 0, 0);

        //视距调整
        if (Camera_Player.localPosition.z < MaxViewDistance && Camera_Player.localPosition.z > MinViewDistance)
            Camera_Player.localPosition += new Vector3(0, 0, deltaViewDistance * Time.deltaTime * Speed_ViewDistanceShift);
    }
}

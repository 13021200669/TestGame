using UnityEngine;

public partial class CharacterControl : MonoBehaviour
{
    public bool CheckDisConnected()
    {
        return !RigPlayer || !ColPlayer || !CamPlayer || !FocusScript || !RotateX || !RotateY || !Body || !AnimPlayer;
    }

    public void AutoConnect()
    {
        if (!RigPlayer)
            RigPlayer = GetComponent<Rigidbody>();
        if (!ColPlayer)
            ColPlayer = GetComponent<CapsuleCollider>();

        if (!CamPlayer)
            CamPlayer = GameObject.Find("CamPlayer").transform;
        if (!FocusScript)
            FocusScript = GetComponentInChildren<CameraFilterPack_Blur_Focus>();

        if (!RotateX)
            RotateX = GameObject.Find("RotateX").transform;
        if (!RotateY)
            RotateY = GameObject.Find("RotateY").transform;

        if (!Body)
            Body = transform.Find("Body");
        if (!AnimPlayer)
            AnimPlayer = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        InitMovementController();

        InitMotionController();

        InitCameraController();
    }

    void Update()
    {
        //视角锁定时开始
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            UpdateMovementController();

            UpdateMotionController();

            UpdateCameraController();

            if (Input.GetKeyDown(KeyCode.Escape))
                Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

    }
}

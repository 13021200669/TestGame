using UnityEngine;

public partial class CharacterControl : MonoBehaviour
{
    //��ģ��
    [SerializeField] public Transform Body;
    [SerializeField] public Transform CamPlayer;
    [SerializeField] public Animator AnimPlayer;
    [SerializeField] public Rigidbody RigPlayer;

    private void Reset()
    {
        if (!Body)
            Body = transform.Find("Body");
        if (!CamPlayer)
            CamPlayer = GameObject.Find("CamPlayer").transform;
        if (!RotateX)
            RotateX = GameObject.Find("RotateX").transform;
        if (!RotateY)
            RotateY = GameObject.Find("RotateY").transform;
        if (!AnimPlayer)
            AnimPlayer = GetComponentInChildren<Animator>();
        if (!RigPlayer)
            RigPlayer = GetComponent<Rigidbody>();
        if (!FocusScript)
            FocusScript = GetComponentInChildren<CameraFilterPack_Blur_Focus>();
    }

    void Start()
    {
        InitMovementController();

        InitMotionController();

        InitCameraController();
    }

    void Update()
    {
        //�ӽ�����ʱ��ʼ
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

    private void FixedUpdate()
    {

    }
}

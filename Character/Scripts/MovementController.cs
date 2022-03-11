using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CharacterController : MonoBehaviour
{
    public float Speed = 10;
    public float JumpForce = 100;

    // Update is called once per frame
    public void UpdateMovementController()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        transform.position += RotateX.right * x * Time.deltaTime * Speed;
        transform.position += RotateX.forward * z * Time.deltaTime * Speed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(RotateX.up * JumpForce);
        }

        if (x != 0 || z != 0)
            Body.rotation = Quaternion.LookRotation(x * RotateX.right + z * RotateX.forward);
    }
}

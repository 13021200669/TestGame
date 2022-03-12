using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public void Stop()
    {
        GetComponentInChildren<ElevatorController>().isMoving = false;
        GetComponentInChildren<ElevatorController>().isUp = !GetComponentInChildren<ElevatorController>().isUp;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public delegate void OnGlobalSpeedChanged(float newSpeed);
    [SerializeField] float GlobalSpeed = 15f;

    public event OnGlobalSpeedChanged onGlobalSpeedChanged;

    public void SetGlobalSpeed(float newSpeed)
    {
        GlobalSpeed = newSpeed;
        onGlobalSpeedChanged?.Invoke(GlobalSpeed);
    }
    public float GetGlobalSpeed()
    {
        return GlobalSpeed;
    }
}

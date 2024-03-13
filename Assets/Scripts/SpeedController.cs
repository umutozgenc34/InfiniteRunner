using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public delegate void OnGlobalSpeedChanged(float newSpeed);
    [SerializeField] float GlobalSpeed = 15f;

    public event OnGlobalSpeedChanged onGlobalSpeedChanged;

    public void ChangeGlobalSpeed(float speedChange , float duration)
    {
        GlobalSpeed += speedChange;
        InformSpeedChange();
        StartCoroutine(RemoveSpeedChange(speedChange, duration));
    }
    public float GetGlobalSpeed()
    {
        return GlobalSpeed;
    }
    
    IEnumerator RemoveSpeedChange(float speedChangeAmount , float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GlobalSpeed -= speedChangeAmount;
        InformSpeedChange();


    }

    private void InformSpeedChange()
    {
        onGlobalSpeedChanged?.Invoke(GlobalSpeed);
    }
}

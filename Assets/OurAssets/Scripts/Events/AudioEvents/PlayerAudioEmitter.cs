using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioEmitter : MonoBehaviour
{
    public void ExecutePunch()
    {
        EventManager.TriggerEvent<PunchAudioEvent, Vector3>(transform.position);
    }
}
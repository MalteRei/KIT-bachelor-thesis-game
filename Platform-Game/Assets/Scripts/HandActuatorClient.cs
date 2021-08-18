using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HandActuatorClient : MonoBehaviour
{
    public string urlOfHand;
    private readonly string VIBRATION_PATTERN_WALK = "normal";
    private readonly string VIBRATION_PATTERN_RUN = "fast";
    private readonly string VIBRATION_PATTERN_SLOW = "slow";

    private bool isPlaying = false;

    public void SetWalkVibration()
    {
        StartCoroutine(SendRequestVibrate($"{urlOfHand}/vibration/set/{VIBRATION_PATTERN_WALK}"));
    }

    public void SetRunVibration()
    {
        StartCoroutine(SendRequestVibrate($"{urlOfHand}/vibration/set/{VIBRATION_PATTERN_RUN}"));
    }

    public void SetSlowWalkVibration()
    {
        StartCoroutine(SendRequestVibrate($"{urlOfHand}/vibration/set/{VIBRATION_PATTERN_SLOW}"));
    }

    public void StopVibration()
    {
        if (isPlaying)
        {
            isPlaying = false;

            StartCoroutine(SendRequestVibrate($"{urlOfHand}/vibration/stop"));

        }
    }

    public void PlayVibration()
    {
        if (!isPlaying)
        {
            isPlaying = true;

            StartCoroutine(SendRequestVibrate($"{urlOfHand}/vibration/play"));

        }
    }

    private IEnumerator SendRequestVibrate(string url)
    {
        UnityWebRequest requestToTriggerFinger = UnityWebRequest.Post(url, "");
        yield return requestToTriggerFinger.SendWebRequest();

        requestToTriggerFinger.Dispose();

        yield break;

    }
}

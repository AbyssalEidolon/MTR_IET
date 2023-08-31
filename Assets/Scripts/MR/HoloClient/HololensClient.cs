using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using TMPro;
using UnityEngine;

public class HololensClient : MonoBehaviour
{
    public GameObject JointVisualiser = null;
    GameObject[] jointVisualisers = new GameObject[5];
    public string Host = "192.168.0.201";
    const short Port = 12345;
    static Client client;
    [Header("Poller Settings")]
    public bool VisualiseTargetJoints = true;
    Poller poller;
    void Start()
    {
        poller = new();
        SetupPoller();
        client = new(Host == "" ? "127.0.0.1" : Host, Port, this);
        CoreServices.InputSystem?.RegisterHandler<IMixedRealitySourceStateHandler>(poller);
    }
    void SetupPoller()
    {
        if (VisualiseTargetJoints)
        {
            for (int i = 0; i < jointVisualisers.Length; i++)
            {
                jointVisualisers[i] = Instantiate(JointVisualiser);
                JointVisualiser visualiser = jointVisualisers[i].AddComponent<JointVisualiser>();
                visualiser.ind = i;
                visualiser.poller = poller;
            }
        }
    }
    public TextMeshProUGUI handPresent = null;
    public TextMeshProUGUI[] figners = new TextMeshProUGUI[5];
    void FixedUpdate()
    {
        poller.PollFingers();
        handPresent.text = poller.hand == null ? "Not Present" : poller.hand.ControllerHandedness == Handedness.Left ? "Left" : "Right";
        for (int i = 0; i < figners.Length; i++)
        {
            figners[i].text = poller.FingerPos[i].ToString("F4");
        }
    }
    IEnumerator updatePositions()
    {
        yield return new WaitForSeconds(0.1f);
        byte[] message = Encoder.JointPosBytes(poller.FingerPos);
        if (message != null)
        {
            client.WriteSocket(message);
            Debug.Log("Update thread active.");
        }
        yield return updatePositions();
    }
    public void startCoroutine() => StartCoroutine(updatePositions());
    public void stopCoroutine() => StopCoroutine(updatePositions());
}

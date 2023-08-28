using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Examples.Demos;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class HololensClient : MonoBehaviour, IMixedRealitySourceStateHandler
{
    const string Host = "192.168.0.201";
    const short Port = 12345;
    static Client client;
    Transform[] defaults;
    void Start(){
        CoreServices.InputSystem?.RegisterHandler<IMixedRealitySourceStateHandler>(this);
        defaults = new Transform[5]{ new GameObject().transform, new GameObject().transform, new GameObject().transform, new GameObject().transform, new GameObject().transform };
        targetJointTransforms = defaults;
        foreach(Transform transform in targetJointTransforms){
            transform.parent = this.transform;
        }
        client = new(Host, Port, this);
        startCoroutine();
    }
    public TextMeshProUGUI handPresent = null;
    public TextMeshProUGUI[] figners = new TextMeshProUGUI[5];

    TrackedHandJoint[] targetJoints => HandInteractionController.i.targetJoints;
    Handedness curHand = Handedness.None;
    public static Transform[] targetJointTransforms;
    IMixedRealityHandJointService jointService = CoreServices.GetInputSystemDataProvider<IMixedRealityHandJointService>();
    public void OnSourceDetected(SourceStateEventData eventData){
        IMixedRealityHand hand = eventData.Controller.Visualizer as IMixedRealityHand;
        if(hand != null){
            curHand = hand.ControllerHandedness;
            handPresent.text = "True";
            updateTargetTransforms();
        }
        handPresent.text = "False";
    }
    void updateTargetTransforms(){
        if(jointService != null){
            for(int i = 0; i < targetJointTransforms.Length; i++){
                targetJointTransforms[i] = jointService.RequestJointTransform(targetJoints[i], curHand);
            }
        }
        else targetJointTransforms = defaults;
        for(int i = 0; i < targetJointTransforms.Length; i++){
            figners[i].text = targetJointTransforms[i].position.ToString("F4");
        }
    }
    IEnumerator updatePositions(){
        byte[] message = Encoder.JointPosBytes(targetJointTransforms);
        if(message != null){
            client.WriteSocket(message);
            Debug.Log("Update thread active.");
        }
        yield return new WaitForSeconds(1);
        yield return updatePositions();
    }
    public void startCoroutine() => StartCoroutine(updatePositions());
    public void stopCoroutine() => StopCoroutine(updatePositions());
    public void OnSourceLost(SourceStateEventData eventData){}
}
public class Client
{
    public HololensClient hololensClient;
    internal bool socketReady = false;
    TcpClient socket;
    NetworkStream ns;
    StreamWriter writer;
    StreamReader reader;
    public string Host;
    private readonly int Port;
    void SetupSocket()
    {
        try
        {
            socket = new TcpClient(Host, Port);
            ns = socket.GetStream();
            writer = new(ns);
            reader = new(ns);
            socketReady = true;
            // hololensClient.startCoroutine();
        }
        catch (Exception e)
        {
            Debug.Log("Socket error: " + e);
        }
    }
    void CloseSocket(){
        if(!socketReady) return;
        hololensClient.stopCoroutine();
        writer.Close();
        reader.Close();
        socket.Close();
        socketReady = false;
    }
    public void WriteSocket(byte[] bytes)
    {
        if (socketReady) ns.Write(bytes);
    }
    public Client(string host = "127.0.0.1", int port = 12345, HololensClient hololensClient = null)
    {
        this.Host = host;
        this.Port = port;
        this.hololensClient = hololensClient;
        SetupSocket();
    }
}
public class Encoder
{
    public static byte[] JointPosBytes(Transform[] transforms){
        if (transforms.Length != 5) return null;
            List<byte> temp = new();
            for (int i = 0; i < transforms.Length; i++)
            {
                temp.AddRange(Encoding.UTF8.GetBytes((transforms[i].position.ToString("F4") + ";").Replace("(", "").Replace(")", "")));
            }
            return temp.ToArray();
    }
}
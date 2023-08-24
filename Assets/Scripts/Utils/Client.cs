using System;
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

public class HololensClient : MonoBehaviour
{
    const string Host = "";
    const short Port = 12345;
    public static Client client;
    void Start(){
        client = new(Host, Port, updateThread);
    }
    public TextMeshProUGUI text = null;
    TrackedHandJoint[] targetJoints => HandInteractionController.i.targetJoints;
    Handedness curHand = Handedness.None;
    public static Transform[] targetJointTransforms = new Transform[5];
    IMixedRealityHandJointService jointService = CoreServices.GetInputSystemDataProvider<IMixedRealityHandJointService>();
    public void OnSourceDetected(SourceStateEventData eventData){
        IMixedRealityHand hand = eventData.Controller.Visualizer as IMixedRealityHand;
        if(hand != null){
            curHand = hand.ControllerHandedness;
            updateTargetTransforms();
        }
    }
    void updateTargetTransforms(){
        if(jointService != null){
            for(int i = 0; i < targetJointTransforms.Length; i++){
                targetJointTransforms[i] = jointService.RequestJointTransform(targetJoints[i], curHand);
            }
        }
    }
    static void updatePositions(){
        byte[] message = Encoder.JointPosBytes(targetJointTransforms);
        if(message != null){
            client.WriteSocket(message);
        }
        Thread.Sleep(1000);
    }
    Thread updateThread = new Thread(new ThreadStart(updatePositions));
}
public class Client
{
    internal bool socketReady = false;
    TcpClient socket;
    NetworkStream ns;
    StreamWriter writer;
    StreamReader reader;
    public string Host;
    private readonly int Port;
    Thread updateThread = null;
    void SetupSocket()
    {
        try
        {
            socket = new TcpClient(Host, Port);
            ns = socket.GetStream();
            writer = new(ns);
            reader = new(ns);
            socketReady = true;
            updateThread.Start();
        }
        catch (Exception e)
        {
            Debug.Log("Socket error: " + e);
        }
    }
    void CloseSocket(){
        if(!socketReady) return;
        updateThread.Abort();
        writer.Close();
        reader.Close();
        socket.Close();
        socketReady = false;
    }
    public void WriteSocket(byte[] bytes)
    {
        if (socketReady) ns.Write(bytes);
    }
    public Client(string host = "127.0.0.1", int port = 12345, Thread thread = null)
    {
        this.Host = host;
        this.Port = port;
        SetupSocket();
        updateThread = thread;
    }
}
public class Encoder
{
    public static byte[] JointPosBytes(Transform[] transforms){
        if (transforms.Length != 5) return null;
            List<byte> temp = new();
            for (int i = 0; i < transforms.Length; i++)
            {
                temp.AddRange(Encoding.UTF8.GetBytes((transforms[i].position.ToString("F4") + ";").Replace("<", "").Replace(">", "")));
            }
            return temp.ToArray();
    }
}
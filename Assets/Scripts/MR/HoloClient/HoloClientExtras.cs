using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

public class Poller : IMixedRealitySourceStateHandler
{
    TrackedHandJoint[] targetJoints;
    public Vector3[] FingerPos { get { return Positions; } }
    public Quaternion[] FingerRot { get { return Rotations; } }
    public Quaternion PalmRot { get { return PalmRotation;}}
    Vector3[] Positions = Enumerable.Repeat(new Vector3(), 5).ToArray();
    Quaternion[] Rotations = Enumerable.Repeat(new Quaternion(), 5).ToArray();
    Quaternion PalmRotation = Quaternion.identity;
    public IMixedRealityHand hand { get { return Hand; } }
    IMixedRealityHand Hand = null;
    public Poller(TrackedHandJoint Thumb = TrackedHandJoint.ThumbProximalJoint, TrackedHandJoint Index = TrackedHandJoint.IndexDistalJoint, TrackedHandJoint Middle = TrackedHandJoint.MiddleDistalJoint, TrackedHandJoint Ring = TrackedHandJoint.RingDistalJoint, TrackedHandJoint Pinky = TrackedHandJoint.PinkyDistalJoint){
        targetJoints = new TrackedHandJoint[]{Thumb, Index, Middle, Ring, Pinky};
    }
    public void OnSourceDetected(SourceStateEventData eventData)
    {
        Hand = eventData.Controller as IMixedRealityHand;
    }
    public void PollFingers(bool DePalmed = false)
    {
        if (Hand != null)
        {
            MixedRealityPose palm = new();
            hand.TryGetJoint(TrackedHandJoint.Wrist, out palm);
            for (int i = 0; i < targetJoints.Length; i++)
            {
                MixedRealityPose pose = new();
                if (hand.TryGetJoint(targetJoints[i], out pose))
                {
                    Positions[i] = DePalmed? pose.Position - palm.Position : pose.Position;
                    Rotations[i] = pose.Rotation;
                }
            }
            if(hand.TryGetJoint(TrackedHandJoint.Wrist, out palm)){
                PalmRotation = palm.Rotation;
            };

        }
    }
    public void OnSourceLost(SourceStateEventData eventData)
    {
        // throw new NotImplementedException();
    }
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
            hololensClient.startCoroutine();
        }
        catch (Exception e)
        {
            Debug.Log("Socket error: " + e);
        }
    }
    void CloseSocket()
    {
        if (!socketReady) return;
        hololensClient.stopCoroutine();
        writer.Close();
        reader.Close();
        socket.Close();
        socketReady = false;
        hololensClient.stopCoroutine();
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
    public static byte[] JointPosBytes(Transform[] transforms)
    {
        if (transforms.Length != 5) return null;
        List<byte> temp = new();
        for (int i = 0; i < transforms.Length; i++)
        {
            temp.AddRange(Encoding.UTF8.GetBytes((transforms[i].position.ToString("F4") + ";").Replace("(", "").Replace(")", "")));
        }
        return temp.ToArray();
    }
    public static List<byte> JointPosBytes(Vector3[] vectors)
    {
        if (vectors.Length != 5) return null;
        List<byte> temp = new();
        for (int i = 0; i < vectors.Length; i++)
        {
            temp.AddRange(Encoding.UTF8.GetBytes((vectors[i].ToString("F4") + ";").Replace("(", "").Replace(")", "")));
        }
        return temp;
    }
    public static byte[] JointPosBytes(Vector3[] vectors, Quaternion palmRotation)
    {
        List<byte> temp = JointPosBytes(vectors);
        temp.AddRange(Encoding.UTF8.GetBytes((palmRotation.ToString("F4") + ";").Replace("(", "").Replace(")", "")));
        return temp.ToArray();
    }
}
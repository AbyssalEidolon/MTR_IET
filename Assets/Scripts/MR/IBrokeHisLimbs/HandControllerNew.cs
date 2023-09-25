using System.Linq;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

public class HandControllerNew : MonoBehaviour
{
    public bool FakeUpdate = true;
    Poller poller;
    TempFingerContraints[] tempConstraints = Enumerable.Repeat(new TempFingerContraints(), 5).ToArray();
    void Awake() { if (FakeUpdate) poller = new(); }
    void Start()
    {
        CoreServices.InputSystem?.RegisterHandler<IMixedRealitySourceStateHandler>(poller);
        
    }

    void FixedUpdate()
    {

    }
    void RealPoll()
    {
        poller.PollFingers();
    }
    void FakePoll()
    {

    }
}
public struct TempFingerContraints
{
    public GameObject Start;
    public GameObject End;
}
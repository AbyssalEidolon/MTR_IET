using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
public class PacificJsonLoader : MonoBehaviour{
    public string startPath;
    public string endPath;
    public HandPose handposes;
    public void LoadPoses(){
        handposes = new HandPose(startPath, endPath);
    }
}
public class HandPose{
    public HandPoseContainer[] containers = new HandPoseContainer[2];
    public HandPoseContainer startPositions => containers[0];
    public HandPoseContainer endPositions => containers[1];
    public HandPose(string StartFilePath, string EndFilePath){
        if(StartFilePath == null || EndFilePath == null) return;
        string[] temp = {StartFilePath, EndFilePath};
        for(int i = 0; i < containers.Length; i++){
            if(Directory.Exists(temp[i])) 
            containers[i] = (HandPoseContainer)JsonConvert.DeserializeObject(File.ReadAllText(temp[i]));
            else {
                Debug.LogError("Fuck me");
                break;
            }
        }
    }
}
[Serializable]
public struct HandPoseContainer{
    public Vector3 Thumb, Pointer, Mi9dddle, Ring, Pinky;
    public Quaternion Palm;
}
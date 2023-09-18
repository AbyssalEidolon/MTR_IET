using JsonUnityLoader;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;
using System.Numerics;
using System.Text.RegularExpressions;

public class Loader
{
    const string Template = """
	%YAML 1.1
	%TAG !u! tag:unity3d.com,2011:
	--- !u!114 &11400000
	MonoBehaviour:
	  m_ObjectHideFlags: 0
	  m_CorrespondingSourceObject: {fileID: 0}
	  m_PrefabInstance: {fileID: 0}
	  m_PrefabAsset: {fileID: 0}
	  m_GameObject: {fileID: 0}
	  m_Enabled: 1
	  m_EditorHideFlags: 0
	  m_Script: {fileID: 11500000, guid: 180dbfbf05f4a024890ace92aead3c8c, type: 3}
	  m_Name: Template
	  m_EditorClassIdentifier: 
	  LeftHand: 0
	  Scale: 1
	  FingerConfigs:
	  - Active: 1
	    startPosition: {x: 0, y: 0, z: 0}
	    endPosition: {x: 0, y: 0, z: 0}
	  - Active: 1
	    startPosition: {x: 0, y: 0, z: 0}
	    endPosition: {x: 0, y: 0, z: 0}
	  - Active: 1
	    startPosition: {x: 0, y: 0, z: 0}
	    endPosition: {x: 0, y: 0, z: 0}
	  - Active: 1
	    startPosition: {x: 0, y: 0, z: 0}
	    endPosition: {x: 0, y: 0, z: 0}
	  - Active: 1
	    startPosition: {x: 0, y: 0, z: 0}
	    endPosition: {x: 0, y: 0, z: 0}
	  palmRots:
	  - {x: 0, y: 0, z: 0, w: 0}
	  - {x: 0, y: 0, z: 0, w: 0}
	""";
    private const string Input = ".\\Input";
    private const string Output = ".\\Output";
	Auto Start;
	Auto End;
	string Name;
	public Loader()
	{
		Start = Load(readLine("start"));//remove vars
        End = Load(readLine("end"));
        Console.WriteLine("Enter File Name");
        Name = readLine();
		Write();
	}
	string readLine()
	{
		string? line = "";
		while (true)
		{
			line = Console.ReadLine();
			if (line == null) continue;
			else break;
		}
		return line;
	}
	string readLine(string prefix) {
        string? line = "";
        while (true)
		{
            Console.WriteLine($"Input file names should be already put into the Inputs folder, Input {prefix} file name.");
			line = readLine();
            line = Path.Combine(Input, line);
            if (!File.Exists(line))
			{
				Console.WriteLine("File does not exist!");
				continue;
			}
			else if(Path.GetExtension(line) != ".json")
			{
                Console.WriteLine("File is not in the right format!");
                continue;
            }
			else break;
        }
		Console.Clear();
		return line;
    }
	/*
    Vector3[] Load(string FilePath)
	{
		string file = File.ReadAllText(FilePath);
		Dictionary<string, Vector3>? data = JsonConvert.DeserializeObject<Dictionary<string, Vector3>>(file);
		if(data == null)
		{
			Console.WriteLine("File read error.");
			throw new Exception();
		}
        data.Remove("Palm");
        //		return data.Values.ToArray();
        return null;
	}
	*/
	Auto Load(string FilePath)
	{
		string file = File.ReadAllText(FilePath);
        Auto? data = JsonConvert.DeserializeObject<Auto>(file);
        //Dictionary<string, Vector3>? data = JsonConvert.DeserializeObject<Dictionary<string, Vector3>>(file);
        if (data == null)
        {
            Console.WriteLine("File read error.");
            throw new Exception();
        }
		data.GenerateHandles();
		return data;
    }
    public void Write()
	{
		string target = new(Template);
		string[] tokens = Template.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
		int i = 0;
		for(int j = 0; j < tokens.Length; j++)
		{
			if (tokens[j].Contains("m_Name: Template")) tokens[j] = tokens[j].Replace("Template", Name);
			else if (tokens[j].Contains("startPosition")) tokens[j] = tokens[j].Replace($"{{x: 0, y: 0, z: 0}}", $"{{x: {Start.Positions[i][0]}, y: {Start.Positions[i][1]}, z: {Start.Positions[i][2]}}}");
			else if (tokens[j].Contains("endPosition"))
			{
                tokens[j] = tokens[j].Replace($"{{x: 0, y: 0, z: 0}}", $"{{x: {End.Positions[i][0]} , y:  {End.Positions[i][1]}, z: {End.Positions[i][2]}}}");
				i++;
            }
			else if (tokens[j].Contains("palmRots"))
			{
				Console.WriteLine(tokens[j+1]);
				tokens[j + 1] = tokens[j + 1].Replace($"{{x: 0, y: 0, z: 0, w: 0}}", $"{{x: {Start.PalmRot[0]} , y:  {Start.PalmRot[1]}, z: {Start.PalmRot[2]}, w: {Start.PalmRot[3]}}}");
                tokens[j + 2] = tokens[j + 2].Replace($"{{x: 0, y: 0, z: 0, w: 0}}", $"{{x: {End.PalmRot[0]} , y:  {End.PalmRot[1]}, z: {End.PalmRot[2]}, w: {End.PalmRot[3]}}}");

            }
        }
		foreach(string token in tokens) Console.WriteLine(token);
        StreamWriter file = File.CreateText(Path.Combine(Output, Name) + ".asset");
        foreach (string token in tokens) file.WriteLine(token);
        file.Close();
	}
}

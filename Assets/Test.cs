using UnityEngine;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Test : MonoBehaviour {

	public AnimationCurve curve;

	private SomeClass someClass;

	private SomeStruct someStruct;

	public void OnEnable()
	{
		someClass = new SomeClass(1f);
		someStruct = new SomeStruct(1f);
	}
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.O))
		{
			Debug.Log(typeof(SomeClass).GetProperty("SomeValue").GetValue(someClass, null));
		}

		if (Input.GetKeyDown(KeyCode.P))
		{
			Debug.Log(typeof(SomeStruct).GetProperty("SomeValue").GetValue(someStruct, null));
		
		}

		if (Input.GetKeyDown(KeyCode.Y))
		{
			var keyValuePairs = new KeyValuePair<int, float>[2]{new KeyValuePair<int, float>(1, 2f), new KeyValuePair<int, float>(3, 4f)};
			var stream = new MemoryStream(); 
			var formatter = new BinaryFormatter();
			formatter.Serialize(stream, keyValuePairs);
			Debug.Log(stream.Length);
		}
	}

	public class SomeClass
	{
		public float SomeValue{get; set;}

		public SomeClass(float someValue)
		{
			SomeValue = someValue;
		}
	}

	public struct SomeStruct
	{
		public float SomeValue{get; set;}

		public SomeStruct(float someValue)
		{
			SomeValue = someValue;
		}
	}
}

using UnityEngine;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;

public class Test : MonoBehaviour {

	public AnimationCurve curve;

	private SomeClass someClass;

	private SomeStruct someStruct;

	private SomeOtherClass someotherClass = new SomeOtherClass();

	private static SomeOtherClass someOtherClass2 = new SomeOtherClass();

	private Dictionary<string, object> dictionary = new Dictionary<string, object>();

	public void OnEnable()
	{
		someClass = new SomeClass(1f);
		someStruct = new SomeStruct(1f);
		dictionary.Add("hello", 1f);
		dictionary.Add("goodbye", 4f);
	}
	// Update is called once per frame
	void Update () 
	{
        /*
        if (Input.GetKeyDown(KeyCode.O) || Input.GetButtonDown("A_1"))
        {
			Debug.Log(typeof(SomeClass).GetProperty("SomeValue").GetValue(someClass, null));
		}
		*/

		//IEnumerable foreach.
		if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetButtonDown("B_1"))
		{
			
		}

        if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("Y_1"))
        {
			var keyPairs = new KeyValuePair<string, object>[dictionary.Count];
			int i = 0;

			foreach (var pair in dictionary)
			{
				keyPairs[i] = pair;
				i++;
			}
		}

		if (Input.GetKeyDown(KeyCode.Y))
		{
			var keyValuePairs = new object[2]{1f, 2f};
			var stream = new MemoryStream(); 
			var formatter = new BinaryFormatter();
			formatter.Serialize(stream, keyValuePairs);
			stream.Position = 0;

			var value = (object[])formatter.Deserialize(stream);

			Debug.Log(value[0]);
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

	public class SomeOtherClass
	{
		public Dictionary<string, object> dictionary = new Dictionary<string, object>();

		public SomeOtherClass()
		{
			dictionary.Add("hello", 2f);
		}
	}
}

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

	private Dictionary<int, float> dictionary;

	private SomeGenericClass<string> generic = new SomeGenericClass<string>();

	public void OnEnable()
	{
		someClass = new SomeClass(1f);
		someStruct = new SomeStruct(1f);
		dictionary = new Dictionary<int, float>();
		dictionary.Add(1, 2f);
		dictionary.Add(3, 4f);
	}
	// Update is called once per frame
	void Update () 
	{
        /*
        if (Input.GetKeyDown(KeyCode.O) || Input.GetButtonDown("A_1"))
        {
			Debug.Log(typeof(SomeClass).GetProperty("SomeValue").GetValue(someClass, null));
		}

		if (Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("B_1"))
        {
			Debug.Log(typeof(SomeStruct).GetProperty("SomeValue").GetValue(someStruct, null));
		}
        */
		//Base line test, this should crash.
		if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetButtonDown("A_1"))
		{
			var list = generic.dictionary.ToList();
			Debug.Log(list[0].Value);
		}
			
		//IEnumerable foreach.
		if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetButtonDown("B_1"))
		{
			var pairs = new KeyValuePair<int, float>[dictionary.Count];
			int i = 0;
			foreach (var keypair in dictionary)
			{
				pairs[i] = keypair;
				i++;
			}
			Debug.Log(pairs[0]);
		}
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("Y_1"))
        {
			var keyValuePairs = new KeyValuePair<int, float>[2]{new KeyValuePair<int, float>(1, 2f), new KeyValuePair<int, float>(3, 4f)};
			var stream = new MemoryStream(); 
			var formatter = new BinaryFormatter();
			formatter.Serialize(stream, keyValuePairs);
			Debug.Log(stream.Length);
		}

		/*if (Input.GetKeyDown(KeyCode.Y))
		{
			var keyValuePairs = new KeyValuePair<int, float>[2]{new KeyValuePair<int, float>(1, 2f), new KeyValuePair<int, float>(3, 4f)};
			var stream = new MemoryStream(); 
			var formatter = new BinaryFormatter();
			formatter.Serialize(stream, keyValuePairs);
			Debug.Log(stream.Length);
		}*/
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

	public class SomeGenericClass<T>
	{
		public Dictionary<string, T> dictionary;

		public SomeGenericClass()
		{
			dictionary = new Dictionary<string, T>();
			dictionary.Add("hello", default(T));
		}
	}
}

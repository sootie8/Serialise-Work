using UnityEngine;
using System.Collections.Generic;

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
			Debug.Log(curve.keys[0] is System.ValueType);
			var d = new Dictionary<int, float>();
			d.DictToList();
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

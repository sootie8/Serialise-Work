using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ClassKeyValuePair<T1, T2>
{
	public T1 key;
	public T2 value;


	public ClassKeyValuePair(T1 _key, T2 _value)
	{
		key = _key;
		value = _value;
	}

	public ClassKeyValuePair(){}

	public static implicit operator KeyValuePair<T1, T2>(ClassKeyValuePair<T1, T2> pair)
	{
		return new KeyValuePair<T1, T2>(pair.key, pair.value);
	}

	public static implicit operator ClassKeyValuePair<T1, T2>(KeyValuePair<T1, T2> pair)
	{
		return new ClassKeyValuePair<T1, T2>(pair.Key, pair.Value);
	}
}
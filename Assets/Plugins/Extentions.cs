using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class Extentions 
{
	public static List<KeyValuePair<T1, T2>> DictToList<T1, T2>(this Dictionary<T1, T2> dict)
	{
		var list = new List<KeyValuePair<T1, T2>>(dict.Count);

		dict.ToArray();

		return list;
	}
}

﻿using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using UnitySerializerNG.FilePreferences;

public static class FilePrefs {
    private static GameObject QuitObject;

	private static DataContainer<string> stringData = new DataContainer<string>("str");

	private static DataContainer<float> floatData = new DataContainer<float>("fpn");

	private static DataContainer<int> intData = new DataContainer<int>("int");

    public static void DeleteAll() {
        stringData.Clear();
        floatData.Clear();
        intData.Clear();
    }

    public static void DeleteKey(string key) {
        stringData.Remove(key);
        floatData.Remove(key);
        intData.Remove(key);
    }

    public static float GetFloat(string key) {
        return (float)floatData.Get(key);
    }

    public static int GetInt(string key) {
        return (int)intData.Get(key);
    }

    public static string GetString(string key) {
        return (string)stringData.Get(key);
    }

    public static bool HasKey(string key) {
        return stringData.Find(key) || floatData.Find(key) || intData.Find(key);
    }

    public static void Save() {
        stringData.Save();
        floatData.Save();
        intData.Save();
    }

    public static void SetFloat(string key, float value) {
        floatData.Set(key, value);
    }

    public static void SetInt(string key, int value) {
        intData.Set(key, value);
    }

    public static void SetString(string key, string value) 
	{
		try
		{
			stringData.Set(key, value);
		}
		catch(System.Exception e)
		{
			Debug.Log(e);
			Debug.Log(stringData);
			Debug.Log(key);
			Debug.Log(value);
		}
    }

    // Only for debugging purposes!
    //public static int Count() {
    //    return stringData.Count() + floatData.Count() + intData.Count();
    //}

    //public static void PrintAll() {
    //    stringData.PrintAll();
    //    floatData.PrintAll();
    //    intData.PrintAll();
    //}
}

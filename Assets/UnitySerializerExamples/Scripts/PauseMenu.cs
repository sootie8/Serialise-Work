using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;
using Serialization;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private bool paused = false;
    [SerializeField]
    private GUITexture pausedGUI;
    [SerializeField]
    private string gameName = "Your Game";
    [SerializeField]
    private static bool logProgress = false;


    public void Save()
    {
        DateTime t = DateTime.Now;
        LevelSerializer.SaveGame(gameName);
        if (logProgress)
        {
            Debug.Log(string.Format("Saved in: {0:0.000} seconds", (DateTime.Now - t).TotalSeconds));
        }
    }

    private void Load(LevelSerializer.SaveEntry sg)
    {
        DateTime t = DateTime.Now;
        LevelSerializer.LoadNow(sg.Data);
        if (logProgress)
        {
            Debug.Log(string.Format("Loaded in: {0:0.000} seconds", (DateTime.Now - t).TotalSeconds));
        }
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    private void Start() {

        Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
		//Serialization.UnitySerializer.AddPrivateType(typeof(AnimationClip));
        if (pausedGUI)
            pausedGUI.enabled = false;

    }

    private void OnEnable() {
        LevelSerializer.Progress += HandleLevelSerializerProgress;
    }

    private void OnDisable() {
        LevelSerializer.Progress -= HandleLevelSerializerProgress;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P) || Input.GetButtonDown("Start_1"))
        {
            paused = !paused;

            if (paused)
            {
                Time.timeScale = 0.0f;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
                if (pausedGUI)
                    pausedGUI.enabled = true;
            }
            else
            {
                Time.timeScale = 1.0f;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
                if (pausedGUI)
                    pausedGUI.enabled = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetButtonDown("A_1"))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.L) || Input.GetButtonDown("Y_1"))
        {
            foreach (LevelSerializer.SaveEntry sg in LevelSerializer.SavedGames[LevelSerializer.PlayerName])
            {
                Load(sg);
                break;
            }
        }

		if (Input.GetButtonDown("B_1") || Input.GetKeyDown(KeyCode.Alpha6))
		{
			try {
				var stored = FilePrefs.GetString("_Save_Game_Data_");
				if (!string.IsNullOrEmpty(stored)) {
					try {
						LevelSerializer.SavedGames = UnitySerializer.Deserialize<Lookup<string, List<LevelSerializer.SaveEntry>>>(Convert.FromBase64String(stored));
					}
					catch {
						LevelSerializer.SavedGames = null;
					}
				}
				if (LevelSerializer.SavedGames == null) {
					LevelSerializer.SavedGames = new Index<string, List<LevelSerializer.SaveEntry>>();
					LevelSerializer.SaveDataToFilePrefs();
				}
			}
			catch (System.Exception e) {
				LevelSerializer.SavedGames = new Index<string, List<LevelSerializer.SaveEntry>>();
				Debug.Log(e);
			}

			foreach (var asm in AppDomain.CurrentDomain.GetAssemblies()) 
			{
				try
				{
					UnitySerializer.ScanAllTypesForAttribute((tp, attr) =>
						LevelSerializer.createdPlugins.Add(Activator.CreateInstance(tp)), asm, typeof(SerializerPlugIn));

					UnitySerializer.ScanAllTypesForAttribute((tp, attr) => 
						{
							LevelSerializer.CustomSerializers[((ComponentSerializerFor)attr).SerializesType] = Activator.CreateInstance(tp) as IComponentSerializer;
						}, asm, typeof(ComponentSerializerFor));
				}
				catch (System.Exception e)
				{
					Debug.Log(e);
				}
			}
		}
    }

	public void Load()
	{
		foreach (LevelSerializer.SaveEntry sg in LevelSerializer.SavedGames[LevelSerializer.PlayerName])
		{
			Load(sg);
			break;
		}
	}

    private void OnGUI() {
        if (!paused) {
            GUILayout.BeginArea(new Rect(200.0f, 10.0f, 400.0f, 20.0f));
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Press P to Pause");
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUILayout.EndArea();
            return;
        }

        GUIStyle box = "box";
        GUILayout.BeginArea(new Rect(Screen.width * 0.5f - 200.0f, Screen.height * 0.5f - 300.0f, 400.0f, 600.0f), box);

        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Save Game")) {
            Save();
        }
        GUILayout.Space(60.0f);
        foreach (LevelSerializer.SaveEntry sg in LevelSerializer.SavedGames[LevelSerializer.PlayerName]) {
            if (GUILayout.Button(sg.Caption))
            {
                Load(sg);
            }
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    private static void HandleLevelSerializerProgress(string section, float complete) {
        if (logProgress) {
            Debug.Log(string.Format("Progress on {0} = {1:0.00%}", section, complete));
        }
    }

    
}

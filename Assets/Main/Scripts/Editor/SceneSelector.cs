using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SceneSelector : EditorWindow
{
    [MenuItem("Featherskull/Scenes/Load Playground")]
    public static void ShowPlaygroundScene()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Main/Scenes/Playground.unity");
    }
    [MenuItem("Featherskull/Utility/Clear All PlayerPrefs")]
    public static void ClearPlayerPrefs()
    {
        if (EditorUtility.DisplayDialogComplex("Clear All Player Prefs", "Are you sure you want to clear all Player Prefs?", "Yes please", "Not a chance", "Cancel") == 0)
        {
            PlayerPrefs.DeleteAll();
        }
    }
}

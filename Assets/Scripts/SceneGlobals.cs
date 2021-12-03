using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneGlobals
{
    public static int Tries { get; set; }
    public static bool FirstRun { get; set; }

    private static string CurrentScene;

    public static void Setup(bool ForceSetup = false)
    {
        Debug.Log("SceneGlobals " + ForceSetup);
        if (ForceSetup || CurrentScene != SceneManager.GetActiveScene().name)
        {
            FirstRun = true;
            CurrentScene = SceneManager.GetActiveScene().name;
        }
    }

}

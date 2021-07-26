using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneSelectDB", menuName = "Project Quimbly/SceneSelectDB", order = 0)]
public class SceneSelectDB : ScriptableObject 
{
    [SerializeField] string[] loadingAreas;

    public string[] GetLoadingAreas()
    {
        return loadingAreas;
    }
}

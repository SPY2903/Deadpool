using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "ScriptableObject/SoundData")]
public class SoundData : ScriptableObject
{
    public List<SoundDetail> lst;
}

[System.Serializable]
public class SoundDetail
{
    public string name;
    public AudioClip audioClip;
}

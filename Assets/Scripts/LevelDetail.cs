using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LevelDetail", menuName = "ScriptableObject/LevelDetail")]
public class LevelDetail : ScriptableObject
{
    public List<bool> expTriggers;
    public void UpdateExpTriggers(int index)
    {
        expTriggers[index] = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObject/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int exp;
    public int levelPass;
    public int currentDamePoint = 1;
    public int currentHealthPoint = 1;
    public int currentRecoveryPoint = 1;
}

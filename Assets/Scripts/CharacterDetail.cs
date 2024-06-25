using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CharacterDetail",menuName = "ScriptableObject/CharacterDetail")]
public class CharacterDetail : ScriptableObject
{
    public int health;
    public int currentHealth;
    public int healthRecover;
    public int dame;
    public float speed;
    public float speedUp;
    public float jumpHeight;
    public float jumpSpeed;
    public float fallSpeed;
}

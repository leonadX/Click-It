using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName ="Enemy")]
public class Enemy : ScriptableObject
{
    // Boss data
    public string enemyName;
    public float health;
    public Sprite enemyImage;

    // Weapon data
    public string weaponName;
    public float damage;

}

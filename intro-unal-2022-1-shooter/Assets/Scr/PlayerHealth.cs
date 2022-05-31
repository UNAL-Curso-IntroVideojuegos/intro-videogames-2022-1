using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Health")]
public class PlayerHealth : ScriptableObject
{
    public int Health;
    public int TotalHealth;
    public Sprite Icon;

    public void UpdateHealth()
    {
        //..
    }
}

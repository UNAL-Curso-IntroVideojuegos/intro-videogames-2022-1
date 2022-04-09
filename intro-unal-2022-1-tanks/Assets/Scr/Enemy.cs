using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int health = 3;
    
    public void TakeDamage()
    {
        Debug.Log("Hit!");
        health--;
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}

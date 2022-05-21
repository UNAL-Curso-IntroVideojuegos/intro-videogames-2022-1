using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDPlayerHealth : MonoBehaviour
{
    [SerializeField]
    private Transform _heartsContiner;
    private List<Image> _hearts;
    
    
    void Start()
    {
        int childCount = _heartsContiner.childCount;
        _hearts = new List<Image>(childCount);
        
        for (int i = 0; i < childCount; i++)
        {
            _hearts.Add(_heartsContiner.GetChild(i).GetComponent<Image>());
        }

        GameEvents.OnPlayerHealthChangeEvent += OnPlayerHealthChange;
    }

    private void OnDestroy()
    {
        GameEvents.OnPlayerHealthChangeEvent -= OnPlayerHealthChange;
    }

    private void OnPlayerHealthChange(int health)
    {
        for (int i = 0; i < _hearts.Count; i++)
        {
            if (i < health)
            {
                _hearts[i].color = Color.white;
            }
            else
            {
                _hearts[i].color = new Color(0.3f, 0.3f, 0.3f, 1);
            }
                
        }
    }
}

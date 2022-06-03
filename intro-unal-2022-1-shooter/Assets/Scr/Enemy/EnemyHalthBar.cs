using UnityEngine;
using UnityEngine.UI;

public class EnemyHalthBar : MonoBehaviour
{
    [Header("2D")]
    public Transform _healthMask;

    //[Header("UI - Test")]
    //public RectTransform _healthBar;
    // Image _healthImage;
    
    public virtual void UpdateHealthBar(float health, float totalHealth, int damage)
    {
        //_healthBar.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up*2);
        //_healthImage.fillAmount = _health / _totalHealth;

        _healthMask.localScale = new Vector3(health / totalHealth, 1, 1);
        gameObject.SetActive(health > 0);
    }
}

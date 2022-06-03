using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDPlayerAmmo : MonoBehaviour
{
   [SerializeField]
   private TextMeshProUGUI _ammoText;

   private void Start()
   {
      GameEvents.OnPlayerAmmoUpdatedEvent += OnPlayerAmmoUpdated;
   }
   
   
   private void OnDestroy()
   {
      GameEvents.OnPlayerAmmoUpdatedEvent -= OnPlayerAmmoUpdated;
   }

   private void OnPlayerAmmoUpdated(int ammo)
   {
      _ammoText.text = ammo.ToString();
   }
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResourceUI : MonoBehaviour
{
    [SerializeField] private Text _text;
    
    public void UpdateUI(int amount)
    {
        _text.text = "Gold: " + amount;
    }
}

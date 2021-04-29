using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
public class DamageText : MonoBehaviour
{
    [SerializeField] private Text _text;
    
    public void SetText(float amount)
    {
        _text.text = amount.ToString();
    }

    // called from animation
    public void DestroyText()
    {
        Destroy(gameObject);
    }
}

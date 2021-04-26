using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetInRangeChecker))]
public abstract class Tower : MonoBehaviour
{
    protected TargetInRangeChecker targetInRangeChecker;

    private Renderer[] _renderers;
    private Dictionary<Material, Color> _materialMap = new Dictionary<Material, Color>();

    private const string shaderColor = "_Color";
    
    private void Awake()
    {
        targetInRangeChecker = GetComponent<TargetInRangeChecker>();
        _renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in _renderers)
        {
            Material[] materials = renderer.materials;

            foreach (Material material in materials)
            {
                _materialMap.Add(material, material.color);
            }
        }
    }
    
    void Update()
    {
        if (!CanAttack()) return;

        Attack();
    }

    protected abstract bool CanAttack();
    protected abstract void Attack();

    /*
    public void SetTowerAlpha(float alpha)
    {
        foreach (var renderer in _renderers)
        {
            foreach (Material material in renderer.materials)
            {
                Color color = material.color;
                color.a = alpha;
                material.SetColor(shaderColor, color);
            }
        }
    }
    */
    public void SetColor(Color color)
    {
        foreach (Renderer renderer in _renderers)
        {
            Material[] materials = renderer.materials;

            foreach (Material material in materials)
            {
                material.color = color;
            }
        }
    }

    public void SetDefaultColor()
    {
        foreach (Renderer renderer in _renderers)
        {
            Material[] materials = renderer.materials;

            foreach (Material material in materials)
            {
                material.color = _materialMap[material];
            }
        }
    }

    public void SetPlacementColor(Color color)
    {
        
    }
}

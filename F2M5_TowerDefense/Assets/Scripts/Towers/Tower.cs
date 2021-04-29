using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] private int _buildCost;
    public int BuildCost => _buildCost;
    
    protected float timer;

    private Renderer[] _renderers;
    private Dictionary<Material, Color> _materialMap = new Dictionary<Material, Color>();

    private const string shaderColor = "_Color";
    
    protected virtual void Awake()
    {
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
    
    protected virtual void Update()
    {
        timer += Time.deltaTime;
        if (!CanAttack()) return;

        Attack();
    }
    
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
    
    protected abstract bool CanAttack();
    protected abstract void Attack();
}

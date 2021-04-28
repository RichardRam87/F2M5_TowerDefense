using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private Tower[] _towers;
    [SerializeField] private LayerMask _layer;
    [SerializeField] [Range(0, 1)] private float _placingAlpha;

    [SerializeField] private Color _invalidPositionColor;
    [SerializeField] private Color _validPositionColor;

    private Tower _selectedTower;
    private bool _isBuilding;
    public bool IsBuilding => _isBuilding;
    private GameObject _towerHolder;

    private void Awake()
    {
        _validPositionColor.a = _placingAlpha;
        _invalidPositionColor.a = _placingAlpha;

        _towerHolder = new GameObject("Towers");
    }

    public Tower SelectTower(int index)
    {
        _selectedTower = Instantiate(_towers[index], _towerHolder.transform);
        _selectedTower.enabled = false;
        _isBuilding = true;
        
        return _selectedTower;
    }

    public Tile UpdateTowerPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layer))
        {
            Tile tile = hit.transform.GetComponent<Tile>();
            _selectedTower.transform.position = tile.transform.position;
                
            // todo: should we check if we need to update
            Color currentColor = tile.Buildable ? _validPositionColor : _invalidPositionColor;
            _selectedTower.SetColor(currentColor);
            
            return tile;
        }
        return null;
    }

    public void PlaceTower(Tile tile)
    {
        tile.SetBuildable(false);
        _selectedTower.SetDefaultColor();
        _selectedTower.enabled = true;
        _isBuilding = false;
    }

    public void CancelBuild()
    {
        _isBuilding = false;
        Destroy(_selectedTower.gameObject);
        _selectedTower = null;
    }
}

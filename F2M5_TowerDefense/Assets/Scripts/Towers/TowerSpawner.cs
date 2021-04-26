using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private Tower[] _towers;
    [SerializeField] private LayerMask _layer;
    [SerializeField] [Range(0, 1)] private float _placingAlpha;

    [SerializeField] private Color _invalidPositionColor;
    [SerializeField] private Color _validPositionColor;
    
    private Tower _selectedTower;
    private bool _isBuilding;

    private void Awake()
    {
        _validPositionColor.a = _placingAlpha;
        _invalidPositionColor.a = _placingAlpha;
    }

    void Update()
    { 
        if (_isBuilding) return;
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SelectTower(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            SelectTower(1);
    }

    public void SelectTower(int index)
    {
        _selectedTower = _towers[index];
        
        StopCoroutine(BuildRoutine());
        StartCoroutine(BuildRoutine());
    }

    //todo: convert routine to into readable functions
    IEnumerator BuildRoutine()
    {
        Tower tower = Instantiate(_selectedTower, transform);
        
        tower.enabled = false;
        _isBuilding = true;

        // todo: set colorstate based on spawn position
        // now we can instantiate a tower without hovering a tile
        
        while (_isBuilding)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layer))
            {
                Tile tile = hit.transform.parent.GetComponent<Tile>();
                tower.transform.position = tile.transform.position;
                
                // todo: should we check if we need to update
                Color currentColor = tile.Buildable ? _validPositionColor : _invalidPositionColor;
                tower.SetColor(currentColor);
                
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    _isBuilding = false;
                    Destroy(tower.gameObject);
                }   
                else if (Input.GetMouseButtonDown(0) && tile.Buildable)
                {
                    tile.SetBuildable(false);
                    tower.SetDefaultColor();
                    tower.enabled = true;
                    _isBuilding = false;
                }
            }
            yield return null;
        }
        _selectedTower = null;
    }
}

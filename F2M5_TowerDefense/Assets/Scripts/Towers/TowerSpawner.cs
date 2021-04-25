using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private Tower[] _towers;
    [SerializeField] private LayerMask _layer;
    
    private Tower _selectedTower;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SelectTower(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            SelectTower(1);
    }

    public void SelectTower(int index)
    {
        _selectedTower = _towers[index];
        StartCoroutine(BuildRoutine());
    }

    //todo: convert routine to into readable functions
    IEnumerator BuildRoutine()
    {
        bool isBuilding = true;
        Tower tower = Instantiate(_selectedTower, transform);
        
        tower.enabled = false;
        _selectedTower = null;
        
        while (isBuilding)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layer))
            {
                Tile tile = hit.transform.GetComponent<Tile>();
                tower.transform.position = tile.transform.position;
                
                // check for valid position
                // todo: implement state for buildes tiles (or set buildalble bool)
                // todo: implement visual feedback for valid build tile
                bool validBuildTile = tile.Buildable;
                
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    isBuilding = false;
                    Destroy(tower.gameObject);
                }   
                else if (Input.GetMouseButtonDown(0))
                {
                    tower.enabled = true;
                    isBuilding = false;
                }
            }
            yield return null;
        }
    }
}

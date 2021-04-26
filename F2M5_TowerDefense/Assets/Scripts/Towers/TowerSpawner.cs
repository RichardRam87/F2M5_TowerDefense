using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private Tower[] _towers;
    [SerializeField] private LayerMask _layer;
    [SerializeField] [Range(0, 1)] private float _placingAlpha;

    [SerializeField] private Color _invalidPositionColor;
    
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
        
        StopCoroutine(BuildRoutine());
        StartCoroutine(BuildRoutine());
    }

    //todo: convert routine to into readable functions
    IEnumerator BuildRoutine()
    {
        bool isBuilding = true;
        Tower tower = Instantiate(_selectedTower, transform);
        Renderer towerRenderer = tower.GetComponent<Renderer>();
        Color towerColor = towerRenderer.material.color;
        Color currentColor = towerColor;
        
        towerColor.a = _placingAlpha;
        _invalidPositionColor.a = _placingAlpha;
        
        tower.enabled = false;
        _selectedTower = null;
        
        while (isBuilding)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layer))
            {
                Tile tile = hit.transform.GetComponent<Tile>();
                // height offset for now
                // todo: should be correctly implemented when art is implemented
                Vector3 heightOffsetPosition = new Vector3(tile.transform.position.x, 0.3f, tile.transform.position.z);
                tower.transform.position = heightOffsetPosition;
                
                // check for valid position
                // todo: implement state for buildes tiles (or set buildalble bool)
                // todo: implement visual feedback for valid build tile
                // todo: should we check if we need to update
                currentColor = tile.Buildable ? towerColor : _invalidPositionColor;
                towerRenderer.material.color = currentColor;
                
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    isBuilding = false;
                    Destroy(tower.gameObject);
                }   
                else if (Input.GetMouseButtonDown(0))
                {
                    towerColor.a = 1;
                    towerRenderer.material.color = towerColor;
                    tower.enabled = true;
                    isBuilding = false;
                }
            }
            yield return null;
        }
    }
}

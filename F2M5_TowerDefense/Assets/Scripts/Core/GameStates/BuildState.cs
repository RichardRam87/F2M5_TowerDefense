using UnityEngine;

// todo: Upgrade interface koppelen
public class BuildState : GameplayState
{
    [SerializeField] private TowerSpawner _towerSpawner;
    [SerializeField] private ResourceHandler _resourceHandler;

    private Tower _selectedTower;
    private void Awake()
    {
        OnStateEnter.AddListener(() =>_towerSpawner.gameObject.SetActive(true));
        OnStateExit.AddListener(() => _towerSpawner.gameObject.SetActive(false));
    }
    
    public override void StateUpdate(float deltaTime)
    {
        if (_towerSpawner.IsBuilding)
        {
            Tile tile = _towerSpawner.UpdateTowerPosition();

            if (Input.GetKeyDown(KeyCode.Escape))
                _towerSpawner.CancelBuild();
            
            if (!Input.GetMouseButtonDown(0)) return;
            if (tile == null) return;
            
            if (_resourceHandler.CanAffort(_selectedTower.BuildCost) && tile.Buildable)
            {
                _towerSpawner.PlaceTower(tile);
                _resourceHandler.AddGold(-_selectedTower.BuildCost);
            }
            else if (!_resourceHandler.CanAffort(_selectedTower.BuildCost))
            {
                Debug.Log("Not Enough Gold");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectTowerToBuild(0);
        }
            
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectTowerToBuild(1);
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (_selectedTower != null)
                    {
                        _selectedTower.ShowUpdateUI(false);
                    }
                    _selectedTower = hit.transform.GetComponent<Tower>();
                    if (_selectedTower == null) return;

                    _selectedTower.ShowUpdateUI(true);
                }
            }
            
            if (Input.GetKeyDown(KeyCode.Escape) && _selectedTower != null)
                _selectedTower.ShowUpdateUI(false);
        }
    }

    public override void StateExit()
    {
        base.StateExit();
        
        if (_towerSpawner.IsBuilding)
            _towerSpawner.CancelBuild();
    }

    public void SelectTowerToBuild(int index)
    {
        if (_selectedTower)
            _selectedTower.ShowUpdateUI(false);
        _selectedTower = _towerSpawner.SelectTower(index);
    }
}
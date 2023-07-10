using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    [SerializeField] private KeyCode _buildKey;
    [SerializeField] private KeyCode _foundationKey;
    [SerializeField] private KeyCode _wallKey;

    [SerializeField] private GameObject[] _buildingsToCreate;
    [SerializeField] private GameObject[] _buildingInstances;

    [SerializeField] private LayerMask _layersToIgnore;
    
    [SerializeField] private Material _allowBuild;
    [SerializeField] private Material _denyBuild;

    private GameObject _activeObject;
    private int _activeID;
    
    public Material AllowBuild => _allowBuild;
    public Material DenyBuild => _denyBuild;
    
    public static BuildingSystem Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        
        Destroy(gameObject);
    }

    private void Start()
    {
        InstantiateBuildings();
    }
    private void Update()
    {
        CreateBuildingInstance(0, _foundationKey);
        CreateBuildingInstance(1, _wallKey);
        
        Build(0, _buildKey);
    }

    public Vector3 BuildPosition()
    {
        RaycastHit hit;
        Vector3 startPos = Camera.main.transform.position;
        Vector3 endPos = Camera.main.transform.forward.normalized;

        if (Physics.Raycast(startPos, endPos, out hit, 100f, _layersToIgnore))
        {
            return hit.point;
        }
        else
        {
            return new Vector3(9999, 9999, 9999);
        }
    }

    private void Build(int buildingID, KeyCode keyCode)
    {
        if (_activeID == buildingID && Input.GetKeyDown(keyCode))
        {
            Instantiate(_buildingsToCreate[buildingID], BuildPosition(), Quaternion.identity);
        }
    }

    private void InstantiateBuildings()
    {
        foreach (GameObject building in _buildingInstances)
        {
            GameObject instance = Instantiate(building, BuildPosition(), Quaternion.identity);
            instance.SetActive(false);
        }
    }
    
    private void CreateBuildingInstance(int buildingID, KeyCode keyCode)
    {
        if (Input.GetKeyDown(keyCode))
        {
            if (_activeObject is not null)
                _activeObject.SetActive(false);
        
            _buildingInstances[buildingID].SetActive(true);
            _activeObject = _buildingInstances[buildingID];
            _activeID = buildingID;
        
            Debug.Log("Called");
        }
    }
    
}

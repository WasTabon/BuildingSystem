using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    [SerializeField] private Transform[] _connectionDots;

    private protected Vector3 _targetPosition;

    private protected HashSet<string> _tagsSet;
    
    private MeshRenderer _meshRenderer;
    
    private static float _halfObject = 2;
    
    
    private float _height;
    public static float DistanceToStop = 0.1f;
    public MeshRenderer MeshRenderer => _meshRenderer;
    public float Height => _height;
    public HashSet<string> TagsSet => _tagsSet;
    public Transform[] ConnectionDots => _connectionDots;

    protected virtual void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        GetObjectHeight();
    }

    private void Update()
    {
        if (gameObject.activeSelf)
            MoveBuilding();
        
        DeactivateObject();
    }

    private void OnTriggerStay(Collider coll)
    {
        if (TagsSet.Contains(coll.gameObject.tag))
        {
            SetBuildStatus(BuildingSystem.Instance.AllowBuild);
        }
        else
        { 
            SetBuildStatus(BuildingSystem.Instance.DenyBuild);
        }
    }

    private void DeactivateObject()
    {
        if (BuildingSystem.Instance.BuildPosition() == new Vector3(9999, 9999, 9999))
            gameObject.SetActive(false);
    }

    private void SetBuildStatus(Material buildingMaterial)
    {
        if (_meshRenderer.material != buildingMaterial)
            _meshRenderer.material = buildingMaterial;
    }

    private protected void GetObjectHeight()
    {
        _height = transform.localScale.y / _halfObject;
    }

    public abstract void MoveBuilding();
}

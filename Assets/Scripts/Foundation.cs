using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Foundation : Building
{
    protected override void Start()
    {
        base.Start();
        
        _tagsSet = new HashSet<string>()
        {
            "Terraing",
            "ConnectionDot",
            "Foundation"
        };
    }
    
    public override void MoveBuilding()
    {
        _targetPosition = new Vector3(BuildingSystem.Instance.BuildPosition().x, BuildingSystem.Instance.BuildPosition().y + Height, BuildingSystem.Instance.BuildPosition().z);
        if (Vector3.Distance(transform.position, _targetPosition) > DistanceToStop)
        {
            transform.position = Vector3.Lerp(transform.position, _targetPosition, 5 * Time.deltaTime);   
        }
    }
}

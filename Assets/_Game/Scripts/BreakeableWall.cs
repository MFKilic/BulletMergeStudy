using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakeableWall : BreakeableObjects , IBreakeable
{
    [SerializeField] private BreakeableType[] _breakeableTypes = null;
    private int _randomNumber;
    private int _breakeableNumber;
    public int OnBreak()
    {
        OnBreakBehavior();

        return _breakeableNumber;
    }

    public override void OnBreakBehavior()
    {
       gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        _randomNumber = Random.Range(0,_breakeableTypes.Length);
        GetComponent<MeshFilter>().mesh = _breakeableTypes[_randomNumber].breakeableMesh;
        GetComponent<MeshRenderer>().material = _breakeableTypes[_randomNumber].breakeableMaterial;
        _breakeableNumber = _breakeableTypes[_randomNumber].breakeableDamageIndex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Breakeable Type", menuName = "Breakeable Type")]
public class BreakeableType : ScriptableObject
{
    public int breakeableDamageIndex;
    public Mesh breakeableMesh;
    public Material breakeableMaterial;
    public int breakeablePriceIndex;

}

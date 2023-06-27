using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Bullet Type", menuName = "Bullet Type")]

public class BulletMergeType : ScriptableObject
{
    public int bulletMergeIndex;
    public int bulletHealth;
    public Mesh bulletMesh;
    public Material bulletMaterial;
    
}

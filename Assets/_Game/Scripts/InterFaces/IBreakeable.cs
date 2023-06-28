
using UnityEngine;

public interface IBreakeable
{
    public int OnBreak();

    public int OnPrice();

    public Mesh BreakeableMesh();

    public Material BreakeableMaterial();

}

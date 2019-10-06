using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct Damage : IComponentData {
    public int value;
    public Vector3 hitPoint;
}
public class DamageComponent : ComponentDataWrapper<Damage> { }
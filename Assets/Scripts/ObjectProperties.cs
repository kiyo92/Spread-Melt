using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "MeltObjects/MeltObject", order = 1)]
public class ObjectProperties : ScriptableObject
{
    public enum ObjectType {
        iceCream,
        cheese,
        yogurt
    }

    public ObjectType objectType;
    public float meltFactor;
    public Color objectColor;
}

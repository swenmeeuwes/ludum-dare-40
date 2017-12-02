using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum Prefab
{
    Fireball
}

[Serializable]
public class PrefabMap
{
    public Prefab key;
    public GameObject prefab;
}
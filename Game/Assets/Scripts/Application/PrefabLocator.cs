using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrefabLocator
{
    private static readonly string PrefabMappingPath = "Prefab Mapping";

    [SerializeField] private PrefabMapping _mapping;

    #region Singleton        
    private static PrefabLocator _instance;
    public static PrefabLocator Instance
    {
        get
        {
            if (_instance == null)
                _instance = new PrefabLocator();

            return _instance;
        }
    }
    #endregion

    public PrefabLocator()
    {
        _mapping = Resources.Load<PrefabMapping>(PrefabMappingPath);
    }

    public GameObject Locate(Prefab prefabId)
    {
        return _mapping.mapping.First(item => item.key == prefabId).prefab;
    }
}
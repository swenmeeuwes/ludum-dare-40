using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrefabLocator
{
    private static readonly string PrefabContextPath = "Prefab Context";

    [SerializeField] private PrefabContext _context;

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
        _context = Resources.Load<PrefabContext>(PrefabContextPath);
    }

    public GameObject Locate(Prefab prefabId)
    {
        return _context.mapping.First(item => item.key == prefabId).prefab;
    }
}
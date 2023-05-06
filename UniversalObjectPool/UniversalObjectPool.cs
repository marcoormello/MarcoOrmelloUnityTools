using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Universal Object Pool system.
/// Usage:
/// <code>InitializePool(prefab)</code> to preinstantiate objects(optional).
/// <code>GetObject(prefab)</code> returns available object from pool, instantiate new one or pull the oldest one.
/// <code>ReturnObject(object, prefab)</code> can be used to store object after use and can also create new pool of that object.
/// </summary>
public class UniversalObjectPool : MonoBehaviour
{
    private Dictionary<GameObject, List<GameObject>> _inactiveObjectPools = new Dictionary<GameObject, List<GameObject>>();
    private Dictionary<GameObject, List<GameObject>> _activeObjectPools = new Dictionary<GameObject, List<GameObject>>();

    [SerializeField] private int maxPoolSize = 10;

    /// <summary>
    /// Initializes the object pool by pre-instantiating the specified prefab and adding it to the inactive object pool.
    /// </summary>
    /// <param name="prefab">The prefab to pre-instantiate.</param>
    public void InitializePool(GameObject prefab)
    {
        for (var i = 0; i < maxPoolSize; i++)
        {
            var obj = Instantiate(prefab);
            ReturnObject(obj, prefab);
        }
    }

    /// <summary>
    /// Returns an inactive object from the object pool for the specified prefab, or instantiates a new one if necessary. 
    /// If the pool size limit was reached, returns the oldest object.
    /// </summary>
    /// <param name="prefab">The prefab to get an object from.</param>
    /// <returns>An available object from the pool.</returns>
    public GameObject GetObject(GameObject prefab)
    {
        if (!_inactiveObjectPools.ContainsKey(prefab))
        {
            _inactiveObjectPools[prefab] = new List<GameObject>();
            _activeObjectPools[prefab] = new List<GameObject>();
        }

        var inactivePool = _inactiveObjectPools[prefab];
        var activePool = _activeObjectPools[prefab];

        if (inactivePool.Count > 0)
        {
            var obj = inactivePool[0];
            inactivePool.RemoveAt(0);
            activePool.Add(obj);
            obj.SetActive(true);
            return obj;
        }
        
        if (activePool.Count < maxPoolSize)
        {
            var obj = Instantiate(prefab);
            activePool.Add(obj);
            return obj;
        }

        var firstElement = activePool[0];
        activePool.RemoveAt(0);
        activePool.Add(firstElement);
        return firstElement;
    }

    /// <summary>
    /// Returns an object to the object pool for the specified prefab and sets it to inactive.
    /// Can be fed with new Object, creating a new pool on runtime automatically.
    /// This approach also allows to start pooling objects that were not instantiated using "GetObject".
    /// </summary>
    /// <param name="obj">The object to return to the pool.</param>
    /// <param name="prefab">The prefab reference of that object.</param>
    public void ReturnObject(GameObject obj, GameObject prefab)
    {
        if (!_inactiveObjectPools.ContainsKey(prefab))
        {
            _inactiveObjectPools[prefab] = new List<GameObject>();
            _activeObjectPools[prefab] = new List<GameObject>();
        }

        var objectPool = _inactiveObjectPools[prefab];
        var activeList = _activeObjectPools[prefab];

        if (objectPool.Count >= maxPoolSize)
        {
            Destroy(obj);
            return;
        }

        obj.SetActive(false);
        activeList.Remove(obj);
        objectPool.Add(obj);
    }
}

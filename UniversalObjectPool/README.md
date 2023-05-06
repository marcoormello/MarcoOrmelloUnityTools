# Universal Object Pool

Universal Object Pool is a C# script for Unity that allows you to easily create and manage object pools. Object pooling can improve performance by reducing the overhead of instantiating and destroying objects at runtime. This script provides a simple and efficient solution for pooling objects that can be used in any Unity project.

## Usage

To use Universal Object Pool in your Unity project, follow these simple steps:

1.  Copy the `UniversalObjectPool.cs` file into your Unity project's `Assets/Scripts` directory.
2.  Attach the `UniversalObjectPool` component to any GameObject in your scene.
3.  Call the `InitializePool` method to pre-instantiate objects (optional).
4.  Call the `GetObject` method to get an available object from the pool.
5.  Call the `ReturnObject` method to return an object to the pool after use.

### Initializing the Object Pool

To initialize the object pool, call the `InitializePool` method and pass in the prefab to pre-instantiate. This will create a pool of objects that can be used later in the game.

    public GameObject prefab;
    public UniversalObjectPool objectPool;
    
    void Start()
    {
        objectPool.InitializePool(prefab);
    }

### Getting an Object from the Pool

To get an object from the pool, call the `GetObject` method and pass in the prefab of the object you want to get. If an available object is found in the pool, it will be returned. If the pool is empty, a new object will be instantiated.

    public GameObject prefab;
    public UniversalObjectPool objectPool;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var obj = objectPool.GetObject(prefab);
            obj.transform.position = Vector3.zero;
        }
    }

### Returning an Object to the Pool

To return an object to the pool, call the `ReturnObject` method and pass in the object to return and the prefab it was instantiated from. If the pool is full, the object will be destroyed. Otherwise, it will be set to inactive and added back to the pool for later use.

    public GameObject prefab;
    public UniversalObjectPool objectPool;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            objectPool.ReturnObject(other.gameObject, prefab);
        }
    }

## Notes

-   The `maxPoolSize` field can be set to limit the size of each object pool. Objects will be recycled in a first-in, first-out (FIFO) order once the pool is full.
-   The script uses two dictionaries to store inactive and active object pools for each prefab. If a new prefab is used with the `GetObject` or `ReturnObject` method, a new pool will be created automatically.
-   The `GetObject` and `ReturnObject` methods can be called from any script in your project as long as the `UniversalObjectPool` component is attached to a GameObject in the scene.
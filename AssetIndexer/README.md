# ObjectIndexer

The ObjectIndexer is a Unity script that allows you to index any object in a given asset folder and provides filtering capabilities. It can be used to quickly and easily filter a large collection of objects by specific criteria, allowing you to find the objects you need more quickly.

## Getting Started

To use the ObjectIndexer script, simply add it to any GameObject in your Unity project. You can then call the `IndexObjects` method to index all of the objects of a specific type in a specified folder path. Once the objects have been indexed, you can use the `Filter` method to filter the cached list of objects to return only those that meet the specified criteria.

## Usage

### Indexing Objects

To index all objects of a specific type in a specified folder path, call the `IndexObjects` method and pass in the path to the folder containing the objects. For example, to index all `GameObject` objects in the "Prefabs" folder, you would call:

    ObjectIndexer<GameObject> indexer = GetComponent<ObjectIndexer<GameObject>>();
    indexer.IndexObjects("Prefabs");

### Filtering Objects

To filter the cached list of objects to return only those that meet the specified criteria, call the `Filter` method and pass in a lambda expression that specifies the criteria that must be met for an object to be included in the filtered list. For example, to filter the indexed objects to only include those that have a `Rigidbody` component attached, you would call:

    IEnumerable<GameObject> filteredObjects = indexer.Filter(obj => obj.GetComponent<Rigidbody>() != null);

**Note:** The `Filter` method returns an `IEnumerable` of objects that meet the specified criteria. You can convert this to an array or list using the `ToArray` or `ToList` extension methods. For example:

    GameObject[] filteredArray = indexer.Filter(obj => obj.name.StartsWith("Cube")).ToArray();
    List<GameObject> filteredList = indexer.Filter(obj => obj.name.StartsWith("Sphere")).ToList();

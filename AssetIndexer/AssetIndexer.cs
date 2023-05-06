using System;
using UnityEngine;
using System.Collections.Generic;
using Object = UnityEngine.Object;

/// <summary>
/// Indexes any object in a given asset folder and provides filtering capabilities.
/// </summary>
public class AssetIndexer<T> : MonoBehaviour where T : Object
{
    /// <summary>
    /// List of all objects found in the indexed folder.
    /// </summary>
    public List<T> allObjects;

    /// <summary>
    /// Indexes all objects of the specified type in the specified folder path.
    /// </summary>
    /// <param name="folderPath">Path to the folder containing the objects.</param>
    public void IndexObjects(string folderPath)
    {
        var objects = Resources.LoadAll<T>(folderPath);

        foreach (var obj in objects)
        {
            allObjects.Add(obj);
        }
    }

    ///<summary>
    /// Filters the cached list of objects to return only those that meet the specified criteria.
    /// </summary>
    /// <param name="criteria">A lambda expression that specifies the criteria that must be met for an object to be included in the filtered list.</param>
    /// <returns>A collection of objects that meet the specified criteria.</returns>
    public IEnumerable<T> Filter(Func<T, bool> criteria)
    {
        foreach (var obj in allObjects)
        {
            if (criteria(obj))
            {
                yield return obj;
            }
        }
    }
}
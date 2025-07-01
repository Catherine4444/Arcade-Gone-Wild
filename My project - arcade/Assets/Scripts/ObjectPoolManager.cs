
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager SharedInstance;
    public Dictionary<GameObject, ObjectPoolItem> objectsToPool;


    public class ObjectPoolItem
    {
        public List<GameObject> pooledObjects;
        public GameObject objectToPool;
        public int amountToPool;
    }

    void Awake()
    {
        // SharedInstance = this;
        // foreach (ObjectPoolItem item in objectsToPool)
        // {
        //     item.pooledObjects = new List<GameObject>();
        //     for (int i = 0; i < item.amountToPool; i++)
        //     {
        //         GameObject obj = Instantiate(item.objectToPool);
        //         obj.SetActive(false);
        //         item.pooledObjects.Add(obj);
        //         obj.transform.SetParent(this.transform); // set as children of Spawn Manager
        //     }
        // }
    }


    public GameObject GetPooledObject(GameObject prefab)
    {
        // // For as many objects as are in the pooledObjects list
        // foreach (ObjectPoolItem item in objectsToPool)
        // {
        //     if (item.objectToPool == prefab)
        //     {
        //         for (int i = 0; i < item.pooledObjects.Count; i++)
        //         {
        //             if (!item.pooledObjects[i].activeInHierarchy)
        //             {
        //                 item.pooledObjects[i].SetActive(true);
        //                 return item.pooledObjects[i];
        //             }
        //         }
        //         // If no object is available and shouldExpand is true, create a new one
        //         if (item.amountToPool > 0) 
        //         {
        //             GameObject obj = Instantiate(item.objectToPool);
        //             obj.SetActive(true);
        //             item.pooledObjects.Add(obj);
        //             return obj;
        //         } 
        //         else 
        //         {
        //             return null;
        //         }
        //     }
        // }
        // // otherwise, return null   
        return null;
    }

    public void ReturnPooledObject(GameObject obj)
    {
        // foreach (ObjectPoolItem item in objectsToPool)
        // {
        //     if (item.pooledObjects.Contains(obj))
        //     {
        //         obj.SetActive(false);
        //         return;
        //     }
        // }
    }

}


// Code adapted from: https://youtu.be/Ah3epb2HGCw?feature=shared
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    //public static ObjectPoolManager SharedInstance;

    [SerializeField] private bool _addToDontDestroyOnLoad = false; 
    
    private static GameObject _emptyHolder;

    private static Dictionary<GameObject, ObjectPool<GameObject>> _objectPools;
    private static Dictionary<GameObject, GameObject> _cloneToPrefabMap;


    private static GameObject _enemiesEmpty;
    private static GameObject _powerupsEmpty;
    private static GameObject _particleSystemsEmpty;
    private static GameObject _soundFXEmpty;
    private static GameObject _humansEmpty;

    public enum PoolType
    {
        Enemies,
        Powerups,
        ParticleSystems,
        SoundFX,
        Humans
    }
    public static PoolType PoolingType;

    private void SetupEmpties()
    {
        _emptyHolder = new GameObject("Object Pools");

        _enemiesEmpty = new GameObject("Enemies");
        _enemiesEmpty.transform.SetParent(_emptyHolder.transform);

        _powerupsEmpty = new GameObject("Power Ups");
        _powerupsEmpty.transform.SetParent(_emptyHolder.transform);

        _particleSystemsEmpty = new GameObject("Particle Systems");
        _particleSystemsEmpty.transform.SetParent(_emptyHolder.transform);
        
        _soundFXEmpty = new GameObject("Sound FX");
        _soundFXEmpty.transform.SetParent(_emptyHolder.transform);

        _humansEmpty = new GameObject("Humans");
        _humansEmpty.transform.SetParent(_emptyHolder.transform);


        if(_addToDontDestroyOnLoad)
        {
            DontDestroyOnLoad(_soundFXEmpty.transform.root);
        }
    }

        public static GameObject SetParentObject(PoolType poolType)
    {
        switch (poolType)
        {
            case PoolType.ParticleSystems:
                return _particleSystemsEmpty;
            
            case PoolType.Enemies:
                return _enemiesEmpty;
            
            case PoolType.Powerups:
                return _powerupsEmpty;

            case PoolType.SoundFX:
                return _soundFXEmpty;

            case PoolType.Humans:
                return _humansEmpty;

            default:
                return null;
        }

    }

    void Awake()
    {
        //SharedInstance = this;
        _objectPools = new Dictionary<GameObject, ObjectPool<GameObject>>();
        _cloneToPrefabMap = new Dictionary<GameObject, GameObject>();

        SetupEmpties();
    }


    private static void CreatePool(GameObject prefab, Vector3 pos, Quaternion rot, PoolType poolType = PoolType.Enemies)
    {
        ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
            createFunc: () => CreateObject(prefab, pos, rot, poolType),
            actionOnGet: OnGetObject,
            actionOnRelease: OnReleaseObject,
            actionOnDestroy: OnDestroyObject
        );

        _objectPools.Add(prefab, pool);

    }

    private static GameObject CreateObject(GameObject prefab, Vector3 pos, Quaternion rot, PoolType poolType = PoolType.Enemies)
    {
        prefab.SetActive(false); // only way to spawn in an object and not have awake or on enable called 

        GameObject obj = Instantiate(prefab, pos, rot); // make sure this 

        prefab.SetActive(true);

        GameObject parentObject = SetParentObject(poolType);
        obj.transform.SetParent(parentObject.transform);

        return obj;
    }

    private static void OnGetObject(GameObject obj)
    {
        // optional logic 
    }

    private static void OnReleaseObject(GameObject obj)
    {
        obj.SetActive(false);

    }

    private static void OnDestroyObject(GameObject obj)
    {
        if(_cloneToPrefabMap.ContainsKey(obj))
        {
            _cloneToPrefabMap.Remove(obj);
        }

    }
        

    private static T SpawnObject<T>(GameObject objectToSpawn, Vector3 spawnPos, Quaternion spawnRotation, PoolType poolType = PoolType.Enemies) where T : Object
    {
        if (!_objectPools.ContainsKey(objectToSpawn))
        {
            CreatePool(objectToSpawn, spawnPos, spawnRotation, poolType);
        }

        GameObject obj = _objectPools[objectToSpawn].Get();

        if(obj != null)
        {
            if(!_cloneToPrefabMap.ContainsKey(obj))
            {
                _cloneToPrefabMap.Add(obj, objectToSpawn);
            }

            obj.transform.position = spawnPos;
            obj.transform.rotation = spawnRotation;
            obj.SetActive(true);

            if(typeof(T) == typeof(GameObject))
            {
                return obj as T;
            }
            T component = obj.GetComponent<T>();
            if(component == null)
            {
                Debug.LogError($"Objec   {objectToSpawn.name} doesn't have component of type {typeof(T)}");
                return null; 
            }
            return component;
        }
        return null; 

    }

    public static T SpawnObject<T>(T typePrefab, Vector3 spawnPos, Quaternion spawnRotation, PoolType poolType = PoolType.Enemies) where T : Component 
    {
        return SpawnObject<T>(typePrefab.gameObject, spawnPos, spawnRotation, poolType);
    }

    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPos, Quaternion spawnRotation, PoolType poolType = PoolType.Enemies)
    {
        return SpawnObject<GameObject>(objectToSpawn, spawnPos, spawnRotation, poolType);
    }

    public static void ReturnObjectToPool(GameObject obj, PoolType poolType = PoolType.Enemies )
    {
        if (_cloneToPrefabMap.TryGetValue(obj, out GameObject prefab))
        {
            GameObject parentObject = SetParentObject(poolType);

            if (obj.transform.parent != parentObject.transform)
            {
                obj.transform.SetParent(parentObject.transform);
            }
            if(_objectPools.TryGetValue(prefab, out ObjectPool<GameObject> pool))
            {
                pool.Release(obj);
            }
        }
        else
        {
            {
                Debug.LogError("Trying to return an object that is not pooled: " +obj.name);
            }
        }
    }

    // public static int CountActive(GameObject objectToCount)
    // {
    //     return _objectPools[objectToCount].CountActive;
    // }

}

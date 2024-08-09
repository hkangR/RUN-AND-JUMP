using System.Collections;
using System.Collections.Concurrent;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    private ConcurrentDictionary<string, ConcurrentQueue<GameObject>> objectPool = new ConcurrentDictionary<string, ConcurrentQueue<GameObject>>();
    
    private void Awake() 
    {
        if (instance == null) 
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    public GameObject GetObject(GameObject prefab, Transform parentTransform)
    {
        string prefabName = prefab.name;
        if (!objectPool.ContainsKey(prefabName) || objectPool[prefabName].Count == 0)
        {
            GameObject newObject = Instantiate(prefab, parentTransform);
            PushObject(newObject);
        }

        GameObject obj;
        if (objectPool.TryGetValue(prefabName, out var queue) && queue.TryDequeue(out obj))
        {
            obj.transform.SetParent(parentTransform);
            obj.transform.localPosition = Vector3.zero;
            obj.SetActive(true);
            return obj;
        }

        return null; 
    }
    public GameObject GetObject(GameObject prefab,Vector3 position ,Transform parentTransform)
    {
        string prefabName = prefab.name;
        if (!objectPool.ContainsKey(prefabName) || objectPool[prefabName].Count == 0)
        {
            GameObject newObject = Instantiate(prefab,position,Quaternion.identity, parentTransform);
            PushObject(newObject);
        }

        GameObject obj;
        if (objectPool.TryGetValue(prefabName, out var queue) && queue.TryDequeue(out obj))
        {
            obj.transform.SetParent(parentTransform);
            obj.transform.localPosition = Vector3.zero;
            obj.SetActive(true);
            return obj;
        }

        return null; 
    }

    public void PushObject(GameObject prefab)
    {
        string prefabName=prefab.name.Replace("(Clone)", string.Empty);;
        prefab.SetActive(false);

        if (!objectPool.ContainsKey(prefabName))
        {
            objectPool.TryAdd(prefabName, new ConcurrentQueue<GameObject>());
        }

        objectPool[prefabName].Enqueue(prefab);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour
    where T: Component
{
    private static T instance;
    public static T Instance 
    {
        get
        {
            if (instance == null)
            {
                T[] objects = FindObjectsOfType(typeof(T)) as T[];
                if (objects.Length > 0)
                {
                    instance = objects[0];
                }
                if (objects.Length > 1)
                {
                    Debug.LogError("There is more than one " + typeof(T).Name + " in the scene.");
                }
                if (instance == null)
                {
                    Debug.LogError("There isn't any " + typeof(T).Name + " in the scene, creating one.");
                    
                    GameObject obj = new GameObject();
                    obj.name = string.Format("_{0}", typeof(T).Name);
                    instance = obj.AddComponent<T>();
                }
            }

            return instance;
        }
    }
}

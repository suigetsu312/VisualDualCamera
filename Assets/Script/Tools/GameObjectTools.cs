using System.Collections;
using UnityEngine;

namespace Assets
{
    public class GameObjectTools
    {
        
        public T? FindChildrenByName<T>(MonoBehaviour mono ,string name) where T : Component
        {
            var result = mono.transform.Find(name);
            if (result != null) return result.GetComponent<T>();
            return null;
        }

    }
}
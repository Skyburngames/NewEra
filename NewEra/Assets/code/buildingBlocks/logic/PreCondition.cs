using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace BuildingBlocksNS
{
    [System.Serializable]
    public class PreCondition : ScriptableObject
    {
   



        protected T GetLastObjectOfTypeFromDataArray<T>(List<Object> dataArray)
        {
            for (int i = dataArray.Count - 1; i >= 0; i--)
            {
                if (dataArray[i] is T)
                {
                    T result = (T)System.Convert.ChangeType(dataArray[i], typeof(T)); //Cast to generic type T (should always succeed since first is checked if it is off type T
                    return result;
                }
            }
            return default(T);
        }

    }
}

using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {

    #region Components
    private static T instance = null;
    #endregion

    #region Properties
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T;
                if (instance == null)
                    Debug.Log("There is No Active '" + typeof(T) + "'in this Scene");

            }
            return instance;
        }
    }
    #endregion
}

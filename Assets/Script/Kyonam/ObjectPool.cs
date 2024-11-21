using System.Collections.Generic;
using UnityEngine;
using System.Text;

public abstract class ObjectPool<Class, T> : Singleton<ObjectPool<Class, T>> where T : Component
{
    #region Values
    private string objName; // 오브젝트 이름 -> 구분용
    [SerializeField]
    private int activeCount, prepareCount;  // 활성화 카운트, 총 준비 카운트
    #endregion

    #region Components
    private GameObject objParent;
    private T originObject;
    protected List<T> objectList = new List<T>();
    public Class childClass;
    #endregion

    // 오브젝트 풀 설정
    protected void InitPool(Class _childClass, T _originObject, string _objName, int _prepareCount = 10, GameObject _objParent = null)  
    {
        childClass = _childClass;
        originObject = _originObject;
        objName = _objName;

        if (_objParent == null)
            objParent = new GameObject(objName + "_Pool");
        else
            objParent = _objParent;

        activeCount = 0;
        prepareCount = _prepareCount;
        PrepareObject(prepareCount);
    }

    // 풀 생성
    private void PrepareObject(int _prepareCount)
    {
        StringBuilder strBulder = new StringBuilder();

        T temp;

        int tempObjectListCount = objectList.Count;
        for (int i = 0; i < _prepareCount; i++)
        {
            // Create Bullet
            temp = null;
            temp = Instantiate(originObject, objParent.transform);

            // Naming Bullet
            strBulder.Clear();
            strBulder.Append(objName);
            strBulder.Append(tempObjectListCount + i);
            temp.name = strBulder.ToString();

            // De-Activate Bullet
            temp.gameObject.SetActive(false);

            // Add to List
            objectList.Add(temp);
        }
    }
    // 풀에서 오브젝트 뗴어 주기.
    public T GetObject(bool active = true)
    {
        T newActiveObject = objectList[activeCount++];

        if (activeCount >= objectList.Count)
            activeCount = 0;

        newActiveObject.gameObject.SetActive(true);
        
        return newActiveObject;
    }
}
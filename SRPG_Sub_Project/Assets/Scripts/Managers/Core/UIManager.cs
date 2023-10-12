using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    int _order = 10;

    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    UI_Scene _scene = null;

    // <summary>
    // UI_Root를 만들고 Root의 자식 obj로 저장하는 변수
    // </summary>
    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

    // <summary>
    // Popup UI의 sorting 순서를 관리하는 함수
    // </summary>
    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort) //sort 사용 (popup)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else //sort 사용 X
        {
            canvas.sortingOrder = 0;
            Debug.Log("sort비사용");
        }
    }

    // <summary>
    // 2D 상황에서 프리펩에는 붙지 않고 직접 붙이고 싶을 때 사용
    // </summary>
    public T Make2DUI<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/2D/{name}");
        if (parent != null)
            go.transform.SetParent(parent);

        Canvas canvas = go.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        return Util.GetOrAddComponent<T>(go);
    }

    // <summary>
    // 씬 UI를 보여주는 함수, 스택(info)로 팝업의 sorting order를 관리
    // </summary>
    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");
        T scene = Util.GetOrAddComponent<T>(go);
        _scene = scene;
        go.transform.SetParent(Root.transform);

        return scene;
    }

    // <summary>
    // Popup UI를 보여주는 함수, 스택(info)로 팝업의 sorting order를 관리
    // </summary>
    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");
        T popup = Util.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);
        go.transform.SetParent(Root.transform);

        return popup;
    }

    // <summary>
    // Popup UI를 닫는 함수, 스택(info)의 순서로 닫기
    // </summary>
    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;

        UI_Popup popup = _popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;
        _order--;
    }

    // <summary>
    // 닫으려는 Popup stack의 peek한 팝업창이 같은지 확인 후, 같다면 팝업창을 닫는 함수
    // </summary>
    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;

        if (_popupStack.Peek() != popup) //peek : 마지막 스택 엿보기
        {
            Debug.Log($"Close Popup Failed, peek : {_popupStack.Peek()}, popup : {popup}");
            return;
        }

        ClosePopupUI();
    }

    // <summary>
    // 모든 Popup UI를 닫는 함수
    // </summary>
    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }

    // <summary>
    // 모든 Popup UI와 Scene UI를 초기화 하는 함수
    // </summary>
    public void Clear()
    {
        CloseAllPopupUI();
        _scene = null;
    }
}

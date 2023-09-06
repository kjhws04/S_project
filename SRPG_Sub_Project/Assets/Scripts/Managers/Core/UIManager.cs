using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    int _order = 10;

    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    UI_Scene _scene = null;

    // <summary>
    // UI_Root�� ����� Root�� �ڽ� obj�� �����ϴ� ����
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
    // �˾� UI�� sorting ������ �����ϴ� �Լ�
    // </summary>
    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort) //sort ��� (popup)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else //sort ��� X
        {
            canvas.sortingOrder = 0;
            Debug.Log("sort����");
        }
    }    
    
    // <summary>
    // �� UI�� �����ִ� �Լ�, ����(info)�� �˾��� sorting order�� ����
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
    // �˾� UI�� �����ִ� �Լ�, ����(info)�� �˾��� sorting order�� ����
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
    // �˾� UI�� �ݴ� �Լ�, ����(info)�� ������ �ݱ�
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
    // �������� �˾�â�� stack�� peek�� �˾�â�� ������ Ȯ�� ��, ���ٸ� �˾�â�� �ݴ� �Լ�
    // </summary>
    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;

        if (_popupStack.Peek() != popup) //peek : ������ ���� ������
        {
            Debug.Log($"Close Popup Failed, peek : {_popupStack.Peek()}, popup : {popup}");
            return;
        }

        ClosePopupUI();
    }

    // <summary>
    // ��� �˾� UI�� �ݴ� �Լ�
    // </summary>
    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }
}
using UnityEngine;
using Firebase.Auth;
using System;
using System.Threading.Tasks;
using Firebase.Extensions;
using System.Collections;
using Firebase.Database;
using Firebase;

public class FireBaseManager
{
    private FirebaseAuth auth; //�α���, ȸ������
    public FirebaseUser user; //������ ���� ����

    public DatabaseReference reference; //������ �����

    public Action<string> OnTextChanged; //ChangeText()
    public Action<bool> LoginStat;
    public string UserId => user.UserId;

    public bool loginSuccess = false;    

    public void Init()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        CheckAsync();
    }

    // <summary>
    // ���� play ����Ȯ���� ���� �Լ�
    // </summary>
    private void CheckAsync()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == Firebase.DependencyStatus.Available)
            {
                //�ʱ�ȭ ���
                auth = FirebaseAuth.DefaultInstance;
                auth.StateChanged -= AuthStateChanged;
                auth.StateChanged += AuthStateChanged;
            }
            else { Debug.Log("CheckAndFixDependenciesAsync Failed"); }
        });
    }

    // <summary>
    // ��������� �ʱ�ȭ �Լ�
    // </summary>
    private void AuthStateChanged(object sender, EventArgs e)
    {
        FirebaseAuth senderAuth = sender as FirebaseAuth;
        if (senderAuth != null)
        {
            user = senderAuth.CurrentUser;
            if (user != null)
            {
                Debug.Log(user.UserId);
            }
        }
    }

    #region Login and Sign
    // <summary>
    // ȸ�� ���� �Լ�
    // </summary>
    public void Register(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled) //��� ��� (���ͳ� ���� Ȯ��)
            {
                string message = $"Registration canceled. Make sure you have a stable internet connection and try again.";
                ChangeText(message);
            }
            if (task.IsFaulted)
            {
                string errorMessage = "Registration failed. Errors : ";
                foreach (Exception exception in task.Exception.Flatten().InnerExceptions)
                {
                    string message = errorMessage += exception.Message + " ";
                    ChangeText(message);
                }
                return;
            }
            if (task.IsCompleted)
            {
            //    //ȸ�� ���� ����
            //    string message = $"Registration successful!";
            //    ChangeText(message);
                  Managers.Fire.RegisterAfterSetting(); //�⺻ ����
            }
        });
    }

    // <summary>
    // try catch ��� �α��� �Լ�
    // </summary>
    public async void Login(string email, string password)
    {
        try
        {
            var task = await auth.SignInWithEmailAndPasswordAsync(email, password);
            user = task.User;
            Managers.Mission.Init();

            string message = $"Loding...";
            ChangeText(message);

            Debug.Log(UserId);
            Managers.Scene.LoadScene(Define.Scene.Main);
        }
        catch (Exception e)
        {
            string message = "Login Failed. Errors : " + e.Message;
            ChangeText(message);
        }
    }

    // <summary>
    // �α׾ƿ� �Լ�
    // </summary>
    public void Logout()
    {
        auth.SignOut();
        string message = "Logout Complete";
        ChangeText(message);
    }

    // <summary>
    // Ÿ ������ �ش� �Լ� ��������Ʈ ȣ��� ���ϴ� �ؽ�Ʈ�� ������Ű�ִ� �Լ�
    // </summary>
    public void ChangeText(string text)
    {
        // ��������Ʈ ȣ��
        OnTextChanged?.Invoke(text);
    }
    #endregion

    #region Chat DB
    // <summary>
    // Firebase�� datadase�� �޾� ChatMessage�� �ִ� ä�� �޽����� �޴� �Լ�
    // </summary>
    public void ReadChatMessage()
    {
        DatabaseReference chatDB = FirebaseDatabase.DefaultInstance.GetReference("ChatMessage");
        chatDB.GetValueAsync().ContinueWithOnMainThread(
            task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError("ReadErr");
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    Debug.Log($"child count : {snapshot.ChildrenCount}");
                    foreach (var message in snapshot.Children)
                    {
                        Debug.Log($"{message.Key} + {message.Child("username").Value.ToString()} + {message.Child("message").Value.ToString()}");
                    }
                }
            });
    }
    #endregion

    #region Item DB
    // <summary>
    // Firebase�� �������� �ٷ� ���� �ϴ� �Լ� [ �ʿ� ����) ticket1 0���� ���� ]
    // </summary>
    public void SaveItems(string _itemName, int itemCount)
    {
        reference.Child("users").Child(UserId).Child("items").Child(_itemName).SetValueAsync(itemCount)
            .ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("Save Cancel");
                }
                else if (task.IsFaulted)
                {
                    Debug.LogError("Save Error: " + task.Exception);
                }
                else
                {
                    Debug.Log("Save Successful");
                }
            });
    }

    // <summary>
    // Firebase�� �������� �����ϴ� �Լ� [ �ʿ� ����) ticket1 1�� �߰� ]
    // </summary>
    public async Task SaveItemsAsync(string itemName, int itemCountChange)
    {
        try
        {
            var dataSnapshot = await reference.Child("users").Child(UserId).Child("items").Child(itemName).GetValueAsync();

            if (dataSnapshot.Exists)
            {
                if (int.TryParse(dataSnapshot.Value.ToString(), out int itemCount))
                {
                    // ������ ������ ���� (���� ���� ����)
                    int newCount = itemCount + itemCountChange;

                    // ������ ���� �ʵ��� �ּҰ��� 0���� ����
                    newCount = Mathf.Max(newCount, 0);

                    // ���ο� ������ ����
                    await reference.Child("users").Child(UserId).Child("items").Child(itemName).SetValueAsync(newCount);
                }
                else
                {
                    Debug.LogError($"Failed to parse {itemName} count as an integer");
                }
            }
            else
            {
                Debug.LogWarning($"Item {itemName} not found");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("An error occurred: " + e.Message);
        }
    }

    // <summary>
    // Firebase�� ������ ������ �������� �Լ� [ �ʿ� ����) firebase�� ticket1 100�� ���� ]
    // </summary>
    public async Task<int> GetItemCountAsync(string itemName)
    {
        try
        {
            DataSnapshot dataSnapshot = await reference.Child("users").Child(UserId).Child("items").Child(itemName).GetValueAsync();

            int itemCount = -1;
            if (dataSnapshot.Exists)
            {
                if (int.TryParse(dataSnapshot.Value.ToString(), out itemCount))
                {
                    //Debug.Log($"{itemName} Count: {itemCount}");
                    return itemCount;
                }
                else
                {
                    Debug.LogError("Failed to parse item count as an integer");
                    return -1; // ������ ��� -1 ��ȯ
                }
            }
            else
            {
                Debug.LogWarning($"Item {itemName} not found");
                return 0; // �������� ���� ��� 0 ��ȯ �Ǵ� �ٸ� �⺻�� ���
            }
        }
        catch (Exception e)
        {
            Debug.LogError("An error occurred: " + e.Message);
            return -1; // ������ ��� -1 ��ȯ
        }
    }

    // <summary>
    // �ʱ� �������� ������ �����ϴ� �Լ�
    // </summary>
    public void RegisterAfterSetting()
    {
        SaveItems("_ticket1", 5000);
        SaveItems("_ticket2", 5000);
        SaveItems("_ticketFriend", 5000);
        SaveItems("_expItem", 5000);
    }
    #endregion

    #region Stage DB    
    // <summary>
    // Firebase�� ���� ���ھ save �ϴ� �Լ�
    // </summary>
    public async Task SaveHighScore(string bossName, int score)
    {
        try
        {
            await reference.Child("users").Child(UserId).Child("boss").Child(bossName).SetValueAsync(score);
            Debug.Log("Save Successful");
        }
        catch (OperationCanceledException)
        {
            Debug.LogError("Save Cancel");
        }
        catch (Exception ex)
        {
            Debug.LogError("Save Error: " + ex);
        }
    }

    // <summary>
    // Firebase�� ���� ���ھ get �ϴ� �Լ�
    // </summary>
    public async Task<int> GetHighScore(string bossName)
    {
        try
        {
            var snapshot = await reference.Child("users").Child(UserId).Child("boss").Child(bossName).GetValueAsync();
            if (snapshot.Exists)
            {
                return Convert.ToInt32(snapshot.Value);
            }
            else
            {
                Debug.LogError("�ش� �ϴ� ���� �̸��� ����");
                return 0;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Get HighScore Error: " + ex);
            throw; // �ʿ��� ��� �ٽ� throw�Ͽ� ����
        }
    }
    #endregion
}

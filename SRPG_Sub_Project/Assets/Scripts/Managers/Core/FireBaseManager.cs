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
    private FirebaseAuth auth; //로그인, 회원가입
    public FirebaseUser user; //인증된 유저 정보

    public DatabaseReference reference; //데이터 저장용

    public Action<bool> LoginStat;
    public string UserId => user.UserId;

    public bool loginSuccess = false;

    string em;
    string pw;

    UserData _userData;

    public void Init()
    {
        _userData = Managers.Game.GetUserData().GetComponent<UserData>();
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        CheckAsync();
    }

    // <summary>
    // 구글 play 버전확인을 위한 함수
    // </summary>
    private void CheckAsync()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == Firebase.DependencyStatus.Available)
            {
                //초기화 목록
                auth = FirebaseAuth.DefaultInstance;
                auth.StateChanged -= AuthStateChanged;
                auth.StateChanged += AuthStateChanged;
            }
            else { Debug.Log("CheckAndFixDependenciesAsync Failed"); }
        });
    }

    // <summary>
    // 정보변경시 초기화 함수
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
    // 회원 가입 함수
    // </summary>
    public void Register(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled) //등록 취소 (인터넷 연결 확인)
            {
                Debug.LogError("Registration canceled. Make sure you have a stable internet connection and try again.");
                return;
            }
            if (task.IsFaulted)
            {
                string errorMessage = "Registration failed. Errors: ";
                foreach (Exception exception in task.Exception.Flatten().InnerExceptions)
                {
                    errorMessage += exception.Message + " ";
                }
                Debug.LogError(errorMessage);
                return;
            }
            if (task.IsCompleted)
            {
                //회원 가입 성공
                Debug.Log("Registration successful.");
                Managers.Fire.RegisterAfterSetting(); //기본 세팅
            }
        });
    }

    // <summary>
    // try catch 사용 로그인 함수
    // </summary>
    public async void Login(string email, string password)
    {
        try
        {
            var task = await auth.SignInWithEmailAndPasswordAsync(email, password);
            user = task.User;
            Debug.Log($"Login Success. Welcome {user.Email}");
            Debug.Log(UserId);
            Managers.Scene.LoadScene(Define.Scene.Main);
        }
        catch (Exception e)
        {
            Debug.Log("Login Failed. Exception message: " + e.Message);
        }
    }

    // <summary>
    // 로그아웃 함수
    // </summary>
    public void Logout()
    {
        auth.SignOut();
        Debug.Log("Logout Complete");
    }
    #endregion

    #region Chat DB
    // <summary>
    // Firebase의 datadase를 받아 ChatMessage에 있는 채팅 메시지를 받는 함수
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
    // Firebase에 아이템을 저장하는 함수
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
    // Firebase에 아이템을 차감 증감하는 함수
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
                    // 차감된 개수를 적용 (음수 값도 가능)
                    int newCount = itemCount + itemCountChange;

                    // 음수로 되지 않도록 최소값을 0으로 설정
                    newCount = Mathf.Max(newCount, 0);

                    // 새로운 개수를 저장
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
    // Firebase에 아이템 비교할 수 있는 함수
    // </summary>
    public async Task<bool> CompareItemAsync(string itemNameToCompare, int compareCount)
    {
        try
        {
            DataSnapshot dataSnapshot = await reference.Child("users").Child(UserId).Child("items").Child(itemNameToCompare).GetValueAsync();

            int itemCount = -1;
            if (dataSnapshot.Exists)
            {
                if (int.TryParse(dataSnapshot.Value.ToString(), out itemCount))
                {
                    Debug.Log($"{itemNameToCompare} Count: {itemCount}");

                    // itemCount와 compareCount 비교
                    return itemCount > compareCount;
                }
                else
                {
                    Debug.LogError("Failed to parse item count as an integer");
                    return false;
                }
            }
            else
            {
                Debug.LogWarning($"Item {itemNameToCompare} not found");
                return false;
            }
        }
        catch (Exception e)
        {
            Debug.LogError("An error occurred: " + e.Message);
            return false;
        }
    }

    // <summary>
    // Firebase에 아이템 개수를 가져오는 함수
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
                    return -1; // 에러일 경우 -1 반환
                }
            }
            else
            {
                Debug.LogWarning($"Item {itemName} not found");
                return 0; // 아이템이 없을 경우 0 반환 또는 다른 기본값 사용
            }
        }
        catch (Exception e)
        {
            Debug.LogError("An error occurred: " + e.Message);
            return -1; // 에러일 경우 -1 반환
        }
    }

    public async Task SaveItemCountAsync(string itemName, int itemCount)
    {
        try
        {
            await reference.Child("users").Child(UserId).Child("items").Child(itemName).SetValueAsync(itemCount);
            Debug.Log($"Saved {itemName} count: {itemCount}");
        }
        catch (Exception e)
        {
            Debug.LogError("An error occurred: " + e.Message);
        }
    }

    public void RegisterAfterSetting()
    {
        SaveItems("_ticket1", 10);
        SaveItems("_ticket2", 0);
        SaveItems("_ticketFriend", 50);
        SaveItems("_expItem", 10);
    }
    #endregion
}

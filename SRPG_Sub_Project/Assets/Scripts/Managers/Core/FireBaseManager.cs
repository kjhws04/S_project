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
            //    //회원 가입 성공
            //    string message = $"Registration successful!";
            //    ChangeText(message);
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
    // 로그아웃 함수
    // </summary>
    public void Logout()
    {
        auth.SignOut();
        string message = "Logout Complete";
        ChangeText(message);
    }

    // <summary>
    // 타 씬에서 해당 함수 델리게이트 호출로 원하는 텍스트를 구독시키주는 함수
    // </summary>
    public void ChangeText(string text)
    {
        // 델리게이트 호출
        OnTextChanged?.Invoke(text);
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
    // Firebase에 아이템을 바로 저장 하는 함수 [ 필요 예시) ticket1 0개로 설정 ]
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
    // Firebase에 아이템을 증감하는 함수 [ 필요 예시) ticket1 1개 추가 ]
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
    // Firebase에 아이템 개수를 가져오는 함수 [ 필요 예시) firebase에 ticket1 100개 있음 ]
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

    // <summary>
    // 초기 아이템의 개수를 저장하는 함수
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
    // Firebase에 하이 스코어를 save 하는 함수
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
    // Firebase에 하이 스코어를 get 하는 함수
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
                Debug.LogError("해당 하는 보스 이름이 없음");
                return 0;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Get HighScore Error: " + ex);
            throw; // 필요한 경우 다시 throw하여 전파
        }
    }
    #endregion
}

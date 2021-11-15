using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Linq;

public class FirebaseManager : MonoBehaviour
{

    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;

    //Login variables
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;

    //Register variables
    [Header("Register")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;

    //User Data variables
    [Header("UserData")]
    public TMP_InputField usernameField;
    public TMP_InputField MemoryScoreField;
    public TMP_InputField MemoryGamesPlayedField;
    public TMP_InputField MemoryBadgesField;
    //public GameObject scoreElement;
    //public Transform scoreboardContent;

    public string emailToRemember;
    public string passwordToRemember;

    //public string UserID;


    private static FirebaseManager _singleton;
    public static FirebaseManager Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
            {
                _singleton = value;
            }
            else if (_singleton != value) {
                Destroy(value.gameObject);
            }
        }
    }


    private void Awake()
    {

        // THIS IS SINGLETTON STUFF AND STUFF DIRECTLY ABOVE
        DontDestroyOnLoad(this.gameObject);
        Singleton = this;

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();

            }
            else
            {
                Debug.Log(" Couldnt resolve firebase dependencies" + dependencyStatus);

            }
        });
        //auth.SignOut();
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up firebase");

        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;

    }

    public void ClearLoginFeilds()
    {
        emailLoginField.text = "";
        passwordLoginField.text = "";
    }
    public void ClearRegisterFeilds()
    {
        usernameRegisterField.text = "";
        emailRegisterField.text = "";
        passwordRegisterField.text = "";
        passwordRegisterVerifyField.text = "";
    }


    public void LoginButton()
    {
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }

    public void RegisterButton()
    {

        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));

    }
    public void SignOutButton()
    {
        auth.SignOut();
        SceneLoader.instance.LoginScreen();
        ClearLoginFeilds();
        ClearRegisterFeilds();
    }

    //public void ScoreboardButton()
    //{
    //    StartCoroutine(LoadScoreboardData());
    //}

    public void SavaDataButton()
    {
        StartCoroutine(UpdateUsernameAuth(usernameField.text));
        StartCoroutine(UpdateUsernameDatabase(usernameField.text));

        StartCoroutine(UpdateMemoryScore(int.Parse(MemoryScoreField.text)));
        StartCoroutine(UpdateMemoryGamesPlayed(int.Parse(MemoryGamesPlayedField.text)));
        StartCoroutine(UpdateMemoryBadges(int.Parse(MemoryBadgesField.text)));

    }

    private IEnumerator Login(string email, string password)
    {
        emailToRemember = email;
        passwordToRemember = password;

        var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(predicate: () => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {
            Debug.Log(message: $"failed to register task with {loginTask.Exception}");
            FirebaseException firebaseEx = loginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "User not found";
                    break;

            }
            warningLoginText.text = message;

        }
        else
        {

            User = loginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            warningLoginText.text = " ";
            confirmLoginText.text = "logged in";

            StartCoroutine(LoadUserData());
            yield return new WaitForSeconds(2);

            usernameField.text = User.DisplayName; 
            
            SceneLoader.instance.UserDataScreen();
            confirmLoginText.text = "";
            ClearLoginFeilds();
            ClearRegisterFeilds();

        }
    }

    private IEnumerator Register(string email, string password, string username)
    {
        if (username == "")
        {
            //If the username field is blank show a warning
            warningRegisterText.text = "Missing Username";
        }
        else if (passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            //If the password does not match show a warning
            warningRegisterText.text = "Password Does Not Match!";
        }
        else
        {
            //Call the Firebase auth signin function passing the email and password
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                warningRegisterText.text = message;
            }
            else
            {
                //User has now been created
                //Now get the result
                User = RegisterTask.Result;

                if (User != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile { DisplayName = username };

                    //Call the Firebase auth update user profile function passing the profile with the username
                    var ProfileTask = User.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Username Set Failed!";
                    }
                    else
                    {
                        //Username is now set
                        //Now return to login screen
                        SceneLoader.instance.LoginScreen();
                        warningRegisterText.text = "";
                        ClearLoginFeilds();
                        ClearRegisterFeilds();
                    }
                }
            }
        }
    }

    private IEnumerator UpdateUsernameAuth(string username)
    {
        UserProfile profile = new UserProfile { DisplayName = username };

        var ProfileTask = User.UpdateUserProfileAsync(profile);

        yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

        if(ProfileTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
        }
        else
        {
            // auth user is updated
        }

    }

    private IEnumerator UpdateUsernameDatabase(string username)
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("username").SetValueAsync(username);

        

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            // databases user is updated
        }

    }

    private IEnumerator UpdateMemoryScore(int memoryScore)
    {
        //Set the currently logged in user xp
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("MemoryScore").SetValueAsync(memoryScore);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Xp is now updated
        }
    }

    private IEnumerator UpdateMemoryGamesPlayed(int games_played)
    {
        //Set the currently logged in user kills
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("MemoryGamesPlayed").SetValueAsync(games_played);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Kills are now updated
        }
    }

    private IEnumerator UpdateMemoryBadges(int badges)
    {
        //Set the currently logged in user deaths
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("MemoryBadges").SetValueAsync(badges);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Deaths are now updated
        }
    }


    private IEnumerator LoadUserData()
    {
        //Get the currently logged in user data
        var DBTask = DBreference.Child("users").Child(User.UserId).GetValueAsync();

        //UserID = User.UserId;

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            //No data exists yet
            MemoryScoreField.text = "0";
            MemoryGamesPlayedField.text = "0";
            MemoryBadgesField.text = "0";
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            MemoryScoreField.text = snapshot.Child("MemoryScore").Value.ToString();
            MemoryGamesPlayedField.text = snapshot.Child("MemoryGamesPlayed").Value.ToString();
            MemoryBadgesField.text = snapshot.Child("MemoryBadges").Value.ToString();
        }
    }

    //private IEnumerator LoadScoreboardData()
    //{
    //    //Get all the users data ordered by kills amount
    //    var DBTask = DBreference.Child("users").OrderByChild("MemoryScore").GetValueAsync();

    //    yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

    //    if (DBTask.Exception != null)
    //    {
    //        Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
    //    }
    //    else
    //    {
    //        //Data has been retrieved
    //        DataSnapshot snapshot = DBTask.Result;

    //        //Destroy any existing scoreboard elements
    //        foreach (Transform child in scoreboardContent.transform)
    //        {
    //            Destroy(child.gameObject);
    //        }

    //        //Loop through every users UID
    //        foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
    //        {
    //            string username = childSnapshot.Child("username").Value.ToString();
    //            int MemoryScore = int.Parse(childSnapshot.Child("MemoryScore").Value.ToString());
    //            int MemoryBadges = int.Parse(childSnapshot.Child("MemoryBadges").Value.ToString());
    //            int MemoryGamesPlayed = int.Parse(childSnapshot.Child("MemoryGamesPlayed").Value.ToString());

    //            //Instantiate new scoreboard elements
    //            GameObject scoreboardElement = Instantiate(scoreElement, scoreboardContent);
    //            scoreboardElement.GetComponent<ScoreElement>().NewScoreElement(username, MemoryScore, MemoryBadges, MemoryGamesPlayed);
    //        }

    //        //Go to scoareboard screen
    //        SceneLoader.instance.ScoreboardScreen();
    //    }
    //}

}

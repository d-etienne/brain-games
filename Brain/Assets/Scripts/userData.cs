using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Auth;
using Firebase.Database;

public class userData : MonoBehaviour
{

    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;

    //User Data variables
    [Header("UserData")]
    public TMP_InputField usernameField;
    public TMP_InputField MemoryScoreField;
    public TMP_InputField MemoryGamesPlayedField;
    public TMP_InputField MemoryBadgesField;
    public TMP_InputField NumeracyScoreField;
    public TMP_InputField NumeracyGamesPlayedField;
    public TMP_Text username;
    public TMP_Text memoryScore;
    public TMP_Text memoryGamesPlayed;
    public TMP_Text numeracyScore;
    public TMP_Text NumeracyGamesPlayed;
    //public GameObject scoreElement;
    //public Transform scoreboardContent;

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

    public GameObject incorrectUser;


    private void Awake()
    {
        incorrectUser.SetActive(false);
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

        Login();
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up firebase");

        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;

    }

    public void Login()
    {
        StartCoroutine(Login(FirebaseManager.Singleton.emailToRemember, FirebaseManager.Singleton.passwordToRemember));
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
        //SceneSwap.instance.Login();
        SceneLoader.instance.LoginScreen();
        //ClearLoginFeilds();
        //ClearRegisterFeilds();
    }



    private IEnumerator Login(string email, string password)
    {
        FirebaseManager.Singleton.emailToRemember = email;
        FirebaseManager.Singleton.passwordToRemember = password;

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
            incorrectUser.SetActive(true);

        }
        else
        {

            User = loginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            //warningLoginText.text = " ";
            //confirmLoginText.text = "logged in";

            usernameField.text = User.DisplayName;
            username.text = User.DisplayName;

            StartCoroutine(LoadUserData());
            yield return new WaitForSeconds(2);


            incorrectUser.SetActive(false);
            SceneLoader.instance.UserDataScreen();
            confirmLoginText.text = "";
            ClearLoginFeilds();
            ClearRegisterFeilds();

        }
    }

   


    private IEnumerator LoadUserData()
    {
        //Get the currently logged in user data
        var DBTask = DBreference.Child("users").Child(User.UserId).GetValueAsync();

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
            NumeracyScoreField.text = "0";
            NumeracyGamesPlayedField.text = "0";
            //MemoryBadgesField.text = "0";
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            MemoryScoreField.text = snapshot.Child("MemoryScore").Value.ToString();
            MemoryGamesPlayedField.text = snapshot.Child("MemoryGamesPlayed").Value.ToString();
            NumeracyScoreField.text = snapshot.Child("NumeracyScore").Value.ToString();
            NumeracyGamesPlayedField.text = snapshot.Child("NumeracyGamesPlayed").Value.ToString();

            memoryScore.text = snapshot.Child("MemoryScore").Value.ToString();
            memoryGamesPlayed.text = snapshot.Child("MemoryGamesPlayed").Value.ToString();
            numeracyScore.text = snapshot.Child("NumeracyScore").Value.ToString();
            NumeracyGamesPlayed.text = snapshot.Child("NumeracyGamesPlayed").Value.ToString();
            //MemoryBadgesField.text = snapshot.Child("MemoryBadges").Value.ToString();
        }
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


}

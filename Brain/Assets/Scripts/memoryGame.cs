using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Firebase;
using Firebase.Auth;
using Firebase.Database;

public class memoryGame : MonoBehaviour
{
    public int turn = 0;
    public int[][] levels = new int[50][];
    public int level = 1;  
    public int process = 1;
    int gameStart = 1;
    int inputIndx = 0;

    public DatabaseReference DBreference;
    public DependencyStatus dependencyStatus;
    public FirebaseUser User;
    public Text textElement, textElement1;

    //public Text incorrectMessage;

    public GameObject thing;
    int currScore = 0;
    int highScore = 0; //****get high score from DB****

    int levelMax = 49;


    private void Awake()
    {
        thing.SetActive(false);
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
                DBreference = FirebaseDatabase.DefaultInstance.RootReference;


            }
            else
            {
                Debug.Log(" Couldnt resolve firebase dependencies" + dependencyStatus);

            }
        });
    }

    void Start()
    {
        textElement.text = string.Concat("Score: ", currScore);
        textElement1.text = string.Concat("Best:  ", highScore);
        if (gameStart == 1)
        {
            gameStart = 0;
            StartCoroutine(initGameProblem());
        }
    }

    void Update()
    {
        textElement.text = string.Concat("Score: ", currScore);
        textElement1.text = string.Concat("Best:  ", highScore);
        if (turn == 1 && process == 1)    //user input
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

                if (hit)
                {
                    if (hit.collider.CompareTag("Square"))
                    {
                        StartCoroutine(userClick(hit));
                        process = 0;
                    }
                }
            }
        }
        if (turn == 0 && process == 1)     //problem display
        {
            if (level == levelMax)
            {
                Application.Quit();
                return;
            }
            process = 0;
            StartCoroutine(probDisplay(levels[level]));
        }

    }

    IEnumerator waitFor(float x)
    {
        yield return new WaitForSeconds(x);
    }

    public IEnumerator probDisplay(int[] prob)
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < level; i++)
        {
            Debug.Log("a");
            Debug.Log(prob[i]);
            GameObject someGameObject = GameObject.Find(prob[i].ToString());
            yield return new WaitForSeconds(0.2f);
            someGameObject.GetComponent<Renderer>().material.color = Color.yellow;
            yield return new WaitForSeconds(0.25f);
            someGameObject.GetComponent<Renderer>().material.color = Color.white;
        }
        process = 1;
        level++;
        turn = 1;
        yield break;
    }

    IEnumerator userClick(RaycastHit2D hit)
    {
        hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.green;
        yield return new WaitForSeconds(0.2f);
        hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.white;

        int curLevel = level - 1;
        int ans = int.Parse(hit.collider.gameObject.name);
        if(levels[curLevel][inputIndx] != ans)
        {
            print("game over!");
            //incorrectMessage.enabled = true;
            thing.SetActive(true);
            
            StartCoroutine(UpdateMemoryScore());
            StartCoroutine(UpdateMemoryGamesPlayed());
            turn = -1;
            yield break;
        }
        process = 1;
        inputIndx++;
        if(inputIndx == level - 1)
        {
            turn = 0;
            inputIndx = 0;
            currScore += 1;
            if(highScore < currScore)
            {
                highScore = currScore;
            }
            yield return new WaitForSeconds(100);
        }
        yield break;
    }

    IEnumerator initGameProblem()
    {
        for (int i = 1; i < levelMax+1; i++)
        {
            int[] problem = new int[i];
            for (int z = 0; z < i; z++)
            {
                problem[z] = (Random.Range(0, 9));
            }
            levels[i] = problem;
        }
        yield break;
    }

    IEnumerator UpdateMemoryScore()
    {

        var DBTask = DBreference.Child("users").Child(FirebaseManager.Singleton.User.UserId).GetValueAsync();
        string currCount = "";

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;
            currCount = snapshot.Child("MemoryScore").Value.ToString();

        }

        int count = int.Parse(currCount);

        if (highScore > count)
        {
            var Task = DBreference.Child("users").Child(FirebaseManager.Singleton.User.UserId).Child("MemoryScore").SetValueAsync(highScore);
        }else
        {
            var Task = DBreference.Child("users").Child(FirebaseManager.Singleton.User.UserId).Child("MemoryScore").SetValueAsync(count);
        }


    }

    IEnumerator UpdateMemoryGamesPlayed()
    {

        var DBTask = DBreference.Child("users").Child(FirebaseManager.Singleton.User.UserId).GetValueAsync();
        string currCount = "";

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;
            currCount = snapshot.Child("MemoryGamesPlayed").Value.ToString();
        }

        int count = int.Parse(currCount);
        var Task = DBreference.Child("users").Child(FirebaseManager.Singleton.User.UserId).Child("MemoryGamesPlayed").SetValueAsync(count + 1);


    }



}

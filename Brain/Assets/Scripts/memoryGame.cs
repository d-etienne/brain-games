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
    int turn = 0;
    int[][] levels = new int[50][];
    int level = 1;  
    int process = 1;
    int gameStart = 1;
    int inputIndx = 0;

    public DatabaseReference DBreference;
    public DependencyStatus dependencyStatus;
    public FirebaseUser User;
    public Text textElement, textElement1;

    int currScore = 0;
    int highScore = 0; //****get high score from DB****


    private void Awake()
    {
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
            if (level == 49)
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

    IEnumerator probDisplay(int[] prob)
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < level; i++)
        {
            Debug.Log("a");
            Debug.Log(prob[i]);
            GameObject someGameObject = GameObject.Find(prob[i].ToString());
            yield return new WaitForSeconds(0.2f);
            someGameObject.GetComponent<Renderer>().material.color = Color.green;
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
        hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.white;

        int curLevel = level - 1;
        int ans = int.Parse(hit.collider.gameObject.name);
        if(levels[curLevel][inputIndx] != ans)
        {
            print("game over!");
            //StartCoroutine(UpdateDatabase());
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
        for (int i = 1; i < 50; i++)
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

    IEnumerator UpdateDatabase()
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("MemoryScore").SetValueAsync(highScore);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {

        }

    }
}

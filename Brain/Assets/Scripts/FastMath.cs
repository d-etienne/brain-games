using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase;
using Firebase.Auth;
using Firebase.Database;


public class FastMath : MonoBehaviour
{
    public Text currScore, questionDisplay;
    public DatabaseReference DBreference;
    public DependencyStatus dependencyStatus;
    public FirebaseUser User;

    bool valid = true;

    public GameObject incorrectMessage;

    int score = 0;
    int prevCorrectAnswerIndex;
    int correctAnswerIndex;
    int wrongAnswer;

    int numOfAnswers = 3;

    int question;
    List<int> answerChoices = new List<int>();

    public Text timer;
    float  currTime = 10f;
    float timeCounter = 0f;



    private void Awake()
    {
        incorrectMessage.SetActive(false);
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
    // Start is called before the first frame update
    void Start()
    {
        timeCounter = currTime;
        question = questionGenerator();
        randomAnswerGenerator(question);
        prevCorrectAnswerIndex = correctAnswerIndex;
  
    }
    public void randomAnswerGenerator(int answer)
    {
        answerChoices = new List<int>();
        prevCorrectAnswerIndex = correctAnswerIndex;

        correctAnswerIndex = Mathf.FloorToInt(Random.value * 3);
        for (int i = 0; i < numOfAnswers; i++)
        {
            if (i == correctAnswerIndex)
            { //If this is the index for the correct answer, put correct answer here
                answerChoices.Add(answer);
            }
            else
            {
                int wrongAnswer = -1;
                int randOpt = Mathf.FloorToInt(Random.value * 2); 
                int randBase = Mathf.FloorToInt(Random.value * 5) + 1; 
                if (randOpt == 0)
                {
                    wrongAnswer = answer + randBase;
                }
                else
                {
                    wrongAnswer = answer - randBase;
                }
                answerChoices.Add(wrongAnswer);
            }
        }
        foreach(var ans in answerChoices){
            Debug.Log(ans);
        }
     
        for (int i = 0; i <numOfAnswers; i++){
            GameObject.Find("Answer " + (i+1).ToString()).GetComponentInChildren<Text>().text = answerChoices[i].ToString();
            GameObject.Find("Answer " + (i+1).ToString() + " Click").GetComponentInChildren<Text>().text = answerChoices[i].ToString();

        }


    }

    public int questionGenerator()
    {
        string question;
        int operand, operand1, operand2, answer;
        int operation = Mathf.FloorToInt(Random.value * 4); //operation type

        int addition = 0;
        int subtraction = 1;
        int multiplication = 2;
        int division = 3;

        int wholeNumberMultiplier = 100;


        if ( operation == addition ) // addition
        {
            operand1 = Mathf.FloorToInt(Random.value * wholeNumberMultiplier);
            operand2 = Mathf.FloorToInt(Random.value * wholeNumberMultiplier);
            question = operand1.ToString() + "+" + operand2.ToString() + " = ? ";
            questionDisplay.text = ("Question: " + question);
            answer = operand1 + operand2;
            


            return (answer);
        }
        else if (operation == subtraction  )
        {
            operand1 = Mathf.FloorToInt(Random.value * wholeNumberMultiplier);
            operand2 = Mathf.FloorToInt(Random.value * wholeNumberMultiplier);
            question = operand1.ToString() + "-" + operand2.ToString() + " = ? ";
            questionDisplay.text = ("Question: " + question);
            answer = operand1 - operand2;
            


            return (answer);
        }
        else if (operation == multiplication)
        {
            operand1 = Mathf.FloorToInt(Random.value * (wholeNumberMultiplier/2));
            operand2 = Random.Range(2, 10);
            question = operand1.ToString() + "*" + operand2.ToString() + " = ? ";
            questionDisplay.text = ("Question: " + question);
            answer = operand1 * operand2;
            Debug.Log(answer);
            
        
            return (answer);
        }
        else if (operation == division)
        {
            operand = Mathf.FloorToInt(Random.value * (wholeNumberMultiplier/2));
            operand2 = Random.Range(2, 10);
            operand1 = operand * operand2;
            question = operand1.ToString() + "/" + operand2.ToString() + " = ? ";
            questionDisplay.text = ("Question: " + question);
            answer = operand1 / operand2;
            


            return (answer);
        }
        return 0;
    }


    void Update()
    {
        while (timeCounter != 0)
        {
            timeCounter -= 1 * Time.deltaTime;
            timer.text = timeCounter.ToString("0");

        }

        currScore.text = ("Score: " + score.ToString());
        
        if (valid)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                
                if (hit)
                {
                    if (hit.collider.CompareTag("Answer"))
                    {
                        StartCoroutine(UserClick(hit));
                    }
                    if (valid)
                    {
                        StartCoroutine(GenerateUI());
                    }               
                }

            }


            if(valid != true)
            {
                Debug.Log("game over");
                incorrectMessage.SetActive(true);
                //Application.Quit();
            }
        }


        
    }

    IEnumerator UserClick(RaycastHit2D hit){
        hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.green;
        yield return new WaitForSeconds(0.2f);
        hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.white;
        int correctAnswer = answerChoices[prevCorrectAnswerIndex];
        if (hit.collider.gameObject.GetComponentInChildren<Text>().text == correctAnswer.ToString())
        {
            score += 1;
            
        }
        else
        {
            Debug.Log("The comparison is false");
            Debug.Log(hit.collider.gameObject.GetComponentInChildren<Text>().text);
            Debug.Log(correctAnswer.ToString());
            valid = false;
            incorrectMessage.SetActive(true);
            StartCoroutine(UpdateNumeracyScore());
            StartCoroutine(UpdateNumeracyGamesPlayed());
        }

        yield return new WaitForSeconds(100);
    }

    IEnumerator GenerateUI(){
        question = questionGenerator();
        randomAnswerGenerator(question);
        yield break;

    }

    IEnumerator UpdateNumeracyScore()
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
            currCount = snapshot.Child("NumeracyScore").Value.ToString();

        }

        int count = int.Parse(currCount);

        if (score > count)
        {
            var Task = DBreference.Child("users").Child(FirebaseManager.Singleton.User.UserId).Child("NumeracyScore").SetValueAsync(score);
        }else
        {
            var Task = DBreference.Child("users").Child(FirebaseManager.Singleton.User.UserId).Child("NumeracyScore").SetValueAsync(count);
        }


    }

    IEnumerator UpdateNumeracyGamesPlayed()
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
            currCount = snapshot.Child("NumeracyGamesPlayed").Value.ToString();
        }

        int count = int.Parse(currCount);
        var Task = DBreference.Child("users").Child(FirebaseManager.Singleton.User.UserId).Child("NumeracyGamesPlayed").SetValueAsync(count + 1);


    }
}

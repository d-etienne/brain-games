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
                int randOpt = Mathf.FloorToInt(Random.value * 2); //Should we add or subtract the random number
                int randBase = Mathf.FloorToInt(Random.value * 5) + 1; //Plus 1 so it doesn't equal zero. Change the 5 to modify how much the generated answers will differ from the real answer
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
        // GameObject.Find("Answer 1").GetComponentInChildren<Text>().text = answerChoices[0].ToString();
        // GameObject.Find("Answer 2").GetComponentInChildren<Text>().text = answerChoices[1].ToString();
        // GameObject.Find("Answer 3").GetComponentInChildren<Text>().text = answerChoices[2].ToString();

        // GameObject.Find("Answer 1 Click").GetComponentInChildren<Text>().text = answerChoices[0].ToString();
        // GameObject.Find("Answer 2 Click").GetComponentInChildren<Text>().text = answerChoices[1].ToString();
        // GameObject.Find("Answer 3 Click").GetComponentInChildren<Text>().text = answerChoices[2].ToString();

        

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
            // GameObject.Find("Answer 1").GetComponentInChildren<Text>().text = "hello";


            return (answer);
        }
        else if (operation == subtraction  )
        {
            operand1 = Mathf.FloorToInt(Random.value * wholeNumberMultiplier);
            operand2 = Mathf.FloorToInt(Random.value * wholeNumberMultiplier);
            question = operand1.ToString() + "-" + operand2.ToString() + " = ? ";
            questionDisplay.text = ("Question: " + question);
            answer = operand1 - operand2;
            // GameObject.Find("Answer 1").GetComponentInChildren<Text>().text = "hello";


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
            // GameObject.Find("Answer 1").GetComponentInChildren<Text>().text = "hello";
        
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
            // GameObject.Find("Answer 1").GetComponentInChildren<Text>().text = "hello";


            return (answer);
        }
        return 0;
    }

    //IEnumerator GenerateQuestion(){

    //}
    // Update is called once per frame
    void Update()
    {
        currScore.text = ("Score: " + score.ToString());
        //Debug.Log("Correct Answer = " + answerChoices[correctAnswerIndex].ToString());
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

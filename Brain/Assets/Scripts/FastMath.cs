using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script utilizes tutorial code from: http://codesaying.com/create-a-math-game-for-kids-using-unity-scripting/

public class FastMath : MonoBehaviour
{
    public Text currScore, questionDisplay;

    bool valid = true;

    int score = 0;
    int prevCorrectAnswerIndex;
    int correctAnswerIndex;
    int wrongAnswer;

    int question;
    List<int> answerChoices = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
        question = questionGenerator();
        randomAnswerGenerator(question);
        prevCorrectAnswerIndex = correctAnswerIndex;
  
    }
    public void randomAnswerGenerator(int answer)
    {
        answerChoices = new List<int>();
        prevCorrectAnswerIndex = correctAnswerIndex;

        correctAnswerIndex = Mathf.FloorToInt(Random.value * 3);
        for (int i = 0; i < 3; i++)
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
=======
        // This funciton generates two random operands, randomly adds,
        // subtracts, multiplies, or divides them, and returns the result.
        int equationAnswerGenerator()
        {
            string question;
            int operand1 = 0;
            int operand2 = 0;
            int rand_operator = Random.Range(0, 3);

            if (rand_operator == 0) // Addition
            {
                operand1 = Random.Range(1, 100);
                operand2 = Random.Range(1, 100);
                question = operand1 + " + " + operand2 + " = ? "; // To be displayed to user
                return (operand1 + operand2);
            }
            else if (rand_operator == 1) // Subtraction
            {
                operand1 = Random.Range(1, 100);
                operand2 = Random.Range(1, 100);
                question = operand1 + " - " + operand2 + " = ? "; // To be displayed to user
                return (operand1 - operand2);
            }
            else if (rand_operator == 2) // Multiplication
            {
                operand1 = Random.Range(1, 50);
                operand2 = Random.Range(1, 10);
                question = operand1 + " * " + operand2 + " = ? "; // To be displayed to user
                return (operand1 * operand2);
            }
            else if (rand_operator == 3) // Division
            {
                operand1 = Random.Range(1, 50);
                operand2 = Random.Range(1, 10);
                question = operand1 + " / " + operand2 + " = ? "; // To be displayed to user
                return (operand1 / operand2);
            }
            return 0;
>>>>>>> ee12ead0a7a3b76d46cbd8bd004d135bc01c7b9b
        }
        foreach(var ans in answerChoices){
            Debug.Log(ans);
        }
                
        GameObject.Find("Answer 1").GetComponentInChildren<Text>().text = answerChoices[0].ToString();
        GameObject.Find("Answer 2").GetComponentInChildren<Text>().text = answerChoices[1].ToString();
        GameObject.Find("Answer 3").GetComponentInChildren<Text>().text = answerChoices[2].ToString();

        GameObject.Find("Answer 1 Click").GetComponentInChildren<Text>().text = answerChoices[0].ToString();
        GameObject.Find("Answer 2 Click").GetComponentInChildren<Text>().text = answerChoices[1].ToString();
        GameObject.Find("Answer 3 Click").GetComponentInChildren<Text>().text = answerChoices[2].ToString();

        

    }

    public int questionGenerator()
    {
        string question;
        int operand1, operand2, answer;
        int rand_operator = Mathf.FloorToInt(Random.value * 4); //operation type

        if ( rand_operator == 0 || rand_operator == 1 || rand_operator == 2 || rand_operator == 3 ) // addition
        {
            // operand1 = Mathf.FloorToInt(Random.value * 100);
            // operand2 = Mathf.FloorToInt(Random.value * 100);
            // question = operand1.ToString() + "+" + operand2.ToString() + " = ? ";
            // questionDisplay.text = ("Question: " + question);
            // answer = operand1 + operand2;
            // // GameObject.Find("Answer 1").GetComponentInChildren<Text>().text = "hello";
            operand1 = Mathf.FloorToInt(Random.value * 50);
            operand2 = Random.Range(2, 10);
            question = operand1.ToString() + "*" + operand2.ToString() + " = ? ";
            questionDisplay.text = ("Question: " + question);
            answer = operand1 * operand2;
            Debug.Log(answer);
            // GameObject.Find("Answer 1").GetComponentInChildren<Text>().text = "hello";


            return (answer);
        }
        // else if (rand_operator == 1 || rand_operator == 3 )
        // {
        //     operand1 = Mathf.FloorToInt(Random.value * 100);
        //     operand2 = Mathf.FloorToInt(Random.value * 100);
        //     question = operand1.ToString() + "-" + operand2.ToString() + " = ? ";
        //     questionDisplay.text = ("Question: " + question);
        //     answer = operand1 - operand2;
        //     // GameObject.Find("Answer 1").GetComponentInChildren<Text>().text = "hello";


<<<<<<< HEAD
        //     return (answer);
        // }
        // else if (rand_operator == 2)
        // {
        //     operand1 = Mathf.FloorToInt(Random.value * 50);
        //     operand2 = Random.Range(2, 10);
        //     question = operand1.ToString() + "*" + operand2.ToString() + " = ? ";
        //     questionDisplay.text = ("Question: " + question);
        //     answer = operand1 * operand2;
        //     Debug.Log(answer);
        //     // GameObject.Find("Answer 1").GetComponentInChildren<Text>().text = "hello";
        // }


        //     return (answer);
        // }
        // else if (rand_operator == 3)
        // {
        //     operand1 = Mathf.FloorToInt(Random.value * 50);
        //     operand2 = Random.Range(2, 10);
        //     question = operand1.ToString() + "/" + operand2.ToString() + " = ? ";
        //     questionDisplay.text = ("Question: " + question);
        //     answer = operand1 / operand2;
        //     // GameObject.Find("Answer 1").GetComponentInChildren<Text>().text = "hello";


        //     return (answer);
        // }
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
=======
        // This funciton produces two numbers that are different from the
        // result returned in equationAnswerGenerator() by adding the result to
        // or subtracting it from a random number within the range of 1 through 10.
        // These three values are appended to a list which will be used to display
        // the three answer choices to the user in a separate function. The position
        // of the correct and incorrect answers are ramdonly selected.
        void randAnswerGenerator(int answer)
        {
            List<int> answerChoices = new List<int>(); // To be displayed to user
            int correctAnswerInd = Random.Range(0, 2);
            int incorrectAnswer = 0;

            // Random number that is added to or subtracted from
            // the correct answer to produce incorrect answers.
            int randNum = 0;

            // This flag ensures that one incorrect answer is the result of adding
            // the random number to the result returned in equationAnswerGenerator()
            // and that the other incorrect answer is the result of subtracting the
            // random number from the result returned in equationAnswerGenerator().
            // This is done to prevent a duplicate incorrect answer. When true, add.
            // When false, subtract.
            bool addSubFlag = true;

            for (int i = 0; i < 3; i++)
            {
                if (i == correctAnswerInd)
                {
                    answerChoices.Add(answer);
                }
                else
                {
                    randNum = Random.Range(1, 10);

                    if (addSubFlag == true) // Add random amount to correct answer
                    {
                        incorrectAnswer = answer + randNum;
                        addSubFlag = false;
                    }
                    else if (addSubFlag == false) // Subtract random amount from correct answer
                    {
                        incorrectAnswer = answer - randNum;
                    }
                    answerChoices.Add(incorrectAnswer);
>>>>>>> ee12ead0a7a3b76d46cbd8bd004d135bc01c7b9b
                }

            }



            if(valid != true)
            {
                Debug.Log("game over");
                //Application.Quit();
            }
        }


        
    }

<<<<<<< HEAD
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
        }

        yield return new WaitForSeconds(100);
    }

    IEnumerator GenerateUI(){
        question = questionGenerator();
        randomAnswerGenerator(question);
        yield break;

=======
    // int startResult = 0;
    // startResult = equationAnswerGenerator();
    // randAnswerGenerator(startResult);

    // Update is called once per frame
    void Update()
    {
      // int updateResult = 0;
      // updateResult = equationAnswerGenerator();
      // randAnswerGenerator(updateResult);
>>>>>>> ee12ead0a7a3b76d46cbd8bd004d135bc01c7b9b
    }
}

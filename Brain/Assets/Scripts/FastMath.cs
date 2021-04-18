using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastMath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string question;

        int addOrSubtract()
        {
            int operand1 = Mathf.FloorToInt(Random.value * 100);
            int operand2 = Mathf.FloorToInt(Random.value * 100);
            int addOrSub = Mathf.FloorToInt(Random.value * 2);

            if (addOrSub == 0)
            {
                question = operand1 + "-" + operand2 + " = ? ";
                return (operand1 - operand2);
            }
            else
            {
                question = operand1 + "+" + operand2 + " = ? ";
                return (operand1 + operand2);
            }
        }

        int multiplyOrDivide()
        {
            int operand1 = Mathf.FloorToInt(Random.value * 50);
            int operand2 = Random.Range(2, 10);
            int mulOrDiv = Mathf.FloorToInt(Random.value * 2);

            if (mulOrDiv == 0)
            {
                question = operand1 + "*" + operand2 + " = ? ";
                return (operand1 * operand2);
            }
            else
            {
                question = operand1 + "/" + operand2 + " = ? ";
                return (operand1 / operand2);
            }
        }

        //this list contains the 3 different answer for the user.
        List<int> answerChoices = new List<int>();
        void randomAnswerGenerator(int answer)
        {
            int correctAnswerIndex = Mathf.FloorToInt(Random.value * 3);
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
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        string question;

        int addOrSubtract()
        {
            int operand1 = Mathf.FloorToInt(Random.value * 100);
            int operand2 = Mathf.FloorToInt(Random.value * 100);
            int addOrSub = Mathf.FloorToInt(Random.value * 2);

            if (addOrSub == 0)
            {
                question = operand1 + "-" + operand2 + " = ? ";
                return (operand1 - operand2);
            }
            else
            {
                question = operand1 + "+" + operand2 + " = ? ";
                return (operand1 + operand2);
            }
        }

        int multiplyOrDivide()
        {
            int operand1 = Mathf.FloorToInt(Random.value * 50);
            int operand2 = Random.Range(2, 10);
            int mulOrDiv = Mathf.FloorToInt(Random.value * 2);

            if (mulOrDiv == 0)
            {
                question = operand1 + "*" + operand2 + " = ? ";
                return (operand1 * operand2);
            }
            else
            {
                question = operand1 + "/" + operand2 + " = ? ";
                return (operand1 / operand2);
            }
        }

        List<int> answerChoices = new List<int>();
        void randomAnswerGenerator(int answer)
        {
            int correctAnswerIndex = Mathf.FloorToInt(Random.value * 3);
            for (int i = 0; i < 3; i++)
            {
                if (i == correctAnswerIndex)
                {
                    answerChoices.Add(answer);
                }
                else
                {
                    int wrongAnswer = -1;
                    int randOpt = Mathf.FloorToInt(Random.value * 2);
                    int randBase = Mathf.FloorToInt(Random.value * 5) + 1;
                    if (randOpt == 0){
                        wrongAnswer = answer + randBase;
                    }
                    else
                    {
                        wrongAnswer = answer - randBase;
                    }
                    answerChoices.Add(wrongAnswer);
                }
            }
        }
    }
}

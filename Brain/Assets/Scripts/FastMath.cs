using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastMath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string question;

        int questionGenerator()
        {
            int operand1, operand2;
            int rand_operator = Mathf.FloorToInt(Random.value * 4);

            if (rand_operator == 0)
            {
                operand1 = Mathf.FloorToInt(Random.value * 100);
                operand2 = Mathf.FloorToInt(Random.value * 100);
                question = operand1 + "+" + operand2 + " = ? ";
                return (operand1 + operand2);
            }
            else if (rand_operator == 1)
            {
                operand1 = Mathf.FloorToInt(Random.value * 100);
                operand2 = Mathf.FloorToInt(Random.value * 100);
                question = operand1 + "-" + operand2 + " = ? ";
                return (operand1 - operand2);
            }
            else if (rand_operator == 2)
            {
                operand1 = Mathf.FloorToInt(Random.value * 50);
                operand2 = Random.Range(2, 10);
                question = operand1 + "*" + operand2 + " = ? ";
                return (operand1 * operand2);
            }
            else if (rand_operator == 3)
            {
                operand1 = Mathf.FloorToInt(Random.value * 50);
                operand2 = Random.Range(2, 10);
                question = operand1 + "/" + operand2 + " = ? ";
                return (operand1 / operand2);
            }
          return 0;
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

    }
}

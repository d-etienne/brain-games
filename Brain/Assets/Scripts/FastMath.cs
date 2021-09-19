using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script utilizes tutorial code from: http://codesaying.com/create-a-math-game-for-kids-using-unity-scripting/

public class FastMath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
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
        }

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
                }
            }
        }
    }

    // int startResult = 0;
    // startResult = equationAnswerGenerator();
    // randAnswerGenerator(startResult);

    // Update is called once per frame
    void Update()
    {
      // int updateResult = 0;
      // updateResult = equationAnswerGenerator();
      // randAnswerGenerator(updateResult);
    }
}

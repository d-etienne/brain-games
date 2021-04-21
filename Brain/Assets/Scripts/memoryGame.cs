using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class memoryGame : MonoBehaviour
{
    int turn = 0;
    int[][] levels = new int[50][];
    int level = 1;   //og = 1
    int process = 1;
    int gameStart = 1;
    int inputIndx = 0;

    void Start()
    {
        if (gameStart == 1)
        {
            gameStart = 0;
            StartCoroutine(initGameProblem());
        }
    }

    void Update()
    {
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
            Application.Quit();
            yield break;
        }
        process = 1;
        inputIndx++;
        if(inputIndx == level - 1)
        {
            turn = 0;
            inputIndx = 0;
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
}

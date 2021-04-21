using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class memorygame : MonoBehaviour
{

    //int iter = 0;
    int turn = 1;
    double delta = 0;
    int[][] levels = new int[50][];
    int level = 1;
    int levelProbs = 1;
    int process = 1;
    int gameStart = 1;

    // Start is called before the first frame update
    void Start()
    {
        if (gameStart == 1)
        {
            Debug.Log("hello world");
            gameStart = 0;
            StartCoroutine(initGameProblem());
        }
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
        //Debug.Log(levels[1][0]);
        yield break;
    }

    // Update is called once per frame
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
            process = 0;
            int[] prob = levels[level];
            StartCoroutine(probDisplay(prob));
            if (level == 49)
            {
                Application.Quit();
                return;
            }
            //level++;
            //levelProbs++;
        }
    }

    IEnumerator probDisplay(int[] prob)
    {
        Debug.Log(prob[0]);
        GameObject someGameObject = GameObject.Find(prob[0].ToString());
        someGameObject.GetComponent<Renderer>().material.color = Color.green;
        yield return new WaitForSeconds(0.5f);
        someGameObject.GetComponent<Renderer>().material.color = Color.white;
        yield return new WaitForSeconds(2f);
        process = 1;
        yield break;
    }

    IEnumerator userClick(RaycastHit2D hit)
    {
        hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.white;
        process = 1;
        yield break;
    }

}

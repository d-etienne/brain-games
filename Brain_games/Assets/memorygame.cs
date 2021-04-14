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
    int levelProbs = 2;

    // Start is called before the first frame update
    void Start()
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
    }

    // Update is called once per frame
    void Update()
    {
        //print(delta);
        delta += 0.05;
        if (Input.GetMouseButtonDown(0) && turn == 1)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit)
            {
                if (hit.collider.CompareTag("Square"))
                {
                    StartCoroutine(userClick(hit));
                }
            }
        }
    }

    IEnumerator userClick(RaycastHit2D hit)
    {
        hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.white;
    }

}

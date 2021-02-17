using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    public Board board;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("0"))
        {
            Debug.Log("KeyPressed");

            Transform setTransform = board.GetSpace(0);

            this.transform.position = setTransform.position;
        }

        if (Input.GetKeyDown("1"))
        {
            Debug.Log("KeyPressed");

            Transform setTransform = board.GetSpace(1);

            this.transform.position = setTransform.position;
        }

        if (Input.GetKeyDown("2"))
        {
            Debug.Log("KeyPressed");

            Transform setTransform = board.GetSpace(2);

            this.transform.position = setTransform.position;
        }

        if (Input.GetKeyDown("3"))
        {
            Debug.Log("KeyPressed");

            Transform setTransform = board.GetSpace(3);

            this.transform.position = setTransform.position;
        }

        if (Input.GetKeyDown("4"))
        {
            Debug.Log("KeyPressed");

            Transform setTransform = board.GetSpace(4);

            this.transform.position = setTransform.position;
        }

        if (Input.GetKeyDown("5"))
        {
            Debug.Log("KeyPressed");

            Transform setTransform = board.GetSpace(5);

            this.transform.position = setTransform.position;
        }

        if (Input.GetKeyDown("6"))
        {
            Debug.Log("KeyPressed");

            Transform setTransform = board.GetSpace(6);

            this.transform.position = setTransform.position;
        }

        if (Input.GetKeyDown("7"))
        {
            Debug.Log("KeyPressed");

            Transform setTransform = board.GetSpace(7);

            this.transform.position = setTransform.position;
        }

        if (Input.GetKeyDown("8"))
        {
            Debug.Log("KeyPressed");

            Transform setTransform = board.GetSpace(8);

            this.transform.position = setTransform.position;
        }
    }
}

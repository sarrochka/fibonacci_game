using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Swipe { None, Up, Down, Left, Right };

public class SwipeController : MonoBehaviour
{
    public float minSwipeLength = 20f;
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    public static Swipe swipeDirection;

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Detecting swipe");
        DetectSwipe();
    }

    public void DetectSwipe()
    {
        if (Input.touches.Length > 0)
        {
            Debug.Log("Touch");
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }

            if (t.phase == TouchPhase.Ended)
            {
                secondPressPos = new Vector2(t.position.x, t.position.y);
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                EstimateSwipeDirection();
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("press");
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log("release");
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
            EstimateSwipeDirection();
        }
        else
        {
            swipeDirection = Swipe.None;
        }
    }

    private void EstimateSwipeDirection()
    {
        // Make sure it was a legit swipe, not a tap
        if (currentSwipe.magnitude < minSwipeLength)
        {
            swipeDirection = Swipe.None;
            return;
        }

        //Debug.Log(currentSwipe.magnitude);
        currentSwipe.Normalize();
        //Debug.Log(currentSwipe.x + " " + currentSwipe.y);

        // Swipe up
        if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
        {
            swipeDirection = Swipe.Up;
            // Swipe down
        }
        else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
        {
            swipeDirection = Swipe.Down;
            // Swipe left
        }
        else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
        {
            swipeDirection = Swipe.Left;
            // Swipe right
        }
        else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
        {
            swipeDirection = Swipe.Right;
        }
    }
}

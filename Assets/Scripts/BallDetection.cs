using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Line"))
        {
            GameInteractions.Instance.BallNearLine = true;
        }
        else if (other.gameObject.CompareTag("Net"))
        {
            GameInteractions.Instance.BallNearNet = true;
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            GameInteractions.Instance.BallNearPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Line"))
        {
            GameInteractions.Instance.BallNearLine = false;
        }
        else if (other.gameObject.CompareTag("Net"))
        {
            GameInteractions.Instance.BallNearNet = false;
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            GameInteractions.Instance.BallNearPlayer = false;
        }
    }

}

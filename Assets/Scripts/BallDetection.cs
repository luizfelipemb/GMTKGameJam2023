using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Line"))
        {
            GameSimulatorAI.Instance.BallNearLine = true;
        }
        else if (other.gameObject.CompareTag("Net"))
        {
            GameSimulatorAI.Instance.BallNearNet = true;
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            GameSimulatorAI.Instance.BallNearPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Line"))
        {
            GameSimulatorAI.Instance.BallNearLine = false;
        }
        else if (other.gameObject.CompareTag("Net"))
        {
            GameSimulatorAI.Instance.BallNearNet = false;
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            GameSimulatorAI.Instance.BallNearPlayer = false;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsInteraction : MonoBehaviour
{
    private bool _closeToTheEdge = false;
    private bool _closeToTheNet = false;
    private bool _closeToTheBody = false;

    [SerializeField][Range(0.15f, 0.9f)] private float _OutOfMoment;
    [SerializeField][Range(0.15f, 0.9f)] private float _RightMoment;
    private float _timer;
    private float _maxTime = 10.0f;

    private Image _suspectometerFill;


    private void Awake()
    {
        _suspectometerFill = transform.GetChild(0).transform.GetChild(4).GetComponent<Image>();
        _suspectometerFill.fillAmount = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_suspectometerFill.fillAmount > 0f)
            _suspectometerFill.fillAmount -= 1.0f / 30 * Time.deltaTime;
    }

    private void FixedUpdate()
    {

    }

    private IEnumerator UnfillSuspectometer ()
    {

        yield return null;
    }

    public void BallOut ()
    {
        if (!_closeToTheEdge) // button at the wrong time
            IncreaseSuspectometer(0.30f);


        if (_closeToTheEdge) //button at the right time
            IncreaseSuspectometer(0.10f);
    }

    public void HitTheBody ()
    {
        if (!_closeToTheBody) // button at the wrong time
            IncreaseSuspectometer(0.30f);


        if (_closeToTheBody) //button at the right time
            IncreaseSuspectometer(0.10f);
    }

    public void BallOnNet ()
    {
        if (!_closeToTheNet) // button at the wrong time
            IncreaseSuspectometer(0.30f);


        if (_closeToTheNet) //button at the right time
            IncreaseSuspectometer(0.10f);
    }

    //out of moment - grows in 30
    //in the right moment - grows 10
    //

    private void IncreaseSuspectometer (float value)
    {
        if (_suspectometerFill.fillAmount >= 0.9f)
        {
            Debug.Log("Entrou");
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }

        _suspectometerFill.fillAmount += value;   
    }
}

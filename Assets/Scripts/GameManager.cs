using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour

{
    public GameObject ship1;
    public GameObject ship2;
    public GameObject ship3;
    public Text questionText;
    public GameObject explosion;
    public GameObject city;
    public Text title;
    public Text buttonText;

    private List<int> answersList;
    private double frame = 0;
    private bool isExecuting = false;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            EventManager.OnPlaneReachedCity += HandleOnPlaneReachedCity;

            explosion.GetComponent<Renderer>().enabled = false;
            city.GetComponent<Renderer>().enabled = false;

        }
        catch (Exception ex)
        {
            var x = ex.Message;
        }
        
    }

    void Update()
    {
        if (!isExecuting)
        {
            if (Input.GetKeyDown("space"))
            {

                title.enabled = false;
                buttonText.enabled = false;
                city.GetComponent<Renderer>().enabled = true;
                isExecuting = true;
                StartCoroutine(StartRound());
            }
        }
        else
        {
            
        }
        
    }

    private void InitializeNewRound()
    {
        explosion.GetComponent<Renderer>().enabled = false;
        city.GetComponent<Renderer>().enabled = false;
        buttonText.enabled = true;
        Destroy(ship1);
        Destroy(ship2);
        Destroy(ship3);
        isExecuting = false;
    }

    private IEnumerator Wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    private IEnumerator StartRound()
    {
        var number1 = UnityEngine.Random.Range(1, 12);
        var number2 = UnityEngine.Random.Range(1, 12);
        var Answer1 = number1 * number2;
        var Answer2 = Answer1 - UnityEngine.Random.Range(1, 10);
        var Answer3 = Answer1 + UnityEngine.Random.Range(1, 10);
        questionText.text = number1.ToString() + " x " + number2.ToString();
        Debug.Log(questionText.text);
        answersList = new List<int>() { Answer1, Answer2, Answer3 };
        answersList = Shuffle(answersList);

        yield return new WaitForSeconds(3);

        questionText.enabled = false;
        Debug.Log(answersList.ToString());
        Spawn(-65, 55f, ship1, answersList[0]);
        Spawn(0, 55f, ship2, answersList[1]);
        Spawn(65, 55f, ship3, answersList[2]);
    }

    void OnKeyDown()
    {
        explosion.GetComponent<Renderer>().enabled = true;
    }

    private void HandleOnPlaneReachedCity()
    {
        explosion.GetComponent<Renderer>().enabled = true;
        InitializeNewRound();
    }

    private List<int> Shuffle(List<int> input)
    {
        var inputArr = input.ToArray();
        var randomIterations = UnityEngine.Random.Range(4, 20);
        int i = 0;
        while (i < randomIterations)
        {
            var temp = inputArr[0];
            input[0] = inputArr[1];
            input[1] = inputArr[2];
            input[2] = temp;
            i = i + 1;
        }
        return new List<int>(input);
    }

    void Spawn(float x, float y, GameObject target, int displayNumber)
    {
        string numberText = displayNumber.ToString();
        Debug.Log("Display Number: " + numberText);
        Vector3 position = new Vector3(x, y, 0);
        Instantiate(target, position, Quaternion.identity);
        target.isStatic = false; 
        target.transform.localScale.Set(-0.1f, -0.1f, -0.1f);
        var numberTextObj = target.transform.Find("GameObject").Find("Canvas").Find("NumberText").gameObject.GetComponent<Text>();
        numberTextObj.text = numberText;
        Debug.Log("Mesh Text:" + numberTextObj.text);
    }
}

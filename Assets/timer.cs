using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{

    public Text uiText;
    public float mainTimer;

    public GameObject GameOverText;

    public duckmanager dm;

    private float clock;
    public bool pressed = false;
    public bool counting = false;
    public bool doOnce = false;

    void Start()
    {
        //clock = mainTimer;
    }

    void Update()
    {

        if (clock > 0.0f && counting)
        {
            clock -= Time.deltaTime;
            uiText.text = clock.ToString("f");
        }

        else if (clock > 0.0f && !counting)
        {
            uiText.text = clock.ToString("f");
        }

        else if (clock <= 0.0f && !doOnce && pressed)
        {
            counting = false;
            doOnce = true;
            uiText.text = "0.00";
            clock = 0.0f;
            GameOver();
        }
    }

    public void StartButton()
    {
        clock = mainTimer;
        pressed = true;
        counting = true;
        doOnce = false;
        GameOverText.SetActive(false);
    }

    public void DoneButton()
    {
        counting = false;
        doOnce = false;
        Evaluate();

    }

    void GameOver()
    {
        GameOverText.SetActive(true);
    }

    void Evaluate()
    {
        dm.Evaluate();

    }

}
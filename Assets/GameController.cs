using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject text;
    public TextMeshProUGUI textMeshPro;
    private int totalGrass;
    public static GameController instance;
    [SerializeField] private int maxStep; //quantidade de passos que voce pode dar (definido por level)
    private int stepsLeft;
    void Start()
    {
        instance = this;
        totalGrass = GameObject.FindGameObjectsWithTag("Grass").Length;
        textMeshPro = text.GetComponent<TextMeshProUGUI>();
        textMeshPro.text = totalGrass.ToString();
        stepsLeft = maxStep;
    }

    void Update()
    {
        UpdateText();
    }


    void UpdateText()
    {
        textMeshPro.text = totalGrass.ToString() + " \nmax step: " +
            "\n" + stepsLeft.ToString();
    }

    public void StepReset()
    {
        stepsLeft = maxStep;
    }
    public void StepDecrease()
    {
        stepsLeft--;
    }

    public void GrassCountDecrease()
    {
        totalGrass = totalGrass - 1;
        
    }
}

using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using TMPro;
public class StageInfo : MonoBehaviour
{
    [SerializeField] private int maxGuuNumber;
    [SerializeField] private TextMeshProUGUI guuNumberText;
    private int currentGuuNumber;
    private bool isStageOn;
    [SerializeField] private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentGuuNumber = maxGuuNumber;
        isStageOn = false;
    }
    public void setGuuNumber()
    {
        guuNumberText.text = currentGuuNumber.ToString();
    }

    void setStagebool(bool isOn)
    {
        isStageOn = isOn;
    }
    public int GetGuuNumber()
    {
        return currentGuuNumber;
    }
    public void UseGuu()
    {
        if (currentGuuNumber > 0)
        {
            currentGuuNumber--;
        }
        guuNumberText.text = currentGuuNumber.ToString();
    }
    public void RefillGuu()
    {
        currentGuuNumber = maxGuuNumber;
        guuNumberText.text = currentGuuNumber.ToString();
    }
    public void OnGuuButtonClick()
    {
        if(currentGuuNumber <= 0)
        {
            return;
        }
        UseGuu();
        player.GetComponent<Player>().GuuUse();
    }

    
}

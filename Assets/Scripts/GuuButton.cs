using UnityEngine;

public class GuuButton : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public void OnGuuButtonClick()
    {
        StageInfo stageInfo = player.GetComponent<Player>().GetCurrentStageInfo();
        if (stageInfo != null)
        {
            stageInfo.OnGuuButtonClick();
        }
    }
    
}

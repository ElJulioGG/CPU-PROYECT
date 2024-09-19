using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public RhythmManager rhythmManager;
    public Text messageText;

    void Update()
    {
        if (rhythmManager.HasLost())
        {
            messageText.text = "Perdiste";
        }
    }


}

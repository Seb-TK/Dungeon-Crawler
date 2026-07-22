using TMPro;
using UnityEditor.Build;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI NitroText;
    [SerializeField] TextMeshProUGUI HealthText;
    [SerializeField] PlayerManager playerManager;
    GameObject player;


    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if (player != null)
        {
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

            NitroText.text = "Nitro: ";
            NitroText.text += Mathf.RoundToInt(playerMovement.nitroFuel).ToString();
            if (playerMovement.nitroFatigue)
            {
                NitroText.color = Color.red;
            }
            else
            {
                NitroText.color = Color.white;
            }
        }

        HealthText.text = "Health: ";
        HealthText.text += playerManager.Health.ToString();
        //if (playerManager.Health)

    }
}

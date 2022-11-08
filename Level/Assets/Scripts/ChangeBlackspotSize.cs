using UnityEngine;

public class ChangeBlackspotSize : MonoBehaviour
{
    float blackspotMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        blackspotMultiplier = gameManager.instance.blackspot.blackSpotMultiplier;
        gameObject.transform.localScale = new Vector3(5.0f * blackspotMultiplier, 0.01f, 5.0f * blackspotMultiplier);
    }

    // Update is called once per frame
    void Update()
    {
        if (blackspotMultiplier != gameManager.instance.blackspot.blackSpotMultiplier)
        {
            blackspotMultiplier = gameManager.instance.blackspot.blackSpotMultiplier;
            gameObject.transform.localScale = new Vector3(5.0f * blackspotMultiplier, 0.01f, 5.0f * blackspotMultiplier);
        }
    }
}

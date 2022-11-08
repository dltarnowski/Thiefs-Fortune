using UnityEngine;

public class Pointer : MonoBehaviour
{
    Vector3 targetPos;
    [SerializeField] GameObject pointer;

    public void SetTarget()
    {
        targetPos = gameManager.instance.miniMapObjectiveIcons[winManager.instance.clueCount].transform.position;
    }

    private void LateUpdate()
    {
        if (targetPos != Vector3.zero && gameManager.instance.miniMapObjectiveIcons[winManager.instance.clueCount] != null)
        {
            Vector3 targetPosScreenPoint = gameManager.instance.miniMapCamera.WorldToScreenPoint(targetPos);
            bool isOffScreen = targetPosScreenPoint.x <= 0 || targetPosScreenPoint.x >= gameManager.instance.miniMapCamera.scaledPixelWidth ||
                               targetPosScreenPoint.y <= 0 || targetPosScreenPoint.y >= gameManager.instance.miniMapCamera.scaledPixelHeight;

            if (isOffScreen)
            {
                pointer.SetActive(true);

                Vector3 toPosition = targetPos;
                Vector3 fromPosition = gameManager.instance.player.transform.position;
                fromPosition.y = 0;
                toPosition.y = 0;
                Vector3 dir = (toPosition - fromPosition).normalized;
                float angle = (Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg) % 360;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
            }
            else
                pointer.SetActive(false);
        }
    }
}

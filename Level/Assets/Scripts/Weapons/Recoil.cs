using UnityEngine;

public class Recoil : MonoBehaviour
{
    Vector3 currentRotation;
    Vector3 targetRotation;

    Vector3 headPos;
    Vector3 targetPos;

    [SerializeField] Vector3 movementVector;

    // Update is called once per frame
    void Update()
    {
        if (gameManager.instance.playerScript.gunStats != null)
        {
            targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, gameManager.instance.playerScript.gunStats.returnSpeed * Time.deltaTime);
            currentRotation = Vector3.Slerp(currentRotation, targetRotation, gameManager.instance.playerScript.gunStats.snappiness * Time.fixedDeltaTime);
            transform.localRotation = Quaternion.Euler(currentRotation);
        }
        if (gameManager.instance.playerScript.swordStat != null)
        {
            targetPos = Vector3.Lerp(targetPos, Vector3.zero, gameManager.instance.playerScript.swordStat.returnSpeed * Time.deltaTime);
            headPos = Vector3.Slerp(headPos, targetPos, gameManager.instance.playerScript.swordStat.snappiness * Time.fixedDeltaTime);
            transform.localPosition = headPos;
        }
    }

    public void RecoilFire()
    {
        targetRotation += new Vector3(gameManager.instance.playerScript.gunStats.recoilX, 
            Random.Range(-gameManager.instance.playerScript.gunStats.recoilY, 
            gameManager.instance.playerScript.gunStats.recoilY), 
            Random.Range(-gameManager.instance.playerScript.gunStats.recoilZ,
            gameManager.instance.playerScript.gunStats.recoilZ));
    }

    public void MeleeSwing()
    {
        targetPos += new Vector3(gameManager.instance.playerScript.swordStat.recoilX, 
            gameManager.instance.playerScript.swordStat.recoilY,
            gameManager.instance.playerScript.swordStat.recoilZ);
    }
}

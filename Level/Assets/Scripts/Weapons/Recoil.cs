using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    Vector3 currentRotation;
    Vector3 targetRotation;

    Vector3 headPos;
    Vector3 targetPos;

    [SerializeField] Vector3 movementVector;

    GunStats gunStatScript;
    MeleeStats meleeStatScript;

    void Start()
    {
        gunStatScript = null;
        meleeStatScript = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (gunStatScript != null)
        {
            targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, gunStatScript.returnSpeed * Time.deltaTime);
            currentRotation = Vector3.Slerp(currentRotation, targetRotation, gunStatScript.snappiness * Time.fixedDeltaTime);
            transform.localRotation = Quaternion.Euler(currentRotation);
        }
        if (meleeStatScript != null)
        {
            targetPos = Vector3.Lerp(targetPos, Vector3.zero, meleeStatScript.returnSpeed * Time.deltaTime);
            headPos = Vector3.Slerp(headPos, targetPos, meleeStatScript.snappiness * Time.fixedDeltaTime);
            transform.localPosition = headPos;
        }
    }

    public void RecoilFire()
    {
        targetRotation += new Vector3(gunStatScript.recoilX, Random.Range(-gunStatScript.recoilY, gunStatScript.recoilY), Random.Range(-gunStatScript.recoilZ, gunStatScript.recoilZ));
        Debug.Log("Recoil");
    }

    public void MeleeSwing()
    {
        targetPos += new Vector3(meleeStatScript.moveX, meleeStatScript.moveY, meleeStatScript.moveZ);
        Debug.Log("Swing");
    }

    public void SetGunStatScript(GunStats stats)
    {
        gunStatScript = stats;
    }

    public void SetMeleeStatScript(MeleeStats stats)
    {
        meleeStatScript = stats;
    }
}

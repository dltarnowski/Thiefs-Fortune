using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    Vector3 currentRotation;
    Vector3 targetRotation;

    GunStats gunStatScript;

    void Start()
    {
        gunStatScript = null;
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
    }

    public void RecoilFire()
    {
        targetRotation += new Vector3(gunStatScript.recoilX, Random.Range(-gunStatScript.recoilY, gunStatScript.recoilY), Random.Range(-gunStatScript.recoilZ, gunStatScript.recoilZ));
        Debug.Log("Recoil");
    }

    public void SetGunStatScript(GunStats stats)
    {
        gunStatScript = stats;
    }
}

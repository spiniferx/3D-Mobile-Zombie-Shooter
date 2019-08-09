using UnityEngine;
using System.Collections;

public class Recoil : MonoBehaviour
{
    public float recoilReleaseSpeed = 2f;

    private void Update()
    {
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(Vector3.zero), Time.deltaTime * recoilReleaseSpeed);
    }

    public void AddRecoil(Vector3 recoil)
    {
        transform.localRotation *= Quaternion.Euler(recoil);
    }
}
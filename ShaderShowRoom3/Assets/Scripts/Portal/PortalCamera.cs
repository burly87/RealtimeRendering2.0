using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform playerCam;
    public Transform portal;
    public Transform otherPortal;

    void Update()
    {
        Vector3 playerOffsetFromPortal = playerCam.position - otherPortal.position;
        this.transform.position = portal.position + playerOffsetFromPortal;

        float angularDiff = Quaternion.Angle(portal.rotation, otherPortal.rotation);

        Quaternion portalRotDiff = Quaternion.AngleAxis(angularDiff, Vector3.up);
        Vector3 newCameraDir = portalRotDiff * playerCam.forward;
        transform.rotation = Quaternion.LookRotation(newCameraDir, Vector3.up);
    }
}

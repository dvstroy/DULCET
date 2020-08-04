using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : BaseCamera
{
    public float m_Distance = 10f;
    public float m_Height = 5f;
    public float m_SmoothSpeed = 2f;

    private Vector3 wantedPosition;
    private Vector3 wantedBack;
    private float wantedYAngle;

    protected override void HandleCamera()
    {
        base.HandleCamera();

        wantedYAngle = Mathf.LerpAngle(wantedYAngle, m_Target.eulerAngles.y, Time.deltaTime * m_SmoothSpeed);

        Vector3 back = Vector3.back;
        back = Quaternion.AngleAxis(wantedYAngle, Vector3.up) * back;

        wantedPosition = (back * m_Distance) + (Vector3.up * m_Height) + m_Target.position;

        transform.position = wantedPosition;
        transform.LookAt(m_Target);
    }
}

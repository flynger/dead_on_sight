using UnityEditor;
using UnityEngine;

namespace StarterAssets
{
    [CustomEditor(typeof(SeekerAI)), CanEditMultipleObjects ]
    public class SeekerAiGizmos : FieldOfVIewGizmos
    {
        private void OnSceneGUI()
        {
            SeekerAI fOV = (SeekerAI)target;
            Handles.color = Color.white;
            Handles.DrawWireArc(fOV.transform.position, Vector3.up, Vector3.forward, 360, fOV.sightRange);

            Vector3 viewAngle1 = DistanceFromAngle(fOV.transform.eulerAngles.y, -fOV.sightAngle / 2);
            Vector3 viewAngle2 = DistanceFromAngle(fOV.transform.eulerAngles.y, fOV.sightAngle / 2);

            Handles.color = Color.yellow;
            Handles.DrawLine(fOV.transform.position, fOV.transform.position + viewAngle1 * fOV.sightRange);
            Handles.DrawLine(fOV.transform.position, fOV.transform.position + viewAngle2 * fOV.sightRange);

            if (fOV.inSightRange)
            {
                Handles.color = Color.green;
                Handles.DrawLine(fOV.transform.position, fOV.gameManager.player.transform.position);
            }
        }

        private Vector3 DistanceFromAngle(float eulerY, float angleInDegrees)
        {
            angleInDegrees += eulerY;

            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimAssist : MonoBehaviour {

    [Tooltip("Assist strength decay as angle to closest target increases")]
    [SerializeField] private AnimationCurve _aimAssistStrength;
    [Tooltip("Max angle for which aim is assisted. Beyond this value there is no assist")]
    [SerializeField][Range(0f, 90f)] private float _maxAngle = 30f;

    /// <summary>
    /// Return the aim assisted direction based on the current input aim
    /// and a set of target transforms. If the angle to the closest
    /// target is greater than _maxAngle then aim is returned unchanged.
    /// </summary>
    /// <param name="aim"></param>
    /// <param name="targets"></param>
    /// <returns></returns>
    public Vector3 GetAssistedAim(Vector3 aim, ref List<Transform> targets)
    {
        /*
         * This method will loop through the provided targets and select the
         * closest in angle to the original aim direction. Then it will
         * return an interpolated vector between the original aim and the
         * direction of the closest target.
         * To change the pull of the assist system, modify the shape of the
         * _aimAssistStrength curve in the editor. To change the maximum angle at which
         * assist begins to kick in, modify the _maxAngle field in the editor.
         */
        if (targets.Count == 0) return aim;
        float minAngle = 180f;
        Vector3 desiredDirection = aim;
        foreach (Transform target in targets) {
            Vector3 directionToTarget = target.position - transform.position;
            /* you can uncomment the line below to limit assist to XZ axes (2-dimensional) */
            //directionToTarget.y = 0f;
            directionToTarget.Normalize();
            float angle = Vector3.Angle(aim, directionToTarget);
            if (angle < minAngle) {
                minAngle = angle;
                desiredDirection = directionToTarget;
            }
        }
        if (minAngle > _maxAngle) return aim;
        float assistStrength = _aimAssistStrength.Evaluate(minAngle / _maxAngle);
        return Vector3.Slerp(aim, desiredDirection, assistStrength);
    }
}

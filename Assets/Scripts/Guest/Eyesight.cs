using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Eyesight : MonoBehaviour
{
    [SerializeField, Range(0,360f)] private float range;
    [SerializeField] private float size;
    [SerializeField] private LayerMask layerMask;

    public GameObject target;

    private void FindTatget()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, size, layerMask);
        for(int i = 0; i < targets.Length; i++)
        {
            Vector3 dirToTarget = (targets[i].transform.position - transform.position).normalized;

            if(Vector3.Dot(transform.forward, dirToTarget) < Mathf.Cos(range * 0.5f * Mathf.Deg2Rad))
            {
                continue;
            }
            target = targets[i].gameObject;
            return;
        }
        target = null;
    }

    private Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, size);

        Vector3 lookDir = AngleToDir(transform.eulerAngles.y);
        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + range * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - range * 0.5f);

        Debug.DrawRay(transform.position, lookDir * size, Color.red);
        Debug.DrawRay(transform.position, rightDir * size, Color.green);
        Debug.DrawRay(transform.position, leftDir * size, Color.green);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Eyesight : MonoBehaviour
{
    [Header("Range")]
    [SerializeField, Range(0,360f)] private float range;
    [SerializeField] private float size;
    [SerializeField] private float atkSize;

    [Header("Target")]
    [SerializeField] private GameObject _target;
    public GameObject target => _target;
    [SerializeField] private GameObject _atkTarget;
    public GameObject atkTarget => _atkTarget;

    [SerializeField] private LayerMask layerMask;
    public void FindTarget()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, size, layerMask);
        for(int i = 0; i < targets.Length; i++)
        {
            _target = targets[i].gameObject;
            return;
        }

        _target = null;
    }

    public void FindAtkTarget()
    {
        Collider[] atkTargets = Physics.OverlapSphere(transform.position, atkSize, layerMask);
        for (int j = 0; j < atkTargets.Length; j++)
        {
            Vector3 dirToTarget = (atkTargets[j].transform.position - transform.position).normalized;
            if (Vector3.Dot(transform.forward, dirToTarget) < Mathf.Cos(range * 0.5f * Mathf.Deg2Rad))
            {
                continue;
            }
            _atkTarget = atkTargets[j].gameObject;
            return;
        }
        _atkTarget = null;
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, atkSize);

        Vector3 lookDir = AngleToDir(transform.eulerAngles.y);
        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + range * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - range * 0.5f);

        Debug.DrawRay(transform.position, lookDir * size, Color.red);
        Debug.DrawRay(transform.position, rightDir * size, Color.green);
        Debug.DrawRay(transform.position, leftDir * size, Color.green);
    }
}

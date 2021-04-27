using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetInRangeChecker : MonoBehaviour
{
   [SerializeField] private float _radius;
   [SerializeField] private LayerMask _layer;

   public Enemy GetFirstEnemyInRange()
   {
      Enemy enemy = null;

      Collider[] hitColliders = Physics.OverlapSphere(transform.position, _radius, _layer);

      foreach (Collider hitCollider in hitColliders)
      {
         Enemy e = hitCollider.transform.parent.GetComponent<Enemy>();
         if (e != null)
         {
            enemy = e;
            break;
         }
      }
      return enemy;
   }

   private void OnDrawGizmos()
   {
      Gizmos.color = Color.cyan;
      Gizmos.DrawWireSphere(transform.position, _radius);
   }
}

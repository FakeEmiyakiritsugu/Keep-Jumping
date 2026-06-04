using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishStar : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        // 利用 attachedRigidbody 确保无论碰到角色的哪个部位都能准确抓取控制器
        if (other.attachedRigidbody != null && other.attachedRigidbody.CompareTag("Player"))
        {
            newThirdPersonController player = other.attachedRigidbody.GetComponent<newThirdPersonController>();

            if (player != null)
            {
                
            }
        }
    }
}

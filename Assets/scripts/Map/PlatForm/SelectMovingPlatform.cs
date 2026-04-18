using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SelectMovingPlatform : MonoBehaviour
{
    [Header("基础移动设置")]
    public Vector3 moveOffset = new Vector3(0f, 0f, 0f);
    public float duration = 2f;
    public Ease easeType = Ease.Linear;

    [Header("感应触发设置")]
    [Tooltip("勾选后：只有玩家站在上面平台才会移动")]
    public bool moveOnlyWhenOccupied = true;
    [Tooltip("勾选后：玩家离开时平台会慢慢回到起点；不勾选则停在原地")]
    public bool returnToStart = true;
    [Tooltip("返回起点所需的时间")]
    public float returnDuration = 3f;

    private Vector3 initialPosition;
    private Vector3 lastPosition;
    private Tween mainMoveTween;
    private Tween returnTween;

    private HashSet<Transform> playersOnPlatform = new HashSet<Transform>();

    void Start()
    {
        initialPosition = transform.position;
        lastPosition = transform.position;

        // 初始化主移动动画（先不播放）
        mainMoveTween = transform.DOMove(initialPosition + moveOffset, duration)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo)
            .SetUpdate(UpdateType.Late)
            .SetAutoKill(false);

        if (moveOnlyWhenOccupied)
        {
            mainMoveTween.Pause();
        }
    }

    void Update()
    {
        // 每帧检测平台上的乘客情况
        bool hasPlayer = playersOnPlatform.Count > 0;

        if (moveOnlyWhenOccupied)
        {
            if (hasPlayer)
            {
                // 1. 有人：停止返回动画，继续主移动动画
                if (returnTween != null && returnTween.IsActive()) returnTween.Kill();

                if (!mainMoveTween.IsPlaying())
                {
                    mainMoveTween.Play();
                }
            }
            else
            {
                // 2. 没人：暂停主动画
                if (mainMoveTween.IsPlaying())
                {
                    mainMoveTween.Pause();
                }

                // 3. 如果开启了返回起点功能，且目前不在起点
                if (returnToStart && transform.position != initialPosition)
                {
                    if (returnTween == null || !returnTween.IsActive())
                    {
                        // 启动返回动画
                        returnTween = transform.DOMove(initialPosition, returnDuration)
                            .SetEase(Ease.InOutQuad)
                            .SetUpdate(UpdateType.Late);
                    }
                }
            }
        }
    }

    void LateUpdate()
    {
        // 核心逻辑不变：只要坐标变了，就把位移差同步给乘客
        // 无论是主动移动还是返回移动，这行代码都能完美覆盖
        Vector3 deltaPosition = transform.position - lastPosition;

        foreach (Transform playerTransform in playersOnPlatform)
        {
            if (playerTransform != null)
            {
                playerTransform.position += deltaPosition;
            }
        }

        lastPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null && other.attachedRigidbody.CompareTag("Player"))
        {
            playersOnPlatform.Add(other.attachedRigidbody.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody != null && other.attachedRigidbody.CompareTag("Player"))
        {
            playersOnPlatform.Remove(other.attachedRigidbody.transform);
        }
    }
}

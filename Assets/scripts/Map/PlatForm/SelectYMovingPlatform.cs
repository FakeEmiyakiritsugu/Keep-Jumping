using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SelectYMovingPlatform : MonoBehaviour
{
    [Header("水平移动 (始终运行)")]
    public Vector3 horizontalOffset = new Vector3(10f, 0f, 0f);
    public float horizontalDuration = 3f;

    [Header("垂直移动 (感应运行)")]
    public float verticalHeight = 5f;
    public float verticalDuration = 2f;
    public float returnDuration = 1.5f;

    private Vector3 initialPos;
    private Rigidbody rb;

    // --- 纯数学进度控制器 ---
    private float horizontalProgress = 0f;
    private bool movingHorizontalForward = true;
    private float currentVerticalOffset = 0f;

    private HashSet<Rigidbody> playersOnPlatform = new HashSet<Rigidbody>();

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // 保证这两个最核心的物理设置处于开启状态
        rb.isKinematic = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        initialPos = rb.position;
    }

    void FixedUpdate()
    {
        // ==========================================
        // 1. 计算水平方向（X/Z轴）的下一帧位置（永远在动）
        // ==========================================
        float hStep = (1f / horizontalDuration) * Time.fixedDeltaTime;
        if (movingHorizontalForward)
        {
            horizontalProgress += hStep;
            if (horizontalProgress >= 1f) { horizontalProgress = 1f; movingHorizontalForward = false; }
        }
        else
        {
            horizontalProgress -= hStep;
            if (horizontalProgress <= 0f) { horizontalProgress = 0f; movingHorizontalForward = true; }
        }

        // 使用 SmoothStep 模拟平滑的起步和刹车手感（类似 Ease.InOut）
        // 如果你想要绝对匀速，把这行改成: float hT = horizontalProgress;
        float hT = Mathf.SmoothStep(0f, 1f, horizontalProgress);

        Vector3 nextHorizontalPos = initialPos + (horizontalOffset * hT);

        // ==========================================
        // 2. 计算垂直方向（Y轴）的下一帧位置（有人才动）
        // ==========================================
        bool hasPlayer = playersOnPlatform.Count > 0;

        // 目标高度：有人就是最高点，没人就是 0
        float targetV = hasPlayer ? verticalHeight : 0f;
        // 当前需要的时间：向上和向下的时间不同
        float currentVDuration = hasPlayer ? verticalDuration : returnDuration;
        // 计算这一帧移动的速度 (距离 / 时间)
        float vSpeed = verticalHeight / currentVDuration;

        // 核心绝杀：MoveTowards 会在一帧帧中平滑地加上距离，绝对不可能发生瞬间跳跃！
        currentVerticalOffset = Mathf.MoveTowards(currentVerticalOffset, targetV, vSpeed * Time.fixedDeltaTime);

        // ==========================================
        // 3. 终极坐标合并与物理推力同步
        // ==========================================
        // 将水平的 XZ 轴与垂直的 Y 轴完美缝合
        Vector3 nextPos = nextHorizontalPos;
        nextPos.y = initialPos.y + currentVerticalOffset;

        Vector3 deltaPosition = nextPos - rb.position;

        // 用最正统的物理引擎接口移动平台
        rb.MovePosition(nextPos);

        // 用最正统的物理引擎接口移动上面的玩家
        foreach (Rigidbody playerRb in playersOnPlatform)
        {
            if (playerRb != null)
            {
                playerRb.MovePosition(playerRb.position + deltaPosition);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody playerRb = other.attachedRigidbody;
        if (playerRb != null && playerRb.CompareTag("Player"))
        {
            playersOnPlatform.Add(playerRb);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody playerRb = other.attachedRigidbody;
        if (playerRb != null && playerRb.CompareTag("Player"))
        {
            playersOnPlatform.Remove(playerRb);
        }
    }
}

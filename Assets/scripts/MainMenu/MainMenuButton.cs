using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [Header("跳转的场景设置")]
    [Tooltip("开始游戏")]
    private string startSceneName = SceneName.SampleScene;
    [Tooltip("结束游戏")]
    private string exitSceneName = "GameOverScene";

    // 绑定到“开始”按钮
    public void OnStartButtonPressed()
    {
        // 重新加载你的 3D 核心游戏场景
        SceneManager.LoadScene(startSceneName);
    }

    // 绑定到“返回主菜单”按钮
    public void OnExitButtonPressed()
    {
        Debug.Log("收到退出游戏请求！");

        // 1. 如果是在 Unity 编辑器里运行，点击时停止播放
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif

        // 2. 如果是打包后的独立程序（Windows/Mac/手机等），执行退出
        Application.Quit();
    }
}

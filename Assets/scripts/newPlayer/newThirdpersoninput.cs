using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector;
using Invector.vCharacterController;

public class newThirdpersoninput : vThirdPersonInput
{
    #region
    [Header("new Controller Input")]
    [Tooltip("冲锋按键")]
    public KeyCode dashinput = KeyCode.LeftShift;//冲锋按键



    [HideInInspector] public newThirdPersonController dd;
    #endregion





    //重写cc的初始化逻辑
    protected override void InitilizeController()
    {
        dd = GetComponent<newThirdPersonController>();
        cc = dd;

        if (dd != null)
            dd.Init();


    }


    protected override void InputHandle()
    {
        MoveInput();
        CameraInput();
        SprintInput();
        StrafeInput();
        DashInput();
        JumpInput();
    }

    //跳跃条件
    protected override bool JumpConditions()
    {
        bool jumpflag = false;
        if (cc.isGrounded && dd.GroundAngle() < dd.slopeLimit && !dd.stopMove && !dd.isJumping)//一段跳
        {
            jumpflag = true;
        }
        else if (!dd.isGrounded && dd.get_currentairjumptimes() < dd.maxairjumptimes && SegmentBar.Instance.ConsumeStamina(1))//允许空中跳跃,消耗一格耐力
        {
            jumpflag = true;
            dd.add_currentairjumptimes(1);
        }
        else
        {
            jumpflag = false;
        }
        return jumpflag;
    }

    /// <summary>
    /// 冲刺条件
    /// </summary>
    /// <returns></returns>
    protected bool DashConditions()
    {
        bool dashflag = false;
        if(!dd.isDashing&&dd.GetCurrentDashTimes()<dd.GetMaxDashTimes()&&SegmentBar.Instance.ConsumeStamina(1))//有耐力，不在冲刺
        {
            dashflag = true;
        }
        return dashflag;
    }


    //冲刺输入检测,暂时没有限制连续冲刺次数
    protected virtual void DashInput()
    {
        if (Input.GetKeyDown(dashinput) && dd.input.magnitude > 0.1f && DashConditions())//冲刺同时按下方向键
        {
            dd.Dash();
        }
    }


}

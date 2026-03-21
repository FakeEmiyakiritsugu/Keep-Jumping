using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector;
using Invector.vCharacterController;

public class newThirdpersoninput : vThirdPersonInput
{
    #region
    [Header("new Controller Input")]
    //[Tooltip("Lerp speed between Camera States")]
    public KeyCode dashinput = KeyCode.LeftShift;//冲锋按键



    [HideInInspector] public newThirdPersonConller dd;
    #endregion





    //重写cc的初始化逻辑
    protected override void InitilizeController()
    {
        dd = GetComponent<newThirdPersonConller>();
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
        else if (!dd.isGrounded && dd.currentairjumptimes < dd.maxairjumptimes)//允许空中条约
        {
            jumpflag = true;
            dd.currentairjumptimes++;
        }
        else
        {
            jumpflag = false;
        }
        return jumpflag;
    }


    //冲刺输入检测
    protected virtual void DashInput()
    {

    }


}

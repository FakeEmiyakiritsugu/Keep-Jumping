using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector;
using Invector.vCharacterController;

public class newThirdpersoninput : vThirdPersonInput
{
    #region
    [Header("new Controller Input")]
    [Tooltip("³å·æ°´¼ü")]
    public KeyCode dashinput = KeyCode.LeftShift;//³å·æ°´¼ü



    [HideInInspector] public newThirdPersonController dd;
    #endregion





    //ÖØÐ´ccµÄ³õÊ¼»¯Âß¼­
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

    //ÌøÔ¾Ìõ¼þ
    protected override bool JumpConditions()
    {
        bool jumpflag = false;
        if (cc.isGrounded && dd.GroundAngle() < dd.slopeLimit && !dd.stopMove && !dd.isJumping)//Ò»¶ÎÌø
        {
            jumpflag = true;
        }
        else if (!dd.isGrounded && dd.get_currentairjumptimes() < dd.maxairjumptimes)//ÔÊÐí¿ÕÖÐÌøÔ¾
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

    protected bool DashConditions()
    {
        bool dashflag = false;

        return dashflag;
    }


    //³å´ÌÊäÈë¼ì²â
    protected virtual void DashInput()
    {
        if (Input.GetKeyDown(dashinput) && JumpConditions())
        {
            cc.Jump();
        }
    }


}

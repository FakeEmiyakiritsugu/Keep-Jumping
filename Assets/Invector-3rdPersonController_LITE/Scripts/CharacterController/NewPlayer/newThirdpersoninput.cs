using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector;
using Invector.vCharacterController;

public class newThirdpersoninput : vThirdPersonInput
{
    #region
    [Header("new Controller Input")]
    public KeyCode dashinput = KeyCode.LeftShift;//³å·æ°´¼ü

    #endregion
    protected override void InputHandle()
    {
        MoveInput();
        CameraInput();
        SprintInput();
        StrafeInput();
        JumpInput();
    }

    protected virtual void DashInput()
    {

    }


}

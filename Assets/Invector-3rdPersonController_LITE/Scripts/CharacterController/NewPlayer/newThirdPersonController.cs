using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector;
using Invector.vCharacterController;

public class newThirdPersonController : vThirdPersonController
{



    #region
    [Header("new Controller")]
    [Tooltip("max air jump times")]
    public int maxairjumptimes = 1;//最大空中跳跃次数



    internal int currentairjumptimes = 0;//当前跳跃次数


    #endregion

    protected override void CheckGround()
    {
        CheckGroundDistance();
        ControlMaterialPhysics();

        if (groundDistance <= groundMinDistance)
        {
            isGrounded = true;

            //在空中与地表发生变化
            IsGroundedChanged();

            if (!isJumping && groundDistance > 0.05f)
                _rigidbody.AddForce(transform.up * (extraGravity * 2 * Time.deltaTime), ForceMode.VelocityChange);

            heightReached = transform.position.y;
        }
        else
        {
            if (groundDistance >= groundMaxDistance)
            {
                // set IsGrounded to false 
                isGrounded = false;
                // check vertical velocity
                verticalVelocity = _rigidbody.velocity.y;
                // apply extra gravity when falling
                if (!isJumping)
                {
                    _rigidbody.AddForce(transform.up * extraGravity * Time.deltaTime, ForceMode.VelocityChange);
                }
            }
            else if (!isJumping)
            {
                _rigidbody.AddForce(transform.up * (extraGravity * 2 * Time.deltaTime), ForceMode.VelocityChange);
            }
        }
    }
    protected virtual void IsGroundedChanged()//空中与地面的状态发生变化
    {
        if (isGrounded)//从空中到地面你
        {
            currentairjumptimes = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector;
using Invector.vCharacterController;

public class newThirdPersonController : vThirdPersonController
{



    #region
    [Header("new Controller")]
    [Tooltip("最大耐力")]
    public int maxstamina = 1;
    [Tooltip("max air jump times")]
    public int maxairjumptimes = 1;//最大空中跳跃次数
    [Tooltip("单次冲刺的持续时间（秒），建议0.15-0.3s")]
    public float DashDuration = 0.2f;
    [Tooltip("冲刺的移动速度（米/秒），建议15-25，过高易穿墙")]
    public float dashSpeed = 20f;
    [Tooltip("冲刺冷却时间（秒），避免连续冲刺")]
    public float dashCooldown = 1f;
    //[Tooltip("是否允许在空中冲刺（适配你的二段跳）")]
    //public bool allowAirSprint = false;
    [Tooltip("无方向输入时，是否朝角色正前方冲刺")]
    public bool dashForwardWhenNoInput = true;
    [Tooltip("空中冲刺时是否禁用重力（避免冲刺时下落）")]
    public bool disableGravityWhenDash = false;





    #endregion
    #region
    public bool _isDashing = false;//是否正在冲刺
    #endregion
    #region
    //脚本内部状态变量
    private int currentstamina = 1;//当前耐力条
    //跳跃相关
    private int currentairjumptimes = 0;//当前跳跃次数

    //冲刺相关
    
    #endregion
    public int get_currentairjumptimes()
    {
        return currentairjumptimes;
    }
    public int add_currentairjumptimes(int number)
    {
        currentairjumptimes += number;
        return currentairjumptimes;
    }

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
    /// <summary>
    /// 发起冲刺
    /// </summary>
    public virtual void Dash()
    {

    }
}

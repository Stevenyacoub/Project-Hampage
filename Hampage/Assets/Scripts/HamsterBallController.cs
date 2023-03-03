using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class HamsterBallController : ControllerCharacter
{
    public override void Awake() {
        // We do not want the HamsterballController to awake until the object is interacted with
    }
    // Once the HamsterBall is interacted with we can make call the original movement Awake()
    public override void OnEnable() {
        base.Awake();
        // triggers animation to enter the ball
        //anim.SetTrigger("EnterBall");
    }
    public override void OnDisable()
    {
        base.OnDisable();
        // triggers animation to exit the ball
        //anim.SetTrigger("ExitBall");
    }
    public override void Update() {
        base.Update();
    }

    public override void handleRotation() { 
        base.handleRotation();
    }

    public override void handleGravity() { 
        base.handleGravity();
    }
    public override void onMoveInput(InputAction.CallbackContext context) {
        currMovement.x = context.ReadValue<Vector2>().x;
        currMovement.z = context.ReadValue<Vector2>().y;

        //Only if our movement is above a threshold, take camera into consideration when chosing move direction
        if (currMovement.magnitude >= 0.1f)
        {
            targetAngle = Mathf.Atan2(currMovement.x, currMovement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            currMovement = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
    }

    public override void onRunInput(InputAction.CallbackContext context) {
        runTriggered = context.ReadValueAsButton();
    }

    public override void onJumpInput(InputAction.CallbackContext context) {
        jumpTriggered = context.ReadValueAsButton();
    }
}





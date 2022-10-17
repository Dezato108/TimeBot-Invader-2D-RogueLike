using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Walk : StateMachineBehaviour
{
    Transform playerToChase;
    Rigidbody2D bossRigidBody;
    Vector3 directionToMoveIn;
    [SerializeField] float speed = 2.5f;
    [SerializeField] float attackRange;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerToChase = GameObject.FindObjectOfType<PlayerController>().transform;
        bossRigidBody = animator.GetComponent<Rigidbody2D>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 newPosition = Vector2.MoveTowards(bossRigidBody.position, playerToChase.position, speed* Time.fixedDeltaTime);

        bossRigidBody.MovePosition(newPosition);

        float distance = Vector2.Distance(playerToChase.position, bossRigidBody.position);

        if (distance < attackRange)
        {
            animator.SetTrigger("Attack");
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }


}

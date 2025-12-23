using UnityEngine;

/// <summary>
/// ¼Ä¤Hª¬ºA
/// </summary>
public class EnemyState : State
{
    protected Enemy enemy;

    public EnemyState(Enemy enemy, StateMachine stateMachine, string name)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.name = name;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log($"<color=#ff3>¶i¤Jª¬ºA : {name}</color>");
    
    }
}

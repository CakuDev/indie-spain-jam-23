using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStatus
{
    CLIMBING,
    ATTACKING,
    CHASING_PLAYER,
    DESTROYING,
    MOVING_TO_DESTROY,
    MOVING_TO_CHANGE_FLOOR,
    CHANGE_FLOOR,
    PARRIED, 
    DYING
}

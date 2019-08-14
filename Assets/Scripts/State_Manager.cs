using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Manager : MonoBehaviour {
    public enum e_TYPE {
        MELEE,
        RANGE,
    }
    public enum e_STATE {
        PATROL,
        ATTACK,
        TARGETING
    }
    
}

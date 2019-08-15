using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Manager : MonoBehaviour {
    public enum e_TYPE {
        Melee,
        Range,
    }

    public enum e_STATE {
        Patrol,
        Attack,
        Targetting
    }

    public enum e_BoostType {
        DiagonalShot,
        RearShot,
        SideShot,
        Laser,
        BonusDmg,
        HPBoost,
        Heal
    }

    public enum e_BulletType {
        Regular,
        Laser
    }

}


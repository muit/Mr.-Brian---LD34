﻿using UnityEngine;
using System.Collections;
using Crab;

[RequireComponent(typeof(Entity))]
[DisallowMultipleComponent]

[System.Serializable]
public class EntityController : MonoBehaviour{
    protected Entity me;
    
    void Start() {
        me = GetComponent<Entity>();
        SendMessage("JustSpawned");
    }

    //Reusable Methods
    void JustSpawned() {}
    void EnterCombat(Entity target) {}
    void FinishCombat(Entity enemy) {}
    void Update() {}
    void JustDead(Entity killer) {}
    void JustKilled(Entity victim) {}
    protected virtual void AnyDamage(int damage, Entity damageCauser, DamageType damageType) { }
}

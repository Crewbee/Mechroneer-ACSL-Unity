using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ParticleEmitterClass
{
    EC_ONDEATH = 0,
    EC_ONAWAKE,
    EC_FOLLOW
}

public enum SpawnableColliderType
{
    CT_NONE = 0,
    CT_BOX,
    CT_CAPSULE,
    CT_SPHERE,
    CT_MESH
}
public enum EmitterDirectionModifierTypes
{
    ED_FORWARD= 0,
    ED_UP = 1,
    ED_BACK = 2,
    ED_DOWNWARD = 3
}

[System.Serializable]
public class ProjectileEmitter
{
    public ParticleEmitterClass Type;
    public List<GameObject> ParticleEmitterObject;
    public EmitterDirectionModifierTypes Direction;
}

[CreateAssetMenu(fileName = "New Projectile Data", menuName = "Projectile Data", order = 51)]

[System.Serializable]
public class ProjectileObject : ScriptableObject
{
    [Header("Projectile")]
    public Mesh ProjectileMesh;
    public Material ProjectileMaterial;
    public Vector3 ProjectileScale;
    public float InitialSpeed;
    public float InitialForceMultiplier;
    public float MaxSpeed;
    public bool ProjectileHasAcceleration;
    public float ProjectileAcceleration;
    public float LifeTime;
    public float ProjectileBaseDamageMin;
    public float ProjectileBaseDamageMax;
    public SpawnableColliderType ProjectileColliderType;
    public bool ProjectileColliderIsTrigger;
    public bool ProjectileHasTrail;
    [SerializeField]
    public TrailRenderer test;
    [Space(10)]

    [Header("Homing")]
    public bool IsHomingProjectile;
    public float HomingAcceleration;
    public bool RotationFollowsVelocity;
    [Space(10)]

    [Header("Explosive")]
    public bool ProjectileIsExplosive;
    public bool ProjectileSpawnsParticles;
    public List<ProjectileEmitter> ProjectilePartilceEmitters;
    public float ExplosionRadius;
    public float ExplosionForce;
    public float ExplosionDamage;
    [Range(0.0f, 1.0f)]
    public float ExplosionDamageDropOff;
    [Space(10)]

    [Header("Physics")]
    public bool ProjectileUsesPhysics;
    public float ProjectileMass;
    public ForceMode ProjectileForceMode = ForceMode.Force;
    public bool ProjectileIsKinematic;
    public CollisionDetectionMode ProjectileCollisionMode;
    public bool ProjectileUsesGravity;
    public float ProjectileDrag;
    public float ProjectileAngularDrag = 0.05f;

}

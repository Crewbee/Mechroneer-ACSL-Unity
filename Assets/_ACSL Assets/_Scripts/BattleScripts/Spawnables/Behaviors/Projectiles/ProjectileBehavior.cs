//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ProjectileBehavior : Spawnable, ISpawnableObject
//{
//    public ProjectileObject ProjectileData;

//    #region Projectile Variables
//    private float m_ProjectileInitialSpeed;
//    private float m_ProjectileInitialForceMultiplier;
//    private float m_ProjectileMaxSpeed;
//    private bool m_ProjectileRotFollowsVelocity;
//    private bool m_ProjectileHasAcceleration;
//    private float m_ProjectileAcceleration;
//    private float m_ProjectileBaseDamageMax;
//    private float m_ProjectileBaseDamageMin;
//    private SpawnableColliderType m_ProjectileColliderType;
//    private bool m_ProjectileColliderIsTrigger;
//    private bool m_ProjectileHasTrail;
//    #endregion

//    #region Homing Variables
//    private bool m_IsHomingProjectile;
//    private float m_HomingAcceleration;
//    #endregion

//    #region Explosive Variables
//    private bool m_ProjectileIsExplosive;
//    private bool m_ProjectileSpawnsParticles;
//    private Dictionary<ParticleEmitterClass, List<GameObject>> m_ProjectilePartilceEmmiters;
//    private float m_ExplosiveRadius;
//    private float m_ExplosionForce;
//    private float m_ExplosionDamage;
//    private float m_ExplosionDamageDropOff;
//    #endregion

//    #region Physics Variables
//    private bool m_ProjectileUsesPhysics;
//    private float m_ProjectileMass;
//    private bool m_ProjectileIsKinematic;
//    private ForceMode m_ProjectileForceMode;
//    private CollisionDetectionMode m_ProjectileCollisionMode;
//    private bool m_ProjectileUsesGravity;
//    private float m_ProjectileDrag;
//    private float m_ProjectileAngularDrag;
//    #endregion

//    #region Local Variables
//    private float m_ProjectileCurrentSpeed = 0.0f;
//    private Vector3[] m_EmitterDirections =
//    {
//        Vector3.one,
//        Vector3.up,
//        -Vector3.one,
//        Vector3.down,
//    };
//    #endregion

//    // Awake is called when the GameObject is instatiated
//    private void Awake()
//    {

//        Spawnable_Timer = new MyTimer();
//        m_ProjectilePartilceEmmiters = new Dictionary<ParticleEmitterClass, List<GameObject>>();
//        if (ProjectileData != null)
//        {
//            LoadProjectile();
//            BuildProjectile();
//        }

//    }

//    // Start is called before the first frame update
//    void Start()
//    {
//        if (!m_ProjectileIsKinematic)
//        {
//            if (Spawnable_RigidBody)
//            {
//                Spawnable_RigidBody.velocity = Vector3.zero;
//                Spawnable_RigidBody.detectCollisions = false;
//            }
//        }
//        if (m_ProjectileForceMode == ForceMode.Impulse)
//        {
//            Spawnable_RigidBody.AddForce(Spawnable_Direction * m_ProjectileInitialSpeed, m_ProjectileForceMode);
//        }
//    }

//    public void SetOwner(Transform owner)
//    {
//        Spawnable_Owner = owner;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        Spawnable_LifeTime -= Time.deltaTime;
//    }

//    //Update Non Kinematic Projectiles
//    void FixedUpdate()
//    {
//        if (!m_ProjectileIsKinematic)
//        {
//            if (m_ProjectileHasAcceleration)
//            {
//                m_ProjectileCurrentSpeed = CalculateProjectileSpeedWithAcceleration(m_ProjectileCurrentSpeed, Time.fixedDeltaTime);
//            }
//            if (Spawnable_RigidBody)
//            {
//                if (m_ProjectileRotFollowsVelocity)
//                {
//                    Vector3 Dir = Spawnable_RigidBody.velocity.normalized;
//                    Quaternion targetRotation = Quaternion.LookRotation(Dir, Vector3.up);
//                    Spawnable_RigidBody.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, 180.0f));
//                }
//                if (m_IsHomingProjectile)
//                {
//                    Vector3 Dir = CalculateHomingDirection(Spawnable_RigidBody.velocity);
//                    Quaternion targetRotation = Quaternion.LookRotation(Dir, Vector3.up);
//                    Spawnable_RigidBody.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, 180.0f));
//                }
//                if (m_ProjectileForceMode != ForceMode.Impulse)
//                {
//                    ApplyForceToReachVelocity(Spawnable_RigidBody, transform.forward * m_ProjectileCurrentSpeed, m_ProjectileInitialForceMultiplier, m_ProjectileForceMode);
//                }
//            }
//            else
//            {
//                Debug.LogWarning("Spawnable must use physics to rotate towards its velocity normalized or use homing mechanics");
//            }

//        }

//        if (m_ProjectileIsKinematic)
//        {
//            Vector3 lastBodyPosition = Spawnable_RigidBody.position;

//            if (m_ProjectileHasAcceleration)
//            {
//                m_ProjectileCurrentSpeed = CalculateProjectileSpeedWithAcceleration(m_ProjectileCurrentSpeed, Time.fixedDeltaTime);
//            }
//            if (m_IsHomingProjectile)
//            {
//                Spawnable_Direction = CalculateHomingDirection(Spawnable_Direction * m_ProjectileCurrentSpeed);
//                Quaternion targetRotation = Quaternion.LookRotation(Spawnable_Direction, Vector3.up);
//                //Spawnable_RigidBody.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, 180.0f));
//            }

//            transform.Translate(Spawnable_Direction * m_ProjectileCurrentSpeed * Time.fixedDeltaTime);

//            if (Spawnable_RigidBody)
//            {
//                Spawnable_RigidBody.MovePosition(transform.position);
//            }
//            if (m_ProjectileRotFollowsVelocity)
//            {
//                if (Spawnable_RigidBody)
//                {
//                    transform.rotation = Quaternion.LookRotation(Spawnable_Direction);
//                }
//                else
//                {
//                    Debug.LogWarning("Spawnable must use physics to rotate towards its current direction");
//                }
//            }
//        }

//        if (Spawnable_LifeTime <= 0.0f)
//        {
//            ReturnToPool();
//        }
//    }

//    ////Update Kinematic Projectiles
//    //void LateUpdate()
//    //{
//    //    if (m_ProjectileIsKinematic)
//    //    {
//    //        Vector3 lastBodyPosition = Spawnable_RigidBody.position;

//    //        if (m_ProjectileHasAcceleration)
//    //        {
//    //            m_ProjectileCurrentSpeed = CalculateProjectileSpeedWithAcceleration(m_ProjectileCurrentSpeed, Time.fixedDeltaTime);
//    //        }
//    //        if (m_IsHomingProjectile)
//    //        {
//    //            Spawnable_Direction = CalculateHomingDirection(Spawnable_Direction * m_ProjectileCurrentSpeed);
//    //            Quaternion targetRotation = Quaternion.LookRotation(Spawnable_Direction, Vector3.up);
//    //            //Spawnable_RigidBody.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, 180.0f));
//    //        }

//    //        transform.Translate(Spawnable_Direction * m_ProjectileCurrentSpeed * Time.fixedDeltaTime);

//    //        if (Spawnable_RigidBody)
//    //        {
//    //            Spawnable_RigidBody.MovePosition(transform.position);
//    //        }
//    //        if (m_ProjectileRotFollowsVelocity)
//    //        {
//    //            if (Spawnable_RigidBody)
//    //            {
//    //                transform.rotation = Quaternion.LookRotation(Spawnable_Direction);
//    //            }
//    //            else
//    //            {
//    //                Debug.LogWarning("Spawnable must use physics to rotate towards its current direction");
//    //            }
//    //        }
//    //    }

//    //    if (Spawnable_LifeTime <= 0.0f)
//    //    {
//    //        ReturnToPool();
//    //    }
//    //}


//    private float CalculateProjectileSpeedWithAcceleration(float currentSpeed, float time)
//    {
//        float ProjectileAccel = m_ProjectileAcceleration * time;

//        currentSpeed += ProjectileAccel;

//        if (currentSpeed > m_ProjectileMaxSpeed)
//        {
//            currentSpeed = m_ProjectileMaxSpeed;
//        }

//        return currentSpeed;
//    }

//    private Vector3 CalculateHomingDirection(Vector3 currentVelocity)
//    {
//        Vector3 currentDirection = currentVelocity.normalized;

//        if (Spawnable_HomingTarget.gameObject != null)
//        {
//            Vector3 newDirection = Spawnable_HomingTarget.position - transform.position;

//            newDirection.Normalize();

//            if (currentDirection != newDirection)
//            {
//                return newDirection;
//            }
//        }
//        return currentDirection;
//    }

//    //Check for collision with colliders
//    void OnCollisionEnter(Collision collision)
//    {
//        if (collision.gameObject.transform.name != "BULLET")
//        {
//            Transform target = collision.gameObject.transform;

//            while (target.parent != null)
//            {
//                target = target.parent;
//            }

//            if (target != null)
//            {
//                #region Explode if Explosive
//                if (m_ProjectileIsExplosive)
//                {
//                    Explode(transform.position);
//                }
//                #endregion
//                //Check if the object being collided with is an enemy and apply damage
//                #region Apply Damage
//                if (target.tag == "Robot" && target != Spawnable_Owner.parent)
//                {
//                    if (target.GetComponent<HealthComponent>() != null)
//                    {
//                        HealthComponent robotHealth = target.GetComponent<HealthComponent>();
//                        robotHealth.ApplyDamage(Spawnable_Damage);
//                    }

//                }
//                #endregion

//                //Return the projectile to the pool
//                #region Return to Pool
//                if (target != Spawnable_Owner.parent)
//                {
//                    PlayParticleEmittersOfType(ParticleEmitterClass.EC_ONDEATH, collision.transform.position);
//                    ReturnToPool();
//                }
//                #endregion
//            }
//        }
//    }

//    //Check for collision with triggers
//    void OnTriggerEnter(Collider other)
//    {
//        if (Spawnable_Owner)
//        {
//            if (other.gameObject.transform.name != "BULLET")
//            {
//                if(other.gameObject.tag == "Robot" || other.gameObject.layer == LayerMask.GetMask("Robot"))
//                { 
//                var robot = other.gameObject.GetComponentInParent<Robot>();
//                    if (robot)
//                    {
//                        var healthComp = robot.GetComponent<HealthComponent>();

//                        #region Explode if Explosive
//                        if (m_ProjectileIsExplosive)
//                        {
//                            Explode(transform.position);
//                        }
//                        #endregion
//                        //Check if the object being collided with is an enemy and apply damage
//                        #region Apply Damage
//                        if (robot.gameObject.transform != Spawnable_Owner.parent)
//                        {

//                            healthComp.ApplyDamage(Spawnable_Damage);
//                        }


//                        #endregion

//                        //Return the projectile to the pool
//                        #region Return to Pool
//                        if (robot.transform != Spawnable_Owner.parent)
//                        {
//                            PlayParticleEmittersOfType(ParticleEmitterClass.EC_ONDEATH, other.ClosestPointOnBounds(transform.position));
//                            ReturnToPool();
//                        }
//                        #endregion
//                    }
//                }
//            }
//        }
//    }



//    //Check for exiting triggers
//    void OnTriggerExit(Collider other)
//    {
//        if (Spawnable_Owner)
//        {
//            Transform target = other.gameObject.transform;

//            while (target.parent != null)
//            {
//                target = target.parent;
//            }

//            if (target != Spawnable_Owner.parent)
//            {
//                if (gameObject.activeSelf == true)
//                {
//                    ReturnToPool();
//                }
//            }
//        }
//    }

//    private void Explode(Vector3 position)
//    {
//        Collider[] ExplosionTargets = Physics.OverlapSphere(position, m_ExplosiveRadius);

//        float DropOffDamagePoint = m_ExplosiveRadius * m_ExplosionDamageDropOff;

//        foreach (Collider nearbyObject in ExplosionTargets)
//        {
//            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
//            if (rb)
//            {
//                rb.AddExplosionForce(m_ExplosionForce, position, m_ExplosiveRadius);
//            }
//            float dist = Vector3.Distance(position, nearbyObject.transform.position);

//            GameObject go = nearbyObject.gameObject;
//            HealthComponent health = go.GetComponent<HealthComponent>();

//            if (dist < DropOffDamagePoint)
//            {
//                if (health)
//                {
//                    health.ApplyDamage(m_ExplosionDamage);
//                }
//            }
//            else
//            {
//                float damage = m_ExplosionDamage / (dist * (m_ExplosiveRadius * m_ExplosionDamageDropOff));

//                if (health)
//                {
//                    health.ApplyDamage(damage);
//                }
//            }
//        }
//    }

//    //Reset all updated variables to initial states 
//    public override void OnObjectSpawn()
//    {
//        base.OnObjectSpawn();
//        //Wake RigidBody
//        if (!m_ProjectileIsKinematic)
//        {
//            Spawnable_RigidBody.detectCollisions = true;
//        }
//        //Enable Colliders
//        foreach (Collider collider in Spawnable_Colliders)
//        {
//            collider.enabled = true;
//            collider.isTrigger = true;
//        }
//        //Reset Emitter capability to default
//        if (ProjectileData.ProjectileSpawnsParticles)
//        {
//            Spawnable_CanSpawnParticles = true;
//        }
//        //Clear body forces
//        Spawnable_RigidBody.velocity = Vector3.zero;
//        //Reset Life
//        Spawnable_LifeTime = ProjectileData.LifeTime;
//        //Reset Initial Speed
//        m_ProjectileCurrentSpeed = ProjectileData.InitialSpeed;

//        //Check for Reset on Homeing Rotation
//        if (m_IsHomingProjectile)
//        {
//            //transform.rotation = Quaternion.LookRotation(Spawnable_Direction, Vector3.up);
//        }

//        if(m_ProjectileHasTrail)
//        {
//            Spawnable_Trail.enabled = true;
//        }

//        //Position and play any Particle emitter 
//        PlayParticleEmittersOfType(ParticleEmitterClass.EC_ONAWAKE, transform.position);

//        if (m_ProjectileForceMode == ForceMode.Impulse)
//        {
//            Spawnable_RigidBody.AddForce(Spawnable_Direction * m_ProjectileInitialSpeed, m_ProjectileForceMode);
//        }

//    }

//    //Disable the Projectile and clear all forces
//    private void ReturnToPool()
//    {
//        //Play any OnDeath Particle Emitters
//        #region Activate OnDeath Emitters
//        if (Spawnable_CanSpawnParticles)
//        {
//            Spawnable_CanSpawnParticles = false;
//        }
//        #endregion

//        //Reset Rigid Body forces and set collision detection to false
//        #region Reset Rigid Body
//        if (!m_ProjectileIsKinematic)
//        {
//            Spawnable_RigidBody.velocity = Vector3.zero;
//            Spawnable_RigidBody.detectCollisions = false;
//        }
//        #endregion

//        foreach (Collider collider in Spawnable_Colliders)
//        {
//            collider.enabled = false;
//        }

//        //Reset the Direcetion Variable
//        Spawnable_Direction = Vector3.zero;
//        transform.rotation = Quaternion.identity;

//        if(m_ProjectileHasTrail)
//        {
//            Spawnable_Trail.enabled = false;
//        }

//        //Disable the Projectile
//        gameObject.SetActive(false);
//    }

//    //Play Particle Emmitter of specified Emitter class
//    private void PlayParticleEmittersOfType(ParticleEmitterClass type, Vector3 postion)
//    {
//        if (type == ParticleEmitterClass.EC_ONAWAKE)
//        {
//            if (m_ProjectilePartilceEmmiters.ContainsKey(ParticleEmitterClass.EC_ONAWAKE))
//            {
//                foreach (GameObject emitter in m_ProjectilePartilceEmmiters[ParticleEmitterClass.EC_ONAWAKE])
//                {
//                    Vector3 emitterDirection = emitter.GetComponent<ParticleScript>().Direction;
//                    Vector3 FinalDirection = new Vector3(Spawnable_Direction.x * emitterDirection.x, Spawnable_Direction.y * emitterDirection.y, Spawnable_Direction.z * emitterDirection.z);
//                    GameObject deathemitter = Instantiate(emitter, postion, Quaternion.LookRotation(FinalDirection));
//                    ParticleSystem Sparksystem = deathemitter.GetComponent<ParticleSystem>();
//                    Sparksystem.Play();
//                    Destroy(deathemitter, Sparksystem.main.duration);
//                }
//            }
//        }
//        else if (type == ParticleEmitterClass.EC_FOLLOW)
//        {
//            if (m_ProjectilePartilceEmmiters.ContainsKey(ParticleEmitterClass.EC_FOLLOW))
//            {
//                foreach (GameObject emitter in m_ProjectilePartilceEmmiters[ParticleEmitterClass.EC_FOLLOW])
//                {
//                    Vector3 emitterDirection = emitter.GetComponent<ParticleScript>().Direction;
//                    Vector3 FinalDirection = new Vector3(Spawnable_Direction.x * emitterDirection.x, Spawnable_Direction.y * emitterDirection.y, Spawnable_Direction.z * emitterDirection.z);
//                    GameObject deathemitter = Instantiate(emitter, postion, Quaternion.LookRotation(FinalDirection));
//                    ParticleSystem Sparksystem = deathemitter.GetComponent<ParticleSystem>();
//                    Sparksystem.Play();
//                    Destroy(deathemitter, Sparksystem.main.duration);
//                }
//            }
//        }
//        else if (type == ParticleEmitterClass.EC_ONDEATH)
//        {
//            if (m_ProjectilePartilceEmmiters.ContainsKey(ParticleEmitterClass.EC_ONDEATH))
//            {
//                foreach (GameObject emitter in m_ProjectilePartilceEmmiters[ParticleEmitterClass.EC_ONDEATH])
//                {
//                    Vector3 emitterDirection = emitter.GetComponent<ParticleScript>().Direction;
//                    Vector3 FinalDirection = new Vector3(Spawnable_Direction.x * emitterDirection.x, Spawnable_Direction.y * emitterDirection.y, Spawnable_Direction.z * emitterDirection.z);
//                    GameObject deathemitter = Instantiate(emitter, postion, Quaternion.LookRotation(FinalDirection));
//                    ParticleSystem Sparksystem = deathemitter.GetComponent<ParticleSystem>();
//                    Sparksystem.Play();
//                    Destroy(deathemitter, Sparksystem.main.duration);
//                }
//            }
//        }

//    }

//    //Load the Data from the Scriptable Object into the local variables
//    private void LoadProjectile()
//    {

//        //Load Projectile Specific Variables
//        #region Projectile

//        m_ProjectileInitialSpeed = ProjectileData.InitialSpeed;
//        m_ProjectileInitialForceMultiplier = ProjectileData.InitialForceMultiplier;
//        m_ProjectileMaxSpeed = ProjectileData.MaxSpeed;
//        m_ProjectileHasAcceleration = ProjectileData.ProjectileHasAcceleration;
//        m_ProjectileAcceleration = ProjectileData.ProjectileAcceleration;
//        m_ProjectileBaseDamageMax = ProjectileData.ProjectileBaseDamageMax;
//        m_ProjectileBaseDamageMin = ProjectileData.ProjectileBaseDamageMin;
//        m_ProjectileColliderType = ProjectileData.ProjectileColliderType;
//        m_ProjectileColliderIsTrigger = ProjectileData.ProjectileColliderIsTrigger;
//        m_ProjectileHasTrail = ProjectileData.ProjectileHasTrail;
//        #endregion


//        //Load Homing Projectile Variables
//        #region Homing
//        m_IsHomingProjectile = ProjectileData.IsHomingProjectile;
//        m_HomingAcceleration = ProjectileData.HomingAcceleration;
//        m_ProjectileRotFollowsVelocity = ProjectileData.RotationFollowsVelocity;
//        #endregion


//        //Load Explosive Projectile Variables
//        #region Explosive
//        m_ProjectileIsExplosive = ProjectileData.ProjectileIsExplosive;
//        m_ProjectileSpawnsParticles = ProjectileData.ProjectileSpawnsParticles;
//        if (ProjectileData.ProjectileSpawnsParticles)
//        {
//            Spawnable_CanSpawnParticles = true;

//            foreach (ProjectileEmitter particleemitter in ProjectileData.ProjectilePartilceEmitters)
//            {
//                m_ProjectilePartilceEmmiters.Add(particleemitter.Type, particleemitter.ParticleEmitterObject);
//                foreach (GameObject go in particleemitter.ParticleEmitterObject)
//                {
//                    go.GetComponent<ParticleScript>().Direction = m_EmitterDirections[(int)particleemitter.Direction];
//                }
//            }
//        }
//        m_ExplosiveRadius = ProjectileData.ExplosionRadius;
//        m_ExplosionForce = ProjectileData.ExplosionForce;
//        m_ExplosionDamage = ProjectileData.ExplosionDamage;
//        m_ExplosionDamageDropOff = ProjectileData.ExplosionDamageDropOff;
//        #endregion


//        //Load Physics Projectiles
//        #region Physics
//        m_ProjectileUsesPhysics = ProjectileData.ProjectileUsesPhysics;
//        m_ProjectileMass = ProjectileData.ProjectileMass;
//        m_ProjectileIsKinematic = ProjectileData.ProjectileIsKinematic;
//        m_ProjectileForceMode = ProjectileData.ProjectileForceMode;
//        m_ProjectileCollisionMode = ProjectileData.ProjectileCollisionMode;
//        m_ProjectileUsesGravity = ProjectileData.ProjectileUsesGravity;
//        m_ProjectileDrag = ProjectileData.ProjectileDrag;
//        m_ProjectileAngularDrag = ProjectileData.ProjectileAngularDrag;
//        #endregion


//        //Load Projectile Local Variables (if required)
//        #region Public Variables
//        Spawnable_LifeTime = ProjectileData.LifeTime;
//        Spawnable_Mesh = ProjectileData.ProjectileMesh;
//        Spawnable_Material = ProjectileData.ProjectileMaterial;
//        #endregion


//    }

//    //Build the neccessary components using the loaded data
//    private void BuildProjectile()
//    {
//        //Add Mesh Loaded from Scriptable Object
//        #region Build Mesh Component
//        MeshFilter ProjectileMesh = this.gameObject.AddComponent<MeshFilter>();
//        ProjectileMesh.mesh = Spawnable_Mesh;
//        #endregion

//        //Scale Mesh if Required
//        #region Projectile Scale
//        gameObject.transform.localScale = ProjectileData.ProjectileScale;
//        #endregion

//        //Add Mesh Renderer with Material from Scriptable Object
//        #region Build Mesh Renderer Component
//        MeshRenderer ProjectileRenderer = this.gameObject.AddComponent<MeshRenderer>();
//        ProjectileRenderer.material = Spawnable_Material;
//        ProjectileRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
//        ProjectileRenderer.allowOcclusionWhenDynamic = true;
//        ProjectileRenderer.renderingLayerMask = 1;
//        #endregion

//        //Check for Collider Selection from Scriptable Object
//        #region Check for Collider Selection
//        if (m_ProjectileColliderType != SpawnableColliderType.CT_NONE)
//        {
//            //Add Box Collider using Mesh Volume
//            #region Add Box Collider
//            if (m_ProjectileColliderType == SpawnableColliderType.CT_BOX)
//            {
//                BoxCollider collider = this.gameObject.AddComponent<BoxCollider>();

//                collider.size = Spawnable_Mesh.bounds.size;
//                collider.center = Spawnable_Mesh.bounds.center;

//                Spawnable_Colliders.Add(collider);

//            }
//            #endregion
//            //Add Capsule Collider using widest between width and depth, and height of Mesh volume
//            #region Add Capsule Collider
//            else if (m_ProjectileColliderType == SpawnableColliderType.CT_CAPSULE)
//            {
//                CapsuleCollider collider = this.gameObject.AddComponent<CapsuleCollider>();

//                Vector3 meshSize = Spawnable_Mesh.bounds.size;

//                if (meshSize.x > meshSize.z)
//                {
//                    collider.radius = meshSize.x;
//                }
//                else
//                {
//                    collider.radius = meshSize.z;
//                }

//                collider.height = meshSize.y;

//                collider.center = Spawnable_Mesh.bounds.center;

//                Spawnable_Colliders.Add(collider);

//            }
//            #endregion
//            //Add Sphere collider using longest axis from Mesh volume as radius
//            #region Add Sphere Collider
//            else if (m_ProjectileColliderType == SpawnableColliderType.CT_SPHERE)
//            {
//                SphereCollider collider = this.gameObject.AddComponent<SphereCollider>();

//                Vector3 meshSize = Spawnable_Mesh.bounds.size;

//                float MaxAxis = 0;

//                for (int i = 0; i < 3; i++)
//                {
//                    if (meshSize[i] > MaxAxis)
//                    {
//                        MaxAxis = meshSize[i];
//                    }
//                }

//                collider.radius = MaxAxis;

//                Spawnable_Colliders.Add(collider);

//            }
//            #endregion
//            //Add Mesh Collider with default cooking options
//            #region Add Mesh Collider
//            else if (m_ProjectileColliderType == SpawnableColliderType.CT_MESH)
//            {
//                MeshCollider collider = this.gameObject.AddComponent<MeshCollider>();

//                collider.cookingOptions = MeshColliderCookingOptions.CookForFasterSimulation & MeshColliderCookingOptions.EnableMeshCleaning & MeshColliderCookingOptions.WeldColocatedVertices;

//                Spawnable_Colliders.Add(collider);
//            }
//            #endregion

//            //Set Collider as trigger if ColliderIsTrigger is true
//            #region Set Trigger Flag
//            if (m_ProjectileColliderType != SpawnableColliderType.CT_MESH)
//            {
//                if (m_ProjectileColliderIsTrigger)
//                {
//                    foreach (Collider collider in Spawnable_Colliders)
//                    {
//                        collider.isTrigger = true;
//                    }
//                }
//            }
//            #endregion
//        }
//        #endregion

//        //Set Up Rigidbody with Physics Selection from Scriptable Object
//        #region Build Rigid Body Component
//        if (m_ProjectileUsesPhysics)
//        {
//            Spawnable_RigidBody = this.gameObject.AddComponent<Rigidbody>();
//            Spawnable_RigidBody.mass = m_ProjectileMass;
//            Spawnable_RigidBody.isKinematic = m_ProjectileIsKinematic;
//            Spawnable_RigidBody.collisionDetectionMode = m_ProjectileCollisionMode;
//            Spawnable_RigidBody.useGravity = m_ProjectileUsesGravity;
//            Spawnable_RigidBody.drag = m_ProjectileDrag;
//            Spawnable_RigidBody.angularDrag = m_ProjectileAngularDrag;
//        }
//        #endregion

//        //Set Up Trial Renderer Component
//        #region Build Trail Renderer Component
//        if (m_ProjectileHasTrail)
//        {
//            Spawnable_Trail = this.gameObject.AddComponent<TrailRenderer>();
//            Spawnable_Trail.startWidth = 0.15f;
//            Spawnable_Trail.endWidth = 0f;
//            Spawnable_Trail.numCapVertices = 6;
//            Spawnable_Trail.material = Spawnable_Material;
//            Spawnable_Trail.time = 0.5f;
//            Spawnable_Trail.generateLightingData = true;
//            Spawnable_Trail.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
//            Spawnable_Trail.enabled = false;
//        }
//        #endregion
//    }

//    //Apply a force to the Rigid Body every fixed fram to attain a specified velocity
//    private void ApplyForceToReachVelocity(Rigidbody rigidbody, Vector3 velocity, float force = 1.0f, ForceMode mode = ForceMode.Force)
//    {
//        if (force == 0 || velocity.magnitude == 0)
//        {
//            return;
//        }

//        velocity = velocity + velocity.normalized * 0.2f * rigidbody.drag;

//        force = Mathf.Clamp(force, -rigidbody.mass / Time.fixedDeltaTime, rigidbody.mass / Time.fixedDeltaTime);

//        if (rigidbody.velocity.magnitude == 0)
//        {
//            rigidbody.AddForce(velocity * force, mode);
//        }

//        else
//        {
//            Vector3 VelocityProjectedToTarget = (velocity.normalized * Vector3.Dot(velocity, rigidbody.velocity) / velocity.magnitude);
//            rigidbody.AddForce((velocity - VelocityProjectedToTarget) * force, mode);
//        }

//    }
//}

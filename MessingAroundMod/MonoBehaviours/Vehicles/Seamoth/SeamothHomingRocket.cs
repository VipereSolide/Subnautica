using UnityEngine;
using UnityEngine.PlayerLoop;
using VipereSolide.Subnautica.MessingAroundMod.Utility.Math;
using VipereSolide.Subnautica.MessingAroundMod.Utility.Vehicles;
using static WorldForces;

namespace VipereSolide.Subnautica.MessingAroundMod.MonoBehaviours.Vehicles.Seamoth
{
    public class SeamothHomingRocket : MonoBehaviour
    {
        public Creature target;
        public Vehicle owner;
        public float speed;
        public float radius;
        public float damage;

        protected SeamothTorpedo torpedoPrefab;
        protected GameObject trailParticles;

        protected void Awake()
        {

        }

        protected void Start()
        {
            if (owner.GetType() == typeof(SeaMoth))
            {
                SeaMoth seamoth = (SeaMoth)owner;
                TorpedoType type = VehicleUtility.GetTorpedo(seamoth, TechType.WhirlpoolTorpedo);
                torpedoPrefab = type.prefab.GetComponent<SeamothTorpedo>();
            }

            if (torpedoPrefab != null)
                trailParticles = Utils.SpawnZeroedAt(torpedoPrefab.trailPrefab, transform, false);

            foreach (Collider childCollider in transform.GetComponentsInChildren<Collider>())
                Destroy(childCollider);
        }

        public SeamothHomingRocket Init(Vehicle owner)
        {
            this.owner = owner;
            this.target = GetTarget();

            return this;
        }

        public SeamothHomingRocket Init(Vehicle owner, Creature target)
        {
            this.owner = owner;
            this.target = target;

            return this;
        }

        public SeamothHomingRocket WithSpeed(float speed)
        {
            this.speed = speed;
            return this;
        }

        public SeamothHomingRocket WithRadius(float radius)
        {
            this.radius = radius;
            return this;
        }

        public SeamothHomingRocket WithDamage(float damage)
        {
            this.damage = damage;
            return this;
        }

        protected virtual Creature GetTarget() { return null; }

        protected void Update()
        {
            if (target == null) return;

            transform.forward = transform.position.DirectionTowards(target.transform.position);
            transform.position += transform.forward * speed * Time.deltaTime;

            if (transform.position.Distance(target.transform.position) <= radius)
            {
                Explode(true);
            }
        }

        protected void OnCollisionEnter(Collision collision)
        {
            Explode(collision.transform == target.transform);
        }

        protected virtual void Explode(bool hasHitTarget)
        {
            if (hasHitTarget) target.liveMixin.TakeDamage(damage, transform.position, DamageType.Explosive, owner.transform.gameObject);
            Debug.Log(target.liveMixin.health);

            CreateExplosion();

            if (this.trailParticles != null)
            {
                this.trailParticles.transform.parent = null;
                this.trailParticles.GetComponent<ParticleSystem>().Stop();
            }

            Destroy(gameObject);
        }

        protected virtual void CreateExplosion()
        {
            if (torpedoPrefab == null) return;

            Transform explosion = Instantiate(torpedoPrefab.explosionPrefab).transform;
            explosion.position = transform.position;
            explosion.rotation = transform.rotation;

            Collider mainCollider = explosion.GetComponent<Collider>();
            if (mainCollider != null) Destroy(mainCollider);

            foreach (Collider childCollider in explosion.GetComponentsInChildren<Collider>())
                Destroy(childCollider);

            Destroy(explosion.gameObject, 0.6F);
        }
    }
}

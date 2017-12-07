using Assets.Scripts.AI.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Weapons
{
    [Serializable]
    public class Gun: Weapon
    {
        public int Ammo;
        public int AmmoLoaded;
        public int AmmoLoadCount;
        public float FireRange;
        public float ShootInterval;
        public float BulletSpeed;
        public float ImpactForce;
        public GameObject HitEffect;

        float LastShootTime = 0;

        protected float shootOffset;

        public override bool Act(Vector3 direction)
        {
            return Shoot(direction);
        }
        
        public virtual bool Shoot()
        {
            return Shoot(GetComponent<Player>().Looking);
        }
        
        public virtual bool Shoot(Vector3 Direction)
        {
            return Shoot(new Ray(transform.position, Direction));
        }
        
        public virtual bool Shoot(Ray shootRay)
        {
            if (Time.time - LastShootTime < ShootInterval)
                return false;
            if (AmmoLoaded <= 0)
                return false;
            AmmoLoaded -= 1;
            LastShootTime = Time.time;
            RenderShoot(shootRay);
            return true;
        }
        
        [Command]
        public virtual void CmdShoot(Ray shootRay)
        {
            AmmoLoaded -= 1;
            LastShootTime = Time.time;
            RaycastHit hit;
            if (Physics.Raycast(shootRay, out hit, FireRange))
            {
                if (hit.transform.gameObject.tag == "Player")
                {
                    var player = hit.transform.gameObject.GetComponent<Player>();
                    //player.OnShotCallback(gameObject.GetComponent<Player>(), shootRay.direction, Damage, ImpactForce);
                    var message = new AttackMessage(new AttackMessage.AttackData(GetComponent<Player>(), player, Damage, shootRay.direction, ImpactForce));
                    message.Dispatch();
                }
            }
        }

        public virtual void RenderShoot(Ray shootRay)
        {
            RaycastHit hit;
            if (Physics.Raycast(shootRay, out hit, FireRange))
            {
                if (HitEffect)
                {
                    var hitAsh = Instantiate(HitEffect);
                    hitAsh.transform.rotation = Quaternion.LookRotation(hit.normal);
                    hitAsh.transform.position = hit.point;
                }
            }
        }

        public virtual bool Reload()
        {
            if (Ammo <= 0)
                return false;
            if(Ammo<AmmoLoadCount)
            {
                AmmoLoaded = Ammo;
                Ammo = 0;
            }
            else
            {
                Ammo -= AmmoLoadCount;
                AmmoLoaded += AmmoLoadCount;
            }
            return true;

        }

        public override bool Act()
        {
            return Shoot();
        }
    }
}

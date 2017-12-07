using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Weapons
{
    public class ARDftGun: Gun
    {
        public override bool Shoot(Vector3 Direction)
        {
            return base.Shoot(Direction);
        }

        public override bool Act()
        {
            return Shoot();
        }

        public override bool Shoot()
        {
            return base.Shoot();

        }
        public override void RpcShoot(Ray shootRay)
        {
            if (isLocalPlayer)
                return;

            base.RpcShoot(shootRay);


            var gunLeft = transform.Find("Wrap/Hands/Gun-L/Gun-Inside/Gun-Barrel").gameObject;
            var gunRight = transform.Find("Wrap/Hands/Gun-R/Gun-Inside/Gun-Barrel").gameObject;
            var rayL = new Ray(gunLeft.transform.position, -gunLeft.transform.right);
            var rayR = new Ray(gunRight.transform.position, -gunRight.transform.right);
            Debug.DrawLine(rayL.origin, rayL.origin + rayL.direction * 100, Color.red);
            Debug.DrawLine(rayR.origin, rayR.origin + rayR.direction * 100, Color.red);
            var bulletL = Instantiate(Resources.Load("Bullet") as GameObject);
            var bulletR = Instantiate(Resources.Load("Bullet") as GameObject);
            bulletL.transform.position = gunLeft.transform.position + rayL.direction * 4;
            bulletR.transform.position = gunRight.transform.position + rayR.direction * 4;
            bulletL.transform.rotation = Quaternion.LookRotation(rayL.direction);
            bulletR.transform.rotation = Quaternion.LookRotation(rayR.direction);
        }
        [Command]
        public override void CmdShoot(Ray shootRay)
        {
            base.CmdShoot(shootRay);
        }

        public ARDftGun() : base()
        {
            this.Ammo = int.MaxValue;
            this.AmmoLoadCount = int.MaxValue;
            this.AmmoLoaded = int.MaxValue;
            this.Damage = 20;
            this.FireRange = 500;
            this.ShootInterval = 0.1f;
            this.BulletSpeed = 2000;
            this.ImpactForce = 20;
        }
    }
}

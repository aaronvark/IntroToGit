using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Guns
{
        public interface IGun
        {
        float damage
        {
            get;
        }
        float fireRate
        {
            get;
        }
        float range
        {
            get;
        }
        float impactForce
        {
            get;
        }
        int clips
        {
            get;
        }
        int maxClips
        {
            get;
        }
        int clipSize
        {
            get;
        }
        int ammo
        {
            get;
        }
        int ammoInUse
        {
            get;
        }
        void Shoot();
        void Reload();


        }


}

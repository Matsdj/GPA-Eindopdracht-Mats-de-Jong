using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA_Eindopdracht_Mats_de_Jong
{
    class Attack : RotatingSpriteGameObject
    {
        protected float cooldownMax; //in seconds
        protected float damage;
        protected float cooldown;
        protected GameObject wielder;
        protected GameObject target;
        protected List<GameObject> damagedObjects = new List<GameObject>();
        public Attack(string assetName, int scale, GameObject wielder, GameObject target, float damage, float cooldownMax = 2) : base(assetName, scale)
        {
            this.wielder = wielder;
            this.damage = damage;
            this.target = target;
            this.cooldownMax = cooldownMax;
            origin = Center;
            Reset();
        }
        public override void Reset()
        {
            if (cooldown <= 0)
            {
                AngularDirection = target.GlobalPosition - wielder.GlobalPosition;
                damagedObjects.Clear();
                cooldown = cooldownMax;
                base.Reset();
            }
        }
        public float Cooldown
        {
            get { return cooldown; }
        }
    }
}

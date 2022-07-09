using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.Events.EventArgs;
using MEC;
using UnityEngine;
using Scp682Handler = Exiled.Events.Handlers.Player;

namespace Scp682_EXILED
{
    [CustomRole(RoleType.Scp93953)]
    public class Scp682Role : CustomRole
    {
        public override string Description { get; set; } = MainClass.singleton.Config.Description;
        public override uint Id { get; set; } = MainClass.singleton.Config.Id;
        public override RoleType Role { get; set; } = RoleType.Scp93953;
        public override int MaxHealth { get; set; } = MainClass.singleton.Config.MaxHealth;
        public override string Name { get; set; } = "SCP-682";
        public override string CustomInfo { get; set; } = MainClass.singleton.Config.CustomInfo;

        public List<CoroutineHandle> coroutines = new List<CoroutineHandle>();


        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties()
        {
            Limit = 1,
            RoleSpawnPoints = new List<RoleSpawnPoint>()
            {
                new RoleSpawnPoint()
                {
                    Chance = 100,
                    Role = RoleType.Scp93953
                }
            }
        };

        protected override void RoleAdded(Player player)
        {
            player.Health = MaxHealth;
            coroutines.Add(Timing.RunCoroutine(Regen(player)));
            Timing.CallDelayed(5f, () =>
            {
                player.Scale = new Vector3(1.22f, 1f, 1.22f);
            });
            base.RoleAdded(player);
        }

        protected override void RoleRemoved(Player player)
        {
            foreach (CoroutineHandle coroutine in coroutines)
            {
                Timing.KillCoroutines(coroutine);
            }
            coroutines.Clear();
            Cassie.Message(MainClass.singleton.Config.CassieDeathMessage);
            base.RoleRemoved(player);
        }

        protected override void SubscribeEvents()
        {
            Scp682Handler.Hurting += OnHurting;
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Scp682Handler.Hurting -= OnHurting;
            base.UnsubscribeEvents();
        }

        public void OnHurting(HurtingEventArgs ev)
        {
            if (Check(ev.Attacker))
                ev.Amount = MainClass.singleton.Config.DamageAmount;
        }

        private IEnumerator<float> Regen(Player player)
        {
            for (;;)
            {
                player.Heal(30);
                yield return Timing.WaitForSeconds(5f);
            }
        }
    }
}
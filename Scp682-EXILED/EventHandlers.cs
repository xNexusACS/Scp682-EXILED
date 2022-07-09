using Exiled.CustomRoles.API.Features;
using UnityEngine;
using Exiled.Events.EventArgs;
using MEC;

namespace Scp682_EXILED
{
    public class EventHandlers
    {
        private readonly MainClass plugin;
        public EventHandlers(MainClass plugin)
        {
            this.plugin = plugin;
        }

        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.NewRole == RoleType.Scp93953)
            {
                int chance = Random.Range(1, 100);
                if (chance <= plugin.Config.SpawnChance)
                {
                    Timing.CallDelayed(0.1f, () =>
                    {
                        CustomRole.Get(typeof(Scp682Role)).AddRole(ev.Player);
                    });
                }
            }
        }
    }
}
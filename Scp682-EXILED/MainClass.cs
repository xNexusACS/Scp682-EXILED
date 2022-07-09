using System;
using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using Scp682Handler = Exiled.Events.Handlers.Player;

namespace Scp682_EXILED
{
    public class MainClass : Plugin<Config>
    {
        public static MainClass singleton;
        public override string Author { get; } = "xNexusACS";
        public override string Name { get; } = "SCP682_EXILED";
        public override string Prefix { get; } = "scp682";
        public override Version Version { get; } = new Version(0, 2, 0);
        public override Version RequiredExiledVersion { get; } = new Version(5, 2, 2);

        public EventHandlers EventHandlers { get; private set; }
        
        public override void OnEnabled()
        {
            singleton = this;
            EventHandlers = new EventHandlers(this);
            CustomRole.RegisterRoles();
            
            Scp682Handler.ChangingRole += EventHandlers.OnChangingRole;
            
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            CustomRole.UnregisterRoles();
            Scp682Handler.ChangingRole -= EventHandlers.OnChangingRole;

            singleton = null;
            EventHandlers = null;
            base.OnDisabled();
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace TacticsToolkit
{
    //using extension methods and a marker interface for generic event types
    public interface ICommandProvider<CommandType> { }

    // Extension method to get the correct type of excrement
    public static class ICommandProviderExtension
    {
        public static CommandType StronglyTypedCommandParam<CommandType>(
            this ICommandProvider<CommandType> iCommandProvider)
            where CommandType : CommandParam
        {
            EventCommand command = iCommandProvider as EventCommand;
            if (null == command)
            {
                Debug.Log("iPooProvider must be a BaseAnimal.");
            }
            return (CommandType)command.CommandParams;
        }
    }

    public class EventCommand
    {
        public CommandParam commandParam;

        public virtual CommandParam CommandParams
        {
            set { commandParam = value; }
            get { return new CommandParam(); }
        }
    }

    public class CastAbilityCommand : EventCommand, ICommandProvider<CastAbilityParams>
    {
        public override CommandParam CommandParams
        {
            set { commandParam = value; }
            get { return commandParam; }
        }
    }


    public class CommandParam { }
    public class CastAbilityParams : CommandParam
    {
        public List<OverlayTile> affectedTiles;
        public AbilityContainer abilityContainer;

        public CastAbilityParams(List<OverlayTile> affectedTiles, AbilityContainer abilityContainer)
        {
            this.affectedTiles = affectedTiles;
            this.abilityContainer = abilityContainer;
        }
    }
}
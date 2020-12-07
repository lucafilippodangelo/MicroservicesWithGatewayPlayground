namespace Ordering.Core.Entities.Base
{
    /// <summary>
    /// LD this is abstract because we do not want to initialize this on it's own
    /// we do put another ganeric layer "EntityBase<int>" and we pass the type for the key -> "int"
    /// </summary>
    public abstract class Entity : EntityBase<int>
    {
    }
}

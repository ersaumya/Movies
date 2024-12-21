namespace Movies.Core.Entites.Base
{
    public interface IEntityBase<TId>
    {
        TId Id { get; }
    }
}
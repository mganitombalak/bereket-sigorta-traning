namespace Bereket.Domain.Interfaces{

    public interface IBaseEntity<TId>
    {
        TId Id { get; set; }
    }
}
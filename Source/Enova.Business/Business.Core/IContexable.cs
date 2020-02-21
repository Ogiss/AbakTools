namespace Enova.Business.Old.Core
{
    public interface IContexable
    {
        System.Data.Objects.ObjectContext DbContext { get; }
    }
}

namespace Alfatraining.Vertrag.Db.Repository
{
    internal interface IEntity
    {
        public byte[] RowVersion { get; set; }
    }
}
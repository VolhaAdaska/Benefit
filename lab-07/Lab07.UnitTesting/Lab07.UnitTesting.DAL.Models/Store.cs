using System.Diagnostics.CodeAnalysis;

namespace Lab07.UnitTesting.DAL.Models
{
    [ExcludeFromCodeCoverage]
    public class Store : BaseEntity
    {
        public string Name { get; set; }

        public string Promocode { get; set; }

        public int StoreTypeId { get; set; }

        public virtual StoreType StoreType { get; set; }
    }
}
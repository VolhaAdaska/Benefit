using System.Diagnostics.CodeAnalysis;

namespace Lab07.UnitTesting.DAL.Models
{
    [ExcludeFromCodeCoverage]
    public class StoreType : BaseEntity
    {
        public string Name { get; set; }
        public int NameInt { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Lab07.UnitTesting.DAL.Models
{
    [ExcludeFromCodeCoverage]
    public class BaseEntity
    {
        public int Id { get; set; }
    }
}
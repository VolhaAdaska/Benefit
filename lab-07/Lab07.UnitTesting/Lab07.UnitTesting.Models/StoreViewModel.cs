using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lab07.UnitTesting.Models
{
    public class StoreViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Promocode { get; set; }

        public IList<SelectListItem> StoreTypeList { get; set; }

        public int StoreTypeId { get; set; }
    }
}
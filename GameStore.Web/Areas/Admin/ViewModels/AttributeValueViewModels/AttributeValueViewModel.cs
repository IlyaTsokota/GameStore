using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.Web.Areas.Admin.ViewModels.AttributeValueViewModels
{
    public class AttributeValueViewModel
    {
        public int AttributeValueId { get; set; }

        public int AttributeId { get; set; }

        [Display(Name ="Атрибут")]
        public string AttributeName { get; set; }

        public int ProductId { get; set; }

        [MaxLength(40, ErrorMessage = "Вы не можете ввести больше чем 40 символов")]
        [Display(Name = "Значение")]
        public string Value { get; set; }
    }
}
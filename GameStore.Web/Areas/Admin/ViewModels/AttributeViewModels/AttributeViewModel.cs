using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.Web.Areas.Admin.ViewModels.AttributeViewModels
{
    public class AttributeViewModel
    {
        public int CategoryId { get; set; }
        public int AttributeId { get; set; }
        [Required(ErrorMessage ="Поле не должно быть пустым")]
        [MaxLength(30, ErrorMessage="Значение {0} не может превышать {1} символов")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Разрешить фильтрацию")]
        public bool AllowFiltering { get; set; }
    }
}
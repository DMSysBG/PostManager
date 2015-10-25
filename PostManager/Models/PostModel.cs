using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PostManager.Models
{
    public class PostModel
    {
        public int PostId { get; set; }

        public int SiteId { get; set; }

        public int CategoryId { get; set; }

        [Display(Name = "Link")]
        public string PostLink { get; set; }

        [Display(Name = "Image")]
        public string PostImage { get; set; }

        [Display(Name = "Заглавие")]
        public string PostTitle { get; set; }

        [Display(Name = "Текст")]
        public string PostText { get; set; }

        [Display(Name = "Цена")]
        public decimal PostPrice { get; set; }
          
        public int TemplateLocationId { get; set; }

        [Display(Name = "Дата")]
        public DateTime PostDate { get; set; }

        public int SitePostedId { get; set; }

        [Display(Name = "Тип цена")]
        public int PostPriceTypeId { get; set; }
    }
}
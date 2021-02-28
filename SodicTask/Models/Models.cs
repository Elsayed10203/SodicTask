using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SodicTask.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CatagNo { get; set; }
        public int ParentNo { get; set; }
        public short LevelNo { get; set; }

        [Required(ErrorMessage = "EnterCatagName")]
        public string CatagName { get; set; }
        public string Size { get; set; }
        public string Colors { get; set; }
         public string Width { get; set; }
         public short? IsAvailable { get; set; }

        public virtual List<Product> Products { get; set; }
    }

    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProdId { get; set; }
        [Required(ErrorMessage = "EnterProductName")]
        public string ProdName { get; set; }

        public int Quantity { get; set; }
        public string ProdDescription { get; set; }
        [ForeignKey("Category")]
        public int CatagNo { get; set; }
        public int SubCatag { get; set; }

        public string ProdMainImg { get; set; }
        public Category category { get; set; }
        public virtual List<ProductImage> Images { get; set; }

    }
    public class ProductImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImageId { get; set; }

        [ForeignKey("Product")]
        public int ProdId { get; set; }
        public string ProdImages { get; set; }
     }
}
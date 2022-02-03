using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Book.Models
{
    public class Category
    {   
        [Key]
        public int categoryid { get; set; }
        [Required]
        [DisplayName("Category Name")]
        public string categoryname { get; set; }
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }

        public DateTime dateTime { get; set; } = DateTime.Now;

    }
}

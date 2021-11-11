using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Entities
{
    /// <summary>
    /// Summary description for Category
    /// </summary>
    public class Category
    {
        public Category()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int Id { get; set; }

        /// <summary>
        /// عنوان دسته بندی
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// آیدی سر دسته
        /// </summary>
        public int? ParentId { get; set; }
        public string ParentTitle { get; set; }
        public Category Parent { get; set; }


        /// <summary>
        /// آیا فعال است؟
        /// </summary>
        public bool IsEnabled { get; set; }


        public List<Category> Childs { get; set; }



        /// <summary>
        /// محصولات این دسته بندی
        /// </summary>
        public List<Product> Products { get; set; }

    }
}
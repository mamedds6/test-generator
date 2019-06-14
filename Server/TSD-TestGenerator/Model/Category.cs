using System;
using System.Collections.Generic;

namespace TSDTestGenerator.Model
{
    public partial class Category
    {
        public Category()
        {

        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Question> Question { get; set; }
    }
}

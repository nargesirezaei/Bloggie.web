namespace Bloggie.web.Models.Domain
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; } 
        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}

namespace Bloggie.web.Models.Domain
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string  ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; } 
        public string Author { get; set; }
        public bool Visible { get; set; }

        //tellig EF core that this class have multiple tags. dvs har relasjon med Tag tabellen
        public ICollection<Category>? Categories { get; set; }

        //navigation property for blogpost like & comment
        public ICollection<BlogPostLike> Likes { get; set; }


    }
}

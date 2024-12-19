using System;
using System.Collections.Generic;

namespace Articles.Domain.Models.Articles
{
    public class ArticleData : IInitialData
    {
        public List<object> GetThumbnails(IEnumerable<string> names)
        {
            var result = new List<object>();
            foreach (var name in names)
            {
                result.Add(new Thumbnail(name));
            }

            return result;
        }

        public List<object> GetColors(IEnumerable<string> names)
        {
            var result = new List<object>();
            foreach (var name in names)
            {
                result.Add(new Color(name));
            }

            return result;
        }

        public List<object> GetCategories(IEnumerable<string> names)
        {
            var result = new List<object>();
            foreach (var name in names)
            {
                result.Add(new Category(name));
            }

            return result;
        }

        public object GetArticle(
            Guid thumbnailId,
            Guid colorId,
            Guid categoryId,
            string title,
            string subtitle,
            string text)
        {
            return new Article(title, subtitle, text, categoryId, colorId, thumbnailId);
        }
    }

}

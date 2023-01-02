using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;
using VkToTelegram.Logger;

namespace VKToTelegramRepost
{
    internal class VKPostReceiver
    {
        private readonly VkApi api;
        private readonly int groupId;
        private readonly DateTime from;
        private static readonly Logger logger = new Logger();

        public VKPostReceiver(VkApi api, int groupId, DateTime from)
        {
            this.api = api;
            this.groupId = groupId;
            this.from = from;
        }

        public List<PostDto> ReadPosts()
        {
            var group = api.Wall.Get(new WallGetParams()
            {
                OwnerId = -groupId,
                Count = 100
            });

            var posts = group
                .WallPosts
                .Where(e => e.Date > from)
                .Select(e => new PostDto
                {
                    Images = GetPhotosUrls(e),
                    Text = e.Text,
                    Url = $"wall-{groupId}_{e.Id}"
                })
                .Reverse()
                .ToList();

            logger.Log(string.Join(",\n", posts));

            return posts;
        }

        private static List<Uri> GetPhotosUrls(Post post)
        {
            var images = post.Attachments.Where(e => e.Type == typeof(Photo));

            return images
                .Select(e => GetPhotoUrl(e.Instance))
                .ToList();
        }

        private static Uri GetPhotoUrl(MediaAttachment att)
        {
            var photo = att as Photo;

            if (photo == null)
            {
                logger.Log("Attachment is not a photo");
                return new Uri(string.Empty);
            }

            return photo
                .Sizes
                .OrderByDescending(e => e.Width)
                .Select(e => e.Url)
                .First();
        }
    }
}

namespace WidePictBoard.API.Controllers.Content
{
    public static class ContentExtenstions
    {
        public static ContentDto ToDto(this Content content)
        {
            if (content == null)
                return null;

            return new ContentDto
            {
                Id = content.Id,
                Name = content.Name,
                Price = content.Price,
                //UserId = content.OnwerUser.Id
            };
        }
    }
}
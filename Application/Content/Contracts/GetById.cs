namespace WidePictBoard.Application.Content.Contracts
{
    public static class GetById
    {
        public sealed class Request
        {
            public int Id { get; set; }
            
        }
        
        public sealed class Response
        {
            public sealed class OwnerResponse
            {
                public int Id { get; set; }
                public string Name { get; set; }
            }

            public string Name { get; set; }
            public string Status { get; set; }
            public decimal Price { get; set; }

            public OwnerResponse Owner { get; set; }
        }
    }
}
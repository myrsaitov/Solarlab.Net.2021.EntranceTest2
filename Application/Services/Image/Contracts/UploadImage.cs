namespace SL2021.Application.Services.Image.Contracts
{
    public static class UploadImage
    {
        public class Response
        {
            public string FileName { get; set; }
            public long FileSize { get; set; }
        }
    }
}
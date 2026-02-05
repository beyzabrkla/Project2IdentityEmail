namespace Project2IdentityEmail.DTOs
{
    public class SendMessageDTO
    {
        public int Id{ get; set; }
        public string ReceiverEmail { get; set; } // Alıcının email adresi
        public string Subject { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; } // Seçilen kategori ID'si
        public IFormFile Attachment { get; set; }
    }
}

namespace Project2IdentityEmail.Entities
{
    public class UserMessage
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }

        // --- Yeni Eklenen Alanlar ---
        public bool IsDraft { get; set; } // Taslaklar için
        public bool IsTrash { get; set; } // Çöp kutusu için
        public bool IsStarred { get; set; } // Yıldızlı mesajlar için
        public bool IsSpam { get; set; } = false; // Varsayılan olarak false
        public string? AttachmentUrl { get; set; }
        // Kategori/Etiket İlişkisi
        public int? CategoryId { get; set; } // Mesajın kategorisi (İş, Sosyal vb.)
        public Category Category { get; set; }
        // ----------------------------

        // İlişkiler (Identity ile bağlantı)
        public string SenderId { get; set; }
        public AppUser Sender { get; set; }

        public string ReceiverId { get; set; }
        public AppUser Receiver { get; set; }
    }
}
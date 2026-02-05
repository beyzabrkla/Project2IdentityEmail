# 🚀 MENDY ADMIN | IDENTITY & MESSAGING SYSTEM
Bu proje, ASP.NET Core 8.0 kullanılarak geliştirilmiş, gelişmiş kullanıcı yönetim sistemine (Identity) ve gerçek zamanlı e-posta doğrulama mekanizmasına sahip modern bir yönetim panelidir. 
Kullanıcı deneyimini ön planda tutan AJAX tabanlı doğrulamalar ve kurumsal seviyede bir mesajlaşma altyapısı sunar.


## 🛠️ KULLANILAN TEKNOLOJİLER
* 💻 **Backend:** #ASP.NET Core 8.0 (MVC)<br>
* 🔐 **Güvenlik:** #Microsoft Identity & #Two-Factor Authentication (2FA)<br>
* 📧 **E-Posta:** #MailKit & #MimeKit SMTP Integration<br>
* 💾 **Veritabanı:** #MSSQL Server & #Entity Framework Core<br>
* ⚡ **Frontend:** #AJAX, #jQuery, #Bootstrap 5<br>


## 🔐 KULLANICI KAYIT VE GÜVENLİK AKIŞI (REGISTER & 2FA)

### 📸 1. Kullanıcı Kayıt Ekranı (Register Page)<br>
* 🔍 **Modern Arayüz:** Mendy Admin teması ile özelleştirilmiş, kullanıcı dostu kayıt formu.<br>
* 🔍 **Profil Fotoğrafı Desteği:** `IFormFile` ile asenkron dosya yükleme ve `wwwroot/UserImages/` klasöründe saklama.<br>
* 🔍 **Hızlı Kayıt:** Kullanıcı bilgileri alındığı anda arka planda 6 haneli doğrulama kodu üretilir.<br>
<img width="1864" height="953" alt="1" src="https://github.com/user-attachments/assets/31e69efc-c4d3-4c09-8e93-674f106a30bc" />

### 📸 2. E-Posta Onay Kodu Gönderimi
* 🔍 **SMTP Entegrasyonu:** `MailKit` ve `MimeKit` kullanılarak Gmail üzerinden anlık mail iletimi.<br>
* 🔍 **Güvenli Kod:** Kullanıcıya özel üretilen (Örn: 969940) doğrulama kodu ile kimlik doğrulama.<br>
<img width="692" height="886" alt="2" src="https://github.com/user-attachments/assets/a4c8622d-71d5-4e55-9d6a-d825c0f65e4a" />

### 📸 3. AJAX Tabanlı E-Posta Doğrulama Modalı
* 🔍 **Kesintisiz Deneyim:** Sayfa yenilenmeden (`e.preventDefault();`) açılan doğrulama pop-up'ı.<br>
* 🔍 **Loading State:** İşlem sırasında "Lütfen Bekleyin" spinner ve buton animasyonu.<br>
* 🔍 **Anlık Kontrol:** Girilen kodun veritabanındaki kodla eşleşmesi sonucu otomatik yönlendirme.<br>
<img width="538" height="256" alt="7" src="https://github.com/user-attachments/assets/21cfa701-4173-4c9f-a68d-deade5d7671c" />


## 🔑 OTURUM YÖNETİMİ VE GİRİŞ SİSTEMİ (LOGIN)

### 📸 KULLANICI GİRİŞ EKRANI
* 🔍 **Identity Auth:** `Microsoft Identity` altyapısı ile güvenli oturum açma işlemi.<br>
* 🔍 **Beni Hatırlat (Persistent Cookie):** Kullanıcı oturumunun tarayıcı kapansa dahi korunması seçeneği.<br>
* 🔍 **Hata Yönetimi:** Yanlış şifre veya kullanıcı adında anlık `ModelState` bildirimleri.<br>
<img width="1869" height="953" alt="3" src="https://github.com/user-attachments/assets/c41902cc-a723-482e-b766-13ba455ba90a" />

## 🛠️ ŞİFRE KURTARMA VE YENİLEME AKIŞI (PASSWORD RESET)

### 📸 1. ŞİFRE SIFIRLAMA TALEBİ
* 🔍 **E-Posta Kontrolü:** Sistemde kayıtlı olan e-posta adresine özel sıfırlama bağlantısı gönderimi.<br>
* 🔍 **Anlık Bildirim:** Talebin başarıyla alındığına dair kullanıcıya sunulan bilgilendirme mesajı.<br>
<img width="525" height="653" alt="4" src="https://github.com/user-attachments/assets/b1e3a1de-f193-40b4-b1a3-c2a88af4c3e8" />

### 📸 2. ŞİFRE SIFIRLAMA E-POSTASI
* 🔍 **HTML Mail Template:** Kurumsal tasarıma uygun, yönlendirme butonuna sahip `MimeKit` tabanlı e-posta içeriği.<br>
* 🔍 **Güvenli Token:** Kullanıcıya özel üretilen benzersiz (unique) şifre sıfırlama bağlantısı.<br>

### 📸 3. YENİ ŞİFRE OLUŞTURMA
* 🔍 **Şifre Doğrulama:** Yeni girilen şifrelerin birbiriyle eşleşme kontrolü (`Compare` attribute).<br>
* 🔍 **Identity Password Update:** Şifrenin veritabanında güvenli bir şekilde hashlenerek güncellenmesi.<br>
* <img width="1618" height="358" alt="6" src="https://github.com/user-attachments/assets/876ab000-43fd-4d96-b6c5-c8812fa0dbc8" />
<img width="574" height="663" alt="5" src="https://github.com/user-attachments/assets/ff72238a-71a4-4703-b85d-b937b4da08a5" />


## 🔔 REAL-TIME NOTIFICATIONS & PROFILE MANAGEMENT

### 📸 BİLDİRİM VE PROFİL KONTROLLERİ
* 🔍 **Header Notification System:** Gelen kutusundaki son 3 okunmamış mesajın anlık özeti ve hızlı erişim linkleri.<br>
* 🔍 **User Quick Actions:** Navbar üzerinden profil düzenleme, gelen kutusu ve güvenli çıkış (`Logout`) menüsü.<br>
* 🔍 **Dynamic Branding:** Giriş yapan kullanıcının adının ve profil fotoğrafının tüm arayüzde asenkron gösterimi.<br>
<img width="392" height="310" alt="18" src="https://github.com/user-attachments/assets/2d85f449-737d-4308-a109-11d5465058fa" />
<img width="383" height="448" alt="20" src="https://github.com/user-attachments/assets/a6df4ed1-c71f-4241-a441-d1a5bf609de6" />


## 🛡️ ADMIN PRIVILEGES & ROLE MANAGEMENT (YÖNETİCİ PANELİ)

### 📸 HIZLI İŞLEM MERKEZİ (ADMIN TOOLS)
* 🔍 **Role Assignment:** Admin olan kullanıcıya açılan tabloda anlık olarak diğer kullanıcılara `Admin`, `User` veya özel rollerin atanması.<br>
* 🔍 **Dynamic User List:** Sistemdeki tüm kullanıcıların e-posta adresleri ile filtrelenebildiği gelişmiş seçim menüsü.<br>
* 🔍 **Role Creation:** Proje gereksinimlerine göre dinamik olarak yeni kullanıcı rollerinin oluşturulması ve yetkilendirilmesi.<br>
<img width="1887" height="551" alt="29" src="https://github.com/user-attachments/assets/14b5e431-241c-4364-877f-6249079e3172" />

## 🖥️ DYNAMIC DASHBOARD & USER INTERFACE (YÖNETİM PANELİ)

### 📸 GENEL İSTATİSTİKLER VE PANEL GÖRÜNÜMÜ
* 🔍 **Smart Statistics:** Toplam mesaj, okunmamış mesaj, kullanıcı sayısı ve kategorilerin dinamik takibi.<br>
* 🔍 **Data Visualization:** Mesaj okunma durumları ve kategori dağılımları için interaktif `Chart.js` grafik entegrasyonu.<br>
* 🔍 **User Directory:** Sisteme kayıtlı tüm kullanıcıların profil kartları üzerinden hızlı erişimi.<br>
<img width="1864" height="952" alt="8" src="https://github.com/user-attachments/assets/2371489f-0d4c-4414-ab7c-3c859f849f62" />

## ✉️ ADVANCED MESSAGING ARCHITECTURE

### 📸 MESAJ AKIŞI VE TAKİBİ
* 🔍 **Message Timeline:** Dashboard üzerinde son etkileşimlerin ve mesaj detaylarının zaman damgalı gösterimi.<br>
* 🔍 **Read/Unread Status:** Mesajların okunma durumuna göre otomatik renk kodlaması (Yeşil: Okundu, Turuncu: Yeni Mesaj).<br>
* 🔍 **Categorization:** Mesajların içeriklerine göre İş Dünyası, Proje, Sosyal gibi kategorilere ayrıştırılması.<br>
<img width="1869" height="950" alt="9" src="https://github.com/user-attachments/assets/6e5cee01-23ec-4b7b-94a3-c608e0a83f43" />


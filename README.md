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
<img width="1618" height="358" alt="6" src="https://github.com/user-attachments/assets/876ab000-43fd-4d96-b6c5-c8812fa0dbc8" />


### 📸 3. YENİ ŞİFRE OLUŞTURMA
* 🔍 **Şifre Doğrulama:** Yeni girilen şifrelerin birbiriyle eşleşme kontrolü (`Compare` attribute).<br>
* 🔍 **Identity Password Update:** Şifrenin veritabanında güvenli bir şekilde hashlenerek güncellenmesi.<br>
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


## ⚙️ KULLANICI PROFİL YÖNETİMİ VE GÜNCELLEME (USER SETTINGS)
### 📸 PROFİL BİLGİLERİNİ DÜZENLEME
* 🔍 **Personal Information Update:** Kullanıcının ad, soyad, e-posta, telefon numarası ve lokasyon gibi bilgilerini güncelleyebildiği dinamik form yapısı.
* 🔍 **Profile Image Management:** `IFormFile` entegrasyonu ile yeni profil fotoğrafı yükleme; mevcut fotoğrafın anlık önizlemesi ve sunucu tarafında güvenli saklanması.
* 🔍 **Identity User Integration:** Güncellenen bilgilerin `Microsoft Identity` altyapısı üzerinden `AspNetUsers` tablosuyla tam senkronize şekilde kaydedilmesi.
<img width="1873" height="948" alt="26" src="https://github.com/user-attachments/assets/b2699bf4-2488-40a6-93bb-3ed07c6bd5fe" />
<img width="1867" height="943" alt="27" src="https://github.com/user-attachments/assets/bf0b22f7-046b-4be5-821e-d5f5a28fdb33" />

## 🔐 GÜVENLİK VE ŞİFRE GÜNCELLEME (SECURITY SETTINGS)
### 📸 ŞİFRE DEĞİŞTİRME PANELİ
* 🔍 **Secure Password Update:** Mevcut şifre doğrulaması ile yeni şifre belirleme süreçlerinin `Identity` standartlarında yönetilmesi.
* 🔍 **Validation Checks:** Şifre karmaşıklığı (büyük/küçük harf, rakam, karakter) ve şifre tekrarı eşleşme kontrollerinin `Fluent Validation` veya `Data Annotations` ile yapılması.
* 🔍 **Real-Time Feedback:** İşlem başarılı olduğunda veya hata alındığında (Örn: "Mevcut şifre hatalı") kullanıcıya sunulan anlık bildirimler.
<img width="1868" height="947" alt="28" src="https://github.com/user-attachments/assets/139fa04a-295b-4755-8c2c-8dc6e8aedb33" />

## 🚀 KULLANICI DENEYİMİ (UX)
* 🔍 **Input Masking:** Telefon numarası gibi alanlarda kullanıcı hatalarını önleyen giriş maskeleri.
* 🔍 **Responsive Form Layout:** Mobil ve masaüstü cihazlarda formların bozulmadan, kullanıcı dostu bir hizada listelenmesi.
* 🔍 **Success Redirects:** Bilgiler güncellendikten sonra kullanıcının Dashboard'a veya Profil özetine otomatik yönlendirilmesi.


## ✍️ YENİ MESAJ OLUŞTURMA VE EDİTÖR (COMPOSE MAIL)
### 📸 PROFESYONEL MESAJ GÖNDERİM EKRANI
* 🔍 **Smart Recipient Management:** `To` (Alıcı) kısmında sistemdeki kayıtlı kullanıcılar arasından seçim yapabilme.<br>
* 🔍 **Rich Text Area:** Konu (`Subject`) ve içerik bölümleriyle ayrılmış, kurumsal mesajlaşma standartlarına uygun yapı.<br>
* 🔍 **Quick Actions:** Mesajı anında gönderme (`Send`) veya taslakları iptal etme (`Discard`) fonksiyonları.<br>
<img width="1867" height="944" alt="21" src="https://github.com/user-attachments/assets/8aacd442-d29c-4532-b2f0-5e7ea9c0978e" />


## 🔍 GELİŞMİŞ FİLTRELEME VE KATEGORİZASYON (FILTERING)
### 📸 DİNAMİK YAN PANEL VE CHECKBOX YÖNETİMİ
* 🔍 **Category Based Filtering:** Sol menüde yer alan checkbox filtreleri ile mesajları türlerine göre ayıklama:
    * 🏷️ **Promotions:** Kampanya ve tanıtım içerikli bildirimler.<br>
    * 🏷️ **Social:** Sosyal ağ ve kullanıcı etkileşimleri.<br>
    * 🏷️ **Updates:** Sistem ve profil güncellemeleri.<br>
* 🔍 **Multi-Selection System:** Tablo başındaki ana checkbox ile tüm mesajları toplu seçme, silme veya taşıma yeteneği.<br>
* 🔍 **Folder Navigation:** Inbox, Sent, Drafts ve Trash klasörleri arasında asenkron ve hızlı geçiş.<br>
<img width="1871" height="950" alt="16" src="https://github.com/user-attachments/assets/3a3d52e4-c480-47f6-ae1a-b5c531ba4468" />


## ⭐ ÖZELLEŞTİRİLMİŞ MESAJ KATMANLARI (STARRED & DRAFTS)
### 📸 YILDIZLI VE TASLAK MESAJ SİSTEMİ
* 🔍 **Starred Messages:** Kritik öneme sahip mesajların tek tıkla "Yıldızlı" olarak işaretlenmesi ve özel sekmede listelenmesi.<br>
* 🔍 **Drafting System:** Henüz gönderilmemiş, üzerinde çalışılan mesajların "Drafts" (Taslaklar) klasörüne otomatik veya manuel kaydedilmesi.<br>
* 🔍 **Advanced Navigation:** Yan menü (Sidebar) üzerinden tüm mesaj statüleri arasında (Gelen, Giden, Taslak, Çöp, Spam) asenkron geçiş.<br>
<img width="1864" height="949" alt="11" src="https://github.com/user-attachments/assets/67ded3ee-86c7-41a8-8641-c8acaecb472f" />
<img width="1868" height="944" alt="12" src="https://github.com/user-attachments/assets/6a4ff333-3008-4132-86a6-061deec65395" />


## 🏷️ ETİKETLEME VE DURUM TAKİBİ (LABELS)
### 📸 GÖRSEL DURUM GÖSTERGELERİ
* 🔍 **Dynamic Status Badges:** Mesajların yanında yer alan renkli etiketler (Yeşil, Mavi, Sarı) ile içerik türünün anlık tespiti.<br>
* 🔍 **Star & Important Marking:** Önemli mesajları yıldız ikonları ile işaretleme ve öncelikli listeye alma.<br>
* 🔍 **Unread Message Counter:** Klasörlerin yanında yer alan dinamik sayaçlar ile okunmamış mesaj sayısının takibi.<br>
<img width="1872" height="958" alt="13" src="https://github.com/user-attachments/assets/5c487342-3a68-4c31-b251-b01804528111" />


## 🗑️ GELİŞMİŞ MESAJ YÖNETİMİ VE ARŞİVLEME (TRASH & SPAM)
### 📸 ÇÖP KUTUSU VE SPAM KONTROLÜ
* 🔍 **Trash Folder Management:** Silinen mesajların sistemden tamamen kalkmadan önce "Trash" klasöründe güvenli bir şekilde depolanması.<br>
* 🔍 **Spam Protection:** İstenmeyen veya güvenlik riski taşıyan mesajların özel "Spam" filtresi ile ayrıştırılması.<br>
* 🔍 **Permanent Delete:** Çöp kutusundaki mesajların veritabanından kalıcı olarak temizlenmesi veya geri yüklenmesi fonksiyonu.<br>
<img width="1863" height="948" alt="17" src="https://github.com/user-attachments/assets/e6a767dc-a866-44f1-9962-05b689294281" />
<img width="1861" height="951" alt="15" src="https://github.com/user-attachments/assets/737970c3-62d4-4e82-8967-d0f49198b609" />


## 🚀 VERİ TUTARLILIĞI VE MİMARİ NOTLAR
* 🔍 **IsDeleted & IsSpam Flags:** Veritabanında (SQL) verilerin silinmek yerine `IsDeleted` gibi flag'lerle işaretlenerek klasörler arası mantıksal taşınması.<br>
* 🔍 **Identity User Context:** Her mesajın ve klasörün, o an oturum açmış olan `User.Identity.Name` bilgisine göre filtrelenmesi.<br>


## 🏷️ ETİKET BAZLI MESAJ LİSTELEME (LABEL FILTERING SYSTEM)
### 📸 ÖZELLEŞTİRİLMİŞ ETİKET SAYFALARI
* 🔍 **Dynamic Label Routing:** Sol menüdeki etiketlere (Promotions, Social, Updates) tıklandığında, sadece o etikete sahip mesajların listelendiği dinamik yönlendirme altyapısı.<br>
* 🔍 **Categorical Data Fetching:** `LINQ` sorguları ile veritabanı seviyesinde filtreleme yaparak, seçilen kategoriye ait verilerin asenkron olarak getirilmesi.<br>
* 🔍 **Visual Tagging:** Liste içerisinde her mesajın hangi etikete sahip olduğunun renkli badge'ler (Yeşil, Mavi, Sarı) ile görsel olarak belirtilmesi.<br>

### 📁 ETİKET YÖNETİMİ VE KULLANICI DENEYİMİ
* 🔍 **Label Navigation:** Kullanıcının karmaşık mail trafiği içerisinde "Sosyal" veya "Tanıtım" gibi özel alanlara tek tıkla odaklanmasını sağlayan UX çözümü.<br>
* 🔍 **No-Reload Filtering:** Sayfa geçişlerinde kullanıcıyı yormayan, hızlı yüklenen optimize edilmiş tablo tasarımları.<br>
* 🔍 **Empty State Handling:** Seçilen etikete ait mesaj bulunmadığında kullanıcıyı bilgilendiren dinamik "Mesaj Bulunamadı" arayüzü.<br>
<img width="1867" height="946" alt="19" src="https://github.com/user-attachments/assets/6637e056-e1b7-4e5d-8ced-d880ce18e367" />


## 📖 MESAJ DETAYLARI VE ETKİLEŞİM (MESSAGE DETAILS)
### 📸 MESAJ OKUMA EKRANI
* 🔍 **Message Content Rendering:** Gönderilen mailin içeriğini, gönderen bilgisini ve tarih detaylarını şık bir arayüzle sunan detay sayfası.<br>
* 🔍 **Sender Identity Integration:** Mesajı gönderen kişinin profil fotoğrafı ve sistemdeki ad-soyad bilgilerinin Identity altyapısından anlık çekilerek gösterilmesi.<br>
* 🔍 **Action Toolbar:** Mesaj detayındayken tek tıkla **Silme (Trash)**, **Yıldızlama (Star)** veya **Yanıtlama (Reply)** aksiyonlarını alma yeteneği.<br>


## ↩️ DİNAMİK YANITLAMA SİSTEMİ (REPLY MECHANISM)
### 📸 HIZLI YANIT VE VERİ ÇEKME
* 🔍 **Auto-Fill Recipient:** "Yanıtla" butonuna basıldığında, alıcı (To) kısmına otomatik olarak orijinal mesajın gönderen bilgilerinin asenkron şekilde aktarılması.<br>
* 🔍 **Context-Aware Messaging:** Orijinal mesajın konusuna otomatik olarak `RE:` ön ekinin eklenmesi ve konu bütünlüğünün korunması.<br>
* 🔍 **Dynamic Data Binding:** `AppUser` tablosundan alıcının e-posta adresinin ve profil bilgilerinin hatasız bir şekilde "Compose" modülüne taşınması.<br>
<img width="1864" height="950" alt="22" src="https://github.com/user-attachments/assets/396cb59d-b4b5-47fe-a3d8-8ab346847926" />

### 📸 KULLANICI BİLGİLERİNİN ÇEKİLMESİ
* 🔍 **Identity User Manager:** `UserManager` üzerinden kullanıcı ID'si ile eşleşen detaylı profil verilerinin (E-posta, İsim, Resim) `View` katmanına aktarımı.<br>
* 🔍 **Efficient SQL Joins:** Mesaj tablosu ile Kullanıcı (Users) tablosunun `Include` metodu ile birleştirilerek, veri kaybı olmadan hızlı listeleme yapılması.<br>
* 🔍 **Smart Redirects:** Mesaj yanıtlandıktan veya silindikten sonra kullanıcının kaldığı klasöre (Inbox/Sent) otomatik ve hızlı yönlendirilmesi.<br>


## 🚀 MESAJLAŞMA DENEYİMİ (UX)
* 🔍 **Full-Width Editor:** Yanıt yazarken kullanıcıyı kısıtlamayan, modern ve geniş metin alanı desteği.<br>
* 🔍 **Real-Time Data Retrieval:** Mesajın okundu durumunun (`IsRead`) veritabanında anlık güncellenerek bildirim sayacından düşürülmesi.<br>
<img width="1867" height="945" alt="23" src="https://github.com/user-attachments/assets/64f95e1b-5d9c-460b-a43c-a36ac2cf6abf" />


## 🔍 GELİŞMİŞ ARAMA VE NAVİGASYON (SEARCH & NAVIGATION)
### 📸 KULLANICI LİSTESİ VE DİNAMİK SCROLLBAR
* 🔍 **Custom Scrollbar Integration:** Kullanıcı listesinin (User Directory) arayüzü bozmadan, belirli bir yükseklikte sabitlenerek şık bir kaydırma çubuğu ile sunulması.<br>
* 🔍 **Real-Time User Search:** Sisteme kayıtlı onlarca kullanıcı arasında isim veya e-posta ile anlık arama yaparak hedef kişiye hızlı erişim.<br>
* 🔍 **Sticky Header UI:** Liste kaydırılsa dahi başlıkların ve arama barının sabit kalarak kullanıcı deneyimini (UX) artırması.<br>
* 🔍 **Anlık Mesaj Gönderimi Butonu** Mesaj gönderilmek istenen kullanıcıyı listeden bulup anında mesaj gönderme sayfasına bilgilerinin getirilmesi<br>
<img width="1206" height="445" alt="24" src="https://github.com/user-attachments/assets/7533bc2a-0256-4a75-b770-158d7c536e22" />

## 📧 MAİL İÇİ ARAMA VE FİLTRELEME (MAIL SEARCH)
### 📸 MESAJ KUTUSU ARAMA MEKANİZMASI
* 🔍 **Global Mail Search:** Gelen (Inbox) veya Giden (Sent) kutusundaki yüzlerce mail içerisinde konu başlığına veya gönderen adına göre akıllı arama.<br>
* 🔍 **Dynamic Result Listing:** Arama terimine uygun sonuçların sayfa yenilenmeden, mevcut tablo yapısı korunarak asenkron olarak filtrelenmesi.<br>
* 🔍 **Contextual Search:** Sadece aktif olan klasör (Spam, Trash, Inbox) içerisinde arama yaparak doğru veriye odaklanma yeteneği.<br>
<img width="1860" height="421" alt="25" src="https://github.com/user-attachments/assets/5c228b9e-773b-4a6f-bd58-4951d9ce9181" />






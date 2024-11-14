Bu proje, şirketlerin çalışanlarını ve departmanlarını yönetmek amacıyla geliştirilmiş bir ASP.NET Core MVC uygulamasıdır.

Teknolojiler
ASP.NET Core MVC: Uygulama, ASP.NET Core MVC framework'ü kullanılarak geliştirilmiştir.

Entity Framework Core (EF Core): Veritabanı işlemleri için Entity Framework Core (EF Core) kullanılmıştır.

Identity Framework: Kullanıcı kimlik doğrulaması ve rol yönetimi için ASP.NET Core Identity kullanılmıştır. Bu framework, kullanıcı kayıt işlemleri, giriş, parola sıfırlama gibi işlemleri yönetir ve kullanıcıları çeşitli rollere ayırarak güvenli erişim sağlar.

SQL Server: Veritabanı olarak Microsoft SQL Server kullanılmıştır. Veritabanı tasarımı ve veri yönetimi için EF Core üzerinden SQL Server ile entegrasyon sağlanmıştır.

Dependency Injection (DI): ASP.NET Core'un sağladığı Dependency Injection (DI) altyapısı kullanılarak, uygulamanın bağımlılıkları yönetilmiş ve sistemin test edilebilirliği artırılmıştır.


Admin Paneli Özellikleri
Profil Görüntüleme ve Güncelleme: Admin kullanıcıları, kendilerine ait profil bilgilerini görebilir ve güncelleyebilir.
Şirket Ekleme ve Görüntüleme: Admin, yeni şirketler ekleyebilir ve mevcut şirketlerin detaylarına göz atabilir.
Yöneticileri Görme ve Yönetme: Admin, tüm şirketlerin yöneticilerini görebilir, yeni yöneticiler ekleyebilir ve yöneticilerin bilgilerini güncelleyebilir.
![Ekran görüntüsü 2024-11-14 215856](https://github.com/user-attachments/assets/a078bd1d-6f8d-41d7-857e-c03c3b14a8a9)

![Ekran görüntüsü 2024-11-14 215926](https://github.com/user-attachments/assets/033e7e3f-8690-454b-b726-3428c149b845)
![Ekran görüntüsü 2024-11-14 215934](https://github.com/user-attachments/assets/e10ef981-1887-44cf-8949-12c83870638a)
![Ekran görüntüsü 2024-11-14 215943](https://github.com/user-attachments/assets/d29c2439-d21c-40c2-8bad-6e75b3056656)
![Ekran görüntüsü 2024-11-14 220001](https://github.com/user-attachments/assets/5bfbcc1b-03ba-4aeb-9402-580004202130)
![Ekran görüntüsü 2024-11-14 220148](https://github.com/user-attachments/assets/e0bf118f-d7bb-4b85-9f86-cbdfce2ada2e)
![Ekran görüntüsü 2024-11-14 220218](https://github.com/user-attachments/assets/e6b55784-df3c-49f4-a054-dc5e1070c8ff)
![Ekran görüntüsü 2024-11-14 220227](https://github.com/user-attachments/assets/6569a65f-c17f-40f9-9807-d4f1b8a1d412)
![Ekran görüntüsü 2024-11-14 220242](https://github.com/user-attachments/assets/22499687-540e-442b-8ba7-1d03fb904a8d)

Manager (Yönetici) Paneli
Kendi Profilini Görüntüleme ve Güncelleme: Manager, kendi profil bilgilerini görüntüleyebilir ve güncelleyebilir.
Çalışanları Listeleme: Yönetici, şirketine ait çalışanları listeleyebilir.
Departman Atama: Yönetici, çalışanlara departman atayabilir.
Personel Ekleme: Yönetici, yeni personel ekleyebilir.
Departman Ekleme: Yönetici, yeni departmanlar oluşturabilir.
İzin, Avans ve Harcama Talepleri Onaylama veya Reddetme: Yönetici, çalışanlardan gelen izin, avans ve harcama taleplerini onaylayabilir veya reddedebilir.

![Ekran görüntüsü 2024-11-14 221505](https://github.com/user-attachments/assets/93b91575-8229-4d20-8707-d3c7fe95c8ed)
![Ekran görüntüsü 2024-11-14 221530](https://github.com/user-attachments/assets/80a9c2a8-6518-41b4-979c-eea2f4381fd3)
![Ekran görüntüsü 2024-11-14 221542](https://github.com/user-attachments/assets/0c156c84-9ae0-4969-845f-a82be92ad73c)
![Ekran görüntüsü 2024-11-14 221928](https://github.com/user-attachments/assets/ab2065ff-fce5-4ba4-9894-608515a2e364)
![Ekran görüntüsü 2024-11-14 221936](https://github.com/user-attachments/assets/cdb29ae6-521e-4683-b1d9-a3dde4461419)
![Ekran görüntüsü 2024-11-14 221947](https://github.com/user-attachments/assets/82da935e-0067-4b90-bbad-9411e0b0e43e)
![Ekran görüntüsü 2024-11-14 221958](https://github.com/user-attachments/assets/a97e09d0-a61c-4b82-abea-65a477d9eafe)
![Ekran görüntüsü 2024-11-14 222013](https://github.com/user-attachments/assets/b51b25de-aa1f-4169-ab04-86d0f4f6cc32)

Employee (Çalışan) Paneli
Kendi Profilini Görüntüleme ve Güncelleme: Çalışan, kendi profil bilgilerini görüntüleyebilir ve güncelleyebilir.
Avans, İzin ve Harcama Talepleri Oluşturma: Çalışan, yöneticisine avans, izin ve harcama talepleri oluşturabilir.
Taleplerin Durumunu Görüntüleme: Çalışan, oluşturduğu taleplerin onay veya reddedilme durumlarını takip edebilir.

![Ekran görüntüsü 2024-11-14 222325](https://github.com/user-attachments/assets/0a1e2ec9-9e66-403d-bfcb-0425b9e798d6)

![Ekran görüntüsü 2024-11-14 222338](https://github.com/user-attachments/assets/2cfee175-218c-44b0-858f-c36ec1cc3e0d)
![Ekran görüntüsü 2024-11-14 222345](https://github.com/user-attachments/assets/ca988507-4afd-4958-9bac-4d0285b3a0e3)
![Ekran görüntüsü 2024-11-14 222416](https://github.com/user-attachments/assets/76d0dc6c-7771-4449-bd5b-375798b98e63)
![Ekran görüntüsü 2024-11-14 222423](https://github.com/user-attachments/assets/f0fcfe72-c76d-477d-90b0-c773873ecd4e)

![Ekran görüntüsü 2024-11-14 222434](https://github.com/user-attachments/assets/79735bb6-5a6b-4189-b3fc-37321e276a50)

![Ekran görüntüsü 2024-11-14 222507](https://github.com/user-attachments/assets/0647ae5c-c0ee-48e9-9ed9-fe938cff8250)

# ✅ .NET 8 Taskify API

Basit bir görev yönetimi API'sidir. Kullanıcılar kayıt olabilir, giriş yapabilir ve sadece kendilerine ait görevleri oluşturup yönetebilir. JWT tabanlı kimlik doğrulama kullanır.

---

## 🚀 Özellikler

- Kullanıcı kayıt ve giriş (Register / Login)
- JWT ile kimlik doğrulama (Token tabanlı koruma)
- Görev oluşturma, listeleme, silme ve güncelleme
- Sadece giriş yapan kullanıcının görevlerine erişim
- Swagger ile test edilebilir API arayüzü

---

## 🛠 Kullanılan Teknolojiler

- ASP.NET Core 8 Web API
- Entity Framework Core 8
- SQL Server
- AutoMapper
- JWT Authentication
- Swagger (Swashbuckle)

---

## ⚙️ Kurulum

```bash
git clone https://github.com/kullaniciadi/dotnet-taskify-api.git
cd dotnet-taskify-api
dotnet restore
dotnet ef database update
dotnet run

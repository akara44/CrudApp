# C# VERİTABANI ÜRÜN-KATEGORİ BİLGİ Sistemi

---

## Proje Hakkında

Bu proje, C# konsol uygulaması ile **SQL Server** veritabanındaki tabloları (`Kategoriler`, `Ürünler`, `Siparişler`) listeleyebilen basit bir **Veritabanı Ürün-Kategori Bilgi Sistemi**dir.  
Projede ADO.NET kullanılarak SQL Server veritabanına bağlanılmış ve tabloların içeriği ekrana yazdırılmıştır.

---

## Proje Dosya Yapısı


---

## Başlangıç (Kurulum ve Çalıştırma)

### 1. Veritabanını Yükleme

Proje klasöründeki `Database` klasöründe bulunan `EgitimKampiDb.bak` dosyası, SQL Server için veritabanı yedeğidir.  
Bu veritabanını kendi bilgisayarınıza şu adımlarla yükleyin:

1. SQL Server Management Studio'yu (SSMS) açın.
2. **Databases** üzerinde sağ tıklayıp **Restore Database** seçeneğini seçin.
3. **Source** kısmında **Device** seçin, yanındaki `...` butonuna tıklayın.
4. `Add` diyerek proje klasöründeki `Database\EgitimKampiDb.bak` dosyasını bulun ve seçin.
5. **Restore** işlemini başlatın.


### 3. Projeyi Çalıştırma

```csharp
// Visual Studio'da projeyi açın
// Program.cs dosyasını çalıştırın

// Konsolda açılan menüden görmek istediğiniz tablo numarasını girin
// Veritabanındaki tablo verileri konsola yazdırılacaktır

// Örnek çalışma akışı:
// 1-Kategoriler
// 2-Ürünler
// 3-Siparişler
// 4-Çıkış Yap
### 2. Proje Bağlantı Ayarlarını Güncelleme

`Program.cs` dosyasındaki bağlantı dizesini (`connection string`) kendi SQL Server ayarlarınıza göre güncelleyin:

```csharp
SqlConnection connection = new SqlConnection("Data Source=YOUR_SERVER_NAME;Initial Catalog=EgitimKampiDb;Integrated Security=True");


```

---


### 4. Kullanılan Teknolojiler

```csharp
 C# (.NET Framework / .NET Core)
 ADO.NET (SqlConnection, SqlCommand, SqlDataAdapter)
 SQL Server
```

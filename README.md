## Kullanılan Teknolojiler
.NET Core 6 API

MSSQL

Swagger UI

Entity Framework - Code First Approach

Repository Design Pattern

N-Tier Architecture

CQRS Design Pattern - Mediatr

Onion Architecture

Veritabanı yönetimi için: SSMS

## Ek Geliştirmeler

CarrierReports adında yeni bir veri tabanı tablosu oluşturulmuştur.

Günün her saatinde tetiklenecek şekilde yapılandırılmış bir background job yazılmıştır. Bu job tetiklendiğinde:
  Sipariş tablosundaki tüm siparişler, kargo ve sipariş tarihi bazında gruplandırıldı.
  
  Gruplanan siparişlerin kargo fiyatları toplandı.
  
  Toplanan değerler, CarrierReports tablosuna kargo bazında kaydedildi.
  
Hangfire Dashboard konfigürasyonu yapılarak, dashboard üzerinden yönetim sağlandı.


## Prensipler
Proje geliştirilirken SOLID prensiplerine uygun, temiz ve anlaşılabilir kod yazmaya özen gösterilmiştir. 

Tekrar eden tablo kolonları için bir Base Entity sınıfı oluşturulmuş ve bu sayede kodun yeniden kullanımı ve bakımı kolaylaştırılmıştır.

Yapılan tüm işlemler sonucunda, işleme ait bir string response olarak sonuç döndürülmüştür.

Exception handling sistemi sayesinde, her işlem sırasında oluşabilecek hatalar uygun şekilde ele alındı.

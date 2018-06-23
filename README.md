# Uygulama
Bir input dosyasından kayıtlar okunacak ve kayıtlarda ki bir anahtar alana göre sıralı bir output dosyası oluşturulacaktır.

    input1.txt  
Kayıtlar sırasız bir şekilde yerleştirilmiş ve bütün kayıtların uzunluğu değişkendir. Kayıtlarda ki alanların uzunlukları da genelde değişkendir ve alanlar birbirinden ‘|’karakteriyle ayrılmaktadır. Kayıtlarda ki alanlar:

Ders kodu (7 characters)
Ders adı (variable length >= 1)
Dersin kredisi (1 character)
Önşart dersi  (variable length >= 0)
Ders açıklaması (variable length >= 1)

    output.txt   
Output Dosyasında yapılacak işlemler: 
Sort - Output dosyasında kayıtlar Ders koduna göre sıralı olarak yerleştirilmiş olacak. 
Duplicate eleme – Dosyada çift kayıtlar (aynı ders koduna sahip dersler var ise bunlar istenirse silinebilecek)
Insert - output.txt'ye sıralı yerine kayıt ekle

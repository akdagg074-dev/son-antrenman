# Son Antrenman

3B Oyun Geliştirme vize ödevi — Unity 6 (6000.3.22f1)

Yarın şampiyonluk maçı var! Ama dün geceki rüzgâr antrenman toplarını mahalleye dağıttı.
Teknik direktörün şartı net: **60 saniyede 10 topu bul, sahaya getir ve kalelere sok!**

## Oynanış
- **WASD / Yön tuşları:** hareket (topa uzakken koşar, topun yanında kontrollü sürer)
- Topları fiziksel olarak iterek kalelerin içindeki çukurlara düşür
- **Space:** önündeki topa şut çek
- **ESC:** oyunu duraklat (Devam Et / Tekrar Oyna / Ana Menü)
- Sarı defans mankenleri sağa-sola kayarak yolu kapatır
- Sahadaki toplar bitince sarı ok etraftaki topları gösterir
- 60 saniye içinde 10 gol: **Mükemmel Antrenman!** — süre biterse: **Antrenman Bitti!**

## Oyna
- **Tarayıcıda (WebGL):** GitHub Pages linki (bu repodaki `docs/` klasörü)
- **Windows:** teslim edilen `Son Antrenman.exe`

## Proje yapısı
- `Assets/Karakter/` — karakter, `Assets/Animasyonlar/` — animasyonlar, `Assets/Sesler/` — sesler ve müzik, `Assets/Modeller/` — modeller, materyaller ve dokular, `Assets/Arayuz/` — menü görselleri
- `Assets/Sahneler/Oyun.unity` — oyun sahnesi
- `Assets/Kodlar/` — `PlayerMovement`, `CameraFollow`, `Hole`, `GameManager`, `BallPointer`, `ShootBall`, `RandomCubeMovement` (ders hocasının kodu)
- Kullanılan hazır varlıklar ve lisansları: [KAYNAKLAR.txt](KAYNAKLAR.txt)

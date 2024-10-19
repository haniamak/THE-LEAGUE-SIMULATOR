namespace Projekt;

public static class Terminarz {

  public static IEnumerable<Kolejka> GenerujKolejki() {

    if (Stale.LiczbaDruzyn < 0 || Stale.LiczbaDruzyn % 2 != 0) {
      throw new ArgumentException("Zla liczba druzyn");
    }

    List<Kolejka> kolejki = new List<Kolejka>();
    List<Kolejka> kolejkiPierwotne = new List<Kolejka>();

    for (int i = 1; i < Stale.LiczbaDruzyn; i++) {
      if (i % 2 == 1) {
        Kolejka kolejkaNieparzysta = KolejkaNieparzysta(i);
        kolejkiPierwotne.Add(kolejkaNieparzysta);
      } else {
        Kolejka kolejkaParzysta = KolejkaParzysta(i);
        kolejkiPierwotne.Add(kolejkaParzysta);
      }
    }

    List<Kolejka> kolejkiRewanzowe = new List<Kolejka>();
    foreach (var kolejkaPierwotna in kolejkiPierwotne) {
      Kolejka kolejkaRewanzowa = KolejkaRewanzowa(kolejkaPierwotna);
      kolejkiRewanzowe.Add(kolejkaRewanzowa);
    }

    kolejki.AddRange(kolejkiPierwotne);
    kolejki.AddRange(kolejkiRewanzowe);

    if (SkontrolujKolejki(kolejki) == false) {
      throw new Exception("Blad generowania kolejek");
    }

    return kolejki.AsEnumerable();
  }

  private static bool SkontrolujKolejki(IEnumerable<Kolejka> kolejki) {
    bool wynikSprawdzenia = true;
    if (wynikSprawdzenia && kolejki.GroupBy(k => k.Numer).Count() != Stale.LiczbaKolejek && kolejki.Min(k => k.Numer) == 1 && kolejki.Max(k => k.Numer) == Stale.LiczbaKolejek) {
      wynikSprawdzenia = false;
    }
    if (wynikSprawdzenia) {
      Dictionary<int, List<int>> uSiebie = new Dictionary<int, List<int>>();
      foreach (var kolejka in kolejki) {
        foreach (var zestawienie in kolejka.Zestawienia) {
          if (!uSiebie.ContainsKey(zestawienie.IdGospodarz)) {
            List<int> goscie = new List<int>();
            goscie.Add(zestawienie.IdGosc);
            uSiebie.Add(zestawienie.IdGospodarz, goscie);
          } else {
            uSiebie.First(id => id.Key == zestawienie.IdGospodarz).Value.Add(zestawienie.IdGosc);
          }
        }
      }
      foreach (var gospodarz in uSiebie) {
        if (gospodarz.Value.GroupBy(id => id).Count() != (Stale.LiczbaDruzyn - 1)) {
          wynikSprawdzenia = false;
          break;
        }
      }
    }
    return wynikSprawdzenia;
  }

  private static Kolejka KolejkaNieparzysta(int numerKolejki) {
    int idGospodarz, idGosc;
    List<Zestawienie> zestawienia = new List<Zestawienie>();
    idGospodarz = numerKolejki / 2 + 1;
    idGosc = Stale.LiczbaDruzyn;
    Zestawienie pierwszeZestawienie = new Zestawienie(idGospodarz, idGosc);
    zestawienia.Add(pierwszeZestawienie);
    int idGospodarzNastepny, idGoscNastepny;
    for (int biezacyIndeks = 1; biezacyIndeks < Stale.LiczbaDruzyn / 2; biezacyIndeks++) {
      idGospodarzNastepny = idGospodarz + biezacyIndeks;
      if (numerKolejki == 1) {
        idGoscNastepny = idGosc - biezacyIndeks;
      } else {
        idGoscNastepny = idGospodarz - biezacyIndeks;
        if (idGoscNastepny < 1) {
          idGoscNastepny = idGosc - biezacyIndeks + (numerKolejki / 2);
        }
      }
      Zestawienie nastepneZestawienie = new Zestawienie(idGospodarzNastepny, idGoscNastepny);
      zestawienia.Add(nastepneZestawienie);
    }
    Kolejka kolejka = new Kolejka(numerKolejki, zestawienia.AsEnumerable());
    return kolejka;
  }

  private static Kolejka KolejkaParzysta(int numerKolejki) {
    int idGospodarz, idGosc;
    List<Zestawienie> zestawienia = new List<Zestawienie>();

    idGospodarz = Stale.LiczbaDruzyn;
    idGosc = numerKolejki + Stale.LiczbaDruzyn / 2 - numerKolejki / 2;
    Zestawienie pierwszeZestawienie = new Zestawienie(idGospodarz, idGosc);
    zestawienia.Add(pierwszeZestawienie);

    int idGospodarzNastepny, idGoscNastepny;
    for (int biezacyIndeks = 1; biezacyIndeks < Stale.LiczbaDruzyn / 2; biezacyIndeks++) {
      idGospodarzNastepny = idGosc + biezacyIndeks;
      if (idGospodarzNastepny > (Stale.LiczbaDruzyn - 1)) {
        idGospodarzNastepny = idGospodarzNastepny - (Stale.LiczbaDruzyn - 1);
      }
      idGoscNastepny = idGosc - biezacyIndeks;
      if (idGoscNastepny > (Stale.LiczbaDruzyn - 1)) {
        idGoscNastepny = idGoscNastepny - (Stale.LiczbaDruzyn - 1);
      }
      Zestawienie kolejneZestawienie = new Zestawienie(idGospodarzNastepny, idGoscNastepny);
      zestawienia.Add(kolejneZestawienie);
    }
    Kolejka kolejka = new Kolejka(numerKolejki, zestawienia.AsEnumerable());
    return kolejka;
  }

  private static Kolejka KolejkaRewanzowa(Kolejka kolejkaPierwotna) {
    int numerKolejki = kolejkaPierwotna.Numer + Stale.LiczbaDruzyn - 1;
    List<Zestawienie> zestawieniaRewanzowe = new List<Zestawienie>();
    foreach (var zestawieniePierwotne in kolejkaPierwotna.Zestawienia) {
      Zestawienie rewanz = new Zestawienie(zestawieniePierwotne.IdGosc, zestawieniePierwotne.IdGospodarz);
      zestawieniaRewanzowe.Add(rewanz);
    }

    Kolejka kolejkaRewanzowa = new Kolejka(numerKolejki, zestawieniaRewanzowe.AsEnumerable());
    return kolejkaRewanzowa;
  }

}

namespace Projekt;

using System.Collections.Immutable;

public class Tabela {

  private ImmutableDictionary<int, Rezultat>? rezultatyUSiebie;
  private ImmutableDictionary<int, Rezultat>? rezultatyNaWyjezdzie;
  private ImmutableDictionary<int, Rezultat>? rezultatyZbiorcze;

  private int maxNazwaDruzyny;
  private IEnumerable<KeyValuePair<int, string>> nazwyDruzyn;

  public IEnumerable<KeyValuePair<int, Rezultat>>? RezultatyUSiebie {
    get {
      return this.rezultatyUSiebie?.AsEnumerable();
    }
  }

  public IEnumerable<KeyValuePair<int, Rezultat>>? RezultatyNaWyjezdzie {
    get {
      return this.rezultatyNaWyjezdzie?.AsEnumerable();
    }
  }

  public IEnumerable<KeyValuePair<int, Rezultat>>? RezultatyZbiorcze {
    get {
      return this.rezultatyZbiorcze?.AsEnumerable();
    }
  }

  public Tabela(IEnumerable<Kolejka> kolejki, IEnumerable<Druzyna> druzyny) {
    this.nazwyDruzyn = druzyny.Select(d => new KeyValuePair<int, string> ( d.Id, d.Nazwa ));
    this.maxNazwaDruzyny = druzyny.Max(d => d.Nazwa.Length);
    this.GenerujRezultaty(kolejki, druzyny);
  }

  public void Pokaz() {
    int pozycja = 1;
    Console.WriteLine("===============================================");
    if (this.RezultatyZbiorcze != null) {
      foreach (var rezultat in this.RezultatyZbiorcze
        .OrderByDescending(r => r.Value.Punkty)
        .ThenByDescending(r => (r.Value.BramkiZdobyte - r.Value.BramkiStracone))
        .ThenByDescending(r => (r.Value.BramkiZdobyte))) {
        this.UstawKolorPozycji(pozycja);
        Console.Write(pozycja.ToString().PadLeft(2) + ". " + this.nazwyDruzyn.FirstOrDefault(d => d.Key == rezultat.Key).Value.PadRight(maxNazwaDruzyny) + " ");
        rezultat.Value.Pokaz();
        pozycja++;
      }
      Console.ResetColor();
    }
  }

  private void GenerujRezultaty(IEnumerable<Kolejka> kolejki, IEnumerable<Druzyna> druzyny) {
    
    Dictionary<int, Rezultat> rezultatyUSiebie = new Dictionary<int, Rezultat>();
    Dictionary<int, Rezultat> rezultatyNaWyjezdzie = new Dictionary<int, Rezultat>();
    
    foreach(var kolejka in kolejki) {
      foreach (var mecz in kolejka.Mecze) {
        WynikMeczu? wynikMeczu = mecz.Wynik;
        if (wynikMeczu != null) {
          Rezultat rezultatGospodarz = new Rezultat(wynikMeczu.PunktyGospodarz, wynikMeczu.BramkiGospodarz, wynikMeczu.BramkiGosc);
          Rezultat rezultatGosc = new Rezultat(wynikMeczu.PunktyGosc, wynikMeczu.BramkiGosc, wynikMeczu.BramkiGospodarz);
          int idGospodarz = mecz.Gospodarz.Id;
          int idGosc = mecz.Gosc.Id;
          if (!rezultatyUSiebie.ContainsKey(idGospodarz)) {
            rezultatyUSiebie.Add(idGospodarz, rezultatGospodarz);
          } else {
            Rezultat r = rezultatyUSiebie.First(r => r.Key == idGospodarz).Value;
            rezultatGospodarz = new Rezultat(r, new Rezultat(wynikMeczu.PunktyGospodarz, wynikMeczu.BramkiGospodarz, wynikMeczu.BramkiGosc));
            rezultatyUSiebie[idGospodarz] = rezultatGospodarz;
          }

          if (!rezultatyNaWyjezdzie.ContainsKey(idGosc)) {
            rezultatyNaWyjezdzie.Add(idGosc, rezultatGosc);
          } else {
            Rezultat r = rezultatyNaWyjezdzie.First(r => r.Key == idGosc).Value;
            rezultatGosc = new Rezultat(r, new Rezultat(wynikMeczu.PunktyGosc, wynikMeczu.BramkiGosc, wynikMeczu.BramkiGospodarz));
            rezultatyNaWyjezdzie[idGosc] = rezultatGosc;
          }
        }
      }
    }

    this.rezultatyUSiebie = rezultatyUSiebie.ToImmutableDictionary();
    this.rezultatyNaWyjezdzie = rezultatyNaWyjezdzie.ToImmutableDictionary();
    this.GenerujRezultatyZbiorcze();
  }

  private void GenerujRezultatyZbiorcze() {
    Dictionary<int, Rezultat> rezultatyZbiorcze = new Dictionary<int, Rezultat>();
    if (this.RezultatyUSiebie != null && this.RezultatyNaWyjezdzie != null) {
      foreach (var item in this.RezultatyUSiebie) {
        int idDruzyny = item.Key;
        rezultatyZbiorcze.Add(idDruzyny, item.Value);
      }
      foreach (var item in this.RezultatyNaWyjezdzie) {
        int idDruzyny = item.Key;
        if (!rezultatyZbiorcze.ContainsKey(idDruzyny)) {
          rezultatyZbiorcze.Add(idDruzyny, item.Value);
        } else {
          Rezultat sumaRezultatowUSiebie = rezultatyZbiorcze.First(r => r.Key == idDruzyny).Value;
          Rezultat sumaRezultatowNaWyjezdzie = this.RezultatyNaWyjezdzie.First(r => r.Key == idDruzyny).Value;
          Rezultat rezultatZbiorczy = new Rezultat(sumaRezultatowUSiebie, sumaRezultatowNaWyjezdzie);
          rezultatyZbiorcze[idDruzyny] = rezultatZbiorczy;
        }
      }
    }
    this.rezultatyZbiorcze = rezultatyZbiorcze.ToImmutableDictionary();
  }

  private void UstawKolorPozycji(int pozycja) {
    if (pozycja < 2) {
      Console.ForegroundColor = ConsoleColor.Cyan;
    } else if (pozycja < 5) {
      Console.ForegroundColor = ConsoleColor.Green;
    } else if (pozycja < 7) {
      Console.ForegroundColor = ConsoleColor.Blue;
    } else if (pozycja < 8) {  
      Console.ForegroundColor = ConsoleColor.Magenta;
    } else if (pozycja < 18) {  
      Console.ForegroundColor = ConsoleColor.White;
    } else {
      Console.ForegroundColor = ConsoleColor.Red;
    }    
  }

}

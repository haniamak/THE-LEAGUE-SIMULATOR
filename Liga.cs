namespace Projekt;

using System.Collections.Generic;
using System.Collections.Immutable;

public class Liga {
  
  private readonly IDane dane;
  private readonly ImmutableArray<Kolejka> kolejki;
  private readonly ImmutableList<Druzyna> druzyny;
  private readonly string nazwa;
  public string Nazwa { 
    get { return this.nazwa; }
  }

  public IEnumerable<Druzyna> Druzyny {
      get { return this.druzyny.AsEnumerable(); }
  } 

  public IEnumerable<Kolejka> Kolejki {
    get { return this.kolejki.AsEnumerable();}
  } 

  public Kolejka? Kolejka(int numer) {
    return Kolejki.FirstOrDefault(d => d.Numer == numer);
  }

  public Druzyna? this[int id] {
    get { return Druzyny.FirstOrDefault(d => d.Id == id); }
  }

  public Druzyna? this[string nazwa] {
    get { return Druzyny.FirstOrDefault(d => d.Nazwa == nazwa); }
  }

  public Liga(string nazwa, IDane dane) {
    this.nazwa = nazwa;
    this.dane = dane;
    this.druzyny = this.dane.CzytajDruzyny(Stale.PlikDruzyny, Stale.SeperatorPol).ToImmutableList();
    this.ZaladujStatystyki(dane.CzytajStatystyki(Stale.PlikStatystykiUSiebie, Stale.SeperatorPol), dane.CzytajStatystyki(Stale.PlikStatystykiNaWyjezdzie, Stale.SeperatorPol));
    this.kolejki = Terminarz.GenerujKolejki().ToImmutableArray();
  }

  public void Rozegraj() {
    foreach (var kolejka in this.Kolejki) {
      kolejka.Rozegraj(this.Druzyny);
      Console.ReadKey(true);
      this.PokazTabelePoKolejce(kolejka.Numer);
      Console.ReadKey(true);
      Console.Clear();
    }
  }

  private void ZaladujStatystyki(IEnumerable<Statystyka> uSiebie, IEnumerable<Statystyka> naWyjezdzie) {
    foreach (var druzyna in this.Druzyny) {
      Statystyka? statystykaUSiebie = uSiebie.FirstOrDefault(e => e.IdDruzyny == druzyna.Id);
      Statystyka? statystykaNaWyjezdzie = naWyjezdzie.FirstOrDefault(e => e.IdDruzyny == druzyna.Id);
      druzyna.ZaladujStatystyki(statystykaUSiebie, statystykaNaWyjezdzie);
    }
  }

  public void Pokaz() {
    int maxNazwaDruzyny = this.Druzyny.Max(d => d.Nazwa.Length);
    Console.WriteLine();
    Console.WriteLine($"{this.Nazwa} - Drużyny");
    Console.WriteLine("****************************************");
    foreach (var druzyna in this.Druzyny) {
      Console.Write(druzyna.Nazwa.PadLeft(maxNazwaDruzyny, ' '));
      Console.Write(" - ");
      Console.WriteLine(druzyna.Sila.ToString());
    }
    Console.WriteLine("****************************************");
    Console.WriteLine();
  }

  private void PokazTabelePoKolejce(int numerKolejki) {
    var kolejki = this.Kolejki.Where(k => k.Numer <= numerKolejki);
    Tabela tabela = new Tabela(kolejki, this.Druzyny);
    tabela.Pokaz();
  }

}

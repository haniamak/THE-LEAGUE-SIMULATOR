namespace Projekt;

public class Druzyna {
  
  private readonly int id;

  public int Id { 
    get { return this.id; }
  }

  private readonly string nazwa;

  public string Nazwa { 
    get { return this.nazwa; }
  }

  private GrupaSily sila;

  public GrupaSily Sila { 
    get { return this.sila; }
  }

  public Statystyka StatystykaUSiebie { get; private set; }
  public Statystyka StatystykaNaWyjezdzie { get; private set; }

  public Druzyna(int id, string nazwa, GrupaSily grupaSily) {
    this.id = id;
    this.nazwa = nazwa;
    this.sila = grupaSily;
    this.StatystykaUSiebie = Statystyka.PustaStatystyka(id);
    this.StatystykaNaWyjezdzie = Statystyka.PustaStatystyka(id);
  }

  public void ZaladujStatystyki(Statystyka? uSiebie, Statystyka? naWyjezdzie) {
    if (uSiebie != null) {
      this.StatystykaUSiebie = uSiebie;
    }
    if (naWyjezdzie != null) {
      this.StatystykaNaWyjezdzie = naWyjezdzie;
    }
  }

  public void PokazStatystyki() {
    Console.WriteLine($"Statystyki bramkowe druzyny: {this.Nazwa}");
    Console.WriteLine("U siebie: SREDNIA, MIN, MAX");
    Console.Write(this.StatystykaUSiebie.ToString());
    Console.WriteLine("Na wyjezdzie: SREDNIA, MIN, MAX");
    Console.Write(this.StatystykaNaWyjezdzie.ToString());
  }

}

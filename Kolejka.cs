namespace Projekt;

using System.Collections.Immutable;
using System.Text;

public class Kolejka {
  
  private readonly int numer;

  public int Numer {
    get { return this.numer; }
  }

  private ImmutableList<Mecz> mecze = new List<Mecz>().ToImmutableList();

  private readonly ImmutableArray<Zestawienie> zestawienia;

  private int najdluzszaNazwaLewa;
  private int najdluzszaNazwaPrawa;
  
  public Kolejka(int numer, IEnumerable<Zestawienie> zestawienia) {
    if (zestawienia.Count() != Stale.LiczbaDruzyn / 2) {
      throw new ArgumentException("Zla liczba zestawien");
    }
    this.numer = numer;
    this.zestawienia = zestawienia.ToImmutableArray<Zestawienie>();
  }

  public IEnumerable<Zestawienie> Zestawienia {
    get { return this.zestawienia.AsEnumerable(); }
  }

  public IEnumerable<Mecz> Mecze {
    get { return this.mecze.AsEnumerable(); }
  }

  public void Rozegraj(IEnumerable<Druzyna> druzyny) {
    this.mecze = zestawienia.Select(zestawienie => new Mecz(
        druzyny.First(druzyna => druzyna.Id ==  zestawienie.IdGospodarz),
        druzyny.First(druzyna => druzyna.Id ==  zestawienie.IdGosc))).ToImmutableList();

    this.najdluzszaNazwaLewa = mecze.Max(m => m.Gospodarz.Nazwa.Length);
    this.najdluzszaNazwaPrawa = mecze.Max(m => m.Gosc.Nazwa.Length);

    this.PokazKolejkePrzedMeczami();
    this.PokazKolejkePoMeczach();
  }

  private void PokazKolejkePrzedMeczami() {
    Console.WriteLine("Kolejka: " + this.Numer.ToString());
    Console.WriteLine("========================================");
    foreach (var mecz in this.Mecze) {
      Console.Write(mecz.Gospodarz.Nazwa.PadLeft(this.najdluzszaNazwaLewa, ' '));
      Console.Write(" - ");
      Console.WriteLine(mecz.Gosc.Nazwa.PadRight(this.najdluzszaNazwaPrawa, ' '));
    }
    Console.ReadKey(true);
  }

  private void PokazKolejkePoMeczach() {
    Console.Clear();
    Console.WriteLine("Kolejka: " + this.Numer.ToString());
    Console.WriteLine("========================================");
    foreach (var mecz in this.Mecze) {
      WynikMeczu wm = mecz.Rozegraj();
      Console.Write(mecz.Gospodarz.Nazwa.PadLeft(this.najdluzszaNazwaLewa, ' '));
      Console.Write(" - ");
      Console.Write(mecz.Gosc.Nazwa.PadRight(this.najdluzszaNazwaPrawa, ' '));
      Console.WriteLine(wm?.ToString());
    }
  }

  public override string ToString() {
    StringBuilder sb = new StringBuilder();
    sb.Append($"Kolejka: {this.numer} ");
    foreach (var zestawienie in Zestawienia) {
      sb.Append(zestawienie.ToString());
    }
    sb.AppendLine();
    return sb.ToString();
  }

}

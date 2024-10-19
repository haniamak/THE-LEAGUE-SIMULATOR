namespace Projekt;

using System.Collections.Immutable;
using System.Text;

public class Statystyka {
  
  private readonly int idDruzyny;
  private readonly ImmutableArray<double> bramkiSrednie;
  private readonly ImmutableArray<double> bramkiMin;
  private readonly ImmutableArray<double> bramkiMax;

  public int IdDruzyny {
    get { return this.idDruzyny; }
  }

  public ImmutableArray<double> BramkiSrednie {
    get { return this.bramkiSrednie; }
  }

  public ImmutableArray<double> BramkiMin {
    get { return this.bramkiMin; }
  }

  public ImmutableArray<double> BramkiMax {
    get { return this.bramkiMax; }
  }

  public Statystyka(int idDruzyny, IEnumerable<double> bramkiSrednie, IEnumerable<double> bramkiMin, IEnumerable<double> bramkiMax) {
    
    if (bramkiSrednie.Count() != Stale.LiczbaGrupSily) {
      throw new ArgumentException("Zla liczba statystyk bramki srednie");
    }

    if (bramkiMin.Count() != Stale.LiczbaGrupSily) {
      throw new ArgumentException("Zla liczba statystyk bramki min");
    }

    if (bramkiMax.Count() != Stale.LiczbaGrupSily) {
      throw new ArgumentException("Zla liczba statystyk bramki max");
    }

    this.idDruzyny = idDruzyny;
    this.bramkiSrednie = bramkiSrednie.ToImmutableArray<double>();
    this.bramkiMin = bramkiMin.ToImmutableArray<double>();
    this.bramkiMax = bramkiMax.ToImmutableArray<double>();
  }

  public static Statystyka PustaStatystyka(int idDruzyny) {
    return new Statystyka(idDruzyny, new double[Stale.LiczbaGrupSily], new double[Stale.LiczbaGrupSily], new double[Stale.LiczbaGrupSily]);
  }

  public override string ToString() {
    StringBuilder sb = new StringBuilder();

    sb.AppendLine($"{GrupaSily.Top.ToString()}: {this.bramkiSrednie[(int)GrupaSily.Top]}, {this.bramkiMin[(int)GrupaSily.Top]}, {this.bramkiMax[(int)GrupaSily.Top]}");
    sb.AppendLine($"{GrupaSily.Solidna.ToString()}: {this.bramkiSrednie[(int)GrupaSily.Solidna]}, {this.bramkiMin[(int)GrupaSily.Solidna]}, {this.bramkiMax[(int)GrupaSily.Solidna]}");
    sb.AppendLine($"{GrupaSily.Sredniak.ToString()}: {this.bramkiSrednie[(int)GrupaSily.Sredniak]}, {this.bramkiMin[(int)GrupaSily.Sredniak]}, {this.bramkiMax[(int)GrupaSily.Sredniak]}");
    sb.AppendLine($"{GrupaSily.Slabeusz.ToString()}: {this.bramkiSrednie[(int)GrupaSily.Slabeusz]}, {this.bramkiMin[(int)GrupaSily.Slabeusz]}, {this.bramkiMax[(int)GrupaSily.Slabeusz]}");

    return sb.ToString();
  }

}

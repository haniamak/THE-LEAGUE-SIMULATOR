namespace Projekt;

public class Symulator : ISymulator {
  
  private readonly Random random = new Random();

  private readonly Mecz mecz;

  public Mecz Mecz {
    get { return this.mecz; }
  }

  public Symulator(Mecz mecz) {
    this.mecz = mecz;
  }

  public WynikMeczu RozegrajMecz() {
    int bramkiGospodarz = this.SymulujBramkiGospodarza();
    int bramkiGosc = this.SymulujBramkiGoscia();
    return new WynikMeczu(bramkiGospodarz, bramkiGosc);
  }

  private int SymulujBramkiGospodarza() {
    int bramkiGospodarz = 0;
    int silaGoscia = (int)Mecz.Gosc.Sila;
    double bramkiSrednieGospodarz = Mecz.Gospodarz.StatystykaUSiebie.BramkiSrednie[silaGoscia];
    double bramkiMinGospodarz = Mecz.Gospodarz.StatystykaUSiebie.BramkiMin[silaGoscia];
    double bramkiMaxGospodarz = Mecz.Gospodarz.StatystykaUSiebie.BramkiMax[silaGoscia];
    double modyfikatorProguSredniej = 0.025;
    double losSredniej = this.random.NextDouble();
    double losBramek;
    double progSredniej = (bramkiSrednieGospodarz / bramkiMaxGospodarz) - modyfikatorProguSredniej;
    if (losSredniej <= progSredniej) {
      losBramek = this.random.NextDouble() * (bramkiSrednieGospodarz - bramkiMinGospodarz);
      if (bramkiMinGospodarz > 0) {
        losBramek = losBramek + bramkiMinGospodarz;
      }
    } else {
      losBramek = this.random.NextDouble() * (bramkiMaxGospodarz);
      losBramek = 1.0 / (losBramek + 1.0);
      losBramek = bramkiSrednieGospodarz + losBramek * (bramkiMaxGospodarz - bramkiSrednieGospodarz);
    }
    bramkiGospodarz = (int)Math.Round(losBramek + this.SymulujBramkiSzczesliwe(bramkiSrednieGospodarz), 0);
    return bramkiGospodarz;
  }

  private int SymulujBramkiGoscia() {
    int bramkiGosc = 0;
    int silaGospodarza = (int)Mecz.Gospodarz.Sila;
    double bramkiSrednieGosc = Mecz.Gosc.StatystykaNaWyjezdzie.BramkiSrednie[silaGospodarza];
    double bramkiMinGosc = Mecz.Gosc.StatystykaNaWyjezdzie.BramkiMin[silaGospodarza];
    double bramkiMaxGosc = Mecz.Gosc.StatystykaNaWyjezdzie.BramkiMax[silaGospodarza];
    double losSredniej = this.random.NextDouble();
    double losBramek;
    double progSredniej = 0.6;
    if (losSredniej <= progSredniej) {
      losBramek = this.random.NextDouble() * (bramkiSrednieGosc - bramkiMinGosc);
      if (bramkiMinGosc > 0) {
        losBramek = losBramek + bramkiMinGosc;
      }
    } else {
      losBramek = this.random.NextDouble() * (bramkiMaxGosc - bramkiSrednieGosc);
      losBramek = bramkiSrednieGosc + losBramek;
    }
    bramkiGosc = (int)Math.Round(losBramek + this.SymulujBramkiSzczesliwe(bramkiSrednieGosc), 0);
    return bramkiGosc;
  }

  private double SymulujBramkiSzczesliwe(double bramkiSrednie) {
    double bramkiSzczesliwe = 0.0;
    double szczescie = 0.1;
    double losSzczescia = this.random.NextDouble();
    if (szczescie > losSzczescia) {
      bramkiSzczesliwe += bramkiSrednie;
    }
    return bramkiSzczesliwe;   
  }

}

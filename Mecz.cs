namespace Projekt;

public class Mecz {
  private readonly Druzyna gospodarz;
  private readonly Druzyna gosc;
  private WynikMeczu? wynik;

  public WynikMeczu? Wynik {
    get { return this.wynik; }
  }

  public Druzyna Gospodarz {
    get { return this.gospodarz; }
  }

  public Druzyna Gosc {
    get { return this.gosc; }
  }

  public Mecz(Druzyna gospodarz, Druzyna gosc) {
    this.gospodarz = gospodarz;
    this.gosc = gosc;
  }

  public WynikMeczu Rozegraj() {
    ISymulator symulator = new Symulator(this);
    WynikMeczu wynik = symulator.RozegrajMecz();
    this.wynik = wynik;
    return wynik;
  }

  public override string ToString() {
    return $"{this.gospodarz.Nazwa} - {this.gosc.Nazwa}";
  }

}

namespace Projekt;

public class Rezultat {

  private readonly int punkty;
  private readonly int bramkiZdobyte;
  private readonly int bramkiStracone;
  private readonly int zwyciestwa;
  private readonly int remisy;
  private readonly int porazki;

  public int Punkty {
    get { return this.punkty; }
  }

  public int BramkiZdobyte {
    get { return this.bramkiZdobyte; }
  }

  public int BramkiStracone {
    get { return this.bramkiStracone; }
  }

  public int Zwyciestwa {
    get { return this.zwyciestwa; }
  }

  public int Remisy {
    get { return this.remisy; }
  }

  public int Porazki {
    get { return this.porazki; }
  }

  public Rezultat(int punkty, int bramkiZdobyte, int bramkiStracone) {
    this.punkty = punkty;
    this.bramkiZdobyte = bramkiZdobyte;
    this.bramkiStracone = bramkiStracone;
    this.zwyciestwa = (bramkiZdobyte > bramkiStracone) ? 1 : 0;
    this.remisy = (bramkiZdobyte == bramkiStracone) ? 1 : 0;
    this.porazki = (bramkiZdobyte < bramkiStracone) ? 1 : 0;
  }

  public Rezultat(Rezultat rezultatPoprzedni, Rezultat rezultatBiezacy) {
    this.punkty = rezultatPoprzedni.Punkty + rezultatBiezacy.Punkty;
    this.bramkiZdobyte = rezultatPoprzedni.BramkiZdobyte + rezultatBiezacy.BramkiZdobyte;
    this.bramkiStracone = rezultatPoprzedni.BramkiStracone + rezultatBiezacy.BramkiStracone;
    this.zwyciestwa = rezultatPoprzedni.Zwyciestwa + rezultatBiezacy.Zwyciestwa;
    this.remisy = rezultatPoprzedni.Remisy + rezultatBiezacy.Remisy;
    this.porazki = rezultatPoprzedni.Porazki +  + rezultatBiezacy.Porazki;
  }

  public void Pokaz() {
    string punkty = this.Punkty.ToString().PadLeft(2);
    string bramkiZdobyte = this.BramkiZdobyte.ToString().PadLeft(2);
    string bramkiStracone = this.BramkiStracone.ToString().PadLeft(2);
    string zwyciestwa = this.Zwyciestwa.ToString().PadLeft(2);
    string remisy = this.Remisy.ToString().PadLeft(2);
    string porazki = this.Porazki.ToString().PadLeft(2);
    Console.WriteLine($"{punkty} : {bramkiZdobyte} - {bramkiStracone} : {zwyciestwa} : {remisy} : {porazki} ");
  }

}

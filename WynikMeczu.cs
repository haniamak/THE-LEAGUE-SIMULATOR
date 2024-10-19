namespace Projekt;

public class WynikMeczu {

  private readonly int bramkiGospodarz;
  private readonly int bramkiGosc;
  private readonly int punktyGospodarz;
  private readonly int punktyGosc;

  public int BramkiGospodarz {
    get { return this.bramkiGospodarz; }
  }

  public int BramkiGosc {
    get { return this.bramkiGosc; }
  }

  public int PunktyGospodarz {
    get { return this.punktyGospodarz; }
  }

  public int PunktyGosc {
    get { return this.punktyGosc; }
  }

  public WynikMeczu(int bramkiGospodarz, int bramkiGosc) {
    this.bramkiGospodarz = bramkiGospodarz;
    this.bramkiGosc = bramkiGosc;
    if (bramkiGospodarz > bramkiGosc) {
      this.punktyGospodarz = 3;
      this.punktyGosc = 0;
    } else if (bramkiGospodarz == bramkiGosc) {
      this.punktyGospodarz = 1;
      this.punktyGosc = 1;
    } else {
      this.punktyGospodarz = 0;
      this.punktyGosc = 3;
    }
  }

  public override string ToString() {
    return $"{new String(' ', 2)}{this.bramkiGospodarz}-{this.bramkiGosc}";
  }
  
}

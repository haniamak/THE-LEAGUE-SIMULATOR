namespace Projekt;

public class Zestawienie {
  private readonly int idGospodarz;
  private readonly int idGosc;

  public int IdGospodarz {
    get { return this.idGospodarz; }
  }

  public int IdGosc {
    get { return this.idGosc; }
  }

  public Zestawienie(int idGospodarz, int idGosc) {
    this.idGospodarz = idGospodarz;
    this.idGosc = idGosc;
  }

  public override string ToString()
  {
    return $" {this.IdGospodarz}-{this.IdGosc}";
  }
}
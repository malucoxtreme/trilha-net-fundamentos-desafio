namespace DesafioFundamentos.Models;
/// <summary>
/// Classe que representa os veiculos
/// </summary>
public class Veiculo
{
    public string Placa { get; set; }
    public DateTime Tempo { get; set; } = DateTime.Now;  
}

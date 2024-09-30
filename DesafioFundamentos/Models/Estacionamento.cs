using Spectre.Console;

namespace DesafioFundamentos.Models;
/// <summary>
/// Classe Controlador Estacionamento
/// </summary>
public class Estacionamento(decimal precoInicial, decimal precoPorHora)
{
    private readonly decimal precoInicial = precoInicial;
    private readonly decimal precoPorHora = precoPorHora;
    private List<Veiculo> veiculos = [];

    /// <summary>
    /// Adicionar um veiculo a lista
    /// </summary>
    /// <param name="placa"></param>
    public void AdicionarVeiculo(string placa)
    {
        if (placa is not null)
        {
            veiculos.Add(new Veiculo
            {
                Placa = placa,
            });
        }

    }
    
    /// <summary>
    /// Método para Remover Veiculo
    /// </summary>
    public void RemoverVeiculo()
    {
        // Pedir para o usuário digitar a placa e armazenar na variável placa
        string placa = AnsiConsole.Ask<string>("Digite a placa do veículo para remover"); ;

        Veiculo veiculo = veiculos.Where(x => x.Placa == placa).FirstOrDefault();
        // Verifica se o veículo existe
        if (veiculo is not null)
        {
            DateTime horaAtual = DateTime.Now;
            TimeSpan tempoEstacionado = horaAtual - veiculo.Tempo;
            
            decimal valorTotal = precoInicial + (tempoEstacionado.Hours * precoPorHora);

            veiculos.Remove(veiculo);

            AnsiConsole.MarkupLine($"O veículo {placa} foi removido e o preço total foi de: R$ {valorTotal}");
        }
        else
        {
            AnsiConsole.MarkupLine("Desculpe, esse veículo não está estacionado aqui.\nConfira se digitou a placa corretamente");
        }
    }
    /// <summary>
    /// Metódo para listar veiculos
    /// </summary>
    public void ListarVeiculos()
    {
        // Verifica se há veículos no estacionamento
        if (veiculos.Count is not 0)
        {
            AnsiConsole.MarkupLine("\nOs veículos estacionados são:");
            foreach (var veiculo in veiculos)
            {
                AnsiConsole.MarkupLine($"Veiculo placa nº {veiculo.Placa}, Hora de entrada {veiculo.Tempo:HH:mm:ss}");
            }
        }
        else
        {
            Console.WriteLine("Não há veículos estacionados.");
        }
    }
}


using DesafioFundamentos.Models;
using Spectre.Console;

namespace DesafioFundamentos;
internal class Program
{
    private static bool exibirRelogio = true;
    private static void Main()
    {
        // Configura o encoding para UTF8 para exibir acentuação
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        bool exibirMenu = true;

        decimal precoInicial = AnsiConsole.Ask<decimal>($"[green]Valor inicial: [/]");

        decimal precoPorHora = AnsiConsole.Ask<decimal>("[green]Valor Por Hora: [/]");

        AnsiConsole.MarkupLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
        AnsiConsole.Clear();

        // iniciar a thread e a deixa em segundo plano
        Thread timeThread = new(ShowTime) { IsBackground = true };
        timeThread.Start();

        Estacionamento es = new(precoInicial, precoPorHora);

        // Realiza o loop do menu
        while (exibirMenu)
        {
            string option = AnsiConsole.Prompt
            (
                new SelectionPrompt<String>()
                    .Title("\nSelecione uma opção:")
                    .AddChoices
                    (
                        "Cadastrar Veículo",
                        "Listar Veículo",
                        "Remover Veículo",
                        "Encerrar"
                    )
            );

            exibirMenu = false;
            exibirRelogio = false;

            AnsiConsole.Clear();

            switch (option)
            {
                case "Cadastrar Veículo":
                    string placa = AnsiConsole.Ask<String>("Digite a placa do veículo");
                    es.AdicionarVeiculo(placa);
                    break;
                case "Remover Veículo":
                    es.RemoverVeiculo();
                    break;
                case "Listar Veículo":
                    es.ListarVeiculos();
                    break;
                case "Encerrar":
                    exibirMenu = false;
                    exibirRelogio = false;
                    AnsiConsole.WriteLine("O programa se encerrou");
                    Environment.Exit(0);
                    break;
                default:
                    AnsiConsole.Markup("Como Você Consegui entrar aqui?");
                    break;
            }
            AnsiConsole.MarkupLine("\nPressione qualquer tecla para Voltar");
            Console.ReadKey();
            AnsiConsole.Clear();

            exibirRelogio = true;
            exibirMenu = true;
        }

    }

    /// <summary>
    /// Função para exibir o relógio
    /// </summary>
    private static void ShowTime()
    {
        while (true)
        {
            string Time = $"[bold]Hora Atual: {DateTime.Now:HH:mm:ss}[/]";
            if (exibirRelogio is true)
            {
                AnsiConsole.Markup(Time);
            }
        }
    }
}

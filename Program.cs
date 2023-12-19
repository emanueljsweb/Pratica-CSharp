using DesafioFilmes.Dominio;
using NPOI.SS.UserModel;

namespace DesafioFilmes;

class Program
{
    static string caminhoArquivo = Path.Combine(Environment.CurrentDirectory, "filmes.xlsx");
    static List<Filme> filmes = new();

    static void Main(string[] args)
    {
        LerExcel();
        // ExercicioUm();
        // ExercicioDois();
        // ExercicioTres();
        // ExercicioQuatro();
        ExercicioCinco();
        // ExercicioSeis();
        // ExercicioSete();
    }

    public static void LerExcel()
    {
        using (var arquivo = new FileStream(caminhoArquivo, FileMode.Open, FileAccess.Read))
        {
            var pastaTrabalho = WorkbookFactory.Create(arquivo);
            var planilha = pastaTrabalho.GetSheetAt(0);

            for (int i = 1; i <= planilha.LastRowNum; i++)
            {
                try
                {
                    IRow linhaAtual = planilha.GetRow(i);

                    string titulo = linhaAtual.GetCell(0).ToString().Trim();
                    string diretor = linhaAtual.GetCell(1).ToString().Trim();
                    string genero = linhaAtual.GetCell(2).ToString().Trim();
                    int anoLancamento = int.Parse(linhaAtual.GetCell(3).ToString());
                    int duracao = int.Parse(linhaAtual.GetCell(4).ToString());
                    double avaliacao = double.Parse(linhaAtual.GetCell(5).ToString());
                    string paisOrigem = linhaAtual.GetCell(6).ToString().Trim();

                    var novoFilme = new Filme(titulo, diretor, genero, anoLancamento, duracao, avaliacao, paisOrigem);

                    filmes.Add(novoFilme);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Houve um problema durante a importação: {ex.Message}.");
                }
            }
        }
    }

    public static void ExercicioUm()
    {
        var quantidadeFilmes = filmes.DistinctBy(f => f.Titulo).Count();
        Console.WriteLine($"Existem {quantidadeFilmes} filmes cadastrados em nossa base.");
    }

    public static void ExercicioDois()
    {
        var generos = filmes.Select(f => f.Genero).Distinct();
        foreach (var genero in generos)
        {
            Console.WriteLine($"-> {genero}");
        }
        Console.WriteLine($"Quantidade Total: {generos.Count()}");
    }

    public static void ExercicioTres()
    {
        // var filmesMaisBemAvaliados = filmes.OrderByDescending(f => f.Avaliacao).Take(5);

        var filmesMaisBemAvaliados = filmes
                                    .GroupBy(f => f.Avaliacao)
                                    .OrderByDescending(g => g.Key)
                                    .Take(5);

        foreach (var nota in filmesMaisBemAvaliados)
        {
            var filmesEmpate = string.Join(", ", nota.Select(f => f.Titulo));
            Console.WriteLine($"-> {filmesEmpate} - {nota.Key}.");
        }
    }

    public static void ExercicioQuatro()
    {
        var filmesPorPais = filmes
                            .GroupBy(f => f.PaisOrigem)
                            .Select(g => new
                            {
                                g.Key,
                                Contagem = g.DistinctBy(f => f.Titulo).Count()
                            });
        foreach (var pais in filmesPorPais)
        {
            Console.WriteLine($"-> {pais.Key} - {pais.Contagem} filmes.");
        }

        Console.WriteLine("\nSolução 2:");

        var filmesPorPaisDois = filmes
                                .GroupBy(f => f.PaisOrigem);

        foreach (var pais in filmesPorPaisDois)
        {
            var contagem = pais.DistinctBy(f => f.Titulo).Count();
            Console.WriteLine($"-> {pais.Key} - {contagem} filmes.");
        }
    }

    public static void ExercicioCinco()
    {
        var duracaoPorGenero = filmes
                                .GroupBy(f => f.Genero)
                                .Select(g => new
                                {
                                    Genero = g.Key,
                                    MediaDuracao = g.Average(f => f.Duracao)
                                });
        foreach (var genero in duracaoPorGenero)
        {
            Console.WriteLine($"-> {genero.Genero} - em média, {FormatarHora((int)genero.MediaDuracao)}.");
        }
    }

    public static void ExercicioSeis()
    {
        var diretoresComMaisFilmes = filmes
                                    .GroupBy(f => f.Diretor)
                                    .Select(g => new
                                    {
                                        Nome = g.Key,
                                        Contagem = g.DistinctBy(f => f.Titulo).Count()
                                    })
                                    .OrderByDescending(o => o.Contagem)
                                    .First();

        Console.WriteLine($"O diretor com mais filmes catalogados é {diretoresComMaisFilmes.Nome} com {diretoresComMaisFilmes.Contagem} filmes cadastrados.");

    }

    public static void ExercicioSete()
    {
        var filtro = filmes
                    .Where(f => f.AnoLancamento > 2000)
                    .DistinctBy(f => f.Titulo)
                    .OrderBy(f => f.AnoLancamento);
        foreach (var filme in filtro)
        {
            Console.WriteLine($"-> {filme.Titulo} - lançado em {filme.AnoLancamento}.");
        }
    }

    public static string FormatarHora(int minutos)
    {
        int horas = minutos / 60;
        int minutosRestantes = minutos % 60;
        return $"{horas.ToString("00")}:{minutosRestantes.ToString("00")}";
    }

}
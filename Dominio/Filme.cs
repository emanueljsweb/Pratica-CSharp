namespace DesafioFilmes.Dominio;

public class Filme
{
    public string Titulo { get; protected set; }
    public string Diretor { get; protected set; }
    public string Genero { get; protected set; }
    public int AnoLancamento { get; protected set; }
    public int Duracao { get; protected set; }
    public double Avaliacao { get; protected set; }
    public string PaisOrigem { get; protected set; }

    public Filme(string titulo, string diretor, string genero, int anoLancamento, int duracao, double avaliacao, string paisOrigem)
    {
        SetTitulo(titulo);
        SetDiretor(diretor);
        SetGenero(genero);
        SetAnoLancamento(anoLancamento);
        SetDuracao(duracao);
        SetAvaliacao(avaliacao);
        SetPaisOrigem(paisOrigem);
    }

    public void SetTitulo(string titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new Exception("O atributo Título não pode ser nulo ou vazio");
        Titulo = titulo;
    }

    public void SetDiretor(string diretor)
    {
        if (string.IsNullOrWhiteSpace(diretor))
            throw new Exception("O atributo Diretor não pode ser nulo ou vazio.");
        Diretor = diretor;
    }

    public void SetGenero(string genero)
    {
        if (string.IsNullOrWhiteSpace(genero))
            throw new Exception("O atributo Gênero não pode ser nulo ou vazio.");
        Genero = genero;
    }

    public void SetAnoLancamento(int anoLancamento)
    {
        if (anoLancamento < 0)
            throw new Exception("O atributo Ano de Lançamento não pode ser negativo.");
        AnoLancamento = anoLancamento;
    }

    public void SetDuracao(int duracao)
    {
        if (duracao < 0)
            throw new Exception("O atributo Duração não pode ser negativo.");
        Duracao = duracao;
    }

    public void SetAvaliacao(double avaliacao)
    {
        if (avaliacao < 0 || avaliacao > 10)
            throw new Exception("O atributo Avaliação deve estar entre 0 e 10.");
        Avaliacao = avaliacao;
    }

    public void SetPaisOrigem(string paisOrigem)
    {
        if (string.IsNullOrWhiteSpace(paisOrigem))
            throw new Exception("O atributo País de Origem não pode ser nulo ou vazio.");
        PaisOrigem = paisOrigem;
    }

}
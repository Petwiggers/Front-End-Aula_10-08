
public class Voo
{
    public int Id { get; set; }
    public string Origem { get; set; }
    public string Destino{ get; set; }
    public DateTime Data { get; set; }
}

public class Passageiro

{

     public int Id { get; set; }

     public string Nome { get; set; }

     public int QuantidadePassagem { get; set; }


}


public class Servico
{

    private List<Voo> flights = new List<Voo>();
    private List<Passageiro> passageiros = new List<Passageiro>();
    private int Counter;
    private int passageiroCounter;
    public void AdicionaVoo(string origem, string destino, DateTime data, int QuantidadePassagem)
    {
        var novoVoo = new Voo
        {
            Id=Counter++,
            Origem=origem,
            Destino=destino, 
            Data = data
        };
        flights.Add(novoVoo);
    }

    public List<Voo> GetVoos()
    {
        return flights;
    }

    public void Comprar(int vooId, string nomePassageiro)
    {
        
        var flight = flights.FirstOrDefault(f => f.Id == vooId);
        if (flight != null)
        {
            
            var passageiro = passageiros.FirstOrDefault(p => p.Nome == nomePassageiro);
            if (passageiro == null)
            {
                passageiro = new Passageiro
                {
                    Id= passageiroCounter++,
                    Nome = nomePassageiro,
                    QuantidadePassagem = 1
                };
                passageiros.Add(passageiro);
            }
            else
            {
                passageiro.QuantidadePassagem++;
            }
        }

    }

    public List<Passageiro> GetPassageiros(string Nome)
    {
        return passageiros.Where(p => p.Nome == Nome).ToList();
    }
} 


class Program
{

    static void Main(string[] args)
    {

        var servicoVoo = new Servico();

        servicoVoo.AdicionaVoo("Florianopolis", "São Paulo", DateTime.Now.AddDays(7),100);
        servicoVoo.AdicionaVoo("Guarulhos", "Brasilia", DateTime.Now.AddDays(10),160);     
        servicoVoo.AdicionaVoo("Florianopolis", "Recife", DateTime.Now.AddDays(14),210);     
        servicoVoo.AdicionaVoo("Rio de Janeiro", "João Pessoa", DateTime.Now.AddDays(15),220);

        while (true)
        {

            Console.WriteLine("1. Listar Voos.");
            Console.WriteLine("2. Comprar Passagem");
            Console.WriteLine("3. Verificar Passagem.");
            Console.WriteLine("4. Sair.");

            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    ListarVoos(servicoVoo);
                    break;
                case 2:
                    ComprarPassagem(servicoVoo);
                    break;
                case 3:
                    ChecarPassagem(servicoVoo);
                    break;
                case 4:
                    return;

            }

        }   

    }

    static void ListarVoos(Servico servico)
    {
        var voos = servico.GetVoos();
        foreach (var voo in voos)
        {
            Console.WriteLine($"{voo.Id} | {voo.Origem} para -> {voo.Destino} || {voo.Data.ToString("dd/MM/yyyy")}");
        } 

    }
    static void ComprarPassagem(Servico servico)
    {

        Console.Write("Informe o seu nome: ");
        string nomePassageiro = Console.ReadLine();
        ListarVoos(servico);
        Console.Write("Informe o Id o Voo: ");
        int idVoo = int.Parse(Console.ReadLine());

        servico.Comprar(idVoo, nomePassageiro);
        Console.Write("Passagem Comprara com Sucesso!");
    }

    static void ChecarPassagem(Servico servico)
    {
        Console.Write("Informe seu nome: ");
        string nome = Console.ReadLine();
        var passagens = servico.GetPassageiros(nome);
        foreach (var passagem in passagens)
        {

            Console.WriteLine($"{passagem.QuantidadePassagem}, para: {passagem.Nome}");

        }

    }

}


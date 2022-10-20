using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Progeto2
{
    internal class Program
    {
        [System.Serializable]
        struct Cliente
        {
            public string NomeCompleto;
            public string MarcaModelo;
            public string Placa;
            public string Telefone;
        }

        static List<Cliente> clientes = new List<Cliente>();


        enum Menu { Listagem = 1, Adicionar = 2, Remover = 3, Sair = 4 }


        static void Main(string[] args)
        {
            Carregar();
            bool EscolheuSair = false;
            // Realiza o loop do menu
            while (!EscolheuSair)
            {
                
                Console.WriteLine("        Sistema de estacionamento. O valor inicial é R$ 5,00!");
                
                Console.WriteLine("     1- Listagem\n     2- Adicionar\n     3- Remover\n     4- Sair");
                int intOpicao = int.Parse(Console.ReadLine());
                Menu opcao = (Menu)intOpicao;

                switch (opcao)
                {
                    case Menu.Adicionar:
                        Adicionar();
                        break;
                    case Menu.Listagem:
                        Listagem();
                        break;
                    case Menu.Remover:
                        Remover();
                        break;
                    case Menu.Sair:
                        EscolheuSair = true;
                        break;

                }
                Console.Clear();
            }


        }

        static void Adicionar()
        {
            Cliente cliente = new Cliente();
            Console.WriteLine("     Cadastro de Veiculo: ");
            Console.WriteLine("     Escreva seu nome completo: ");
            cliente.NomeCompleto = Console.ReadLine();
            Console.WriteLine("     Marca e Modelo do seu veiculo: ");
            cliente.MarcaModelo = Console.ReadLine();
            Console.WriteLine("     Digite a placa do seu veiculo: ");
            cliente.Placa = Console.ReadLine();
            Console.WriteLine("     Numero para contato: ");
            cliente.Telefone = Console.ReadLine();

            clientes.Add(cliente);
            Salvar();
            Console.WriteLine("     Cadastro concluído, aperte ENTER para sair. ");
            Console.ReadLine();
        }

        static void Listagem()
        {
            if (clientes.Count > 0)
            {
                Console.WriteLine("Lista de veiculos cadastrados: ");
                int i = 0;
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine($"     ID: {i}");
                    Console.WriteLine($"     Nome: {cliente.NomeCompleto}");
                    Console.WriteLine($"     E-mail: {cliente.MarcaModelo}");
                    Console.WriteLine($"     CPF: {cliente.Placa}");
                    Console.WriteLine($"     Contato: {cliente.Telefone}");
                    Console.WriteLine("\n     Aperte enter para sair.");
                    Console.WriteLine("================================================");
                    i++;
                }

            }
            else
            {
                Console.WriteLine("     Nenhum cliente cadastrado! ");
                Console.ReadLine();
            }

            Console.ReadLine();

        }
        static void Remover()
        {
            Listagem();
            Console.WriteLine("     Digite o ID do veiculo que você deseja remover");
            int id = int.Parse(Console.ReadLine());

            if (id >= 0 && id < clientes.Count)
            {
                decimal Hora = 2;
                Console.WriteLine("     Informe o total de horas que o veiculo permanaçeu estacionado: ");
                decimal TotalDeHorasAPagar = Convert.ToDecimal(Console.ReadLine());
                decimal ResultadoDoValor = TotalDeHorasAPagar *= Hora;
                Console.WriteLine($"     O  valor á pagar é R$ {ResultadoDoValor + 5}.");
                
                clientes.RemoveAt(id);
                Salvar();
                Console.ReadLine();
            }   
            else
            {
                Console.WriteLine("     ID digitado é invalido, tente digitar novamente!");
                Console.ReadLine();
            }
        }
        static void Salvar()
        {
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);
            BinaryFormatter enconder = new BinaryFormatter();

            enconder.Serialize(stream, clientes);
            stream.Close();
        }
        static void Carregar()
        {   // Aqui salva os usuarios cadastrados em uma pasta local
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);
            try
            {

                BinaryFormatter enconder = new BinaryFormatter();

                clientes = (List<Cliente>)enconder.Deserialize(stream);

                if (clientes == null)
                {
                    clientes = new List<Cliente>();
                }


            }
            catch (Exception e)
            {
                clientes = new List<Cliente>();
            }
            stream.Close();
        }
    }

}

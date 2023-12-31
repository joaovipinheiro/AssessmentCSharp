﻿using System;
using System.Collections.Generic;
using CamadaDados;
using CamadaNegocios;


namespace CamadaApresentacao
{
    public class Program
    {
        private static IRepositorio repositorio;

        static void Main(string[] args)
        {
            Console.WriteLine("Escolha onde vai querer salvar os dados: ");
            Console.WriteLine("[1] - Disco Local");
            Console.WriteLine("[2] - Memoria \n");
            Console.Write("Escolha uma um número: ");

            string escolhaArquivo = Console.ReadLine();

            if (escolhaArquivo == "1") 
            {
                repositorio = new BrinquedoArquivo(); //Disco 
            } else if (escolhaArquivo == "2")
            {
                repositorio = new BrinquedoLista();   //Memoria
            } else
            {
                Console.WriteLine("Opção inválida. Programa será encerrado!");
                return;
            }

            var ultimosBrinquedos = repositorio.Listar();
            ExibirBrinquedosCadastrados(ultimosBrinquedos);
            Brinquedo brinquedo = new Brinquedo();

            while (true)
            {
                Console.WriteLine("MENU - CRUD");
                Console.WriteLine("[1] - Inclusão");
                Console.WriteLine("[2] - Pesquisa");
                Console.WriteLine("[3] - Alteração");
                Console.WriteLine("[4] - Exclusão");
                Console.WriteLine("[5] - Sair\n");
                Console.Write("Escolha uma um número: ");

                int num;
                if (int.TryParse(Console.ReadLine(), out num))
                {
                    switch (num)
                    {
                        case 1:
                            IncluirBrinquedo();
                            break;
                        case 2:
                            PesquisarBrinquedo();
                            break;
                        case 3:
                            AlterarBrinquedo();
                            break;
                        case 4:
                            ExcluirBrinquedo();
                            break;
                        case 5:
                            return;
                        default:
                            Console.WriteLine("Opção inválida. Tente novamente. \n");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Opção inválida. Tente novamente.\n");
                }
            }
        }

        static void ExibirBrinquedosCadastrados(List<Brinquedo> ultimosBrinquedos)
        {
            Console.WriteLine("Últimos Brinquedos Cadastrados:");

            if (ultimosBrinquedos.Count == 0)
            {
                Console.WriteLine("Nenhum brinquedo cadastrado.");
            }
            else
            {
                int maxItens = Math.Min(5, ultimosBrinquedos.Count);
                for (int i = 0; i < maxItens; i++)
                {
                    Brinquedo brinquedo = ultimosBrinquedos[i];
                    Console.WriteLine($"{i + 1}. Nome: {brinquedo.Nome}, Preço: {brinquedo.Preco:F2}");
                }
            }
        }

        private static void IncluirBrinquedo()
        {
            Console.WriteLine("Inclusão de brinquedo");

            Brinquedo brinquedo = new Brinquedo();

            Console.Write("Nome do brinquedo: ");
            brinquedo.Nome = Console.ReadLine();

            Console.Write("Data de fabricação (dd/mm/aaaa): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime dataFabricacao))
            {
                brinquedo.DataFabricacao = dataFabricacao;
            }
            else
            {
                Console.WriteLine("Data inválida. O brinquedo será incluído sem data.");
            }

            Console.Write("Faixa etária: ");
            if (int.TryParse(Console.ReadLine(), out int faixaEtaria))
            {
                brinquedo.FaixaEtaria = faixaEtaria;
            }
            else
            {
                Console.WriteLine("Faixa etária inválida. O brinquedo será incluído sem faixa etária.");
            }

            Console.Write("Disponível (true/false): ");
            if (bool.TryParse(Console.ReadLine(), out bool disponivel))
            {
                brinquedo.Disponivel = disponivel;
            }
            else
            {
                Console.WriteLine("Valor inválido. O brinquedo será incluído como disponível.");
            }

            Console.Write("Preço: ");
            if (double.TryParse(Console.ReadLine(), out double preco))
            {
                brinquedo.Preco = preco;
            }
            else
            {
                Console.WriteLine("Preço inválido. O brinquedo será incluído sem preço.");
            }

            repositorio.Incluir(brinquedo);
            Console.WriteLine("Brinquedo cadastrado com sucesso!");
        }

        private static void PesquisarBrinquedo()
        {
            Console.WriteLine("\nPesquisa de brinquedo");

            Console.Write("Digite o nome do brinqudo: ");
            string nomeBrinquedo = Console.ReadLine();

            List<Brinquedo> resultados = repositorio.Pesquisar(nomeBrinquedo);

            if (resultados.Count == 0)
            {
                Console.WriteLine("Nenhum brinquedo encontrado.");
            }
            else
            {
                Console.WriteLine("Resultados da pesquisa: ");
                for (int i = 0; i < resultados.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {resultados[i].Nome}");
                }

                Console.Write("Digite o número do brinquedo para ver detalhes ou digite 0 para voltar ao incio: ");
                if (int.TryParse(Console.ReadLine(), out int escolha) && escolha > 0 && escolha <= resultados.Count)
                {
                    ExibirDetalhes(resultados[escolha - 1]);
                }
            }
        }

        private static void AlterarBrinquedo()
        {
            Console.WriteLine("\nAlteração de brinquedo");

            Console.Write("Informe o ID do brinquedo que deseja alterar: ");
            if (Guid.TryParse(Console.ReadLine(), out Guid id))
            {
                Brinquedo brinquedo = repositorio.BuscarPorId(id);

                if (brinquedo != null)
                {
                    Console.WriteLine("Dados atuais do brinquedo: ");
                    ExibirDetalhes(brinquedo);

                    Console.WriteLine("Informe os novos dados do brinquedo: ");

                    Console.Write("Nome do brinquedo: ");
                    brinquedo.Nome = Console.ReadLine();

                    Console.Write("Data de fabricação (dd/mm/aaaa): ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime dataFabricacao))
                    {
                        brinquedo.DataFabricacao = dataFabricacao;
                    }
                    else
                    {
                        Console.WriteLine("Data inválida. O brinquedo será alterado sem data.");
                    }

                    Console.Write("Faixa etária: ");
                    if (int.TryParse(Console.ReadLine(), out int faixaEtaria))
                    {
                        brinquedo.FaixaEtaria = faixaEtaria;
                    }
                    else
                    {
                        Console.WriteLine("Faixa etária inválida. O brinquedo será alterado sem faixa etária.");
                    }

                    Console.Write("Disponível (true/false): ");
                    if (bool.TryParse(Console.ReadLine(), out bool disponivel))
                    {
                        brinquedo.Disponivel = disponivel;
                    }
                    else
                    {
                        Console.WriteLine("Valor inválido. O brinquedo será alterado como disponível.");
                    }

                    Console.Write("Preço: ");
                    if (double.TryParse(Console.ReadLine(), out double preco))
                    {
                        brinquedo.Preco = preco;
                    }
                    else
                    {
                        Console.WriteLine("Valor inválido. O brinquedo será alterado sem preço.");
                    }

                    repositorio.Alterar(brinquedo);
                    Console.WriteLine("Brinquedo alterado com sucesso!");
                }
                else
                {
                    Console.WriteLine("Brinquedo não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
        }

        private static void ExcluirBrinquedo()
        {
            Console.WriteLine("\nExclusão de Brinquedo");

            Console.Write("Informe o ID do brinquedo que deseja excluir: ");
            if (Guid.TryParse(Console.ReadLine(), out Guid id))
            {
                Brinquedo brinquedo = repositorio.BuscarPorId(id);

                if (brinquedo != null)
                {
                    Console.WriteLine("Dados do brinquedo que será excluído: ");
                    ExibirDetalhes(brinquedo);

                    Console.Write("Tem certeza que deseja excluir este brinquedo? (Sim/Não): ");
                    string simNao = Console.ReadLine();

                    if (simNao.Equals("Sim", StringComparison.OrdinalIgnoreCase))
                    {
                        repositorio.Excluir(id);
                        Console.WriteLine("Brinquedo excluído com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Operação de exclusão cancelada.");
                    }
                }
                else
                {
                    Console.WriteLine("Brinquedo não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
        }
        private static void ExibirDetalhes(Brinquedo brinquedo)
        {
            Console.WriteLine($"Nome: {brinquedo.Nome}");
            Console.WriteLine($"ID: {brinquedo.Id}");
            Console.WriteLine($"Data de Fabricação: {brinquedo.DataFabricacao:dd/mm/yyyy}");
            Console.WriteLine($"Faixa Etária: {brinquedo.FaixaEtaria}");
            Console.WriteLine($"Preço: R${brinquedo.Preco:F2}");
            Console.WriteLine($"Disponível: {brinquedo.Disponivel}");
            Console.WriteLine(brinquedo.CalcTempoEstoque());
        }
    }
}
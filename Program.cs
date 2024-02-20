using System;
using System.Collections.Generic;
using System.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

class Program
{
    static void Main()
    {
        // localize a pasta onde esta o dot net, no meu esta aqui C:\Users\danig\source\repos\ConsoleApp1\ConsoleApp1\bin\Debug\net8.0
        string imagePath = "ImagemParaTrocar.png"; //coloque a imagem no repositorio e mude o nome para ImagemParaTrocar ira ser gerado uma Imagem la chamada de  NovaImagem
        using (Image<Rgba32> imagemOriginal = Image.Load<Rgba32>(imagePath))
        {
            Image<Rgba32> novaImagem = SubstituirCorPredominante(imagemOriginal);


            novaImagem.Save("nova_imagem.png");
        }

        Console.WriteLine("Processo concluído. Nova imagem salva.");
    }

    static Image<Rgba32> SubstituirCorPredominante(Image<Rgba32> imagemOriginal)
    {
        Rgba32 corPredominante = EncontrarCorPredominante(imagemOriginal);

       
        Rgba32 corMenosPredominante = EncontrarCorMenosPredominante(imagemOriginal);

       
        Image<Rgba32> novaImagem = new Image<Rgba32>(imagemOriginal.Width, imagemOriginal.Height);

   
        for (int x = 0; x < novaImagem.Width; x++)
        {
            for (int y = 0; y < novaImagem.Height; y++)
            {
                Rgba32 corAtual = imagemOriginal[x, y];

              
                if (corAtual.Equals(corPredominante))
                {
                    
                    novaImagem[x, y] = corMenosPredominante;
                }
                else
                {
                    
                    novaImagem[x, y] = corAtual;
                }
            }
        }

        return novaImagem;
    }

    static Rgba32 EncontrarCorPredominante(Image<Rgba32> imagem)
    {
       
        List<Rgba32> cores = new List<Rgba32>();
        for (int x = 0; x < imagem.Width; x++)
        {
            for (int y = 0; y < imagem.Height; y++)
            {
                cores.Add(imagem[x, y]);
            }
        }

       
        Rgba32 corPredominante = cores.GroupBy(c => c).OrderByDescending(g => g.Count()).First().Key;

        return corPredominante;
    }

    static Rgba32 EncontrarCorMenosPredominante(Image<Rgba32> imagem)
    {
       
        List<Rgba32> cores = new List<Rgba32>();
        for (int x = 0; x < imagem.Width; x++)
        {
            for (int y = 0; y < imagem.Height; y++)
            {
                cores.Add(imagem[x, y]);
            }
        }

   
        Rgba32 corMenosPredominante = cores.GroupBy(c => c).OrderBy(g => g.Count()).First().Key;

        return corMenosPredominante;
    }
}

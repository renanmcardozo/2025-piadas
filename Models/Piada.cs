using System.ComponentModel.Design.Serialization;

namespace Piadas.Models;

public class Piada
{
    public int id { get; set; }
    public string Pergunta { get; set; }
    public string Resposta { get; set; }

    public Piada()
    {
        
    }
}
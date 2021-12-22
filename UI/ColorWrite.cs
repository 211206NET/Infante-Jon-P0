namespace UI;
/// <summary>
/// Able to write default and another color in one line. 
/// Uses regex to detect strings between chars '[' and ']'
/// </summary>
public class ColorWrite {
    public void WriteColor(string message, ConsoleColor color){
        var pieces = Regex.Split(message, @"(\[[^\]]*\])");

        for(int i=0;i<pieces.Length;i++)
        {
            string piece = pieces[i];
            
            if (piece.StartsWith("[") && piece.EndsWith("]"))
            {
                Console.ForegroundColor = color;
                piece = piece.Substring(1,piece.Length-2);          
            }
            
            Console.Write(piece);
            Console.ResetColor();
        }
        
        Console.WriteLine();
    }
}
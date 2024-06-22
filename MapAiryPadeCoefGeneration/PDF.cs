namespace MapAiryPadeCoefGeneration {
    internal class PDF {
        static void Main() {
            PDFPlus.Execute();
            PDFMinus.Execute();
            PDFMinusLimit.Execute();
            PDFPlusLimit.Execute();

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
